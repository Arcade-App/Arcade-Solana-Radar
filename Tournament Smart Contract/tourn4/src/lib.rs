use anchor_lang::prelude::*;
use anchor_lang::solana_program::{
    program::invoke, // Used to call external programs, such as the system program for transfers
    system_instruction,
};
use std::collections::BTreeMap; // For maintaining an ordered map of participants

// Declare the program ID 
declare_id!("3dVx2mUwQnppaVTkmA9iHnHMHsugrvkqBJLG6pMKKwgh");

#[program]
pub mod tournament_contract {
    use super::*;

    /// Creates a new tournament with specified parameters.
    pub fn create_tournament(
        ctx: Context<CreateTournament>, // Context containing accounts and instruction data
        tournament_id: u64,             // Unique ID for the tournament
        start_timestamp: i64,           // Start time of the tournament (Unix timestamp)
        end_timestamp: i64,             // End time of the tournament (Unix timestamp)
        entry_fee: u64,                 // Entry fee required to join the tournament
        prize_pool: u64,                // Initial prize pool amount
    ) -> Result<()> {
        // Get the public key of the tournament account
        let tournament_key = ctx.accounts.tournament.key();

        // Validate that the end timestamp is after the start timestamp
        require!(
            end_timestamp > start_timestamp,
            ErrorCode::InvalidTimestamps
        );

        // Initialize the tournament account with provided details
        let tournament = &mut ctx.accounts.tournament;
        tournament.tournament_id = tournament_id;
        tournament.start_timestamp = start_timestamp;
        tournament.end_timestamp = end_timestamp;
        tournament.entry_fee = entry_fee;
        tournament.prize_pool = prize_pool;
        tournament.creator = ctx.accounts.creator.key();
        tournament.num_participants = 0;
        tournament.is_active = true;
        tournament.participants = BTreeMap::new(); // Empty map of participants

        // If an initial prize pool is provided, transfer lamports from the creator to the tournament account
        if prize_pool > 0 {
            // Create a system instruction to transfer lamports
            let transfer_instruction = system_instruction::transfer(
                &ctx.accounts.creator.key(), // From creator
                &tournament_key,             // To tournament account
                prize_pool,                  // Amount to transfer
            );

            // Invoke the transfer instruction
            invoke(
                &transfer_instruction,
                &[
                    ctx.accounts.creator.to_account_info(),
                    ctx.accounts.tournament.to_account_info(),
                    ctx.accounts.system_program.to_account_info(),
                ],
            )?;

            // Log a message indicating the successful transfer
            msg!(
                "Transferred {} lamports from creator {} to tournament {}.",
                prize_pool,
                ctx.accounts.creator.key(),
                tournament_key
            );
        }

        Ok(())
    }

    /// Allows a participant to join an active tournament.
    pub fn join_tournament(ctx: Context<JoinTournament>, tournament_id: u64) -> Result<()> {
        let tournament = &mut ctx.accounts.tournament;
        let participant_account = &mut ctx.accounts.participant_account;

        // Get the current Unix timestamp
        let now = Clock::get()?.unix_timestamp;

        // Ensure the tournament is active and open for joining
        require!(tournament.is_active, ErrorCode::TournamentClosed);
        require!(
            now >= tournament.start_timestamp && now <= tournament.end_timestamp,
            ErrorCode::TournamentClosed
        );

        // Check that the participant hasn't already joined
        require!(
            !tournament
                .participants
                .contains_key(&ctx.accounts.participant_signer.key()),
            ErrorCode::AlreadyJoined
        );

        // If an entry fee is required, transfer it from the participant to the tournament account
        if tournament.entry_fee > 0 {
            // Create and invoke the transfer instruction
            invoke(
                &system_instruction::transfer(
                    &ctx.accounts.participant_signer.key(), // From participant
                    &tournament.key(),                      // To tournament account
                    tournament.entry_fee,                   // Entry fee amount
                ),
                &[
                    ctx.accounts.participant_signer.to_account_info(),
                    tournament.to_account_info(),
                    ctx.accounts.system_program.to_account_info(),
                ],
            )?;

            // Update the tournament's prize pool
            tournament.prize_pool = tournament
                .prize_pool
                .checked_add(tournament.entry_fee)
                .ok_or(ErrorCode::UnexpectedError)?;
        }

        // Initialize the participant's account
        participant_account.tournament_id = tournament_id;
        participant_account.score = 0; // Initial score
        participant_account.player = ctx.accounts.participant_signer.key();

        // Update the tournament's participant list
        tournament.num_participants += 1;
        tournament.participants.insert(
            ctx.accounts.participant_signer.key(),
            participant_account.score,
        );

        Ok(())
    }

    /// Allows a participant to submit their score for the tournament.
    pub fn submit_score(
        ctx: Context<SubmitScore>,
        _tournament_id: u64, // Not used, but kept for consistency
        new_score: u64,      // The new score to add
    ) -> Result<()> {
        let participant_account = &mut ctx.accounts.participant_account;
        let tournament = &mut ctx.accounts.tournament;

        // Ensure the tournament is still active
        require!(tournament.is_active, ErrorCode::TournamentClosed);

        // Update the participant's score
        participant_account.score = participant_account
            .score
            .checked_add(new_score)
            .ok_or(ErrorCode::UnexpectedError)?;

        // Update the participant's score in the tournament's map
        tournament
            .participants
            .insert(participant_account.player, participant_account.score);

        Ok(())
    }

    /// Ends the tournament and distributes prizes to the top three participants.
    pub fn end_tournament(
        ctx: Context<EndTournament>,
        _tournament_id: u64,     // Tournament ID (not used in the function)
        _first_place: Pubkey,    // Public key of the first-place winner
        _second_place: Pubkey,   // Public key of the second-place winner
        _third_place: Pubkey,    // Public key of the third-place winner
    ) -> Result<()> {
        msg!("EndTournament instruction invoked.");

        // Get the account info and tournament data
        let tournament_account_info = ctx.accounts.tournament.to_account_info();
        let tournament = &mut ctx.accounts.tournament;

        // Ensure the tournament has ended based on the current time
        require!(
            Clock::get()?.unix_timestamp >= tournament.end_timestamp,
            ErrorCode::TournamentOngoing
        );
        // Ensure the tournament hasn't already been ended
        require!(tournament.is_active, ErrorCode::TournamentAlreadyEnded);

        // Mark the tournament as inactive
        tournament.is_active = false;
        msg!("Tournament marked as inactive.");

        // Calculate the total prize pool
        let total_prize_pool = tournament.prize_pool;
        msg!("Total prize pool: {}", total_prize_pool);

        // Calculate the prize amounts for the top three winners
        let first_prize = total_prize_pool
            .checked_mul(50)
            .ok_or(ErrorCode::UnexpectedError)?
            / 100; // 50%
        let second_prize = total_prize_pool
            .checked_mul(30)
            .ok_or(ErrorCode::UnexpectedError)?
            / 100; // 30%
        let third_prize = total_prize_pool
            .checked_mul(20)
            .ok_or(ErrorCode::UnexpectedError)?
            / 100; // 20%

        msg!(
            "Prizes calculated - First: {}, Second: {}, Third: {}",
            first_prize,
            second_prize,
            third_prize
        );

        {
            // Adjust the lamports (SOL balance) of the accounts to distribute prizes
            **tournament_account_info.try_borrow_mut_lamports()? -=
                first_prize + second_prize + third_prize;

            // Transfer prizes to the winners
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

        // Reset the tournament state
        tournament.prize_pool = 0;
        tournament.num_participants = 0;
        tournament.participants.clear();
        msg!("Tournament state reset.");

        msg!("EndTournament instruction completed successfully.");
        Ok(())
    }
}

/////////////////////////////////////
// Account Structures and Contexts //
/////////////////////////////////////

/// Context for creating a tournament.
#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct CreateTournament<'info> {
    /// The tournament account to initialize.
    #[account(
        init, // Initialize a new account
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()], // Seed for PDA
        bump, // Bump seed for PDA
        payer = creator, // Account that pays for the initialization
        space = 8 + Tournament::LEN // Space required for the account data
    )]
    pub tournament: Account<'info, Tournament>,

    /// The creator of the tournament.
    #[account(mut)]
    pub creator: Signer<'info>,

    /// The system program.
    pub system_program: Program<'info, System>,
}

/// Context for joining a tournament.
#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct JoinTournament<'info> {
    /// The tournament to join.
    #[account(
        mut, // The account will be modified
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump // Bump seed for PDA
    )]
    pub tournament: Account<'info, Tournament>,

    /// The participant's account to initialize.
    #[account(
        init, // Initialize a new account
        seeds = [b"participant".as_ref(), &_tournament_id.to_le_bytes(), participant_signer.key().as_ref()], // Seed for PDA
        bump, // Bump seed for PDA
        payer = participant_signer, // Account that pays for the initialization
        space = 8 + Participant::LEN // Space required for the account data
    )]
    pub participant_account: Account<'info, Participant>,

    /// The participant joining the tournament.
    #[account(mut)]
    pub participant_signer: Signer<'info>,

    /// The system program.
    pub system_program: Program<'info, System>,
}

/// Context for submitting a score in a tournament.
#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct SubmitScore<'info> {
    /// The participant's account.
    #[account(
        mut, // The account will be modified
        seeds = [b"participant".as_ref(), &_tournament_id.to_le_bytes(), player.key().as_ref()],
        bump, // Bump seed for PDA
        has_one = player // Ensures the account belongs to the signer
    )]
    pub participant_account: Account<'info, Participant>,

    /// The tournament account.
    #[account(
        mut, // The account will be modified
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump // Bump seed for PDA
    )]
    pub tournament: Account<'info, Tournament>,

    /// The player submitting the score.
    pub player: Signer<'info>,
}

/// Context for ending a tournament and distributing prizes.
#[derive(Accounts)]
#[instruction(_tournament_id: u64)]
pub struct EndTournament<'info> {
    /// The tournament to end.
    #[account(
        mut, // The account will be modified
        seeds = [b"tournament".as_ref(), &_tournament_id.to_le_bytes()],
        bump // Bump seed for PDA
    )]
    pub tournament: Account<'info, Tournament>,

    /// Account of the first-place winner.
    #[account(mut)]
    pub first_place: AccountInfo<'info>,

    /// Account of the second-place winner.
    #[account(mut)]
    pub second_place: AccountInfo<'info>,

    /// Account of the third-place winner.
    #[account(mut)]
    pub third_place: AccountInfo<'info>,

    /// The creator of the tournament (could be used for authorization).
    #[account(mut)]
    pub creator: Signer<'info>,

    /// The system program.
    pub system_program: Program<'info, System>,
}

/// Data structure representing a tournament.
#[account]
pub struct Tournament {
    pub tournament_id: u64,           // Unique identifier for the tournament
    pub start_timestamp: i64,         // Start time (Unix timestamp)
    pub end_timestamp: i64,           // End time (Unix timestamp)
    pub entry_fee: u64,               // Entry fee for participants
    pub prize_pool: u64,              // Total prize pool amount
    pub creator: Pubkey,              // Public key of the tournament creator
    pub num_participants: u64,        // Number of participants
    pub is_active: bool,              // Is the tournament currently active?
    pub participants: BTreeMap<Pubkey, u64>, // Map of participant public keys to their scores
}

impl Tournament {
    /// The total size (in bytes) of the Tournament account data.
    pub const LEN: usize = 8   // tournament_id
        + 8   // start_timestamp
        + 8   // end_timestamp
        + 8   // entry_fee
        + 8   // prize_pool
        + 32  // creator
        + 8   // num_participants
        + 1   // is_active
        + 1024; // participants (approximate size, adjust as needed)
}

/// Data structure representing a participant in a tournament.
#[account]
pub struct Participant {
    pub tournament_id: u64,   // ID of the tournament
    pub player: Pubkey,       // Public key of the participant
    pub score: u64,           // Participant's score
}

impl Participant {
    /// The total size (in bytes) of the Participant account data.
    pub const LEN: usize = 8   // tournament_id
        + 32  // player
        + 8;  // score
}

/// Custom error codes for the program.
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
