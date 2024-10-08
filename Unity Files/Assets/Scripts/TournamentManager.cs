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

public class TournamentManager : MonoBehaviour
{


    [HideInInspector]
    public string mnemonicsKey = "MnemonicsKey";
    [HideInInspector]
    public string privateKey = "PrivateKey";
    [HideInInspector]
    public string currentAddressIndexKey = "CurrentAddressIndexKey";

    //Only have one account per wallet
    [SerializeField] private int accountNumLimit = 1;
    public List<string> addressList;

    public event Action<float> onGetBalance;

    private Wallet wallet;
    //private string faucetEndpoint = "https://faucet.devnet.aptoslabs.com";  //devnet faucet endpoint
    private string faucetEndpoint = "https://faucet.testnet.aptoslabs.com";   //testnet faucet endpoint



    public TMP_Text seedPharseText;
    public GameObject createWalletPanelGO;
    public GameObject walletInfoPanelGO;
    public TMP_Text walletAddressText;
    public TMP_Text walletBalanceText;
    public TMP_Text walletNetworkText;
    public GameObject tournamentPanelGO;



    public string tournamentAddr = "0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed";
    //public Action<Aptos.Unity.Rest.Model.Transaction, ResponseInfo> onTransactionResult; // Callback for transaction results
    //public Account userAccount;

    private void Awake()
    {
        //Setting Network to Testnet
        //RestClient.Instance.SetEndPoint(Constants.TESTNET_BASE_URL);
    }

    // Start is called before the first frame update
    void Start()
    {
        onGetBalance += UpdateWalletBalance;
        InitWalletFromCache();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Initiate wallet from cache
    public void InitWalletFromCache()
    {
        wallet = new Wallet(PlayerPrefs.GetString(mnemonicsKey));
        GetWalletAddress();
        LoadCurrentWalletBalance();
    }

    /// <summary>
    /// Create a new Wallet by generating a new Seed Phrase 
    /// Involves creating wallet, gettgin wallet address, and loading current wallet balance
    /// Set wallet to Testnet
    /// Airdrop 1APT to the wallet
    /// </summary>
    public bool CreateNewWallet()
    {
        Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
        wallet = new Wallet(mnemo);
        
        PlayerPrefs.SetString(mnemonicsKey, mnemo.ToString());
        PlayerPrefs.SetInt(currentAddressIndexKey, 0);

        //Displays Seed Phrase
        Debug.Log(mnemo);
        seedPharseText.text = "Seed Phrase: " + mnemo;

        GetWalletAddress();
        LoadCurrentWalletBalance();
        StartCoroutine(AirDrop(100000000));


        if (mnemo.ToString() != string.Empty)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public bool RestoreWallet(string _mnemo)
    //{
    //    try
    //    {
    //        wallet = new Wallet(_mnemo);
    //        PlayerPrefs.SetString(mnemonicsKey, _mnemo);
    //        PlayerPrefs.SetInt(currentAddressIndexKey, 0);

    //        GetWalletAddress();
    //        LoadCurrentWalletBalance();

    //        return true;
    //    }
    //    catch
    //    {

    //    }

    //    return false;
    //}


    /// <summary>  
    /// Gets all addresses associated with the wallet
    /// Currently limiting to 1 address only
    /// <summary>
    public List<string> GetWalletAddress()
    {
        addressList = new List<string>();

        for (int i = 0; i < accountNumLimit; i++)
        {
            var account = wallet.GetAccount(i);
            var addr = account.AccountAddress.ToString();

            addressList.Add(addr);

            Debug.Log("Address: " + addr);
        }

        return addressList;
    }

    //public string GetCurrentWalletAddress()
    //{
    //    return addressList[PlayerPrefs.GetInt(currentAddressIndexKey)];
    //}

    //public string GetCurrentWalletAddress()
    //{
    //    return wallet.Account.PrivateKey;
    //}

    /// <summary>  
    /// Loads the current wallet balance for the current account address stored in PlayerPrefs
    /// <summary>  
    public void LoadCurrentWalletBalance()
    {
        AccountResourceCoin.Coin coin = new AccountResourceCoin.Coin();
        ResponseInfo responseInfo = new ResponseInfo();

        StartCoroutine(RestClient.Instance.GetAccountBalance((_coin, _responseInfo) =>
        {
            coin = _coin;
            responseInfo = _responseInfo;

            if (responseInfo.status != ResponseInfo.Status.Success)
            {
                onGetBalance?.Invoke(0.0f);
            }
            else
            {
                onGetBalance?.Invoke(float.Parse(coin.Value));
            }

        }, wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey)).AccountAddress));
    }

    /// <summary>  
    /// Airdrops 1 APT to the  current account address stored in PlayerPrefs
    /// 1 sec after airdrop, refreshes the current wallet balance
    /// <summary>  
    public IEnumerator AirDrop(int _amount)
    {
        Coroutine cor = StartCoroutine(FaucetClient.Instance.FundAccount((success, returnResult) =>
        {
        }, wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey)).AccountAddress.ToString()
            , _amount
            , faucetEndpoint));

        yield return cor;

        yield return new WaitForSeconds(1f);
        LoadCurrentWalletBalance();

    }

    void UpdateWalletBalance(float _amount)
    {
        walletBalanceText.text = AptosTokenToFloat(_amount).ToString("0.0000") + " APT";
    }

    public float AptosTokenToFloat(float _token)
    {
        return _token / 100000000f;
    }

    public void OnCreateWalletClicked()
    {
        CreateNewWallet();
    }

    public void OnWalletInfoClicked()
    {
        createWalletPanelGO.SetActive(false);
        walletInfoPanelGO.SetActive(true);

        walletAddressText.text = addressList[0];

        LoadCurrentWalletBalance();

        walletNetworkText.text = "Testnet";
    }

    public void OnGetAirdropClicked()
    {
        StartCoroutine(AirDrop(100000000));
    }

    public void OnRefreshBalanceClicked()
    {
        LoadCurrentWalletBalance();
    }

    public void OnTournamentPageClicked()
    {
        createWalletPanelGO.SetActive(false);
        walletInfoPanelGO.SetActive(false);
        tournamentPanelGO.SetActive(true);
    }


    //public void OnEnterTournamentClicked()
    //{
    //    TransactionPayload payload = new TransactionPayload()
    //    {
    //        Type = Constants.ENTRY_FUNCTION_PAYLOAD,
    //        Function = "0x61e23ad160c0fd438a39ae82698585492452fcec6e1aee32863b426dcac0782f::tourn2::reset_tournament",
    //        TypeArguments = new string[] { },
    //        Arguments = new Arguments
    //        {
    //            ArgumentStrings = new string[] { tournamentAddress }
    //        }
    //    };

    //    userAccount = wallet.GetAccount(0);

    //    StartCoroutine(RestClient.Instance.SubmitTransaction(HandleTransactionResponse, userAccount, payload));


    //}

    ///// <summary>
    ///// Callback for handling the result of a transaction.
    ///// </summary>
    ///// <param name="transaction">The transaction that was submitted.</param>
    ///// <param name="responseInfo">Information about the transaction response.</param>
    //private void HandleTransactionResponse(Aptos.Unity.Rest.Model.Transaction transaction, ResponseInfo responseInfo)
    //{
    //    if (responseInfo.status == ResponseInfo.Status.Success)
    //    {
    //        Debug.Log("Transaction succeeded: " + responseInfo.message);
    //    }
    //    else
    //    {
    //        Debug.LogError("Transaction failed: " + responseInfo.message);
    //    }

    //    onTransactionResult?.Invoke(transaction, responseInfo);
    //}


    //public IEnumerator EnterTournament(string tournamentAddress)
    //{
    //    Aptos.Unity.Rest.Model.Transaction enterTournamentTxn = null;
    //    ResponseInfo responseInfo = new ResponseInfo();

    //    // Retrieve the participant's account
    //    Account participant = wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey));
    //    if (participant == null)
    //    {
    //        Debug.Log("Account not found");
    //        yield break;
    //    }

    //    // Start the coroutine to execute the transaction
    //    Coroutine enterTournamentCor = StartCoroutine(RestClient.Instance.EnterTournamentTransactionWrapper((_enterTournamentTxn, _responseInfo) =>
    //    {
    //        enterTournamentTxn = _enterTournamentTxn;
    //        responseInfo = _responseInfo;
    //    }, participant, tournamentAddress));
    //    yield return enterTournamentCor;

    //    //Debug.Log("T Address 1: " + tournamentAddress);

    //    // Check if the response is successful
    //    if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
    //    {
    //        Debug.Log("Successfully entered the tournament.");
    //    }
    //    else
    //    {
    //        Debug.Log(responseInfo?.message ?? "Failed to enter the tournament.");
    //    }

    //    // Optionally wait before proceeding
    //    yield return new WaitForSeconds(1f);

    //    // Log or handle the transaction hash
    //    string transactionHash = enterTournamentTxn?.Hash;
    //    if (!string.IsNullOrEmpty(transactionHash))
    //    {
    //        Debug.Log("Transaction Hash: " + transactionHash);
    //    }
    //    else
    //    {
    //        Debug.LogError("Transaction failed or hash is null.");
    //    }
    //}


    //public void ExecuteEnterTournament()
    //{
    //    // Validate the input for the tournament address
    //    string tournamentAddress = tournamentAddr.Trim();
    //    if (string.IsNullOrEmpty(tournamentAddress))
    //    {
    //        Debug.Log("Please provide a valid tournament address.");
    //        return;
    //    }

    //    // Start the coroutine to enter the tournament
    //    StartCoroutine(EnterTournament("0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed"));
    //}


    //public IEnumerator RecordScore(string tournamentAddress, ulong score)
    //{
    //    Aptos.Unity.Rest.Model.Transaction recordScoreTxn = null;
    //    ResponseInfo responseInfo = new ResponseInfo();

    //    // Retrieve the participant's account
    //    Account participant = wallet.GetAccount(PlayerPrefs.GetInt(currentAddressIndexKey));
    //    if (participant == null)
    //    {
    //        Debug.Log("Account not found.");
    //        yield break;
    //    }

    //    // Start the coroutine to execute the transaction
    //    Coroutine recordScoreCor = StartCoroutine(RestClient.Instance.RecordScoreTransactionWrapper((_recordScoreTxn, _responseInfo) =>
    //    {
    //        recordScoreTxn = _recordScoreTxn;
    //        responseInfo = _responseInfo;
    //    }, participant, tournamentAddress, score));
    //    yield return recordScoreCor;

    //    // Check if the response is successful
    //    if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
    //    {
    //        Debug.Log("Score recorded successfully.");
    //    }
    //    else
    //    {
    //        Debug.Log("Failed to record score.");
    //    }

    //    // Optionally wait before proceeding
    //    yield return new WaitForSeconds(1f);

    //    // Log or handle the transaction hash
    //    string transactionHash = recordScoreTxn?.Hash;
    //    if (!string.IsNullOrEmpty(transactionHash))
    //    {
    //        Debug.Log("Transaction Hash: " + transactionHash);
    //    }
    //    else
    //    {
    //        Debug.LogError("Transaction failed or hash is null.");
    //    }
    //}

    //public void ExecuteRecordScore()
    //{
    //    // Validate the input for the tournament address and score
    //    string tournamentAddress = tournamentAddr.Trim();
    //    ulong score = 1000;

    //    if (string.IsNullOrEmpty(tournamentAddress))
    //    {
    //        Debug.Log("Please provide a valid tournament address.");
    //        return;
    //    }

    //    //if (!ulong.TryParse(scoreInputField.text, out score))
    //    //{
    //    //    UIController.Instance.ToggleNotification(ResponseInfo.Status.Failed, "Please provide a valid score.");
    //    //    return;
    //    //}

    //    // Start the coroutine to record the score
    //    StartCoroutine(RecordScore("0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed", score));
    //}


    //public IEnumerator GetParticipantScore(string tournamentAddress, string participantAddress)
    //{
    //    ulong participantScore = 0;
    //    ResponseInfo responseInfo = new ResponseInfo();

    //    // Start the coroutine to execute the query
    //    Coroutine getParticipantScoreCor = StartCoroutine(RestClient.Instance.GetParticipantScoreTransactionWrapper((_participantScore, _responseInfo) =>
    //    {
    //        participantScore = _participantScore;
    //        responseInfo = _responseInfo;
    //    }, tournamentAddress, participantAddress));
    //    yield return getParticipantScoreCor;

    //    // Check if the response is successful
    //    if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
    //    {
    //        Debug.Log($"Participant Address: {participantAddress} Score: {participantScore}");
    //    }
    //    else
    //    {
    //        Debug.Log(responseInfo?.message ?? "Failed to retrieve participant score.");
    //    }
    //}



    //public void ExecuteGetParticipantScore()
    //{
    //    // Validate the input for the tournament address and participant address
    //    string tournamentAddress = tournamentAddr;
    //    string participantAddress = addressList[0].Trim();

    //    if (string.IsNullOrEmpty(tournamentAddress) || string.IsNullOrEmpty(participantAddress))
    //    {
    //        Debug.Log("Please provide valid tournament and participant addresses.");
    //        return;
    //    }

    //    // Start the coroutine to get the participant's score
    //    StartCoroutine(GetParticipantScore("0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed", participantAddress));
    //}


    //public IEnumerator GetParticipants(string tournamentAddress)
    //{
    //    List<Participant> participants = new List<Participant>(); // Change to List<Participant>
    //    ResponseInfo responseInfo = new ResponseInfo();

    //    // Start the coroutine to execute the query
    //    Coroutine getParticipantsCor = StartCoroutine(RestClient.Instance.GetParticipantsTransactionWrapper((_participants, _responseInfo) =>
    //    {
    //        participants = _participants; // Update type to List<Participant>
    //        responseInfo = _responseInfo;
    //    }, tournamentAddress));
    //    yield return getParticipantsCor;

    //    // Check if the response is successful
    //    if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
    //    {
    //        // Display each participant's details
    //        foreach (var participant in participants)
    //        {
    //            Debug.Log($"Account: {participant.account}, Score: {participant.score}");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log(responseInfo?.message ?? "Failed to retrieve participants.");
    //    }
    //}


    //public void ExecuteGetParticipants()
    //{
    //    // Validate the input for the tournament address
    //    string tournamentAddress = tournamentAddr.Trim();

    //    if (string.IsNullOrEmpty(tournamentAddress))
    //    {
    //        Debug.Log("Please provide a valid tournament address.");
    //        return;
    //    }

    //    // Start the coroutine to get the list of participants
    //    StartCoroutine(GetParticipants("0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed")); // Use the validated address
    //}


    //public IEnumerator GetTournamentBalance(string tournamentAddress)
    //{
    //    ulong tournamentBalance = 0;
    //    ResponseInfo responseInfo = new ResponseInfo();

    //    // Start the coroutine to execute the query
    //    Coroutine getTournamentBalanceCor = StartCoroutine(RestClient.Instance.GetTournamentBalanceTransactionWrapper((_tournamentBalance, _responseInfo) =>
    //    {
    //        tournamentBalance = _tournamentBalance;
    //        responseInfo = _responseInfo;
    //    }, tournamentAddress));
    //    yield return getTournamentBalanceCor;

    //    // Check if the response is successful
    //    if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
    //    {
    //        Debug.Log($"Tournament Balance: {AptosTokenToFloat((float) tournamentBalance)} APT");

    //    }
    //    else
    //    {
    //        Debug.Log(responseInfo?.message ?? "Failed to retrieve tournament balance.");
    //    }
    //}

    //public void ExecuteGetTournamentBalance()
    //{
    //    // Validate the input for the tournament address
    //    string tournamentAddress = tournamentAddr.Trim();

    //    if (string.IsNullOrEmpty(tournamentAddress))
    //    {
    //        Debug.Log("Please provide a valid tournament address.");
    //        return;
    //    }

    //    // Start the coroutine to get the tournament balance
    //    StartCoroutine(GetTournamentBalance("0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed"));
    //}

    public IEnumerator GetTournamentInfo(string tournamentAddress)
    {
        TournamentInfo tournamentInfo = null;
        ResponseInfo responseInfo = new ResponseInfo();

        // Start the coroutine to execute the query
        Coroutine getTournamentInfoCor = StartCoroutine(RestClient.Instance.GetTournamentInfoTransactionWrapper((_tournamentInfo, _responseInfo) =>
        {
            tournamentInfo = _tournamentInfo;
            responseInfo = _responseInfo;
        }, tournamentAddress));
        yield return getTournamentInfoCor;

        // Check if the response is successful
        if (responseInfo != null && responseInfo.status == ResponseInfo.Status.Success)
        {
            // Convert start and end times from Unix timestamp in microseconds to local time
            DateTime startTime = UnixTimeStampToDateTime(tournamentInfo.StartTime);
            DateTime endTime = UnixTimeStampToDateTime(tournamentInfo.EndTime);

            // Format the start and end times with AM/PM
            string formattedStartTime = startTime.ToString("dd:MM:yy hh:mm:ss tt");
            string formattedEndTime = endTime.ToString("dd:MM:yy hh:mm:ss tt");

            Debug.Log($"Tournament Info - Participants: {tournamentInfo.NumParticipants}, Start Time: {formattedStartTime}, End Time: {formattedEndTime}");
        }
        else
        {
            Debug.Log(responseInfo?.message ?? "Failed to retrieve tournament info.");
        }
    }




    public void ExecuteGetTournamentInfo()
    {
        // Validate the input for the tournament address
        string tournamentAddress = tournamentAddr.Trim();

        if (string.IsNullOrEmpty(tournamentAddress))
        {
            Debug.Log("Please provide a valid tournament address.");
            return;
        }

        // Start the coroutine to get the tournament info
        StartCoroutine(GetTournamentInfo("0xe5da06c67c3020364e21e7f17f2c24297ee6da15026a61a10cdb51d23d9341ed"));
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
