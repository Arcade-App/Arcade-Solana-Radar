using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Aptos.Unity.Rest;
using Aptos.Unity.Rest.Model;
using System.Collections;
using Aptos.HdWallet;
using NBitcoin;
using Aptos.Accounts;

public class WalletManager : MonoBehaviour
{

    [HideInInspector]
    public string mnemonicsKey = "MnemonicsKey";
    [HideInInspector]
    public string privateKey = "PrivateKey";
    //[HideInInspector]
    //public string currentAddressIndexKey = "CurrentAddressIndexKey";

    private Wallet wallet;

    private string faucetEndpoint = "https://faucet.testnet.aptoslabs.com";   //testnet faucet endpoint

    public event Action<float> onGetWalletBalance;

    

    private void Awake()
    {
        //RestClient.Instance.SetEndPoint(Constants.TESTNET_BASE_URL);
        //RestClient.Instance.SetEndPoint("https://faucet.testnet.aptoslabs.com");
    }

    // Start is called before the first frame update
    void Start()
    {
        onGetWalletBalance += UpdateWalletBalance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTournamentManagerData(string tournamentManagerAddress, string tournamentContractName)
    {
        RestClient.Instance.tournamentManagerAddress = tournamentManagerAddress;
        RestClient.Instance.tournamentContractName = tournamentContractName;
    }

    public void CreateNewWallet()
    {
        Mnemonic mnemonics = new Mnemonic(Wordlist.English, WordCount.Twelve);
        wallet = new Wallet(mnemonics);

        Manager.instance.userInfoManager.walletMnemonics = mnemonics.ToString();

        //PlayerPrefs.SetString(mnemonicsKey, mnemo.ToString());
        //PlayerPrefs.SetInt(currentAddressIndexKey, 0);



        //Displays Seed Phrase
        Debug.Log(mnemonics);   
        


        GetWalletAddress();

    }

    // Initiate wallet from cache
    public void GetWalletFromMenmonicsKey(string mnemonics)
    {
        wallet = new Wallet(mnemonics);
        GetWalletAddress();
        LoadCurrentWalletBalance();
    }



    public void GetWalletAddress()
    {

        var account = wallet.GetAccount(0);
        var addr = account.AccountAddress.ToString();

        Debug.Log("Address: " + addr);

        Manager.instance.userInfoManager.walletAddress = addr;

    }


    public IEnumerator AirDrop(int _amount)
    {

        Debug.Log("-------------------> PROBLEM <-------------------");

        Coroutine cor = StartCoroutine(FaucetClient.Instance.FundAccount((success, returnResult) =>
        {
        }, Manager.instance.userInfoManager.walletAddress
            , _amount
            , faucetEndpoint));

        yield return cor;

        Debug.Log("Debug 6: After Fund Account" + cor);


        yield return new WaitForSeconds(1f);
        LoadCurrentWalletBalance();

    }

    public void LoadCurrentWalletBalance()
    {

        Debug.Log("Debug 7: Loading Current Balance");


        AccountResourceCoin.Coin coin = new AccountResourceCoin.Coin();
        ResponseInfo responseInfo = new ResponseInfo();

        Debug.Log("Debug 8: Created coin & responseInfo");

        Debug.Log("Debug 9: Before GetAccountBalance Coroutine");

        StartCoroutine(RestClient.Instance.GetAccountBalance((_coin, _responseInfo) =>
        {
            coin = _coin;
            responseInfo = _responseInfo;

            Debug.Log("Debug 14: GetAccountBalance Coroutine Response: " + responseInfo);


            if (responseInfo.status != ResponseInfo.Status.Success)
            {
                Debug.Log("Debug 15: GetAccountBalance Coroutine Response Not Success: " + responseInfo.status);
                onGetWalletBalance?.Invoke(0.0f);
            }
            else
            {
                Debug.Log("Debug 15: GetAccountBalance Coroutine Response Success: " + responseInfo.status);
                onGetWalletBalance?.Invoke(float.Parse(coin.Value));
            }

        }, wallet.GetAccount(0).AccountAddress));

    }

    void UpdateWalletBalance(float _amount)
    {
        Debug.Log("Debug 16: Update Wallet Balance Function");

        Debug.Log("Storing Wallet Balance");

        Manager.instance.userInfoManager.walletBalanceInt = (int)_amount;
        Debug.Log("Debug 17: Getting userInfoManager walletBalance: " + (int)_amount);

        Manager.instance.userInfoManager.walletBalanceFloat = AptosTokenToFloat(_amount);
        Debug.Log("Debug 18: Getting userInfoManager walletBalanceFloat: " + AptosTokenToFloat(_amount));

    }

    public float AptosTokenToFloat(float _token)
    {
        return _token / 100000000f;
    }




    public IEnumerator StartNewTournament(
                                          string tournamentId,
                                          string prizePoolAmount,
                                          string entryFee,
                                          string startDate,
                                          string endDate,
                                          string startTime,
                                          string endTime)
    {
        Aptos.Unity.Rest.Model.Transaction startTournamentTxn = null;
        ResponseInfo responseInfo = new ResponseInfo();

        // Retrieve the account (assumes account is already initialized)
        // Removing PlayerPrefs
        //Account account = wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey));
        
        Account account = wallet.GetAccount(0);

        if (account == null)
        {
            Debug.Log("Account not found");
            yield break;
        }

        // Start the coroutine to execute the start_new_tournament transaction
        Coroutine startTournamentCor = StartCoroutine(RestClient.Instance.StartNewTournamentTransactionWrapper(
            (_startTournamentTxn, _responseInfo) =>
            {
                startTournamentTxn = _startTournamentTxn;
                responseInfo = _responseInfo;
            },
            account,
            tournamentId,
            prizePoolAmount,
            entryFee,
            startDate,
            endDate,
            startTime,
            endTime
        ));

        yield return startTournamentCor;

        // Check if the transaction was successful
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            Debug.Log("Successfully started the tournament.");
        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to start the tournament.");
        }

        // Optionally wait for a short time before continuing
        yield return new WaitForSeconds(1f);

        // Log or handle the transaction hash
        string transactionHash = startTournamentTxn?.Hash;
        if (!string.IsNullOrEmpty(transactionHash))
        {
            Debug.Log("Transaction Hash: " + transactionHash);

        }
        else
        {
            Debug.LogError("Transaction failed or hash is null.");
        }
    }

    public IEnumerator ExecuteStartNewTournament(string tournamentId, string prizePoolAmount, string entryFee, string startDate, string endDate, string startTime, string endTime)
    {

        //// Validate inputs (simple validation, customize as needed)
        //if (string.IsNullOrEmpty(tournamentId) ||
        //    string.IsNullOrEmpty(prizePoolAmount) ||
        //    string.IsNullOrEmpty(entryFee) ||
        //    string.IsNullOrEmpty(startDate) ||
        //    string.IsNullOrEmpty(endDate) ||
        //    string.IsNullOrEmpty(startTime) ||
        //    string.IsNullOrEmpty(endTime))
        //{
        //    Debug.Log("Please fill in all required fields.");
        //    return;
        //}



        


        // Start the coroutine to initiate the start_new_tournament transaction
        yield return StartCoroutine(StartNewTournament(
            tournamentId,
            prizePoolAmount,
            entryFee,
            startDate,
            endDate,
            startTime,
            endTime
        ));
    }


    public IEnumerator EnterTournament(string tournamentId, string userId)
    {
        Aptos.Unity.Rest.Model.Transaction enterTournamentTxn = null;
        ResponseInfo responseInfo = new ResponseInfo();

        // Retrieve the participant's account (assumes account is already initialized)
        // Removing PlayerPrefs
        //Account participant = wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey));

        Account participant = wallet.GetAccount(0);

        if (participant == null)
        {
            Debug.Log("Account not found");
            yield break;
        }

        // Start the coroutine to execute the enter_tournament transaction
        Coroutine enterTournamentCor = StartCoroutine(RestClient.Instance.EnterTournamentTransactionWrapper(
            (_enterTournamentTxn, _responseInfo) =>
            {
                enterTournamentTxn = _enterTournamentTxn;
                responseInfo = _responseInfo;
            },
            participant,
            tournamentId,
            userId
        ));

        yield return enterTournamentCor;

        // Check if the transaction was successful
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            Debug.Log("Successfully entered the tournament.");
        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to enter the tournament.");
        }

        // Optionally wait for a short time before continuing
        yield return new WaitForSeconds(1f);

        // Log or handle the transaction hash
        string transactionHash = enterTournamentTxn?.Hash;
        if (!string.IsNullOrEmpty(transactionHash))
        {
            Debug.Log("Transaction Hash: " + transactionHash);
        }
        else
        {
            Debug.LogError("Transaction failed or hash is null.");
        }
    }


    public IEnumerator ExecuteEnterTournament(string tournamentId, string userId)
    {

        // Validate inputs (simple validation, customize as needed)
        //if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrEmpty(userId))
        //{
        //    Debug.Log("Please fill in all required fields.");
        //    return;
        //}

        // Start the coroutine to enter the tournament
        yield return StartCoroutine(EnterTournament(tournamentId, userId));



    }



    public IEnumerator RecordScore(string tournamentId, string score)
    {
        Aptos.Unity.Rest.Model.Transaction recordScoreTxn = null;
        ResponseInfo responseInfo = new ResponseInfo();

        // Retrieve the participant's account (assumes account is already initialized)
        // Removing PlayerPrefs
        //Account participant = wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey));

        Account participant = wallet.GetAccount(0);
        if (participant == null)
        {
            Debug.Log("Account not found");
            yield break;
        }

        // Start the coroutine to execute the record_score transaction
        Coroutine recordScoreCor = StartCoroutine(RestClient.Instance.RecordScoreTransactionWrapper(
            (_recordScoreTxn, _responseInfo) =>
            {
                recordScoreTxn = _recordScoreTxn;
                responseInfo = _responseInfo;
            },
            participant,
            tournamentId,
            score
        ));

        yield return recordScoreCor;

        // Check if the transaction was successful
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            Debug.Log("Successfully recorded the score.");


        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to record the score.");
        }

        // Optionally wait for a short time before continuing
        yield return new WaitForSeconds(1f);

        // Log or handle the transaction hash
        string transactionHash = recordScoreTxn?.Hash;
        if (!string.IsNullOrEmpty(transactionHash))
        {
            Debug.Log("Transaction Hash: " + transactionHash);
        }
        else
        {
            Debug.LogError("Transaction failed or hash is null.");
        }
    }

    public IEnumerator ExecuteRecordScore(string tournamentId, string score)
    {

        //// Validate inputs (simple validation, customize as needed)
        //if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrEmpty(score))
        //{
        //    Debug.Log("Please fill in all required fields.");
        //    return;
        //}

        // Start the coroutine to record the score
        yield return StartCoroutine(RecordScore(tournamentId, score));
    }


    public IEnumerator FetchLeaderboard(string tournamentId)
    {
        List<Participant> leaderboard = null;
        ResponseInfo responseInfo = new ResponseInfo();

        // Start the coroutine to fetch the leaderboard
        Coroutine leaderboardCor = StartCoroutine(RestClient.Instance.GetLeaderboardTransactionWrapper(
            (_leaderboard, _responseInfo) =>
            {
                leaderboard = _leaderboard;
                responseInfo = _responseInfo;
            },
            tournamentId
        ));

        yield return leaderboardCor;

        // Check if the leaderboard was fetched successfully
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            Debug.Log("Leaderboard fetched successfully.");

            // Display or process the leaderboard
            if (leaderboard != null && leaderboard.Count > 0)
            {
                foreach (var participant in leaderboard)
                {
                    Debug.Log($"Participant: {participant.account}, UserID: {participant.userId}, Score: {participant.score}");
                }
            }
            else
            {
                Debug.Log("No participants in the leaderboard.");
            }
        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to fetch the leaderboard.");
        }
    }

    public void ExecuteFetchLeaderboard(string tournamentId)
    {

        // Validate inputs (simple validation, customize as needed)
        if (string.IsNullOrEmpty(tournamentId))
        {
            Debug.Log("Please fill in the tournament ID.");
            return;
        }

        // Start the coroutine to fetch the leaderboard
        StartCoroutine(FetchLeaderboard(tournamentId));
    }



    public IEnumerator FetchTournamentInfo(string tournamentId)
    {
        TournamentInfo tournamentInfo = null;
        ResponseInfo responseInfo = new ResponseInfo();

        // Start the coroutine to fetch the tournament information
        Coroutine tournamentInfoCor = StartCoroutine(RestClient.Instance.GetTournamentInfoTransactionWrapper(
            (_tournamentInfo, _responseInfo) =>
            {
                tournamentInfo = _tournamentInfo;
                responseInfo = _responseInfo;
            },
            tournamentId
        ));

        yield return tournamentInfoCor;

        // Check if the tournament information was fetched successfully
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            Debug.Log("Tournament information fetched successfully.");

            // Display or process the tournament info
            if (tournamentInfo != null)
            {

                // Convert start and end times from Unix timestamp in microseconds to local time
                DateTime startTime = UnixTimeStampToDateTime(tournamentInfo.StartTime);
                DateTime endTime = UnixTimeStampToDateTime(tournamentInfo.EndTime);

                // Format the start and end times with AM/PM
                string formattedStartTime = startTime.ToString("dd:MM:yy hh:mm:ss tt");
                string formattedEndTime = endTime.ToString("dd:MM:yy hh:mm:ss tt");

                Debug.Log($"Tournament Info - Start Time: {tournamentInfo.StartTime}, End Time: {tournamentInfo.EndTime}, " +
                          $"Participants: {tournamentInfo.NumParticipants}, Creator: {tournamentInfo.CreatorAddress}, " +
                          $"Entry Fee: {tournamentInfo.EntryFee}, Prize Pool: {tournamentInfo.PrizePoolAmount}, Status: {tournamentInfo.Status}");
            }
            else
            {
                Debug.Log("Failed to fetch tournament information.");
            }
        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to fetch tournament information.");
        }
    }


    public void ExecuteFetchTournamentInfo(string tournamentId)
    {

        // Validate inputs (simple validation, customize as needed)
        if (string.IsNullOrEmpty(tournamentId))
        {
            Debug.Log("Please fill in the tournament ID.");
            return;
        }

        // Start the coroutine to fetch the tournament information
        StartCoroutine(FetchTournamentInfo(tournamentId));
    }

    public IEnumerator FetchParticipantScore(string tournamentId, string userId)
    {
        ulong participantScore = 0;
        ResponseInfo responseInfo = new ResponseInfo();

        // Start the coroutine to fetch the participant's score
        Coroutine participantScoreCor = StartCoroutine(RestClient.Instance.GetParticipantScoreTransactionWrapper(
            (_participantScore, _responseInfo) =>
            {
                participantScore = _participantScore;
                responseInfo = _responseInfo;
            },
            tournamentId,
            userId
        ));

        yield return participantScoreCor;

        // Check if the participant's score was fetched successfully
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            Debug.Log($"Participant Score fetched successfully. Score: {participantScore}");
        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to fetch the participant's score.");
        }
    }


    public void ExecuteFetchParticipantScore(string tournamentId, string userId)
    {
        // Retrieve inputs from the UI (assuming you have input fields for tournamentId and userId)
        tournamentId = tournamentId.Trim();
        userId = userId.Trim();

        // Validate inputs (simple validation, customize as needed)
        if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrEmpty(userId))
        {
            Debug.Log("Please fill in the tournament ID and user ID.");
            return;
        }

        // Start the coroutine to fetch the participant's score
        StartCoroutine(FetchParticipantScore(tournamentId, userId));
    }



    private DateTime UnixTimeStampToDateTime(ulong unixTimeStampInMicroseconds)
    {
        // Convert microseconds to seconds
        double unixTimeStampInSeconds = unixTimeStampInMicroseconds / 1_000_000.0;

        // Unix timestamp is seconds past epoch (January 1, 1970)
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Add the seconds to the epoch time and convert to local time
        DateTime dateTime = epoch.AddSeconds(unixTimeStampInSeconds).ToLocalTime();

        return dateTime;
    }





}
