use anchor_lang::prelude::*;
use anchor_lang::solana_program::{
    program::invoke, // Removed invoke_signed
    system_instruction,
};
use std::collections::BTreeMap;

declare_id!("3dVx2mUwQnppaVTkmA9iHnHMHsugrvkqBJLG6pMKKwgh");

#[program]
pub mod tournament_contract {
    use super::*;

    pub fn create_tournament(
        ctx: Context<CreateTournament>,
        tournament_id: u64,
        start_timestamp: i64,
        end_timestamp: i64,
        entry_fee: u64,
        prize_pool: u64,
    ) -> Result<()> {
        let tournament_key = ctx.accounts.tournament.key();
        require!(
            end_timestamp > start_timestamp,
            ErrorCode::InvalidTimestamps
        );

        let tournament = &mut ctx.accounts.tournament;
        tournament.tournament_id = tournament_id;
        tournament.start_timestamp = start_timestamp;
        tournament.end_timestamp = end_timestamp;
        tournament.entry_fee = entry_fee;
        tournament.prize_pool = prize_pool;
        tournament.creator = ctx.accounts.creator.key();
        tournament.num_participants = 0;
        tournament.is_active = true;
        tournament.participants = BTreeMap::new();

        if prize_pool > 0 {
            let transfer_instruction = system_instruction::transfer(
                &ctx.accounts.creator.key(),
                &tournament_key,
                prize_pool,
            );

            invoke(
                &transfer_instruction,
                &[
                    ctx.accounts.creator.to_account_info(),
                    ctx.accounts.tournament.to_account_info(),
                    ctx.accounts.system_program.to_account_info(),
                ],
            )?;

            msg!(
                "Transferred {} lamports from creator {} to tournament {}.",
                prize_pool,
                ctx.accounts.creator.key(),
                tournament_key
            );
        }

        Ok(())
    }

    pub fn join_tournament(ctx: Context<JoinTournament>, tournament_id: u64) -> Result<()> {
        let tournament = &mut ctx.accounts.tournament;
        let participant_account = &mut ctx.accounts.participant_account;
        let now = Clock::get()?.unix_timestamp;

        require!(tournament.is_active, ErrorCode::TournamentClosed);
        require!(
            now >= tournament.start_timestamp && now <= tournament.end_timestamp,
            ErrorCode::TournamentClosed
        );
        require!(
            !tournament
                .participants
                .contains_key(&ctx.accounts.participant_signer.key()),
            ErrorCode::AlreadyJoined
        );

        if tournament.entry_fee > 0 {
            invoke(
                &system_instruction::transfer(
                    &ctx.accounts.participant_signer.key(),
                    &tournament.key(),
                    tournament.entry_fee,
                ),
                &[
                    ctx.accounts.participant_signer.to_account_info(),
                    tournament.to_account_info(),
                    ctx.accounts.system_program.to_account_info(),
                ],
            )?;
            tournament.prize_pool = tournament
                .prize_pool
                .checked_add(tournament.entry_fee)
                .ok_or(ErrorCode::UnexpectedError)?;
        }

        participant_account.tournament_id = tournament_id;
        participant_account.score = 0;
        participant_account.player = ctx.accounts.participant_signer.key();
        tournament.num_participants += 1;
        tournament.participants.insert(
            ctx.accounts.participant_signer.key(),
            participant_account.score,
        );

        Ok(())
    }

    pub fn submit_score(
        ctx: Context<SubmitScore>,
        _tournament_id: u64,
        new_score: u64,
    ) -> Result<()> {
        let participant_account = &mut ctx.accounts.participant_account;
        let tournament = &mut ctx.accounts.tournament;

        require!(tournament.is_active, ErrorCode::TournamentClosed);

        participant_account.score = participant_account
            .score
            .checked_add(new_score)
            .ok_or(ErrorCode::UnexpectedError)?;
        tournament
            .participants
            .insert(participant_account.player, participant_account.score);

        Ok(())
    }

    pub fn end_tournament(
        ctx: Context<EndTournament>,
        _tournament_id: u64,
        _first_place: Pubkey,
        _second_place: Pubkey,
        _third_place: Pubkey,
    ) -> Result<()> {
        msg!("EndTournament instruction invoked.");

      
        let tournament_account_info = ctx.accounts.tournament.to_account_info();

        let tournament = &mut ctx.accounts.tournament;
    


        require!(
            Clock::get()?.unix_timestamp >= tournament.end_timestamp,
            ErrorCode::TournamentOngoing
        );
        require!(tournament.is_active, ErrorCode::TournamentAlreadyEnded);

        tournament.is_active = false;
        msg!("Tournament marked as inactive.");

        let total_prize_pool = tournament.prize_pool;
        msg!("Total prize pool: {}", total_prize_pool);

        let first_prize = total_prize_pool
            .checked_mul(50)
            .ok_or(ErrorCode::UnexpectedError)?
            / 100;
        let second_prize = total_prize_pool
            .checked_mul(30)
            .ok_or(ErrorCode::UnexpectedError)?
            / 100;
        let third_prize = total_prize_pool
            .checked_mul(20)
            .ok_or(ErrorCode::UnexpectedError)?
            / 100;

        msg!(
            "Prizes calculated - First: {}, Second: {}, Third: {}",
            first_prize,
            second_prize,
            third_prize
        );

        {
            
            **tournament_account_info.try_borrow_mut_lamports()? -=
                first_prize + second_prize + third_prize;

            **ctx
                .accounts
                .first_place
                .to_account_info()
                .try_borrow_mut_lamports()? += first_prize;
            **ctx
                .accounts
                .second_place
                .to_account_info()
                .try_borrow_mut_lamports()? += second_prize;
            **ctx
                .accounts
                .third_place
                .to_account_info()
                .try_borrow_mut_lamports()? += third_prize;
        }

        tournament.prize_pool = 0;
        tournament.num_participants = 0;
        tournament.participants.clear();
        msg!("Tournament state reset.");

        msg!("EndTournament instruction completed successfully.");
        Ok(())
    }
}

#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct CreateTournament<'info> {
    #[account(
        init,
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump,
        payer = creator,
        space = 8 + Tournament::LEN
    )]
    pub tournament: Account<'info, Tournament>,

    #[account(mut)]
    pub creator: Signer<'info>,

    pub system_program: Program<'info, System>,
}

#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct JoinTournament<'info> {
    #[account(
        mut,
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump
    )]
    pub tournament: Account<'info, Tournament>,

    #[account(
        init,
        seeds = [b"participant".as_ref(), &_tournament_id.to_le_bytes(), participant_signer.key().as_ref()],
        bump,
        payer = participant_signer,
        space = 8 + Participant::LEN
    )]
    pub participant_account: Account<'info, Participant>,

    #[account(mut)]
    pub participant_signer: Signer<'info>,

    pub system_program: Program<'info, System>,
}

#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct SubmitScore<'info> {
    #[account(
        mut,
        seeds = [b"participant".as_ref(), &_tournament_id.to_le_bytes(), player.key().as_ref()],
        bump,
        has_one = player
    )]
    pub participant_account: Account<'info, Participant>,

    #[account(
        mut,
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump
    )]
    pub tournament: Account<'info, Tournament>,

    pub player: Signer<'info>,
}

#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct EndTournament<'info> {
    #[account(
        mut,
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump
    )]
    pub tournament: Account<'info, Tournament>,

    #[account(mut)]
    pub first_place: AccountInfo<'info>,

    #[account(mut)]
    pub second_place: AccountInfo<'info>,

    #[account(mut)]
    pub third_place: AccountInfo<'info>,

    #[account(mut)]
    pub creator: Signer<'info>,

    pub system_program: Program<'info, System>,
}

#[account]
pub struct Tournament {
    pub tournament_id: u64,
    pub start_timestamp: i64,
    pub end_timestamp: i64,
    pub entry_fee: u64,
    pub prize_pool: u64,
    pub creator: Pubkey,
    pub num_participants: u64,
    pub is_active: bool,
    pub participants: BTreeMap<Pubkey, u64>,
}

impl Tournament {
    pub const LEN: usize = 8 + 8 + 8 + 8 + 32 + 8 + 1 + 1024;
}

#[account]
pub struct Participant {
    pub tournament_id: u64,
    pub player: Pubkey,
    pub score: u64,
}

impl Participant {
    pub const LEN: usize = 8 + 32 + 8;
}

/// **Custom error codes for the program**
#[error_code]
pub enum ErrorCode {
    #[msg("Tournament is not open for joining at this time.")]
    TournamentClosed,

    #[msg("Tournament is still ongoing.")]
    TournamentOngoing,

    #[msg("Tournament has already ended.")]
    TournamentAlreadyEnded,

    #[msg("Participant has already joined the tournament.")]
    AlreadyJoined,

    #[msg("Invalid start or end timestamps.")]
    InvalidTimestamps,

    #[msg("Unauthorized action.")]
    Unauthorized,

    #[msg("Unexpected error occurred.")]
    UnexpectedError,

    #[msg("Invalid winner account provided.")]
    InvalidWinnerAccount,
}
