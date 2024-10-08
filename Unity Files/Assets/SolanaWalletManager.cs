using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Numerics;
using Cysharp.Threading.Tasks;
using Solana.Unity.Programs;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
using Solana.Unity.Wallet.Bip39;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SolanaWalletManager : MonoBehaviour
{

    private IRpcClient rpcClient;
    private Account account;
    private string ProgramId = "3dVx2mUwQnppaVTkmA9iHnHMHsugrvkqBJLG6pMKKwgh"; // Program ID
    private string programName = "tourn4"; // Program Name
    public const long SolLamports = 1000000000;

    private void Start()
    {
        // Initialize RPC client to DevNet
        rpcClient = ClientFactory.GetClient(Cluster.DevNet);
        //rpcClient = ClientFactory.GetClient("https://devnet.sonic.game");
        Debug.Log("Initialized RPC client to DevNet");
    }

    public void SetTournamentManagerData(string tournamentManagerAddress, string tournamentContractName)
    {
        ProgramId = tournamentManagerAddress;
        programName = tournamentContractName;
    }

    public void CreateNewAccount()
    {
        // Generate new mnemonic and wallet
        var mnemonic = new Mnemonic(WordList.English, WordCount.Twelve);

        Manager.instance.userInfoManager.walletMnemonics = mnemonic.ToString();

        var wallet = new Wallet(mnemonic);
        account = wallet.GetAccount(0);

        Manager.instance.userInfoManager.walletAddress = account.PublicKey;

        Debug.Log("New account created and stored. Public Key: " + account.PublicKey);

        //UniTask.Void(LoadCurrentWalletBalance);


    }

    public void LoadAccount(string mnemonics)
    {

        var mnemonic = new Mnemonic(mnemonics);
        var wallet = new Wallet(mnemonic);
        account = wallet.GetAccount(0);

        Manager.instance.userInfoManager.walletAddress = account.PublicKey;

        Debug.Log("Account loaded. Public Key: " + account.PublicKey);

        //UniTask.Void(LoadCurrentWalletBalance);

    }




    public async UniTaskVoid LoadCurrentWalletBalance()
    {
        Debug.Log("Loading Current Wallet Balance");
        var balanceResult = await rpcClient.GetBalanceAsync(Manager.instance.userInfoManager.walletAddress);

        if (!balanceResult.WasSuccessful)
        {            
            Debug.LogError("Loading Current Wallet Balance failed: " + balanceResult.Reason);
            Manager.instance.canvasManager.ShowErrorPopup("Loading Current Wallet Balance failed: " + balanceResult.Reason);

            return;
        }

        Debug.Log("Loading Current Wallet Balance Successful: " + balanceResult);
        Debug.Log("Loading Current Wallet Balance Successful Result: " + balanceResult.Result);
        Debug.Log("Loading Current Wallet Balance Successful Result Value: " + balanceResult.Result.Value);

        Manager.instance.userInfoManager.walletBalanceInt = (int)(balanceResult.Result?.Value ?? 0);
        Manager.instance.userInfoManager.walletBalanceFloat = (float)(balanceResult.Result?.Value ?? 0) / SolLamports;

        Manager.instance.canvasManager.SetUISolanaBalance();

    }

    public async void CallAirdropSol()
    {
        Debug.Log("Starting Airdrop Sol");
        await AirdropSol();
    }

    public IEnumerator AirdropSolCoroutine()
    {
        var airdropSolTask = AirdropSolFun();
        Debug.Log("Started AirdropSolCoroutine");
        yield return airdropSolTask.ToCoroutine(); // Properly awaiting the async task
        Debug.Log("Finished AirdropSolCoroutine");

    }

    public async UniTask AirdropSolFun()
    {
        Debug.Log("Started AirdropSolFun");
        await AirdropSol();
        Debug.Log("Finished AirdropSolFun");
    }


    public async UniTask AirdropSol()
    {
        try
        {
            Debug.Log("Requesting SOL airdrop...");
            var airdropResult = await rpcClient.RequestAirdropAsync(account.PublicKey, 500_000_000); // 0.5 SOL
            if (!airdropResult.WasSuccessful)
            {
                Debug.LogError("Airdrop failed: " + airdropResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Airdrop failed: " + airdropResult.Reason);

                return;
            }

            Debug.Log("Airdrop successful. Transaction ID: " + airdropResult.Result);

            // Wait for confirmation before proceeding
            await WaitForConfirmation(airdropResult.Result);

            UniTask.Void(LoadCurrentWalletBalance);
        }
        catch (Exception ex)
        {
            Debug.LogError("Airdrop error: " + ex.Message);
            Manager.instance.canvasManager.ShowErrorPopup("Airdrop error: " + ex.Message);

        }
    }


    public IEnumerator CreateTournamentCoroutine()
    {
        var createTournamentTask = CreateTournamentFun();
        Debug.Log("Started CreateTournamentCoroutine");
        yield return createTournamentTask.ToCoroutine(); // Properly awaiting the async task
        Debug.Log("Finished CreateTournamentCoroutine");

    }

    public async UniTask CreateTournamentFun()
    {
        Debug.Log("Started CreateTournamentFun");
        await CreateTournament();
        Debug.Log("Finished CreateTournamentFun");
    }

    public async UniTask CreateTournament()
    {
        Debug.Log("Preparing to create a tournament...");

        try
        {

            ulong tournamentId = (ulong)Manager.instance.tournamentDataManager.tournamentId;
            //ulong tournamentId = 63;

            long startTimestamp = ConvertToUnixTimestamp(Manager.instance.tournamentDataManager.startDate, Manager.instance.tournamentDataManager.startTime);
            long endTimestamp = ConvertToUnixTimestamp(Manager.instance.tournamentDataManager.endDate, Manager.instance.tournamentDataManager.endTime);
            ulong entryFee = (ulong)(Manager.instance.tournamentDataManager.playerJoiningFee * SolLamports);
            ulong prizePool = (ulong)(Manager.instance.tournamentDataManager.prizePool * SolLamports);

            // Fetching the latest blockhash
            var blockHashResult = await rpcClient.GetLatestBlockHashAsync();
            if (!blockHashResult.WasSuccessful)
            {

                Debug.LogError("Failed to get latest block hash: " + blockHashResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Failed to get latest block hash: " + blockHashResult.Reason);

                return;
            }

            // Compute the instruction discriminator for 'create_tournament'
            var discriminator = ComputeInstructionDiscriminator("global:create_tournament");

            // Build the instruction data
            byte[] data = new byte[discriminator.Length + 8 + 8 + 8 + 8 + 8];
            Buffer.BlockCopy(discriminator, 0, data, 0, discriminator.Length);
            int offset = discriminator.Length;

            Debug.Log("1. data 1: " + data);

            // Serialize the parameters (in little-endian)
            Buffer.BlockCopy(SerializeU64(tournamentId), 0, data, offset, 8);
            offset += 8;

            
            Debug.Log("2. tournamentId:" + tournamentId + " data 2: " + data);


            Buffer.BlockCopy(SerializeI64(startTimestamp), 0, data, offset, 8);
            offset += 8;

            Debug.Log("3. startTimestamp :" + startTimestamp + " data 3: " + data);


            Buffer.BlockCopy(SerializeI64(endTimestamp), 0, data, offset, 8);
            offset += 8;

            Debug.Log("4. endTimestamp :" + endTimestamp + " data 4: " + data);

            Buffer.BlockCopy(SerializeU64(entryFee), 0, data, offset, 8);
            offset += 8;

            Debug.Log("5. entryFee :" + entryFee + " data 5: " + data);

            Buffer.BlockCopy(SerializeU64(prizePool), 0, data, offset, 8);

            Debug.Log("6. prizePool :" + prizePool + " 6. data 6: " + data);

            // Create the tournament account (PDA)
            var tournamentPda = await GetTournamentPda(tournamentId);



            Debug.Log("7. Create Tournament Tournament PDA: " + tournamentPda);

            //string ProgramId = Manager.instance.tournamentDataManager.tournamentManagerAddress;

            Debug.Log("8. Create Tournament Program Id: " + ProgramId);


            // Build transaction instruction
            var transactionInstruction = new TransactionInstruction
            {
                ProgramId = new PublicKey(ProgramId),
                Keys = new List<AccountMeta>
            {
                AccountMeta.Writable(tournamentPda, false), // Tournament account
                AccountMeta.Writable(account.PublicKey, true), // Creator signer
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false), // System program
            },
                Data = data
            };

            Debug.Log("9. Transaction Instruction: " + transactionInstruction.ToString());


            // Build and send the transaction
            var transactionBuilder = new TransactionBuilder()
                .SetRecentBlockHash(blockHashResult.Result.Value.Blockhash)
                .SetFeePayer(account.PublicKey)
                .AddInstruction(transactionInstruction);

            Debug.Log("10. Transaction Builder: " + transactionBuilder.ToString());



            var transaction = transactionBuilder.Build(account);


            Debug.Log("11. Create Tournament Transaction: " + transaction);

            var transactionResult = await rpcClient.SendTransactionAsync(transaction);

            Debug.Log("12. Create Tournament Transaction Result: " + transactionResult);


            if (transactionResult.WasSuccessful)
            {

                Debug.Log("13. Tournament created successfully. Signature: " + transactionResult.Result);

                // Wait for confirmation before proceeding
                await WaitForConfirmation(transactionResult.Result);

                UniTask.Void(LoadCurrentWalletBalance);
            }
            else
            {
                Debug.LogError("14. Tournament creation failed: " + transactionResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("14. Tournament creation failed: " + transactionResult.Reason);

            }
        }
        catch (Exception ex)
        {
            Debug.LogError("15. Error during tournament creation: " + ex.Message);
            Manager.instance.canvasManager.ShowErrorPopup("15. Error during tournament creation: " + ex.Message);

        }
    }


    public IEnumerator JoinTournamentCoroutine()
    {
        var joinTournamentTask = JoinTournamentFun();
        Debug.Log("Started JoinTournamentCoroutine");
        yield return joinTournamentTask.ToCoroutine(); // Properly awaiting the async task
        Debug.Log("Finished JoinTournamentCoroutine");

    }

    public async UniTask JoinTournamentFun()
    {
        Debug.Log("Started JoinTournamentFun");
        await JoinTournament();
        Debug.Log("Finished JoinTournamentFun");
    }


    private async UniTask JoinTournament()
    {
        Debug.Log("Preparing to join a tournament...");

        try
        {
            ulong tournamentId = (ulong)Manager.instance.tournamentDataManager.tournamentId;
            //ulong tournamentId = 63;



            // Fetching the latest blockhash
            var blockHashResult = await rpcClient.GetLatestBlockHashAsync();
            if (!blockHashResult.WasSuccessful)
            {
                Debug.LogError("Failed to get latest block hash: " + blockHashResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Failed to get latest block hash: " + blockHashResult.Reason);

                return;
            }

            // Compute the instruction discriminator for 'join_tournament'
            var discriminator = ComputeInstructionDiscriminator("global:join_tournament");

            // Build the instruction data
            byte[] data = new byte[discriminator.Length + 8];
            Buffer.BlockCopy(discriminator, 0, data, 0, discriminator.Length);
            Buffer.BlockCopy(SerializeU64(tournamentId), 0, data, discriminator.Length, 8);

            // Create the tournament account (PDA)
            var tournamentPda = await GetTournamentPda(tournamentId);
            Debug.Log("Join Tournament Tournament PDA: " + tournamentPda);


            // Create the participant account (PDA)
            var participantPda = await GetParticipantPda(tournamentId, account.PublicKey);
            Debug.Log("Join Tournament Participant PDA: " + participantPda);


            // Build transaction instruction
            var transactionInstruction = new TransactionInstruction
            {
                ProgramId = new PublicKey(ProgramId),
                Keys = new List<AccountMeta>
                {
                    AccountMeta.Writable(tournamentPda, false), // tournament account
                    AccountMeta.Writable(participantPda, false), // participant account
                    AccountMeta.Writable(account.PublicKey, true), // participant signer
                    AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false), // system program
                },
                Data = data
            };

            // Build and send the transaction
            var transactionBuilder = new TransactionBuilder()
                .SetRecentBlockHash(blockHashResult.Result.Value.Blockhash)
                .SetFeePayer(account.PublicKey)
                .AddInstruction(transactionInstruction);

            var transaction = transactionBuilder.Build(account);

            Debug.Log("Join Tournament Transaction: " + transaction);


            var transactionResult = await rpcClient.SendTransactionAsync(transaction);

            Debug.Log("Join Tournament Transaction Result: " + transactionResult);


            if (transactionResult.WasSuccessful)
            {
                Debug.Log("Joined tournament successfully. Signature: " + transactionResult.Result);

                // Wait for confirmation before proceeding
                await WaitForConfirmation(transactionResult.Result);

                UniTask.Void(LoadCurrentWalletBalance);
            }
            else
            {
                Debug.LogError("Joining tournament failed: " + transactionResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Joining tournament failed: " + transactionResult.Reason);

            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error during joining tournament: " + ex.Message);
            Manager.instance.canvasManager.ShowErrorPopup("Error during joining tournament: " + ex.Message);

        }
    }

    public IEnumerator SubmitScoreCoroutine()
    {
        var submitScoreTask = SubmitScoreFun();
        Debug.Log("Started SubmitScoreCoroutine");
        yield return submitScoreTask.ToCoroutine(); // Properly awaiting the async task
        Debug.Log("Finished SubmitScoreCoroutine");

    }

    public async UniTask SubmitScoreFun()
    {
        Debug.Log("Started SubmitScoreFun");
        await SubmitScore();
        Debug.Log("Finished SubmitScoreFun");
    }



    private async UniTask SubmitScore()
    {
        Debug.Log("Preparing to submit a score...");

        ulong tournamentId = (ulong)Manager.instance.tournamentDataManager.tournamentId;
        //ulong tournamentId = 63;


        ulong newScore = (ulong)Manager.instance.userInfoManager.tournamentScore;


        try
        {
            // Fetching the latest blockhash
            var blockHashResult = await rpcClient.GetLatestBlockHashAsync();
            if (!blockHashResult.WasSuccessful)
            {
                Debug.LogError("Failed to get latest block hash: " + blockHashResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Failed to get latest block hash: " + blockHashResult.Reason);

                return;
            }

            // Compute the instruction discriminator for 'submit_score'
            var discriminator = ComputeInstructionDiscriminator("global:submit_score");

            // Build the instruction data
            byte[] data = new byte[discriminator.Length + 8 + 8]; // Adjusted length
            Buffer.BlockCopy(discriminator, 0, data, 0, discriminator.Length);
            int offset = discriminator.Length;

            // Serialize tournamentId
            Buffer.BlockCopy(SerializeU64(tournamentId), 0, data, offset, 8);
            offset += 8;

            // Serialize newScore
            Buffer.BlockCopy(SerializeU64(newScore), 0, data, offset, 8);

            // Create the tournament account (PDA)
            var tournamentPda = await GetTournamentPda(tournamentId);

            // Create the participant account (PDA)
            var participantPda = await GetParticipantPda(tournamentId, account.PublicKey);

            // Check if participant account exists
            var participantAccountInfo = await rpcClient.GetAccountInfoAsync(participantPda);
            if (!participantAccountInfo.WasSuccessful || participantAccountInfo.Result.Value == null)
            {
                Debug.LogError("Participant account does not exist. Please join the tournament first.");
                Manager.instance.canvasManager.ShowErrorPopup("Participant account does not exist. Please join the tournament first.");

                return;
            }

            // Build transaction instruction
            var transactionInstruction = new TransactionInstruction
            {
                ProgramId = new PublicKey(ProgramId),
                Keys = new List<AccountMeta>
            {
                AccountMeta.Writable(participantPda, false), // participant account
                AccountMeta.Writable(tournamentPda, false),   // tournament account
                AccountMeta.ReadOnly(account.PublicKey, true), // player signer
            },
                Data = data
            };

            // Build and send the transaction
            var transactionBuilder = new TransactionBuilder()
                .SetRecentBlockHash(blockHashResult.Result.Value.Blockhash)
                .SetFeePayer(account.PublicKey)
                .AddInstruction(transactionInstruction);

            var transaction = transactionBuilder.Build(account);

            // Simulate the transaction before sending (optional but recommended)
            var simulateResult = await rpcClient.SimulateTransactionAsync(transaction);
            if (!simulateResult.WasSuccessful)
            {
                Debug.LogError("Transaction simulation failed: " + simulateResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Transaction simulation failed: " + simulateResult.Reason);

                return;
            }

            // Send the transaction
            var transactionResult = await rpcClient.SendTransactionAsync(transaction);

            if (transactionResult.WasSuccessful)
            {
                Debug.Log("Score submitted successfully. Signature: " + transactionResult.Result);

                // Wait for confirmation before proceeding
                await WaitForConfirmation(transactionResult.Result);

                UniTask.Void(LoadCurrentWalletBalance);
            }
            else
            {
                Debug.LogError("Submitting score failed: " + transactionResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Submitting score failed: " + transactionResult.Reason);

            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error during score submission: " + ex.Message);
            Manager.instance.canvasManager.ShowErrorPopup("Error during score submission: " + ex.Message);

        }
    }


    //TO DO
    private async UniTaskVoid EndTournament()
    {


        Debug.Log("Preparing to end the tournament...");

        try
        {

            ulong tournamentId = (ulong)Manager.instance.tournamentDataManager.tournamentId;
            //ulong tournamentId = 63;



            //Get top 3 participant walelt address/public key

            PublicKey firstPlacePubKey = account.PublicKey;
            PublicKey secondPlacePubKey = account.PublicKey;
            PublicKey thirdPlacePubKey = account.PublicKey;


            // Fetch the latest blockhash
            var blockHashResult = await rpcClient.GetLatestBlockHashAsync();
            if (!blockHashResult.WasSuccessful)
            {
                Debug.LogError("Failed to get latest block hash: " + blockHashResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Failed to get latest block hash: " + blockHashResult.Reason);

                return;
            }

            // Compute the instruction discriminator for 'end_tournament'
            var discriminator = ComputeInstructionDiscriminator("global:end_tournament");

            // Build the instruction data (tournament ID + top 3 public keys)
            byte[] data = new byte[discriminator.Length + 8 + 32 + 32 + 32]; // Adjusted length
            Buffer.BlockCopy(discriminator, 0, data, 0, discriminator.Length);
            int offset = discriminator.Length;

            // Serialize tournamentId (u64)
            Buffer.BlockCopy(SerializeU64(tournamentId), 0, data, offset, 8);
            offset += 8;

            // Serialize firstPlacePubKey
            Buffer.BlockCopy(firstPlacePubKey.KeyBytes, 0, data, offset, 32);
            offset += 32;

            // Serialize secondPlacePubKey
            Buffer.BlockCopy(secondPlacePubKey.KeyBytes, 0, data, offset, 32);
            offset += 32;

            // Serialize thirdPlacePubKey
            Buffer.BlockCopy(thirdPlacePubKey.KeyBytes, 0, data, offset, 32);

            // Create the PDA for the tournament account
            var tournamentPda = await GetTournamentPda(tournamentId);
            Debug.Log("End Tournament Tournament PDA: " + tournamentPda);

            // Build transaction instruction
            var transactionInstruction = new TransactionInstruction
            {
                ProgramId = new PublicKey(ProgramId),
                Keys = new List<AccountMeta>
            {
                AccountMeta.Writable(tournamentPda, false),            // Tournament account
                AccountMeta.Writable(firstPlacePubKey, false),         // First place participant account
                AccountMeta.Writable(secondPlacePubKey, false),        // Second place participant account
                AccountMeta.Writable(thirdPlacePubKey, false),         // Third place participant account
                AccountMeta.Writable(account.PublicKey, false),        // Creator account
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false) // System program
            },
                Data = data
            };

            // Build and send the transaction
            var transactionBuilder = new TransactionBuilder()
                .SetRecentBlockHash(blockHashResult.Result.Value.Blockhash)
                .SetFeePayer(account.PublicKey)
                .AddInstruction(transactionInstruction);

            var transaction = transactionBuilder.Build(account);

            // Optional: Simulate the transaction before sending
            var simulateResult = await rpcClient.SimulateTransactionAsync(transaction);
            if (!simulateResult.WasSuccessful)
            {
                Debug.LogError("Transaction simulation failed: " + simulateResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Transaction simulation failed: " + simulateResult.Reason);

                return;
            }

            // Send the transaction
            var transactionResult = await rpcClient.SendTransactionAsync(transaction);
            if (transactionResult.WasSuccessful)
            {
                Debug.Log("Tournament ended successfully. Signature: " + transactionResult.Result);

                // Wait for confirmation before proceeding
                await WaitForConfirmation(transactionResult.Result);

                UniTask.Void(LoadCurrentWalletBalance);
            }
            else
            {
                Debug.LogError("Ending tournament failed: " + transactionResult.Reason);
                Manager.instance.canvasManager.ShowErrorPopup("Ending tournament failed: " + transactionResult.Reason);

            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error during ending tournament: " + ex.Message);
            Manager.instance.canvasManager.ShowErrorPopup("Error during ending tournament: " + ex.Message);

        }
    }


    private async UniTask WaitForConfirmation(string signature)
    {
        Manager.instance.canvasManager.transactionPopupPanelGameObject.SetActive(true);
        Manager.instance.canvasManager.transactionPopupStatusText.text = "Waiting for confirmation of transaction: " + signature;
        Debug.Log("Waiting for confirmation of transaction: " + signature);

        for (int i = 0; i < 10; i++) // Retry up to 10 times
        {
            // Update on-screen message for the user
            Manager.instance.canvasManager.transactionPopupStatusText.text += $"\nChecking confirmation... Attempt {i + 1}/10";

            // Check transaction status
            var result = await rpcClient.GetTransactionAsync(signature);
            if (result.WasSuccessful && result.Result != null && result.Result.Meta != null)
            {
                // Display logs once confirmed
                Manager.instance.canvasManager.transactionPopupStatusText.text += "\nTransaction confirmed. Logs:\n" + string.Join("\n", result.Result.Meta.LogMessages);
                Debug.Log("Transaction confirmed. Logs:\n" + string.Join("\n", result.Result.Meta.LogMessages));

                await UniTask.Delay(1000);

                Manager.instance.canvasManager.transactionPopupStatusText.text = null;
                Manager.instance.canvasManager.transactionPopupPanelGameObject.SetActive(false);

                return;
            }

            // Display a progress indicator while waiting for 3 seconds
            for (int j = 0; j < 3; j++) // This will run every second for 3 seconds
            {
                Manager.instance.canvasManager.transactionPopupStatusText.text += ".";
                await UniTask.Delay(1000); // Wait for 1 second
            }

            // Reset the progress indicator for the next attempt
            Manager.instance.canvasManager.transactionPopupStatusText.text += "\nTransaction not confirmed yet. Retrying...";
            Debug.Log("Transaction not confirmed yet. Retrying...");
        }

        // If transaction confirmation fails after retries, show error
        Manager.instance.canvasManager.transactionPopupStatusText.text += "\nTransaction confirmation timed out.";
        Debug.LogError("Transaction confirmation timed out.");
        Manager.instance.canvasManager.ShowErrorPopup("Transaction confirmation timed out.");

    }



    // Method to compute the instruction discriminator
    private byte[] ComputeInstructionDiscriminator(string functionName)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(functionName));
            byte[] discriminator = new byte[8];
            Array.Copy(hash, 0, discriminator, 0, 8);
            return discriminator;
        }
    }

    // Helper method to get the tournament PDA
    private async UniTask<PublicKey> GetTournamentPda(ulong tournamentId)
    {
        byte[] tournamentIdBytes = SerializeU64(tournamentId);
        PublicKey.TryFindProgramAddress(
            new[] { Encoding.UTF8.GetBytes("tournament"), tournamentIdBytes },
            new PublicKey(Manager.instance.tournamentDataManager.tournamentManagerAddress),
            out PublicKey pda,
            out _
        );
        return pda;
    }

    private async UniTask<PublicKey> GetParticipantPda(ulong tournamentId, PublicKey participant)
    {
        byte[] tournamentIdBytes = SerializeU64(tournamentId);
        PublicKey.TryFindProgramAddress(
            new[] { Encoding.UTF8.GetBytes("participant"), tournamentIdBytes, participant.KeyBytes },
            new PublicKey(ProgramId),
            out PublicKey pda,
            out _
        );
        return pda;
    }

    private byte[] SerializeU64(ulong value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return bytes;
    }

    private byte[] SerializeI64(long value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return bytes;
    }

    public long ConvertToUnixTimestamp(int date, int time)
    {
        // Extract year, month, and day from the date (yyyyMMdd)
        int year = date / 10000;
        int month = (date % 10000) / 100;
        int day = date % 100;

        // Extract hours and minutes from the time (HHmm)
        int hours = time / 100;
        int minutes = time % 100;

        // Create a DateTime object in UTC
        DateTime dateTimeUtc = new DateTime(year, month, day, hours, minutes, 0, DateTimeKind.Utc);

        // Convert to UNIX timestamp (seconds since Jan 1, 1970)
        long unixTimestamp = (long)(dateTimeUtc - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

        return unixTimestamp;
    }
}
