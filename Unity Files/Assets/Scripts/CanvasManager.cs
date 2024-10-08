using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Globalization;
using Cysharp.Threading.Tasks;

public class CanvasManager : MonoBehaviour
{

    [Header("Start Register Sign-In Panel")]
    [Header("Start Panel")]
    public GameObject startRegisterSignInPanelGO;
    public GameObject startPanelGO;


    [Header("Register Panel")]
    public GameObject registerPanelGO;
    public GameObject createAccountGO;


    public InputField usernameInputFieldLegacy;
    public InputField emailInputFieldLegacy;
    public InputField passwordInputFieldLegacy;
    public InputField confirmPasswordInputFieldLegacy;



    //public TMP_InputField usernameInputFieldLegacy;
    //public TMP_InputField emailInputFieldLegacy;
    //public TMP_InputField passwordInputFieldLegacy;
    //public TMP_InputField confirmPasswordInputFieldLegacy;
    public Button createAccountButton;
    public TMP_Text createAccountButtonText;

    public Color inputFieldFilledButtonTextColor;

    [Space(5)]
    public GameObject showPasswordButton;
    public GameObject showConfirmPasswordButton;
    public Sprite showPasswordSprite;
    public Sprite hidePasswordSprite;

    [Space(5)]
    public Color creatingAccountProgressTextColor;

    public GameObject creatingAccountProgressGO;

    public GameObject creatingAccountGroupImageGO;
    public GameObject creatingAccountGroupSelectedImageGO;
    public TMP_Text creatingAccoutntText;

    public GameObject creatingWalletGroupImageGO;
    public GameObject creatingWalletGroupSelectedImageGO;
    public TMP_Text creatingWalletText;

    public GameObject airdroppingGroupImageGO;
    public GameObject airdroppingGroupSelectedImageGO;
    public TMP_Text airdroppingText;

    public GameObject allSetGroupImageGO;
    public GameObject allSetGroupSelectedImageGO;
    public TMP_Text allSetText;

    

    [Header("Sign In Panel")]
    public GameObject signInPanelGO;
    public GameObject signInGO;

    [Space(5)]

    public InputField signInUsernameInputFieldLegacy;
    public InputField signInPasswordInputFieldLegacy;

    //public TMP_InputField signInUsernameInputFieldLegacy;
    //public TMP_InputField signInPasswordInputFieldLegacy;
    public Button signInButton;
    public TMP_Text signInButtonText;


    public GameObject signInShowPasswordButton;




    [Header("Home Profile Tournament Create Panel")]
    public GameObject homeProfileTournamentCreatePanelGO;
    public GameObject testnetIndicatorGroupGO;
    public GameObject aptosBalanceGroupGO;
    public GameObject bottomSeperatorImageGO;
    public GameObject bottomBarToggleGroupGO;
    public GameObject homePanelGO;
    public GameObject tournamentPanelGO;
    public GameObject createPanelGO;
    public GameObject profilePanelGO;

    [Space(5)]
    public Toggle homeToggle;
    public Toggle tournamentToggle;
    public Toggle createToggle;
    public Toggle profileToggle;
    public GameObject homeToggleOnLabelGO;
    public GameObject homeToggleOffLabelGO;
    public GameObject tournamentToggleOnLabelGO;
    public GameObject tournamentToggleOffLabelGO; 
    public GameObject createToggleOnLabelGO;
    public GameObject createToggleOffLabelGO; 
    public GameObject profileToggleOnLabelGO;
    public GameObject profileToggleOffLabelGO;

 


    [Header("Solana Balance Group")]
    public TMP_Text solanaBalanceText;

    
    [Header("Home Panel")]
    public GameObject homePanelHomeGO;

    [Space(5)]
    public List<GameObject> homePanelPopularGamesButtonList = new List<GameObject>();

    [Space(5)]
    public GameObject top30GamesPanelGO;
    public GameObject homePanelTop30GamesButtonPrefab;
    public GameObject homePanelTop30GamesButtonPrefabParent;

    [Space(5)]
    public TMP_Text homePanelTournamentButtonTitle;
    public TMP_Text homePanelTournamentPrizePoolText;
    public TMP_Text homePanelTournamentTimeRemainingText;
    public TMP_Text homePanelTournamentPlayingText;
    public Image homePanelTournamentImage;
    public Image homePanelTournamentButton;



    [Header("Tournament Panel")]
    public GameObject tournamentPanelHomeGO;

    [Space(10)]
    public GameObject tournamentPanelLiveTournamentButtonPrefab;
    public GameObject tournamentPanelLiveTournamentButtonPrefabParent;

    [Space(5)]
    public GameObject liveTournamentPanelGO;
    public GameObject liveTournamentConfirmationPanelGO;
    public TMP_Text liveTournamentConfirmationPanelBalanceText;

    [Space(5)]
    public Image liveTournamentButtonImage;
    public Image liveTournamentButtonIconImage;
    public TMP_Text liveTournamentButtonPlayingText;
    public TMP_Text liveTournamentButtonPrizeText;
    public TMP_Text liveTournamentButtonEndTimeText;
    public GameObject liveTournamentHostedByButton; 
    public TMP_Text hostedByButtonText;
    public GameObject liveTournamentButtonEmptyLeaderabordGO;
    public GameObject liveTournamentButtonFilledLeaderabordScrollPanelGO;
    public GameObject liveTournamentButtonLeaderboardScrollPrefab;
    public GameObject liveTournamentButtonLeaderboardScrollPrefabParent;

    public List<int> liveTournamentLeaderboardUserIdList = new List<int>();
    public List<string> liveTournamentLeaderboardUsernameList = new List<string>();
    public List<int> liveTournamentLeaderboardUserScoreList = new List<int>();

    public TMP_Text liveTournamentJoinButtonFreeText;
    public GameObject liveTournamentJoinButtonPaidGO;
    public TMP_Text liveTournamentJoinButtonPaidText;

    public GameObject joinLiveTournamentButtonGO;
    public TMP_Text liveTournamentPlayButtonFreeText;
    public GameObject liveTournamentPlayButtonPaidGO;
    public TMP_Text liveTournamentPlayButtonPaidText;
    public GameObject playAgainLiveTournamentButtonGO;



    [Space(10)]
    public GameObject tournamentPanelUpcomingTournamentButtonPrefab;
    public GameObject tournamentPanelUpcomingTournamentButtonPrefabParent;



    [Space(10)]
    public GameObject tournamentPanelPastTournamentButtonPrefab;
    public GameObject tournamentPanelPastTournamentButtonPrefabParent;

    [Space(5)]
    public GameObject pastTournamentPanelGO;

    [Space(5)]
    public Image pastTournamentButtonImage;
    public Image pastTournamentButtonIconImage;
    public GameObject pastTournamentHostedByButton;
    public TMP_Text pastTournamentHostedByButtonText;
    public TMP_Text pastTournamentUserRank;
    public TMP_Text pastTournamentUserScore;
    public GameObject pastTournamentButtonFilledLeaderabordScrollPanelGO;
    public GameObject pastTournamentButtonLeaderboardScrollPrefab;
    public GameObject pastTournamentButtonLeaderboardScrollPrefabParent;

    public List<int> pastTournamentLeaderboardUserIdList = new List<int>();
    public List<string> pastTournamentLeaderboardUsernameList = new List<string>();
    public List<int> pastTournamentLeaderboardUserScoreList = new List<int>();




    [Space(10)]
    public GameObject joinedTournamentPanelGO;

    [Space(5)]
    public Image joinedTournamentButtonImage;
    public Image joinedTournamentButtonIconImage;
    public TMP_Text joinedTournamentButtonPlayingText;
    public TMP_Text joinedTournamentButtonPrizeText;
    public TMP_Text joinedTournamentButtonEndTimeText;
    public GameObject joinedTournamentHostedByButton;
    public TMP_Text joinedTournamentHostedByButtonText;
    public GameObject joinedTournamentButtonFilledLeaderabordScrollPanelGO;
    public GameObject joinedTournamentButtonLeaderboardScrollPrefab;
    public GameObject joinedTournamentButtonLeaderboardScrollPrefabParent;

    public List<int> joinedTournamentLeaderboardUserIdList = new List<int>();
    public List<string> joinedTournamentLeaderboardUsernameList = new List<string>();
    public List<int> joinedTournamentLeaderboardUserScoreList = new List<int>();

    

    [Space(5)]
    public GameObject tournamentPanelJoinedTournamentButtonPrefab;
    public GameObject tournamentPanelJoinedTournamentButtonPrefabParent;






    [Header("Create Panel")]
    public GameObject createPanelHomeGO;
    public GameObject createGamePanelGO;

    [Space(5)]

    public GameObject chooseGameTemplatePanelGO;
    public GameObject chooseGameTemplateButtonPrefab;
    public GameObject gameListPanelGO;

    [Space(5)]
    public GameObject selectFacePanelGO;
    public GameObject selectFaceTogglePrefab;
    public GameObject selectFaceContentGO;
    public Sprite[] selectFaceSpriteArray;
    public List<GameObject> selectFaceToggleList = new List<GameObject>();
    public Button selectFaceNextButton;
    public TMP_Text selectFaceNextButtonText;

    [Space(5)]
    public GameObject selectBackgroundPanelGO;
    public GameObject selectBackgroundTogglePrefab;
    public GameObject selectBackgroundContentGO;
    public Sprite[] selectBackgroundSpriteArray;
    public List<GameObject> selectBackgroundToggleList = new List<GameObject>();
    public Button selectBackgroundNextButton;
    public TMP_Text selectBackgroundNextButtonText;



    [Space(5)]
    public GameObject selectJumpAudioPanelGO;
    public GameObject selectJumpAudioTogglePrefab;
    public GameObject selectJumpAudioToggleContentGO;
    public AudioClip[] selectJumpAudioClipArray;
    public List<GameObject> selectJumpAudioToggleList = new List<GameObject>();
    public Button selectJumpAudioNextButton;
    public TMP_Text selectJumpAudioNextButtonText;


    [Space(5)]
    public GameObject selectBGAudioPanelGO;
    public GameObject selectBGAudioTogglePrefab;
    public GameObject selectBGAudioToggleContentGO;
    public AudioClip[] selectBGAudioClipArray;
    public List<GameObject> selectBGAudioToggleList = new List<GameObject>();
    public Button selectBGAudioNextButton;
    public TMP_Text selectBGAudioNextButtonText;


    [Space(5)]
    public GameObject selectGameOverAudioPanelGO;
    public GameObject selectGameOverAudioTogglePrefab;
    public GameObject selectGameOverAudioToggleContentGO;
    public AudioClip[] selectGameOverAudioClipArray;
    public List<GameObject> selectGameOverAudioToggleList = new List<GameObject>();
    public Button selectGameOverAudioNextButton;
    public TMP_Text selectGameOverAudioNextButtonText;


    [Space(5)]
    public GameObject addGameNamePanelGO;
    public InputField addGameNameInputFieldLegacy;
    //public TMP_InputField addGameNameInputFieldLegacy;


    [Space(5)]
    public GameObject gameSharePanelGO;
    public Image gameShareGameIcon;
    public TMP_Text gameShareGameNameText;


    [Space(10)]
    public GameObject createTournamentPanelGO;
    public GameObject chooseTournamentGamePanelGO;
    public GameObject chooseTournamentGameButtonPrefab;
    public GameObject chooseTournamentGameButtonContentGO;

    [Space(5)]
    public GameObject tournamentDetailsPanelGO;

    public InputField tournamentNameInputFieldLegacy;
    public InputField hostNameInputFieldLegacy;
    public InputField socialLinkInputFieldLegacy;
    public InputField playerJoiningFeeInputFieldLegacy;

    //public TMP_InputField tournamentNameInputFieldLegacy;
    //public TMP_InputField hostNameInputFieldLegacy;
    //public TMP_InputField socialLinkInputFieldLegacy;
    //public TMP_InputField playerJoiningFeeInputFieldLegacy;

    public TMP_InputField startDateInputField;
    public TMP_InputField endDateInputField;
    public TMP_Text startTimeText;
    public GameObject enterStartTimePanelGO;
    public InputField chooseStartTimeHourInputField;
    public InputField chooseStartTimeMinuteInputField;

    public TMP_Text endTimeText;
    public GameObject enterEndTimePanelGO;
    public InputField chooseEndTimeHourInputField;
    public InputField chooseEndTimeMinuteInputField;

    public Button tournamentsDetailsNextButton;
    public TMP_Text tournamentsDetailsNextButtonText;

    [Space(5)]
    public GameObject prizeDetailsPanelGO;

    public InputField prizePoolAmountInputFieldLegacy;
    public InputField winnerAmountInputFieldLegacy;
    public InputField runnerUpAmountInputFieldLegacy;
    public InputField secondRunnerUpAmountInputFieldLegacy;

    //public TMP_InputField prizePoolAmountInputFieldLegacy;
    //public TMP_InputField winnerAmountInputFieldLegacy;
    //public TMP_InputField runnerUpAmountInputFieldLegacy;
    //public TMP_InputField secondRunnerUpAmountInputFieldLegacy;
    public Button prizeDetailsNextButton;
    public TMP_Text prizeDetailsNextButtonText;

    [Space(5)]
    public GameObject confirmationPanelGO;
    public TMP_Text confirmationPanelWalletBalanceText;
    public TMP_Text confirmationPanelConfirmationText;



    [Header("Profile Panel")]
    public GameObject profilePanelHomeGO;
    public TMP_Text profileUsernameText;
    public TMP_Text profileEmailText;
    public TMP_Text profileWalletAddressText;
    public TMP_Text profileWalletBalanceText;
    public TMP_Text profileTournamentsJoinedText;
    public ScrollRect profileScrollRect;

    [Space(5)]
    public List<GameObject> profileMyGamesButtonGOList = new List<GameObject>();
    public GameObject profileMyGamesEmptyButton;



    [Space(5)]
    public GameObject profileTournamentButtonGO;
    public TMP_Text profileTournamentButtonTitleText;
    public TMP_Text profileTournamentButtonPrizePoolText;
    public Image profileTournamentImage;
    public Image profileTournamentButtonImage;
    public TMP_Text profileTournamentButtonTimeReaminingText;
    public GameObject profileTournamentEmptyButtonGO;


    [Space(5)]
    public GameObject profilePanelMyTournamentsPanelGO;
    public GameObject profilePanelMyTournamentsButtonPrefab;
    public GameObject profilePanelMyTournamentsButtonParentGO;

    [Space(5)]
    public GameObject profilePanelMyGamesPanelGO;
    public GameObject profilePanelMyGamesButtonPrefab;
    public GameObject profilePanelMyGamesButtonParentGO;

    [Space(10)]
    [Header("System Bar")]
    public GameObject systemBarGO;

    [Space(10)]
    [Header("Transaction Pop-up")]
    public GameObject transactionPopupPanelGameObject;
    public TMP_Text transactionPopupStatusText;


    [Space(10)]
    [Header("Error Pop-up")]
    public GameObject errorPopupGameObject;
    public TMP_Text errorPopupText;

    //RENAME AND COMMENT FUNCTIONS BASED ON USERFLOW AND SECTIONS

    // Start is called before the first frame update
    void Start()
    {

        


        InitializeSceneUI();


        //Register Panel
        createAccountButton.interactable = false;
        usernameInputFieldLegacy.onValueChanged.AddListener(delegate { CheckCreateAccountInputFields(); });
        emailInputFieldLegacy.onValueChanged.AddListener(delegate { CheckCreateAccountInputFields(); });
        passwordInputFieldLegacy.onValueChanged.AddListener(delegate { CheckCreateAccountInputFields(); });
        confirmPasswordInputFieldLegacy.onValueChanged.AddListener(delegate { CheckCreateAccountInputFields(); });

        //Sign in Panel
        signInButton.interactable = false;
        signInUsernameInputFieldLegacy.onValueChanged.AddListener(delegate { CheckSignInInputFields(); });
        signInPasswordInputFieldLegacy.onValueChanged.AddListener(delegate { CheckSignInInputFields(); });


        //Home Tournament Create Profile Toggles
        homeToggle.onValueChanged.AddListener(delegate { OnHomeToggleChanged(); });
        tournamentToggle.onValueChanged.AddListener(delegate { OnTournamentToggleChanged(); });
        createToggle.onValueChanged.AddListener(delegate { OnCreateToggleChanged(); });
        profileToggle.onValueChanged.AddListener(delegate { OnProfileToggleChanged(); });


        //Create Tournament Details Input Fields
        tournamentsDetailsNextButton.interactable = false;
        tournamentNameInputFieldLegacy.onValueChanged.AddListener(delegate { CheckTournamentDetailsInputFields(); });
        hostNameInputFieldLegacy.onValueChanged.AddListener(delegate { CheckTournamentDetailsInputFields(); });
        socialLinkInputFieldLegacy.onValueChanged.AddListener(delegate { CheckTournamentDetailsInputFields(); });
        playerJoiningFeeInputFieldLegacy.onValueChanged.AddListener(delegate { CheckTournamentDetailsInputFields(); });
        startDateInputField.onValueChanged.AddListener(delegate { CheckTournamentDetailsInputFields(); });
        endDateInputField.onValueChanged.AddListener(delegate { CheckTournamentDetailsInputFields(); });

        //Create Tournament Prize Details Input Fields
        prizeDetailsNextButton.interactable = false;
        prizePoolAmountInputFieldLegacy.onValueChanged.AddListener(delegate { CheckPrizeDetailsInputField(); });
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void InitializeSceneUI()
    {
        homeProfileTournamentCreatePanelGO.SetActive(false);

        startRegisterSignInPanelGO.SetActive(true);
        startPanelGO.SetActive(true);
        registerPanelGO.SetActive(false);
        signInPanelGO.SetActive(false);

        systemBarGO.SetActive(true);

        transactionPopupPanelGameObject.SetActive(false);
        errorPopupGameObject.SetActive(false);

        //Disabling Games
        Manager.instance.gameManager.game1Parent.SetActive(false);

    }

    public void OnStartPanelSkipCicked()
    {


        CloseStartRegisterSignInPanel();

    }

    public void CloseStartRegisterSignInPanel()
    {
        startRegisterSignInPanelGO.SetActive(false);
        startPanelGO.SetActive(false);
        registerPanelGO.SetActive(false);
        createAccountGO.SetActive(false);
        creatingAccountProgressGO.SetActive(false);

        signInPanelGO.SetActive(false);
        signInGO.SetActive(false);


        homeProfileTournamentCreatePanelGO.SetActive(true);
        
        testnetIndicatorGroupGO.SetActive(true);
        aptosBalanceGroupGO.SetActive(true);
        bottomSeperatorImageGO.SetActive(true);
        bottomBarToggleGroupGO.SetActive(true);
        homePanelGO.SetActive(true);
        tournamentPanelGO.SetActive(false);
        createPanelGO.SetActive(false);
        profilePanelGO.SetActive(false);


        homeToggle.isOn = true;
        OnHomeToggleChanged();

        SetUISolanaBalance();
    }

    
    public void SetUISolanaBalance()
    {
        solanaBalanceText.text = TruncateToTwoDecimalPlaces(Manager.instance.userInfoManager.walletBalanceFloat) + " SOL";
        profileWalletBalanceText.text = TruncateToTwoDecimalPlaces(Manager.instance.userInfoManager.walletBalanceFloat) + " SOL";
    }

    public void OnStartPanelCreateAccountClicked()
    {
        startPanelGO.SetActive(false);
        registerPanelGO.SetActive(true);
        createAccountGO.SetActive(true);
        creatingAccountProgressGO.SetActive(false);
    }


    void CheckCreateAccountInputFields()
    {
        // Check if all input fields are not empty
        if (!string.IsNullOrWhiteSpace(usernameInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(emailInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(passwordInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(confirmPasswordInputFieldLegacy.text))
        {
            createAccountButtonText.color = inputFieldFilledButtonTextColor;
            createAccountButton.interactable = true; // Enable the button
        }
        else
        {
            createAccountButtonText.color = Color.white;
            createAccountButton.interactable = false; // Disable the button
        }
    }





    /// <summary>
    /// Calls "Register User" from WebManager.cs
    /// </summary>
    public void OnCreateAccountButtonClicked()
    {

        if((passwordInputFieldLegacy.text == confirmPasswordInputFieldLegacy.text) && passwordInputFieldLegacy.text != null && usernameInputFieldLegacy.text != null && emailInputFieldLegacy.text != null)
        {
            Manager.instance.webManager.RegisterUser(usernameInputFieldLegacy.text, emailInputFieldLegacy.text, passwordInputFieldLegacy.text);
        }

    }


    /// <summary>
    /// Used to how/hide Password text
    /// </summary>
    public void OnPasswordShowClicked()
    {
        //if(passwordInputFieldLegacy.contentType == TMP_InputField.ContentType.Password)
        //{
        //    showPasswordButton.GetComponent<Image>().sprite = hidePasswordSprite;
        //    passwordInputFieldLegacy.contentType = TMP_InputField.ContentType.Standard;
        //    passwordInputFieldLegacy.ForceLabelUpdate();
        //}
        //else if (passwordInputFieldLegacy.contentType == TMP_InputField.ContentType.Standard)
        //{
        //    showPasswordButton.GetComponent<Image>().sprite = showPasswordSprite;
        //    passwordInputFieldLegacy.contentType = TMP_InputField.ContentType.Password;
        //    passwordInputFieldLegacy.ForceLabelUpdate();
        //}

        if (passwordInputFieldLegacy.contentType == InputField.ContentType.Password)
        {
            showPasswordButton.GetComponent<Image>().sprite = hidePasswordSprite;
            passwordInputFieldLegacy.contentType = InputField.ContentType.Standard;
            passwordInputFieldLegacy.ForceLabelUpdate();
        }
        else if (passwordInputFieldLegacy.contentType == InputField.ContentType.Standard)
        {
            showPasswordButton.GetComponent<Image>().sprite = showPasswordSprite;
            passwordInputFieldLegacy.contentType = InputField.ContentType.Password;
            passwordInputFieldLegacy.ForceLabelUpdate();
        }
    }


    /// <summary>
    /// Used to how/hide Confirm Password text
    /// </summary>
    public void OnConfirmPasswordShowClicked()
    {
        //if (confirmPasswordInputFieldLegacy.contentType == TMP_InputField.ContentType.Password)
        //{
        //    showConfirmPasswordButton.GetComponent<Image>().sprite = hidePasswordSprite;
        //    confirmPasswordInputFieldLegacy.contentType = TMP_InputField.ContentType.Standard;
        //    confirmPasswordInputFieldLegacy.ForceLabelUpdate();
        //}
        //else if (confirmPasswordInputFieldLegacy.contentType == TMP_InputField.ContentType.Standard)
        //{
        //    showConfirmPasswordButton.GetComponent<Image>().sprite = showPasswordSprite;
        //    confirmPasswordInputFieldLegacy.contentType = TMP_InputField.ContentType.Password;
        //    confirmPasswordInputFieldLegacy.ForceLabelUpdate();
        //}

        if (confirmPasswordInputFieldLegacy.contentType == InputField.ContentType.Password)
        {
            showConfirmPasswordButton.GetComponent<Image>().sprite = hidePasswordSprite;
            confirmPasswordInputFieldLegacy.contentType = InputField.ContentType.Standard;
            confirmPasswordInputFieldLegacy.ForceLabelUpdate();
        }
        else if (confirmPasswordInputFieldLegacy.contentType == InputField.ContentType.Standard)
        {
            showConfirmPasswordButton.GetComponent<Image>().sprite = showPasswordSprite;
            confirmPasswordInputFieldLegacy.contentType = InputField.ContentType.Password;
            confirmPasswordInputFieldLegacy.ForceLabelUpdate();
        }
    }

    public void OnCreateAccountAlreadyLoginClicked()
    {

        //Reset input fields
        usernameInputFieldLegacy.text = null;
        emailInputFieldLegacy.text = null;
        passwordInputFieldLegacy.text = null;
        confirmPasswordInputFieldLegacy.text = null;
        createAccountButtonText.color = Color.white;

        registerPanelGO.SetActive(false);
        createAccountGO.SetActive(false);
        creatingAccountProgressGO.SetActive(false);
        signInPanelGO.SetActive(true);
        signInGO.SetActive(true);
    }


    public IEnumerator CreateAccountProgress()
    {



        creatingAccountGroupImageGO.SetActive(false);
        creatingAccountGroupSelectedImageGO.SetActive(true);
        creatingAccoutntText.color = creatingAccountProgressTextColor;

        creatingWalletGroupImageGO.SetActive(true);
        creatingWalletGroupSelectedImageGO.SetActive(false);
        creatingWalletText.color = Color.white;

        airdroppingGroupImageGO.SetActive(true);
        airdroppingGroupSelectedImageGO.SetActive(false);
        airdroppingText.color = Color.white;

        allSetGroupImageGO.SetActive(true);
        allSetGroupSelectedImageGO.SetActive(false);
        allSetText.color = Color.white;

        createAccountGO.SetActive(false);
        creatingAccountProgressGO.SetActive(true);

        yield return new WaitForSeconds(0.5f);


        //Create wallet
        creatingAccountGroupImageGO.SetActive(true);
        creatingAccountGroupSelectedImageGO.SetActive(false);
        creatingAccoutntText.color = Color.white;

        creatingWalletGroupImageGO.SetActive(false);
        creatingWalletGroupSelectedImageGO.SetActive(true);
        creatingWalletText.color = creatingAccountProgressTextColor;

        airdroppingGroupImageGO.SetActive(true);
        airdroppingGroupSelectedImageGO.SetActive(false);
        airdroppingText.color = Color.white;

        allSetGroupImageGO.SetActive(true);
        allSetGroupSelectedImageGO.SetActive(false);
        allSetText.color = Color.white;

        //Manager.instance.walletManager.CreateNewWallet();
        Manager.instance.solanaWalletManager.CreateNewAccount();
        yield return new WaitForSeconds(0.5f);


        //Airdrop 0.5 SOL
        creatingAccountGroupImageGO.SetActive(true);
        creatingAccountGroupSelectedImageGO.SetActive(false);
        creatingAccoutntText.color = Color.white;

        creatingWalletGroupImageGO.SetActive(true);
        creatingWalletGroupSelectedImageGO.SetActive(false);
        creatingWalletText.color = Color.white;

        airdroppingGroupImageGO.SetActive(false);
        airdroppingGroupSelectedImageGO.SetActive(true);
        airdroppingText.color = creatingAccountProgressTextColor;

        allSetGroupImageGO.SetActive(true);
        allSetGroupSelectedImageGO.SetActive(false);
        allSetText.color = Color.white;

        //yield return StartCoroutine(Manager.instance.walletManager.AirDrop(100000000));
        //UniTask.Void(Manager.instance.solanaWalletManager.AirdropSol);

        //Manager.instance.solanaWalletManager.CallAirdropSol();
        yield return StartCoroutine(Manager.instance.solanaWalletManager.AirdropSolCoroutine());

        //Manager.instance.walletManager.LoadCurrentWalletBalance();

        yield return new WaitForSeconds(0.5f);

        //Load Current Wallet Balance
        UniTask.Void(Manager.instance.solanaWalletManager.LoadCurrentWalletBalance);

        SetUISolanaBalance();


        //Display All Set 
        creatingAccountGroupImageGO.SetActive(true);
        creatingAccountGroupSelectedImageGO.SetActive(false);
        creatingAccoutntText.color = Color.white;

        creatingWalletGroupImageGO.SetActive(true);
        creatingWalletGroupSelectedImageGO.SetActive(false);
        creatingWalletText.color = Color.white;

        airdroppingGroupImageGO.SetActive(true);
        airdroppingGroupSelectedImageGO.SetActive(false);
        airdroppingText.color = Color.white;

        allSetGroupImageGO.SetActive(false);
        allSetGroupSelectedImageGO.SetActive(true);
        allSetText.color = creatingAccountProgressTextColor;


        StartCoroutine(Manager.instance.webManager.UpdateWallet(Manager.instance.userInfoManager.userId, 
                                                                Manager.instance.userInfoManager.walletAddress, 
                                                                Manager.instance.userInfoManager.walletBalanceInt, 
                                                                Manager.instance.userInfoManager.walletMnemonics));

        yield return new WaitForSeconds(0.5f);


        //Reset input fields
        usernameInputFieldLegacy.text = null;
        emailInputFieldLegacy.text = null;
        passwordInputFieldLegacy.text = null;
        confirmPasswordInputFieldLegacy.text = null;
        createAccountButtonText.color = Color.white;


        //Proceed to Home


        CloseStartRegisterSignInPanel();

    }

    public void OnCreateAccountBackButtonClicked()
    {

        //Reset input fields
        usernameInputFieldLegacy.text = null;
        emailInputFieldLegacy.text = null;
        passwordInputFieldLegacy.text = null;
        confirmPasswordInputFieldLegacy.text = null;
        createAccountButtonText.color = Color.white;

        startPanelGO.SetActive(true);
        registerPanelGO.SetActive(false);
        createAccountGO.SetActive(false);
        creatingAccountProgressGO.SetActive(false);
    }


    //ADD SIGNIN CODE!!!!!
    public void OnStartPanelSignInClicked()
    {
        startPanelGO.SetActive(false);
        signInPanelGO.SetActive(true);
        signInGO.SetActive(true);
    }

    public void OnSignInShowPasswordClicked()
    {

        //if (signInPasswordInputFieldLegacy.contentType == TMP_InputField.ContentType.Password)
        //{
        //    signInShowPasswordButton.GetComponent<Image>().sprite = hidePasswordSprite;
        //    signInPasswordInputFieldLegacy.contentType = TMP_InputField.ContentType.Standard;
        //    signInPasswordInputFieldLegacy.ForceLabelUpdate();
        //}
        //else if (signInPasswordInputFieldLegacy.contentType == TMP_InputField.ContentType.Standard)
        //{
        //    signInShowPasswordButton.GetComponent<Image>().sprite = showPasswordSprite;
        //    signInPasswordInputFieldLegacy.contentType = TMP_InputField.ContentType.Password;
        //    signInPasswordInputFieldLegacy.ForceLabelUpdate();
        //}

        if (signInPasswordInputFieldLegacy.contentType == InputField.ContentType.Password)
        {
            signInShowPasswordButton.GetComponent<Image>().sprite = hidePasswordSprite;
            signInPasswordInputFieldLegacy.contentType = InputField.ContentType.Standard;
            signInPasswordInputFieldLegacy.ForceLabelUpdate();
        }
        else if (signInPasswordInputFieldLegacy.contentType == InputField.ContentType.Standard)
        {
            signInShowPasswordButton.GetComponent<Image>().sprite = showPasswordSprite;
            signInPasswordInputFieldLegacy.contentType = InputField.ContentType.Password;
            signInPasswordInputFieldLegacy.ForceLabelUpdate();
        }

    }


    void CheckSignInInputFields()
    {
        // Check if all input fields are not empty
        if (!string.IsNullOrWhiteSpace(signInUsernameInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(signInPasswordInputFieldLegacy.text))
        {
            signInButtonText.color = inputFieldFilledButtonTextColor;
            signInButton.interactable = true; // Enable the button
        }
        else
        {
            signInButtonText.color = Color.white;
            signInButton.interactable = false; // Disable the button
        }
    }



    public void OnSignInBackButtonClicked()
    {

        //Reset input fields
        signInUsernameInputFieldLegacy.text = null;
        signInPasswordInputFieldLegacy.text = null;
        signInButtonText.color = Color.white;


        startPanelGO.SetActive(true);
        signInPanelGO.SetActive(false);
        signInGO.SetActive(false);
    }


    public void OnSignInCreateAccountButtonClicked()
    {
        //Reset input fields
        signInUsernameInputFieldLegacy.text = null;
        signInPasswordInputFieldLegacy.text = null;
        signInButtonText.color = Color.white;


        signInPanelGO.SetActive(false);
        signInGO.SetActive(false);
        registerPanelGO.SetActive(true);
        createAccountGO.SetActive(true);
        creatingAccountProgressGO.SetActive(false);
        
    }

    public void OnSignInButtonClicked()
    {

        if (signInUsernameInputFieldLegacy.text != null && signInPasswordInputFieldLegacy.text != null)
        {
            Manager.instance.webManager.SignInUser(signInUsernameInputFieldLegacy.text, signInPasswordInputFieldLegacy.text);
        }

    }

    public IEnumerator OnSuccessfulSignInProceedHome()
    {

        //Get current wallet balance
        //Manager.instance.walletManager.LoadCurrentWalletBalance();
        UniTask.Void(Manager.instance.solanaWalletManager.LoadCurrentWalletBalance);


        yield return new WaitForSeconds(1f);

        //Set Wallet Balance in Home
        SetUISolanaBalance();

        //Reset Sign-in Input Fields
        signInUsernameInputFieldLegacy.text = null;
        signInPasswordInputFieldLegacy.text = null;
        signInButtonText.color = Color.white;


        //Close Sign GO & Show Home GO
        CloseStartRegisterSignInPanel();
    }


    //Home Landing CODE!!!!



    //HOME TOGGLE CODE
    public void OnHomeToggleChanged()
    {
        homePanelHomeGO.SetActive(true);
        top30GamesPanelGO.SetActive(false);

        StartCoroutine(OnHomeToggleChangedCoroutine());

    }

    public IEnumerator OnHomeToggleChangedCoroutine()
    {
        if (homeToggle.isOn)
        {
            Debug.Log("Home Toggle ON");

            homeToggle.isOn = true;
            tournamentToggle.isOn = false;
            createToggle.isOn = false;
            profileToggle.isOn = false;

            homePanelGO.SetActive(true);
            homePanelHomeGO.SetActive(true);

            homeToggleOnLabelGO.SetActive(true);
            homeToggleOffLabelGO.SetActive(false);

            tournamentPanelGO.SetActive(false);
            tournamentPanelHomeGO.SetActive(false);
            pastTournamentPanelGO.SetActive(false);
            liveTournamentPanelGO.SetActive(false);
            liveTournamentConfirmationPanelGO.SetActive(false);

            tournamentToggleOnLabelGO.SetActive(false);
            tournamentToggleOffLabelGO.SetActive(true);

            createPanelGO.SetActive(false);
            createPanelHomeGO.SetActive(false);
            createGamePanelGO.SetActive(false);
            createTournamentPanelGO.SetActive(false);

            createToggleOnLabelGO.SetActive(false);
            createToggleOffLabelGO.SetActive(true);

            profilePanelGO.SetActive(false);
            profilePanelHomeGO.SetActive(false);

            profileToggleOnLabelGO.SetActive(false);
            profileToggleOffLabelGO.SetActive(true);

            homeToggle.interactable = false;
            tournamentToggle.interactable = true;
            createToggle.interactable = true;
            profileToggle.interactable = true;


            //Load Current Wallet Balance
            UniTask.Void(Manager.instance.solanaWalletManager.LoadCurrentWalletBalance);

            //Set the Popular Games on the Home Panel
            // - Get list of all games sorted by playCount
            // - Display top 3 games
            yield return StartCoroutine(Manager.instance.webManager.GetTop30Games());

            yield return StartCoroutine(PopulateTop30GamesHomePanel());



            //Set Live Torunament on the Home Panel
            // - Get list of all tournaments
            // - Categorize all tournaments into past, live, & upcoming lists         
            // - From the live tournaments, show the one which has highest prize pool                        
            yield return StartCoroutine(Manager.instance.webManager.GetAllTournaments());

            yield return StartCoroutine(CategorizeAllTournamentData());

            //Get User Games...Required to show Tournament Image
            yield return StartCoroutine(Manager.instance.webManager.GetUserGames(Manager.instance.userInfoManager.userId));

            yield return StartCoroutine(PopulateLivePastTournamentHomePanel());


        }
    }




    public IEnumerator PopulateTop30GamesHomePanel()
    {

        //Handling top 3 games
        foreach (GameObject buttonGO in homePanelPopularGamesButtonList)
        {
            buttonGO.SetActive(false);
        }

        // Get the count of games, but limit to a maximum of 3
        int maxGamesTop3 = Mathf.Min(Manager.instance.gameDataManager.top30GameIdList.Count, 3);

        for (int i = 0; i < maxGamesTop3; i++)
        {

            int userGameTemplateIdInt = Manager.instance.gameDataManager.top30GameTemplateIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<Image>().sprite = Manager.instance.gameDataManager.gameTemplateImageList[userGameTemplateIdInt];
            homePanelPopularGamesButtonList[i].GetComponentInChildren<TMP_Text>().text = Manager.instance.gameDataManager.top30GameNameList[i];


            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameId = Manager.instance.gameDataManager.top30GameIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().userId = Manager.instance.gameDataManager.top30UserIdList[i];

            int gameTemplateId = Manager.instance.gameDataManager.top30GameTemplateIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameTemplateId = gameTemplateId;
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameFaceId = Manager.instance.gameDataManager.top30GameFaceIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameBackgroundId = Manager.instance.gameDataManager.top30GameBackgroundIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameJumpAudioId = Manager.instance.gameDataManager.top30GameJumpAudioIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameBGAudioId = Manager.instance.gameDataManager.top30GameBGAudioIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameGameOverAudioId = Manager.instance.gameDataManager.top30GameGameOverAudioIdList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gameGameName = Manager.instance.gameDataManager.top30GameNameList[i];
            homePanelPopularGamesButtonList[i].GetComponent<HomeTop3GameButton>().gamePlayCount = Manager.instance.gameDataManager.top30GamePlayCountList[i];

            homePanelPopularGamesButtonList[i].SetActive(true);


        }



        // Get the count of games, but limit to a maximum of 30
        int maxGamesTop30 = Mathf.Min(Manager.instance.gameDataManager.top30GameIdList.Count, 30);

        foreach (Transform child in homePanelTop30GamesButtonPrefabParent.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }


        for (int i = 0; i < maxGamesTop30; i++)
        {
            Debug.Log("===================> Spawn: " + i);
            GameObject gameButton = Instantiate(homePanelTop30GamesButtonPrefab, homePanelTop30GamesButtonPrefabParent.transform);
            
            gameButton.GetComponent<Top30GameButton>().gameId = Manager.instance.gameDataManager.top30GameIdList[i];
            gameButton.GetComponent<Top30GameButton>().userId = Manager.instance.gameDataManager.top30UserIdList[i];

            int gameTemplateId = Manager.instance.gameDataManager.top30GameTemplateIdList[i];
            gameButton.GetComponent<Top30GameButton>().gameTemplateId = gameTemplateId;
            gameButton.GetComponent<Top30GameButton>().gameFaceId = Manager.instance.gameDataManager.top30GameFaceIdList[i];
            gameButton.GetComponent<Top30GameButton>().gameBackgroundId = Manager.instance.gameDataManager.top30GameBackgroundIdList[i];
            gameButton.GetComponent<Top30GameButton>().gameJumpAudioId = Manager.instance.gameDataManager.top30GameJumpAudioIdList[i];
            gameButton.GetComponent<Top30GameButton>().gameBGAudioId = Manager.instance.gameDataManager.top30GameBGAudioIdList[i];
            gameButton.GetComponent<Top30GameButton>().gameGameOverAudioId = Manager.instance.gameDataManager.top30GameGameOverAudioIdList[i];
            gameButton.GetComponent<Top30GameButton>().gameGameName = Manager.instance.gameDataManager.top30GameNameList[i];
            gameButton.GetComponent<Top30GameButton>().gamePlayCount = Manager.instance.gameDataManager.top30GamePlayCountList[i];

            //use the template id to get image 
            gameButton.GetComponent<Image>().sprite = Manager.instance.gameDataManager.gameTemplateImageList[gameTemplateId];
            gameButton.GetComponentInChildren<TMP_Text>().text = Manager.instance.gameDataManager.top30GameNameList[i];

        }

        yield return null;
    }

    public void OnHomePanelViewTop30GamesClicked()
    {
        top30GamesPanelGO.SetActive(true);
    }


    public void OnViewTop30GamesBackClicked()
    {
        top30GamesPanelGO.SetActive(false);
    }


    public IEnumerator CategorizeAllTournamentData()
    {

        ClearPastTournamentLists();
        ClearUpcomingTournamentLists();
        ClearLiveTournamentLists();


        for (int i = 0; i < Manager.instance.tournamentDataManager.allTournamentIdList.Count; i++)
        {
            string startDate = Manager.instance.tournamentDataManager.allStartDateList[i].ToString();
            string startTime = Manager.instance.tournamentDataManager.allStartTimeList[i].ToString();
            string endDate = Manager.instance.tournamentDataManager.allEndDateList[i].ToString();
            string endTime = Manager.instance.tournamentDataManager.allEndTimeList[i].ToString();

            // Get the tournament status
            string eventStatus = GetEventStatus(startDate, startTime, endDate, endTime);

            if (eventStatus.Contains("Tournament Ended"))
            {
                // If the tournament has ended, add corr data to the past tournament list

                Manager.instance.tournamentDataManager.pastTournamentIdList.Add(Manager.instance.tournamentDataManager.allTournamentIdList[i]);
                Manager.instance.tournamentDataManager.pastUserIdList.Add(Manager.instance.tournamentDataManager.allUserIdList[i]);
                Manager.instance.tournamentDataManager.pastGameIdList.Add(Manager.instance.tournamentDataManager.allGameIdList[i]);
                Manager.instance.tournamentDataManager.pastTournamentNameList.Add(Manager.instance.tournamentDataManager.allTournamentNameList[i]);
                Manager.instance.tournamentDataManager.pastTournamentHostNameList.Add(Manager.instance.tournamentDataManager.allTournamentHostNameList[i]);
                Manager.instance.tournamentDataManager.pastSocialLinkList.Add(Manager.instance.tournamentDataManager.allSocialLinkList[i]);
                Manager.instance.tournamentDataManager.pastPlayerJoiningFeeList.Add(Manager.instance.tournamentDataManager.allPlayerJoiningFeeList[i]);
                Manager.instance.tournamentDataManager.pastStartDateList.Add(Manager.instance.tournamentDataManager.allStartDateList[i]);
                Manager.instance.tournamentDataManager.pastStartTimeList.Add(Manager.instance.tournamentDataManager.allStartTimeList[i]);
                Manager.instance.tournamentDataManager.pastEndDateList.Add(Manager.instance.tournamentDataManager.allEndDateList[i]);
                Manager.instance.tournamentDataManager.pastEndTimeList.Add(Manager.instance.tournamentDataManager.allEndTimeList[i]);
                Manager.instance.tournamentDataManager.pastPrizePoolList.Add(Manager.instance.tournamentDataManager.allPrizePoolList[i]);
                Manager.instance.tournamentDataManager.pastStatusList.Add(Manager.instance.tournamentDataManager.allStatusList[i]);
                Manager.instance.tournamentDataManager.pastPlayCountList.Add(Manager.instance.tournamentDataManager.allPlayCountList[i]);
                Manager.instance.tournamentDataManager.pastUserCountList.Add(Manager.instance.tournamentDataManager.allUserCountList[i]);
                Manager.instance.tournamentDataManager.pastWinnerIdList.Add(Manager.instance.tournamentDataManager.allWinnerIdList[i]);
                Manager.instance.tournamentDataManager.pastRunnerUpIdList.Add(Manager.instance.tournamentDataManager.allRunnerUpIdList[i]);
                Manager.instance.tournamentDataManager.pastSecondRunnerUpIdList.Add(Manager.instance.tournamentDataManager.allSecondRunnerUpIdList[i]);

}
            else if (eventStatus.Contains("Starts in"))
            {
                // If the tournament hasn't started, add corr data to the upcoming tournament list

                Manager.instance.tournamentDataManager.upcomingTournamentIdList.Add(Manager.instance.tournamentDataManager.allTournamentIdList[i]);
                Manager.instance.tournamentDataManager.upcomingUserIdList.Add(Manager.instance.tournamentDataManager.allUserIdList[i]);
                Manager.instance.tournamentDataManager.upcomingGameIdList.Add(Manager.instance.tournamentDataManager.allGameIdList[i]);
                Manager.instance.tournamentDataManager.upcomingTournamentNameList.Add(Manager.instance.tournamentDataManager.allTournamentNameList[i]);
                Manager.instance.tournamentDataManager.upcomingTournamentHostNameList.Add(Manager.instance.tournamentDataManager.allTournamentHostNameList[i]);
                Manager.instance.tournamentDataManager.upcomingSocialLinkList.Add(Manager.instance.tournamentDataManager.allSocialLinkList[i]);
                Manager.instance.tournamentDataManager.upcomingPlayerJoiningFeeList.Add(Manager.instance.tournamentDataManager.allPlayerJoiningFeeList[i]);
                Manager.instance.tournamentDataManager.upcomingStartDateList.Add(Manager.instance.tournamentDataManager.allStartDateList[i]);
                Manager.instance.tournamentDataManager.upcomingStartTimeList.Add(Manager.instance.tournamentDataManager.allStartTimeList[i]);
                Manager.instance.tournamentDataManager.upcomingEndDateList.Add(Manager.instance.tournamentDataManager.allEndDateList[i]);
                Manager.instance.tournamentDataManager.upcomingEndTimeList.Add(Manager.instance.tournamentDataManager.allEndTimeList[i]);
                Manager.instance.tournamentDataManager.upcomingPrizePoolList.Add(Manager.instance.tournamentDataManager.allPrizePoolList[i]);
                Manager.instance.tournamentDataManager.upcomingStatusList.Add(Manager.instance.tournamentDataManager.allStatusList[i]);
                Manager.instance.tournamentDataManager.upcomingPlayCountList.Add(Manager.instance.tournamentDataManager.allPlayCountList[i]);
                Manager.instance.tournamentDataManager.upcomingUserCountList.Add(Manager.instance.tournamentDataManager.allUserCountList[i]);
                Manager.instance.tournamentDataManager.upcomingWinnerIdList.Add(Manager.instance.tournamentDataManager.allWinnerIdList[i]);
                Manager.instance.tournamentDataManager.upcomingRunnerUpIdList.Add(Manager.instance.tournamentDataManager.allRunnerUpIdList[i]);
                Manager.instance.tournamentDataManager.upcomingSecondRunnerUpIdList.Add(Manager.instance.tournamentDataManager.allSecondRunnerUpIdList[i]);

            }
            else if (eventStatus.Contains("Ends in"))
            {
                // If the tournament is currently live, add corr data to the live tournament list

                Manager.instance.tournamentDataManager.liveTournamentIdList.Add(Manager.instance.tournamentDataManager.allTournamentIdList[i]);
                Manager.instance.tournamentDataManager.liveUserIdList.Add(Manager.instance.tournamentDataManager.allUserIdList[i]);
                Manager.instance.tournamentDataManager.liveGameIdList.Add(Manager.instance.tournamentDataManager.allGameIdList[i]);
                Manager.instance.tournamentDataManager.liveTournamentNameList.Add(Manager.instance.tournamentDataManager.allTournamentNameList[i]);
                Manager.instance.tournamentDataManager.liveTournamentHostNameList.Add(Manager.instance.tournamentDataManager.allTournamentHostNameList[i]);
                Manager.instance.tournamentDataManager.liveSocialLinkList.Add(Manager.instance.tournamentDataManager.allSocialLinkList[i]);
                Manager.instance.tournamentDataManager.livePlayerJoiningFeeList.Add(Manager.instance.tournamentDataManager.allPlayerJoiningFeeList[i]);
                Manager.instance.tournamentDataManager.liveStartDateList.Add(Manager.instance.tournamentDataManager.allStartDateList[i]);
                Manager.instance.tournamentDataManager.liveStartTimeList.Add(Manager.instance.tournamentDataManager.allStartTimeList[i]);
                Manager.instance.tournamentDataManager.liveEndDateList.Add(Manager.instance.tournamentDataManager.allEndDateList[i]);
                Manager.instance.tournamentDataManager.liveEndTimeList.Add(Manager.instance.tournamentDataManager.allEndTimeList[i]);
                Manager.instance.tournamentDataManager.livePrizePoolList.Add(Manager.instance.tournamentDataManager.allPrizePoolList[i]);
                Manager.instance.tournamentDataManager.liveStatusList.Add(Manager.instance.tournamentDataManager.allStatusList[i]);
                Manager.instance.tournamentDataManager.livePlayCountList.Add(Manager.instance.tournamentDataManager.allPlayCountList[i]);
                Manager.instance.tournamentDataManager.liveUserCountList.Add(Manager.instance.tournamentDataManager.allUserCountList[i]);
                Manager.instance.tournamentDataManager.liveWinnerIdList.Add(Manager.instance.tournamentDataManager.allWinnerIdList[i]);
                Manager.instance.tournamentDataManager.liveRunnerUpIdList.Add(Manager.instance.tournamentDataManager.allRunnerUpIdList[i]);
                Manager.instance.tournamentDataManager.liveSecondRunnerUpIdList.Add(Manager.instance.tournamentDataManager.allSecondRunnerUpIdList[i]);

            }
        }

        yield return null;
    }

    


    public void ClearPastTournamentLists()
    {
        Manager.instance.tournamentDataManager.pastTournamentIdList.Clear();
        Manager.instance.tournamentDataManager.pastUserIdList.Clear();
        Manager.instance.tournamentDataManager.pastGameIdList.Clear();
        Manager.instance.tournamentDataManager.pastTournamentNameList.Clear();
        Manager.instance.tournamentDataManager.pastTournamentHostNameList.Clear();
        Manager.instance.tournamentDataManager.pastSocialLinkList.Clear();
        Manager.instance.tournamentDataManager.pastPlayerJoiningFeeList.Clear();
        Manager.instance.tournamentDataManager.pastStartDateList.Clear();
        Manager.instance.tournamentDataManager.pastStartTimeList.Clear();
        Manager.instance.tournamentDataManager.pastEndDateList.Clear();
        Manager.instance.tournamentDataManager.pastEndTimeList.Clear();
        Manager.instance.tournamentDataManager.pastPrizePoolList.Clear();
        Manager.instance.tournamentDataManager.pastStatusList.Clear();
        Manager.instance.tournamentDataManager.pastPlayCountList.Clear();
        Manager.instance.tournamentDataManager.pastUserCountList.Clear();
        Manager.instance.tournamentDataManager.pastWinnerIdList.Clear();
        Manager.instance.tournamentDataManager.pastRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.pastSecondRunnerUpIdList.Clear();
    }

    public void ClearUpcomingTournamentLists()
    {
        Manager.instance.tournamentDataManager.upcomingTournamentIdList.Clear();
        Manager.instance.tournamentDataManager.upcomingUserIdList.Clear();
        Manager.instance.tournamentDataManager.upcomingGameIdList.Clear();
        Manager.instance.tournamentDataManager.upcomingTournamentNameList.Clear();
        Manager.instance.tournamentDataManager.upcomingTournamentHostNameList.Clear();
        Manager.instance.tournamentDataManager.upcomingSocialLinkList.Clear();
        Manager.instance.tournamentDataManager.upcomingPlayerJoiningFeeList.Clear();
        Manager.instance.tournamentDataManager.upcomingStartDateList.Clear();
        Manager.instance.tournamentDataManager.upcomingStartTimeList.Clear();
        Manager.instance.tournamentDataManager.upcomingEndDateList.Clear();
        Manager.instance.tournamentDataManager.upcomingEndTimeList.Clear();
        Manager.instance.tournamentDataManager.upcomingPrizePoolList.Clear();
        Manager.instance.tournamentDataManager.upcomingStatusList.Clear();
        Manager.instance.tournamentDataManager.upcomingPlayCountList.Clear();
        Manager.instance.tournamentDataManager.upcomingUserCountList.Clear();
        Manager.instance.tournamentDataManager.upcomingWinnerIdList.Clear();
        Manager.instance.tournamentDataManager.upcomingRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.upcomingSecondRunnerUpIdList.Clear();
    }

    public void ClearLiveTournamentLists()
    {
        Manager.instance.tournamentDataManager.liveTournamentIdList.Clear();
        Manager.instance.tournamentDataManager.liveUserIdList.Clear();
        Manager.instance.tournamentDataManager.liveGameIdList.Clear();
        Manager.instance.tournamentDataManager.liveTournamentNameList.Clear();
        Manager.instance.tournamentDataManager.liveTournamentHostNameList.Clear();
        Manager.instance.tournamentDataManager.liveSocialLinkList.Clear();
        Manager.instance.tournamentDataManager.livePlayerJoiningFeeList.Clear();
        Manager.instance.tournamentDataManager.liveStartDateList.Clear();
        Manager.instance.tournamentDataManager.liveStartTimeList.Clear();
        Manager.instance.tournamentDataManager.liveEndDateList.Clear();
        Manager.instance.tournamentDataManager.liveEndTimeList.Clear();
        Manager.instance.tournamentDataManager.livePrizePoolList.Clear();
        Manager.instance.tournamentDataManager.liveStatusList.Clear();
        Manager.instance.tournamentDataManager.livePlayCountList.Clear();
        Manager.instance.tournamentDataManager.liveUserCountList.Clear();
        Manager.instance.tournamentDataManager.liveWinnerIdList.Clear();
        Manager.instance.tournamentDataManager.liveRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.liveSecondRunnerUpIdList.Clear();
    }


    public IEnumerator PopulateLivePastTournamentHomePanel()
    {
        if(Manager.instance.tournamentDataManager.livePrizePoolList.Count != 0)
        {
            int liveHighestPrizePoolIndex = GetHighestPrizePoolIndex(Manager.instance.tournamentDataManager.livePrizePoolList);

            homePanelTournamentButtonTitle.text = Manager.instance.tournamentDataManager.liveTournamentNameList[liveHighestPrizePoolIndex];
            homePanelTournamentPrizePoolText.text = TruncateToTwoDecimalPlaces(Manager.instance.tournamentDataManager.livePrizePoolList[liveHighestPrizePoolIndex]) + " SOL";

            string startDate = Manager.instance.tournamentDataManager.liveStartDateList[liveHighestPrizePoolIndex].ToString();
            string startTime = Manager.instance.tournamentDataManager.liveStartTimeList[liveHighestPrizePoolIndex].ToString();
            string endDate = Manager.instance.tournamentDataManager.liveEndDateList[liveHighestPrizePoolIndex].ToString();
            string endTime = Manager.instance.tournamentDataManager.liveEndTimeList[liveHighestPrizePoolIndex].ToString();

            homePanelTournamentTimeRemainingText.text = GetEventStatus(startDate, startTime, endDate, endTime);

            homePanelTournamentPlayingText.text = Manager.instance.tournamentDataManager.liveUserCountList[liveHighestPrizePoolIndex] + " playing";



            ////Get Tournament Image
            //// - same as game template image
            //// - get game id for tournament 
            //// - get corr game template id for the game id from the userGameList
            //// - set the image from gameTemplateImageList

            int gameId = Manager.instance.tournamentDataManager.liveGameIdList[liveHighestPrizePoolIndex];

            

            //Get game info for the gameId 
            //Then set the srite and color using the template id

            yield return StartCoroutine(Manager.instance.webManager.GetGameTemplateId(gameId));

            homePanelTournamentImage.sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.tournamentDataManager.gameTemplateId];
            homePanelTournamentButton.color = Manager.instance.gameDataManager.gameTemplateColorList[Manager.instance.tournamentDataManager.gameTemplateId];


            //int gameIdIndex = Manager.instance.gameDataManager.userGameIdList.IndexOf(gameId);
            //int gameTemplateId = Manager.instance.gameDataManager.userGameTemplateIdList[gameIdIndex];
            //homePanelTournamentImage.sprite = Manager.instance.gameDataManager.gameTemplateImageList[gameTemplateId];
            //homePanelTournamentButton.color = Manager.instance.gameDataManager.gameTemplateColorList[gameTemplateId];

        }
        else
        {

            if(Manager.instance.tournamentDataManager.pastPrizePoolList.Count != 0)
            {
                int pastHighestPrizePoolIndex = GetHighestPrizePoolIndex(Manager.instance.tournamentDataManager.pastPrizePoolList);

                homePanelTournamentButtonTitle.text = Manager.instance.tournamentDataManager.pastTournamentNameList[pastHighestPrizePoolIndex];
                homePanelTournamentPrizePoolText.text = TruncateToTwoDecimalPlaces(Manager.instance.tournamentDataManager.pastPrizePoolList[pastHighestPrizePoolIndex]) + " SOL";

                string startDate = Manager.instance.tournamentDataManager.pastStartDateList[pastHighestPrizePoolIndex].ToString();
                string startTime = Manager.instance.tournamentDataManager.pastStartTimeList[pastHighestPrizePoolIndex].ToString();
                string endDate = Manager.instance.tournamentDataManager.pastEndDateList[pastHighestPrizePoolIndex].ToString();
                string endTime = Manager.instance.tournamentDataManager.pastEndTimeList[pastHighestPrizePoolIndex].ToString();

                homePanelTournamentTimeRemainingText.text = GetEventStatus(startDate, startTime, endDate, endTime);

                homePanelTournamentPlayingText.text = Manager.instance.tournamentDataManager.pastUserCountList[pastHighestPrizePoolIndex] + " playing";



                ////Get Tournament Image
                //// - same as game template image
                //// - get game id for tournament 
                //// - get corr game template id for the game id from the userGameList
                //// - set the image from gameTemplateImageList

                int gameId = Manager.instance.tournamentDataManager.pastGameIdList[pastHighestPrizePoolIndex];

                //Get game info for the gameId 
                //Then set the srite and color using the template id

                yield return StartCoroutine(Manager.instance.webManager.GetGameTemplateId(gameId));
                

                homePanelTournamentImage.sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.tournamentDataManager.gameTemplateId];
                homePanelTournamentButton.color = Manager.instance.gameDataManager.gameTemplateColorList[Manager.instance.tournamentDataManager.gameTemplateId];


                //int gameIdIndex = Manager.instance.gameDataManager.userGameIdList.IndexOf(gameId);
                //int gameTemplateId = Manager.instance.gameDataManager.userGameTemplateIdList[gameIdIndex];

            }


        }

        yield return null;

    }


    // Method to find the index of the highest prize pool
    int GetHighestPrizePoolIndex(List<float> prizePoolList)
    {
        if (prizePoolList.Count == 0)
        {
            return -1; // Return -1 if the list is empty
        }

        float highestPrizePool = prizePoolList[0];
        int highestIndex = 0;

        for (int i = 1; i < prizePoolList.Count; i++)
        {
            if (prizePoolList[i] > highestPrizePool)
            {
                highestPrizePool = prizePoolList[i];
                highestIndex = i;
            }
        }

        return highestIndex; // Return the index of the highest prize pool
    }

    public void OnHomePanelLivePastTournamentViewAllClicked()
    {
        tournamentToggle.isOn = true;
        StartCoroutine(OnTournamentToggleChangedCoroutine());
    }


    public void OnTournamentToggleChanged()
    {

        StartCoroutine(OnTournamentToggleChangedCoroutine());

    }

    public IEnumerator OnTournamentToggleChangedCoroutine()
    {
        if (tournamentToggle.isOn)
        {

            Debug.Log("Tournament Toggle ON");

            homeToggle.isOn = false;
            tournamentToggle.isOn = true;
            createToggle.isOn = false;
            profileToggle.isOn = false;

            homePanelGO.SetActive(false);
            homePanelHomeGO.SetActive(false);

            homeToggleOnLabelGO.SetActive(false);
            homeToggleOffLabelGO.SetActive(true);

            tournamentPanelGO.SetActive(true);
            tournamentPanelHomeGO.SetActive(true);
            pastTournamentPanelGO.SetActive(false);
            liveTournamentPanelGO.SetActive(false);
            liveTournamentConfirmationPanelGO.SetActive(false);

            tournamentToggleOnLabelGO.SetActive(true);
            tournamentToggleOffLabelGO.SetActive(false);

            createPanelGO.SetActive(false);
            createPanelHomeGO.SetActive(false);
            createGamePanelGO.SetActive(false);
            createTournamentPanelGO.SetActive(false);

            createToggleOnLabelGO.SetActive(false);
            createToggleOffLabelGO.SetActive(true);

            profilePanelGO.SetActive(false);
            profilePanelHomeGO.SetActive(false);

            profileToggleOnLabelGO.SetActive(false);
            profileToggleOffLabelGO.SetActive(true);

            homeToggle.interactable = true;
            tournamentToggle.interactable = false;
            createToggle.interactable = true;
            profileToggle.interactable = true;


            
            // - Get list of all tournaments
            // - Categorize all tournaments into past, live, & upcoming lists         
            // - From the live tournaments, show the one which has highest prize pool                        
            yield return StartCoroutine(Manager.instance.webManager.GetAllTournaments());

            yield return StartCoroutine(CategorizeAllTournamentData());

            //Get User Games...Required to show Tournament Image
            yield return StartCoroutine(Manager.instance.webManager.GetUserGames(Manager.instance.userInfoManager.userId));


            //We already have the live, past, upcoming Torunament data fetched during Home Toggle
            // - Populate Live Tournaments
            StartCoroutine(PopulateTournamentPanelLiveTournaments());

            // - Populate Upcoming Tournaments
            StartCoroutine(PopulateTournamentPanelUpcomingTournaments());

            // - Populate Past Tournaments
            StartCoroutine(PopulateTournamentPanelPastTournaments());


            //Get joined tournament data from server tournamentscores table
            yield return StartCoroutine(Manager.instance.webManager.GetUserJoinedTournamentDetails(Manager.instance.userInfoManager.userId));

            // - Populate Joined Tournaments
            StartCoroutine(PopulateTournamentPanelJoinedTournaments());


        }
    }

    public IEnumerator PopulateTournamentPanelLiveTournaments()
    {
        

        if (Manager.instance.tournamentDataManager.liveTournamentIdList.Count != 0)
        {

            foreach (Transform child in tournamentPanelLiveTournamentButtonPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Manager.instance.tournamentDataManager.liveTournamentIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject liveTournamentButton = Instantiate(tournamentPanelLiveTournamentButtonPrefab, tournamentPanelLiveTournamentButtonPrefabParent.transform);

                //store tournament info
                liveTournamentButton.GetComponent<LiveTournamentButton>().tournamentId = Manager.instance.tournamentDataManager.liveTournamentIdList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().userId = Manager.instance.tournamentDataManager.liveUserIdList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().gameId = Manager.instance.tournamentDataManager.liveGameIdList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().tournamentName = Manager.instance.tournamentDataManager.liveTournamentNameList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().tournamentHostName = Manager.instance.tournamentDataManager.liveTournamentHostNameList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().socialLink = Manager.instance.tournamentDataManager.liveSocialLinkList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().playerJoiningFee = Manager.instance.tournamentDataManager.livePlayerJoiningFeeList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().startDate = Manager.instance.tournamentDataManager.liveStartDateList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().startTime = Manager.instance.tournamentDataManager.liveStartTimeList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().endDate = Manager.instance.tournamentDataManager.liveEndDateList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().endTime = Manager.instance.tournamentDataManager.liveEndTimeList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().prizePool = Manager.instance.tournamentDataManager.livePrizePoolList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().status = Manager.instance.tournamentDataManager.liveStatusList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().playCount = Manager.instance.tournamentDataManager.livePlayCountList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().userCount = Manager.instance.tournamentDataManager.liveUserCountList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().winnerId = Manager.instance.tournamentDataManager.liveWinnerIdList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().runnerUpId = Manager.instance.tournamentDataManager.liveRunnerUpIdList[i];
                liveTournamentButton.GetComponent<LiveTournamentButton>().secondRunnerUpId = Manager.instance.tournamentDataManager.liveSecondRunnerUpIdList[i];

                //populate live tournament button data
                StartCoroutine(liveTournamentButton.GetComponent<LiveTournamentButton>().PopulateTournamentData());

            }

        }

        yield return null;

    }

 
    public IEnumerator OnLiveTournamentCanvasButtonClicked(int tournamentId, int gameId, string tournamentName, string tournamentHostName, string socialLink,
                                                           float playerJoiningFee, int startDate, int startTime, int endDate, int endTime, float prizePool,
                                                           int status, int playCount, int userCount, float winnerId, float runnerUpId, float secondRunnerUpId,
                                                           Sprite tournamentSprite, Color tournamentColor, string endingIn)
    {


        Manager.instance.tournamentDataManager.tournamentId = tournamentId;
        Manager.instance.tournamentDataManager.gameId = gameId;
        Manager.instance.tournamentDataManager.tournamentName = tournamentName;
        Manager.instance.tournamentDataManager.tournamentHostName = tournamentHostName;
        Manager.instance.tournamentDataManager.socialLink = socialLink;
        Manager.instance.tournamentDataManager.playerJoiningFee = playerJoiningFee;
        Manager.instance.tournamentDataManager.startDate = startDate;
        Manager.instance.tournamentDataManager.startTime = startTime;
        Manager.instance.tournamentDataManager.endDate = endDate;
        Manager.instance.tournamentDataManager.endTime = endTime;
        Manager.instance.tournamentDataManager.prizePool = prizePool;
        Manager.instance.tournamentDataManager.status = status;
        Manager.instance.tournamentDataManager.playCount = playCount;
        Manager.instance.tournamentDataManager.userCount = userCount;
        Manager.instance.tournamentDataManager.winnerId = winnerId;
        Manager.instance.tournamentDataManager.runnerUpId = runnerUpId;
        Manager.instance.tournamentDataManager.secondRunnerUpId = secondRunnerUpId;





        liveTournamentPanelGO.SetActive(true);
        liveTournamentButtonIconImage.sprite = tournamentSprite;
        liveTournamentButtonImage.color = tournamentColor;

        liveTournamentButtonPlayingText.text = userCount + " playing";      
        liveTournamentButtonPrizeText.text = TruncateToTwoDecimalPlaces(prizePool) + " SOL Prize";

        //Check if tournamentId is in Joined tournament List, if yes, show Play Again Button
        if (Manager.instance.tournamentDataManager.joinedTournamentIdList.Contains(tournamentId))
        {
            joinLiveTournamentButtonGO.SetActive(false);
            playAgainLiveTournamentButtonGO.SetActive(true);
        }
        else
        {

            joinLiveTournamentButtonGO.SetActive(true);
            playAgainLiveTournamentButtonGO.SetActive(false);

            if (playerJoiningFee != 0)
            {
                liveTournamentJoinButtonPaidText.text = TruncateToTwoDecimalPlaces(playerJoiningFee) + " SOL";
                liveTournamentJoinButtonPaidGO.SetActive(true);
                liveTournamentJoinButtonFreeText.gameObject.SetActive(false);

                liveTournamentPlayButtonPaidText.text = (playerJoiningFee) + " SOL";
                liveTournamentPlayButtonPaidGO.SetActive(true);
                liveTournamentPlayButtonFreeText.gameObject.SetActive(false);
            }
            else
            {
                liveTournamentJoinButtonPaidGO.SetActive(false);
                liveTournamentJoinButtonFreeText.gameObject.SetActive(true);

                liveTournamentPlayButtonPaidGO.SetActive(false);
                liveTournamentPlayButtonFreeText.gameObject.SetActive(true);
            }
        }

        

        liveTournamentButtonEndTimeText.text = endingIn;
        hostedByButtonText.text = "Hosted By: <color=#42EFEC><u>" + tournamentHostName + "</u></color>";      
        liveTournamentHostedByButton.GetComponent<LiveTournamentHostButton>().liveTournamentHostUrl = socialLink;


        //Get Tournament Leaderbaord Data using tournamentId
        // - based on leaderboard enable empty/filled leaderboard gameObject
        // - if filled, spawn leaderboard prefabs unser scroll parent

        yield return StartCoroutine(Manager.instance.webManager.GetTournamentScores(tournamentId, isLive: true));

        if(liveTournamentLeaderboardUserIdList.Count != 0)
        {
            liveTournamentButtonEmptyLeaderabordGO.SetActive(false);
            liveTournamentButtonFilledLeaderabordScrollPanelGO.SetActive(true);

            foreach (Transform child in liveTournamentButtonLeaderboardScrollPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < liveTournamentLeaderboardUserIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject leaderboardEntry = Instantiate(liveTournamentButtonLeaderboardScrollPrefab, liveTournamentButtonLeaderboardScrollPrefabParent.transform);
                leaderboardEntry.GetComponent<LiveTournamentLeaderbaordEntry>().SetLeaverboardEntryData(liveTournamentLeaderboardUsernameList[i], liveTournamentLeaderboardUserScoreList[i]);
            }
        }
        else
        {
            liveTournamentButtonEmptyLeaderabordGO.SetActive(true);
            liveTournamentButtonFilledLeaderabordScrollPanelGO.SetActive(false);
        }



    }

    public void OnLiveTournamentJoinButtonClicked()
    {

        //Show confirmation pop-up text balance
        liveTournamentConfirmationPanelBalanceText.text = TruncateToTwoDecimalPlaces(Manager.instance.userInfoManager.walletBalanceFloat) + " SOL";


        //Show the confirmation popup
        // - open popupp go
        liveTournamentConfirmationPanelGO.SetActive(true);

        
    }


    public void OnLiveTournamentJoinConfirmButtonClicked()
    {

        StartCoroutine(OnLiveTournamentJoinConfirmButtonClickedCoroutine());
    }

    public IEnumerator OnLiveTournamentJoinConfirmButtonClickedCoroutine()
    {
        //Run smart contract 
        // - on successful execution, store the userId, tournamentId, and score, in the tournamentscore table
        // - launch game after successful db storage

        //yield return StartCoroutine(Manager.instance.walletManager.ExecuteEnterTournament(Manager.instance.tournamentDataManager.tournamentId.ToString(), 
        //                                                                                  Manager.instance.userInfoManager.userId.ToString()));

        yield return StartCoroutine(Manager.instance.solanaWalletManager.JoinTournamentCoroutine());

        //Increment server tournament user count
        yield return StartCoroutine(Manager.instance.webManager.IncrementTournamentUserCountCoroutine(Manager.instance.tournamentDataManager.tournamentId));

        //Storing tournament score for user
        yield return StartCoroutine(Manager.instance.webManager.StoreTournamentScoreCoroutine(Manager.instance.userInfoManager.userId,
                                                                                              Manager.instance.tournamentDataManager.tournamentId,
                                                                                              Manager.instance.userInfoManager.tournamentScore));

        //Launch the game using game info as inputs
        yield return StartCoroutine(LaunchTournamentGameCoroutine());




    }

    public IEnumerator LaunchTournamentGameCoroutine()
    {

        int gameId = Manager.instance.tournamentDataManager.gameId;
        //Get game data using gameId & store it GameDataManager
        yield return StartCoroutine(Manager.instance.webManager.GetGameDetailsCoroutine(gameId));



        //Get user score for tournament game (userId, tournamentId, gameId)
        
        
        // - Store user tournament game score
        yield return StartCoroutine(Manager.instance.webManager.GetOrCreateTournamentScoreCoroutine(Manager.instance.userInfoManager.userId, Manager.instance.tournamentDataManager.tournamentId));



        Manager.instance.gameManager.OnCanvasGameSelectedPlayClicked(tournament: true);


        //TO DO
        //Disable CanvasRegisterSign-in Panel 
        //Disable HomeProfileTournamentreate Panel 


        //handle live tournament ui
        liveTournamentConfirmationPanelGO.SetActive(false);
        liveTournamentPanelGO.SetActive(false);
        tournamentPanelGO.SetActive(false);

        //handle joined tournaments ui
        joinedTournamentPanelGO.SetActive(false);   


        homeProfileTournamentCreatePanelGO.SetActive(false);
        systemBarGO.SetActive(false);

    }

    public void OnLiveTournamentJoinCancelButtonClicked()
    {
        //Show the confirmation popup
        // - close popup go
        liveTournamentConfirmationPanelGO.SetActive(false);

    }

    public void OnLiveTournamentBackButtonClicked()
    {
        liveTournamentPanelGO.SetActive(false);
    }

    public IEnumerator PopulateTournamentPanelUpcomingTournaments()
    {


        if (Manager.instance.tournamentDataManager.upcomingTournamentIdList.Count != 0)
        {

            foreach (Transform child in tournamentPanelUpcomingTournamentButtonPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Manager.instance.tournamentDataManager.upcomingTournamentIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject upcomingTournamentButton = Instantiate(tournamentPanelUpcomingTournamentButtonPrefab, tournamentPanelUpcomingTournamentButtonPrefabParent.transform);

                //store tournament info
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().tournamentId = Manager.instance.tournamentDataManager.upcomingTournamentIdList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().userId = Manager.instance.tournamentDataManager.upcomingUserIdList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().gameId = Manager.instance.tournamentDataManager.upcomingGameIdList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().tournamentName = Manager.instance.tournamentDataManager.upcomingTournamentNameList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().tournamentHostName = Manager.instance.tournamentDataManager.upcomingTournamentHostNameList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().socialLink = Manager.instance.tournamentDataManager.upcomingSocialLinkList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().playerJoiningFee = Manager.instance.tournamentDataManager.upcomingPlayerJoiningFeeList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().startDate = Manager.instance.tournamentDataManager.upcomingStartDateList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().startTime = Manager.instance.tournamentDataManager.upcomingStartTimeList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().endDate = Manager.instance.tournamentDataManager.upcomingEndDateList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().endTime = Manager.instance.tournamentDataManager.upcomingEndTimeList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().prizePool = Manager.instance.tournamentDataManager.upcomingPrizePoolList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().status = Manager.instance.tournamentDataManager.upcomingStatusList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().playCount = Manager.instance.tournamentDataManager.upcomingPlayCountList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().userCount = Manager.instance.tournamentDataManager.upcomingUserCountList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().winnerId = Manager.instance.tournamentDataManager.upcomingWinnerIdList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().runnerUpId = Manager.instance.tournamentDataManager.upcomingRunnerUpIdList[i];
                upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().secondRunnerUpId = Manager.instance.tournamentDataManager.upcomingSecondRunnerUpIdList[i];

                //populate live tournament button data
                StartCoroutine(upcomingTournamentButton.GetComponent<UpcomingTournamentButton>().PopulateTournamentData());

            }

        }

        yield return null;

    }

    public IEnumerator PopulateTournamentPanelPastTournaments()
    {


        if (Manager.instance.tournamentDataManager.pastTournamentIdList.Count != 0)
        {

            foreach (Transform child in tournamentPanelPastTournamentButtonPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }


            for (int i = 0; i < Manager.instance.tournamentDataManager.pastTournamentIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject pastTournamentButton = Instantiate(tournamentPanelPastTournamentButtonPrefab, tournamentPanelPastTournamentButtonPrefabParent.transform);

                //store tournament info
                pastTournamentButton.GetComponent<PastTournamentButton>().tournamentId = Manager.instance.tournamentDataManager.pastTournamentIdList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().userId = Manager.instance.tournamentDataManager.pastUserIdList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().gameId = Manager.instance.tournamentDataManager.pastGameIdList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().tournamentName = Manager.instance.tournamentDataManager.pastTournamentNameList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().tournamentHostName = Manager.instance.tournamentDataManager.pastTournamentHostNameList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().socialLink = Manager.instance.tournamentDataManager.pastSocialLinkList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().playerJoiningFee = Manager.instance.tournamentDataManager.pastPlayerJoiningFeeList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().startDate = Manager.instance.tournamentDataManager.pastStartDateList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().startTime = Manager.instance.tournamentDataManager.pastStartTimeList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().endDate = Manager.instance.tournamentDataManager.pastEndDateList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().endTime = Manager.instance.tournamentDataManager.pastEndTimeList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().prizePool = Manager.instance.tournamentDataManager.pastPrizePoolList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().status = Manager.instance.tournamentDataManager.pastStatusList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().playCount = Manager.instance.tournamentDataManager.pastPlayCountList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().userCount = Manager.instance.tournamentDataManager.pastUserCountList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().winnerId = Manager.instance.tournamentDataManager.pastWinnerIdList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().runnerUpId = Manager.instance.tournamentDataManager.pastRunnerUpIdList[i];
                pastTournamentButton.GetComponent<PastTournamentButton>().secondRunnerUpId = Manager.instance.tournamentDataManager.pastSecondRunnerUpIdList[i];

                //populate live tournament button data
                StartCoroutine(pastTournamentButton.GetComponent<PastTournamentButton>().PopulateTournamentData());

            }

        }

        yield return null;

    }

    public IEnumerator OnPastTournamentCanvasButtonClicked(int tournamentId, int gameId, string tournamentName, string tournamentHostName, string socialLink,
                                                           float playerJoiningFee, int startDate, int startTime, int endDate, int endTime, float prizePool,
                                                           int status, int playCount, int userCount, float winnerId, float runnerUpId, float secondRunnerUpId,
                                                           Sprite tournamentSprite, Color tournamentColor)
    {


        //Manager.instance.tournamentDataManager.tournamentId = tournamentId;
        //Manager.instance.tournamentDataManager.gameId = gameId;
        //Manager.instance.tournamentDataManager.tournamentName = tournamentName;
        //Manager.instance.tournamentDataManager.tournamentHostName = tournamentHostName;
        //Manager.instance.tournamentDataManager.socialLink = socialLink;
        //Manager.instance.tournamentDataManager.playerJoiningFee = playerJoiningFee;
        //Manager.instance.tournamentDataManager.startDate = startDate;
        //Manager.instance.tournamentDataManager.startTime = startTime;
        //Manager.instance.tournamentDataManager.endDate = endDate;
        //Manager.instance.tournamentDataManager.endTime = endTime;
        //Manager.instance.tournamentDataManager.prizePool = prizePool;
        //Manager.instance.tournamentDataManager.status = status;
        //Manager.instance.tournamentDataManager.playCount = playCount;
        //Manager.instance.tournamentDataManager.userCount = userCount;
        //Manager.instance.tournamentDataManager.winnerId = winnerId;
        //Manager.instance.tournamentDataManager.runnerUpId = runnerUpId;
        //Manager.instance.tournamentDataManager.secondRunnerUpId = secondRunnerUpId;





        pastTournamentPanelGO.SetActive(true);
        pastTournamentButtonIconImage.sprite = tournamentSprite;
        pastTournamentButtonImage.color = tournamentColor;

        //Show your rank and score
        // - Find the score corr to userId
        // - Find the rank corr to userId, basically list index + 1

        //public List<int> pastTournamentLeaderboardUserIdList = new List<int>();
        //public List<string> pastTournamentLeaderboardUsernameList = new List<string>();
        //public List<int> pastTournamentLeaderboardUserScoreList = new List<int>();

        if (pastTournamentLeaderboardUserIdList.Contains(Manager.instance.userInfoManager.userId))
        {
            int userIndex = pastTournamentLeaderboardUserIdList.IndexOf(Manager.instance.userInfoManager.userId);

            pastTournamentUserRank.text = "Your Rank : " + userIndex;
            pastTournamentUserScore.text = "Your Score : " + pastTournamentLeaderboardUserScoreList[userIndex];
        }
        else
        {
            pastTournamentUserRank.text = "Your Rank : NA" ;
            pastTournamentUserScore.text = "Your Score : NA";
        }

        //pastTournamentButtonPlayingText.text = userCount + " playing";
        //pastTournamentButtonPrizeText.text = prizePool + " APT Prize";


        pastTournamentHostedByButtonText.text = "Hosted By: <color=#42EFEC><u>" + tournamentHostName + "</u></color>";
        pastTournamentHostedByButton.GetComponent<PastTournamentHostButton>().pastTournamentHostUrl = socialLink;


        //Get Tournament Leaderbaord Data using tournamentId
        // - based on leaderboard enable empty/filled leaderboard gameObject
        // - if filled, spawn leaderboard prefabs unser scroll parent

        yield return StartCoroutine(Manager.instance.webManager.GetTournamentScores(tournamentId, isLive: false));

        if (pastTournamentLeaderboardUserIdList.Count != 0)
        {
            //pastTournamentButtonEmptyLeaderabordGO.SetActive(false);
            pastTournamentButtonFilledLeaderabordScrollPanelGO.SetActive(true);


            foreach (Transform child in pastTournamentButtonLeaderboardScrollPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < pastTournamentLeaderboardUserIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject leaderboardEntry = Instantiate(pastTournamentButtonLeaderboardScrollPrefab, pastTournamentButtonLeaderboardScrollPrefabParent.transform);
                leaderboardEntry.GetComponent<PastTournamentLeaderboardEntry>().SetLeaverboardEntryData(pastTournamentLeaderboardUsernameList[i], pastTournamentLeaderboardUserScoreList[i]);
            }
        }
        //else
        //{
        //    liveTournamentButtonEmptyLeaderabordGO.SetActive(true);
        //    liveTournamentButtonFilledLeaderabordScrollPanelGO.SetActive(false);
        //}



    }

    public void OnPastTournamentBackButtonClicked()
    {
        pastTournamentPanelGO.SetActive(false);
    }
    public IEnumerator PopulateTournamentPanelJoinedTournaments()
    {


        if (Manager.instance.tournamentDataManager.joinedTournamentIdList.Count != 0)
        {

            foreach (Transform child in tournamentPanelJoinedTournamentButtonPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Manager.instance.tournamentDataManager.joinedTournamentIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject joinedTournamentButton = Instantiate(tournamentPanelJoinedTournamentButtonPrefab, tournamentPanelJoinedTournamentButtonPrefabParent.transform);

                //store tournament info
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().tournamentId = Manager.instance.tournamentDataManager.joinedTournamentIdList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().gameId = Manager.instance.tournamentDataManager.joinedGameIdList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().tournamentName = Manager.instance.tournamentDataManager.joinedTournamentNameList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().tournamentHostName = Manager.instance.tournamentDataManager.joinedTournamentHostNameList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().socialLink = Manager.instance.tournamentDataManager.joinedSocialLinkList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().playerJoiningFee = Manager.instance.tournamentDataManager.joinedPlayerJoiningFeeList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().startDate = Manager.instance.tournamentDataManager.joinedStartDateList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().startTime = Manager.instance.tournamentDataManager.joinedStartTimeList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().endDate = Manager.instance.tournamentDataManager.joinedEndDateList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().endTime = Manager.instance.tournamentDataManager.joinedEndTimeList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().prizePool = Manager.instance.tournamentDataManager.joinedPrizePoolList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().status = Manager.instance.tournamentDataManager.joinedStatusList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().playCount = Manager.instance.tournamentDataManager.joinedPlayCountList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().userCount = Manager.instance.tournamentDataManager.joinedUserCountList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().winnerId = Manager.instance.tournamentDataManager.joinedWinnerIdList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().runnerUpId = Manager.instance.tournamentDataManager.joinedRunnerUpIdList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().secondRunnerUpId = Manager.instance.tournamentDataManager.joinedSecondRunnerUpIdList[i];
                joinedTournamentButton.GetComponent<JoinedTournamentButton>().score = Manager.instance.tournamentDataManager.joinedScoreList[i];

                //populate live tournament button data
                StartCoroutine(joinedTournamentButton.GetComponent<JoinedTournamentButton>().PopulateTournamentData());

            }

        }

        yield return null;

    }


    public IEnumerator OnJoinedTournamentCanvasButtonClicked(int tournamentId, int gameId, string tournamentName, string tournamentHostName, string socialLink,
                                                           float playerJoiningFee, int startDate, int startTime, int endDate, int endTime, float prizePool,
                                                           int status, int playCount, int userCount, float winnerId, float runnerUpId, float secondRunnerUpId,
                                                           Sprite tournamentSprite, Color tournamentColor ,string endingIn)
    {


        Manager.instance.tournamentDataManager.tournamentId = tournamentId;
        Manager.instance.tournamentDataManager.gameId = gameId;
        Manager.instance.tournamentDataManager.tournamentName = tournamentName;
        Manager.instance.tournamentDataManager.tournamentHostName = tournamentHostName;
        Manager.instance.tournamentDataManager.socialLink = socialLink;
        Manager.instance.tournamentDataManager.playerJoiningFee = playerJoiningFee;
        Manager.instance.tournamentDataManager.startDate = startDate;
        Manager.instance.tournamentDataManager.startTime = startTime;
        Manager.instance.tournamentDataManager.endDate = endDate;
        Manager.instance.tournamentDataManager.endTime = endTime;
        Manager.instance.tournamentDataManager.prizePool = prizePool;
        Manager.instance.tournamentDataManager.status = status;
        Manager.instance.tournamentDataManager.playCount = playCount;
        Manager.instance.tournamentDataManager.userCount = userCount;
        Manager.instance.tournamentDataManager.winnerId = winnerId;
        Manager.instance.tournamentDataManager.runnerUpId = runnerUpId;
        Manager.instance.tournamentDataManager.secondRunnerUpId = secondRunnerUpId;





        joinedTournamentPanelGO.SetActive(true);
        joinedTournamentButtonIconImage.sprite = tournamentSprite;
        joinedTournamentButtonImage.color = tournamentColor;

        joinedTournamentButtonPlayingText.text = userCount + " playing";
        joinedTournamentButtonPrizeText.text = TruncateToTwoDecimalPlaces(prizePool) + " SOL Prize";


        joinedTournamentButtonEndTimeText.text = endingIn;
        joinedTournamentHostedByButtonText.text = "Hosted By: <color=#42EFEC><u>" + tournamentHostName + "</u></color>";
        joinedTournamentHostedByButton.GetComponent<JoinedTournamentHostButton>().joinedTournamentHostUrl = socialLink;


        //Get Tournament Leaderbaord Data using tournamentId
        // - based on leaderboard enable empty/filled leaderboard gameObject
        // - if filled, spawn leaderboard prefabs unser scroll parent

        yield return StartCoroutine(Manager.instance.webManager.GetTournamentScores(tournamentId, isLive: true));

        if (joinedTournamentLeaderboardUserIdList.Count != 0)
        {
            joinedTournamentButtonFilledLeaderabordScrollPanelGO.SetActive(true);

            foreach (Transform child in joinedTournamentButtonLeaderboardScrollPrefabParent.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < joinedTournamentLeaderboardUserIdList.Count; i++)
            {
                //spawn live tournament button
                GameObject leaderboardEntry = Instantiate(joinedTournamentButtonLeaderboardScrollPrefab, joinedTournamentButtonLeaderboardScrollPrefabParent.transform);
                leaderboardEntry.GetComponent<JoinedTournamentLeaderbaordEntry>().SetLeaverboardEntryData(joinedTournamentLeaderboardUsernameList[i], joinedTournamentLeaderboardUserScoreList[i]);
            }
        }


    }

    public void OnJoinedTournamentPlayAgainClicked()
    {
        //Launch the game using game info as inputs
        StartCoroutine(OnJoinedTournamentPlayAgainClickedCoroutine());
    }

    public IEnumerator OnJoinedTournamentPlayAgainClickedCoroutine()
    {
        yield return StartCoroutine(LaunchTournamentGameCoroutine());
    }


    public void OnJoinedTournamentBackButtonClicked()
    {
        joinedTournamentPanelGO.SetActive(false);
    }



    public void OnHomeCreateBannerButtonClicked()
    {

        createToggle.isOn = true;
        OnCreateToggleChanged();
    }


    public void OnCreateToggleChanged()
    {

        if (createToggle.isOn)
        {
            Debug.Log("Create Toggle ON");


            homeToggle.isOn = false;
            tournamentToggle.isOn = false;
            createToggle.isOn = true;
            profileToggle.isOn = false;

            homePanelGO.SetActive(false);
            homePanelHomeGO.SetActive(false);

            homeToggleOnLabelGO.SetActive(false);
            homeToggleOffLabelGO.SetActive(true);

            tournamentPanelGO.SetActive(false);
            tournamentPanelHomeGO.SetActive(false);
            pastTournamentPanelGO.SetActive(false);
            liveTournamentPanelGO.SetActive(false);
            liveTournamentConfirmationPanelGO.SetActive(false);

            tournamentToggleOnLabelGO.SetActive(false);
            tournamentToggleOffLabelGO.SetActive(true);

            createPanelGO.SetActive(true);
            createPanelHomeGO.SetActive(true);
            createGamePanelGO.SetActive(false);
            createTournamentPanelGO.SetActive(false);

            createToggleOnLabelGO.SetActive(true);
            createToggleOffLabelGO.SetActive(false);

            profilePanelGO.SetActive(false);
            profilePanelHomeGO.SetActive(false);

            profileToggleOnLabelGO.SetActive(false);
            profileToggleOffLabelGO.SetActive(true);

            homeToggle.interactable = true;
            tournamentToggle.interactable = true;
            createToggle.interactable = false;
            profileToggle.interactable = true;

        }

    }

    public void OnProfileToggleChanged()
    {

        if (profileToggle.isOn)
        {
            Debug.Log("Profile Toggle ON");

            homeToggle.isOn = false;
            tournamentToggle.isOn = false;
            createToggle.isOn = false;
            profileToggle.isOn = true;

            homePanelGO.SetActive(false);
            homePanelHomeGO.SetActive(false);

            homeToggleOnLabelGO.SetActive(false);
            homeToggleOffLabelGO.SetActive(true);

            tournamentPanelGO.SetActive(false);
            tournamentPanelHomeGO.SetActive(false);
            pastTournamentPanelGO.SetActive(false);
            liveTournamentPanelGO.SetActive(false);
            liveTournamentConfirmationPanelGO.SetActive(false);

            tournamentToggleOnLabelGO.SetActive(false);
            tournamentToggleOffLabelGO.SetActive(true);

            createPanelGO.SetActive(false);
            createPanelHomeGO.SetActive(false);
            createGamePanelGO.SetActive(false);
            createTournamentPanelGO.SetActive(false);

            createToggleOnLabelGO.SetActive(false);
            createToggleOffLabelGO.SetActive(true);

            profilePanelGO.SetActive(true);
            profilePanelHomeGO.SetActive(true);

            profileToggleOnLabelGO.SetActive(true);
            profileToggleOffLabelGO.SetActive(false);

            homeToggle.interactable = true;
            tournamentToggle.interactable = true;
            createToggle.interactable = true;
            profileToggle.interactable = false;

            //Load Current Wallet Balance
            UniTask.Void(Manager.instance.solanaWalletManager.LoadCurrentWalletBalance);

            StartCoroutine(SetProfileInfo());

            profileScrollRect.verticalNormalizedPosition = 1f;
        }

    }


    //PROFILE PANEL
    public IEnumerator SetProfileInfo()
    {
        profileUsernameText.text = Manager.instance.userInfoManager.username;
        profileEmailText.text = Manager.instance.userInfoManager.email;

        profileWalletAddressText.text = ShortenString(Manager.instance.userInfoManager.walletAddress);

        profileWalletBalanceText.text = TruncateToTwoDecimalPlaces(Manager.instance.userInfoManager.walletBalanceFloat) + " SOL";
        profileTournamentsJoinedText.text = Manager.instance.userInfoManager.tournamentsJoined.ToString(); 



        //Get user game data from server
        Debug.Log("Get user game data from server");
        yield return StartCoroutine(GetTournamentUserGameDataFromServer());

        //Populate My Games Panel in Profile
        Debug.Log("Populate My Games Panel in Profile");
        yield return StartCoroutine(PopulateTopMyGamesProfilePanel());

        //Populate Tournament Button in Profile
        Debug.Log("Populate Tournament Button in Profile");
        yield return StartCoroutine(PopulateRecentTournamentProfilePanel());

    }

    //shorten string
    string ShortenString(string str)
    {
        return str.Substring(0, 6) + "...." + str.Substring(str.Length - 4);
    }

    public void OnWalletExplorerClicked()
    {
        // Base URL for Solscan explorer
        string baseUrl = "https://solscan.io/account/";

        // Specify the network (testnet in this case)
        string network = "?cluster=devnet";

        // Combine the base URL, wallet address, and network
        string fullUrl = baseUrl + Manager.instance.userInfoManager.walletAddress + network;

        // Open the full URL in the user's default web browser
        Application.OpenURL(fullUrl);
    }

    public void OnLogoutClicked()
    {
        Manager.instance.userInfoManager.ResetUserData();
        InitializeSceneUI();
    }

    //Update url to that of Arcade's Twitter Account
    public void OnFollowUsOnXClicked()
    {
        string twitterFollowUrl = "https://x.com/intent/follow?screen_name=arcadedotapp";
        Application.OpenURL(twitterFollowUrl);
    }


    //CREATE GAME
    public void OnCreateGameButtonClicked()
    {

        Debug.Log("Create Game Button Clicked ");

        createPanelHomeGO.SetActive(false);
        createGamePanelGO.SetActive(true);
        createTournamentPanelGO.SetActive(false);
        chooseGameTemplatePanelGO.SetActive(true);
        selectFacePanelGO.SetActive(false);
        selectBackgroundPanelGO.SetActive(false);
        selectJumpAudioPanelGO.SetActive(false);
        selectBGAudioPanelGO.SetActive(false);
        selectGameOverAudioPanelGO.SetActive(false);
        addGameNamePanelGO.SetActive(false);
        gameSharePanelGO.SetActive(false);

        StartCoroutine(PopulateGameTemplateListPanel());
    }

    public IEnumerator PopulateGameTemplateListPanel()
    {
        Debug.Log("Populating Game Template List Panel");

        yield return StartCoroutine(DestroyPopulateGameTemplateListPanelCoroutine());

        for (int i = 0; i < Manager.instance.gameDataManager.gameTemplateNameList.Count; i++)
        {
            GameObject gameTemplateButton = Instantiate(chooseGameTemplateButtonPrefab, gameListPanelGO.transform);
            gameTemplateButton.GetComponent<GameTemplateButton>().chooseGameTemplateId = i;
            gameTemplateButton.GetComponent<Image>().sprite = Manager.instance.gameDataManager.gameTemplateImageNameList[i];
            //gameTemplateButton.GetComponentInChildren<TMP_Text>().text = Manager.instance.gameDataManager.gameTemplateNameList[i];
        }
    }


    public IEnumerator DestroyPopulateGameTemplateListPanelCoroutine()
    {
        foreach (Transform child in gameListPanelGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }


        yield return null;
    }


    public void OnChooseGameTemplateButtonClicked()
    {
        chooseGameTemplatePanelGO.SetActive(false);
        selectFacePanelGO.SetActive(true);

        StartCoroutine(SpawnFaceToggles());
    }

    public void OnCreateGameTemplateBackClicked()
    {
        createPanelHomeGO.SetActive(true);
        createGamePanelGO.SetActive(false);
        createTournamentPanelGO.SetActive(false);
        chooseGameTemplatePanelGO.SetActive(false);

    }


    public IEnumerator SpawnFaceToggles()
    {

        Debug.Log("Spawning Face Toggles");


        yield return StartCoroutine(DestroySpawnFaceTogglesCoroutine());

        for (int i = 0; i < selectFaceSpriteArray.Length; i++)
        {
            GameObject selectFaceToggleGO = Instantiate(selectFaceTogglePrefab, selectFaceContentGO.transform);
            selectFaceToggleGO.GetComponent<SelectFaceToggleHandler>().selectFaceToggleId = i;

            selectFaceToggleGO.GetComponent<SelectFaceToggleHandler>().selectFaceToggleImage.sprite = selectFaceSpriteArray[i];
            selectFaceToggleGO.GetComponent<SelectFaceToggleHandler>().canvasManager = gameObject.GetComponent<CanvasManager>();
            selectFaceToggleGO.GetComponent<SelectFaceToggleHandler>().selectFaceToggleIndex = i;
            selectFaceToggleList.Add(selectFaceToggleGO);
        }

    }


    public IEnumerator DestroySpawnFaceTogglesCoroutine()
    {
        foreach (Transform child in selectFaceContentGO.transform)
        {
            // destroy the child gameobject
            Destroy(child.gameObject);
        }

        selectFaceToggleList.Clear();

        yield return null; 
    }


    public void OnCreateGameFaceBackClicked()
    {
        chooseGameTemplatePanelGO.SetActive(true);
        selectFacePanelGO.SetActive(false);
    }

    public void OnSelectFaceNextClicked()
    {
        selectFacePanelGO.SetActive(false);
        selectBackgroundPanelGO.SetActive(true);

        StartCoroutine(SpawnBackgroundToggles());
    }

    public IEnumerator SpawnBackgroundToggles()
    {
        Debug.Log("Spawning Background Toggles");

        yield return StartCoroutine(DestroySpawnBackgroundTogglesCoroutine());

        for (int i = 0; i < selectBackgroundSpriteArray.Length; i++)
        {
            GameObject selectBackgroundToggleGO = Instantiate(selectBackgroundTogglePrefab, selectBackgroundContentGO.transform);
            selectBackgroundToggleGO.GetComponent<SelectBackgroundToggleHandler>().selectBackgroundToggleId = i;

            selectBackgroundToggleGO.GetComponent<SelectBackgroundToggleHandler>().selectBackgroundToggleImage.sprite = selectBackgroundSpriteArray[i];
            selectBackgroundToggleGO.GetComponent<SelectBackgroundToggleHandler>().canvasManager = gameObject.GetComponent<CanvasManager>();
            selectBackgroundToggleGO.GetComponent<SelectBackgroundToggleHandler>().selectBackgroundToggleIndex = i;
            selectBackgroundToggleList.Add(selectBackgroundToggleGO);
        }
    }

    public IEnumerator DestroySpawnBackgroundTogglesCoroutine()
    {
        foreach (Transform child in selectBackgroundContentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        selectBackgroundToggleList.Clear();

        yield return null;
    }

    public void OnCreateGameBackgroundBackClicked()
    {
        selectFacePanelGO.SetActive(true);
        selectBackgroundPanelGO.SetActive(false);
    }

    public void OnSelectBackgroundNextClicked()
    {
        selectBackgroundPanelGO.SetActive(false);
        selectJumpAudioPanelGO.SetActive(true);

        StartCoroutine(SpawnJumpAudioToggles());
    }



    public IEnumerator SpawnJumpAudioToggles()
    {
        Debug.Log("Spawning Jump Audio Toggles");

        yield return StartCoroutine(DestroySpawnJumpAudioTogglesCoroutine());


        for (int i = 0; i < selectJumpAudioClipArray.Length; i++)
        {
            GameObject selectJumpAudioToggleGO = Instantiate(selectJumpAudioTogglePrefab, selectJumpAudioToggleContentGO.transform);
            selectJumpAudioToggleGO.GetComponent<SelectJumpAudioToggleHandler>().selectJumpAudioToggleId = i;

            selectJumpAudioToggleGO.GetComponent<SelectJumpAudioToggleHandler>().selectJumpAudioToggleAudioClip = selectJumpAudioClipArray[i];
            selectJumpAudioToggleGO.GetComponent<SelectJumpAudioToggleHandler>().selectJumpAudioToggleAudioClipNameText.text = selectJumpAudioClipArray[i].name;
            selectJumpAudioToggleGO.GetComponent<SelectJumpAudioToggleHandler>().canvasManager = gameObject.GetComponent<CanvasManager>();
            selectJumpAudioToggleGO.GetComponent<SelectJumpAudioToggleHandler>().selectJumpAudioToggleIndex = i;
            selectJumpAudioToggleList.Add(selectJumpAudioToggleGO);
        }

    }

    public IEnumerator DestroySpawnJumpAudioTogglesCoroutine()
    {
        foreach (Transform child in selectJumpAudioToggleContentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        selectJumpAudioToggleList.Clear();

        yield return null; 
    }

    public void OnCreateGameJumpAudioBackClicked()
    {
        selectBackgroundPanelGO.SetActive(true);
        selectJumpAudioPanelGO.SetActive(false);
    }


    public void OnSelectJumpAudioNextClicked()
    {
        selectJumpAudioPanelGO.SetActive(false);
        selectBGAudioPanelGO.SetActive(true);

        StartCoroutine(SpawnBGAudioToggles());
    }

    public IEnumerator SpawnBGAudioToggles()
    {

        Debug.Log("Spawning BF Audio Toggles");

        yield return StartCoroutine(DestroySpawnBGAudioTogglesCoroutine());

        for (int i = 0; i < selectBGAudioClipArray.Length; i++)
        {
            GameObject selectBGAudioToggleGO = Instantiate(selectBGAudioTogglePrefab, selectBGAudioToggleContentGO.transform);
            selectBGAudioToggleGO.GetComponent<SelectBGAudioToggleHandler>().selectBGAudioToggleId = i;

            selectBGAudioToggleGO.GetComponent<SelectBGAudioToggleHandler>().selectBGAudioToggleAudioClip = selectBGAudioClipArray[i];
            selectBGAudioToggleGO.GetComponent<SelectBGAudioToggleHandler>().selectBGAudioToggleAudioClipNameText.text = selectBGAudioClipArray[i].name;
            selectBGAudioToggleGO.GetComponent<SelectBGAudioToggleHandler>().canvasManager = gameObject.GetComponent<CanvasManager>();
            selectBGAudioToggleGO.GetComponent<SelectBGAudioToggleHandler>().selectBGAudioToggleIndex = i;
            selectBGAudioToggleList.Add(selectBGAudioToggleGO);
        }

    }

    public IEnumerator DestroySpawnBGAudioTogglesCoroutine()
    {


        foreach (Transform child in selectBGAudioToggleContentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        selectBGAudioToggleList.Clear();

        yield return null;
    }

    public void OnCreateGameBGAudioBackClicked()
    {
        selectJumpAudioPanelGO.SetActive(true);
        selectBGAudioPanelGO.SetActive(false);
    }

    public void OnSelectBGAudioNextClicked()
    {
        selectBGAudioPanelGO.SetActive(false);
        selectGameOverAudioPanelGO.SetActive(true);

        StartCoroutine(SpawnGameOverAudioToggles());
    }


    public IEnumerator SpawnGameOverAudioToggles()
    {

        Debug.Log("Spawning Game Over Audio Toggles");


        yield return StartCoroutine(DestroySpawnGameOverAudioTogglesCoroutine());

        for (int i = 0; i < selectGameOverAudioClipArray.Length; i++)
        {
            GameObject selectGameOverAudioToggleGO = Instantiate(selectGameOverAudioTogglePrefab, selectGameOverAudioToggleContentGO.transform);
            selectGameOverAudioToggleGO.GetComponent<SelectGameOverAudioToggleHandler>().selectGameOverAudioToggleId = i;

            selectGameOverAudioToggleGO.GetComponent<SelectGameOverAudioToggleHandler>().selectGameOverAudioToggleAudioClip = selectGameOverAudioClipArray[i];
            selectGameOverAudioToggleGO.GetComponent<SelectGameOverAudioToggleHandler>().selectGameOverAudioToggleAudioClipNameText.text = selectGameOverAudioClipArray[i].name;
            selectGameOverAudioToggleGO.GetComponent<SelectGameOverAudioToggleHandler>().canvasManager = gameObject.GetComponent<CanvasManager>();
            selectGameOverAudioToggleGO.GetComponent<SelectGameOverAudioToggleHandler>().selectGameOverAudioToggleIndex = i;
            selectGameOverAudioToggleList.Add(selectGameOverAudioToggleGO);
        }

    }

    public IEnumerator DestroySpawnGameOverAudioTogglesCoroutine()
    {
        foreach (Transform child in selectGameOverAudioToggleContentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        selectGameOverAudioToggleList.Clear();

        yield return null;
    }

    public void OnCreateGameGameOverAudioBackClicked()
    {
        selectBGAudioPanelGO.SetActive(true);
        selectGameOverAudioPanelGO.SetActive(false);
    }

    public void OnSelectGameOverAudioNextClicked()
    {
        addGameNamePanelGO.SetActive(true);
    }


    public void OnAddGameNameSaveClicked()
    {
        StartCoroutine(OnAddGameNameSaveClickedCoroutine());
    }


    public IEnumerator OnAddGameNameSaveClickedCoroutine()
    {

        Debug.Log("Adding Game Data to Server");


        Manager.instance.gameDataManager.gameGameName = addGameNameInputFieldLegacy.text;

        //Save data on the server
        // - Create new php script to save game data
        // - Create new webManager function to send data to server
        yield return Manager.instance.webManager.StoreGameData(Manager.instance.userInfoManager.userId,
                                                    Manager.instance.gameDataManager.gameTemplateId,
                                                    Manager.instance.gameDataManager.gameFaceId,
                                                    Manager.instance.gameDataManager.gameBackgroundId,
                                                    Manager.instance.gameDataManager.gameJumpAudioId,
                                                    Manager.instance.gameDataManager.gameBGAudioId,
                                                    Manager.instance.gameDataManager.gameGameOverAudioId,
                                                    Manager.instance.gameDataManager.gameGameName,
                                                    0);

        


    }

    public void OnCreateGameNameSaveBackClicked()
    {
        addGameNamePanelGO.SetActive(false);
    }

    public void ShowGameSharePanel()
    {
        //Call after successful server data save from the webManager script
        gameSharePanelGO.SetActive(true);

        //Set Game Icon
        //Set Game Name

        gameShareGameIcon.sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.gameDataManager.gameTemplateId];
        gameShareGameNameText.text = Manager.instance.gameDataManager.gameGameName;

        selectGameOverAudioPanelGO.SetActive(false);
        addGameNamePanelGO.SetActive(false);
    }


    //===================================================== IMPLEMENT TOMORROW =====================================================
    public void OnGameSharePlayNowClicked()
    {
        //Start the game by loading the corr. Game scene using the data stored in the GameDataManager script
        Debug.Log("Starting Game");


        //Give user the abilty to play game 
        StartCoroutine(OnGameSharedPlayNowClickedCoroutine());
    }

    public IEnumerator OnGameSharedPlayNowClickedCoroutine()
    {
        yield return StartCoroutine(Manager.instance.webManager.GetOrCreateGameScoreCoroutine(Manager.instance.userInfoManager.userId, Manager.instance.gameDataManager.gameId));

        Manager.instance.gameManager.OnCanvasGameSelectedPlayClicked(false);

        //Disable CanvasRegisterSign-in Panel 
        //Disable HomeProfileTournamentreate Panel 

        gameSharePanelGO.SetActive(false);
        createGamePanelGO.SetActive(false);
        createPanelGO.SetActive(false);
        homeProfileTournamentCreatePanelGO.SetActive(false);
        systemBarGO.SetActive(false);
    }

    public void OnGameShareShareClicked()
    {
        //Sahre the game url with friends
        Debug.Log("Sharing Game");

    }

    public void OnGameShareCloseClicked()
    {
        createPanelHomeGO.SetActive(true);
        createGamePanelGO.SetActive(false);
        createTournamentPanelGO.SetActive(false);
        chooseGameTemplatePanelGO.SetActive(false);
        selectFacePanelGO.SetActive(false);
        selectBackgroundPanelGO.SetActive(false);
        selectJumpAudioPanelGO.SetActive(false);
        selectBGAudioPanelGO.SetActive(false);
        selectGameOverAudioPanelGO.SetActive(false);
        addGameNamePanelGO.SetActive(false);
        gameSharePanelGO.SetActive(false);
    }


    //CREATE TOURNAMENT

    public void OnCreateTournamentButtonClicked()
    {
        StartCoroutine(OnCreateTournamentButtonClickedCoroutine());
    }

    public IEnumerator OnCreateTournamentButtonClickedCoroutine()
    {
        //Debug.Log("Create Tournament Button Clicked ");

        createPanelHomeGO.SetActive(false);
        createGamePanelGO.SetActive(false);
        createTournamentPanelGO.SetActive(true);

        chooseTournamentGamePanelGO.SetActive(true);
        tournamentDetailsPanelGO.SetActive(false);
        prizeDetailsPanelGO.SetActive(false);
        confirmationPanelGO.SetActive(false);

        yield return StartCoroutine(GetTournamentUserGameDataFromServer());
    }

    //Called when "Create New Game" Button is clicked in Create Tournament -> Choose Game
    public void OnCreateNewGameUnderTournamentClicked()
    {
        chooseTournamentGamePanelGO.SetActive(false);
        tournamentDetailsPanelGO.SetActive(false);
        prizeDetailsPanelGO.SetActive(false);
        confirmationPanelGO.SetActive(false);

        OnCreateGameButtonClicked();
    }


    public IEnumerator GetTournamentUserGameDataFromServer()
    {

        //Debug.Log("Getting Tournament User Game Data From Server");


        //Get user game list data of the server
        // - send user id 
        // - get gameId, gameTemplateId, gameFaceId, gameBackgroundId, gameJumpAudioId, gameBGAudioId, gameGameOverAudioId, gameGameName, gamePlayCount
        // Store the above in corr Lists in GameDataManager
        yield return StartCoroutine(Manager.instance.webManager.GetUserGames(Manager.instance.userInfoManager.userId));

        StartCoroutine(PopulateChooseTournamentGamePanel());

    }

    public IEnumerator PopulateChooseTournamentGamePanel()
    {

        //Debug.Log("Populating Choose Game Panel");



        //Parse the list to     
        // - spawn ChooseGameButton Prefab under "Your Games Scroll View Content" panel 
        // - Assign Game Sprite and Game Name
        // - while spawning, assign an id in the ChooseGameButton Prefab attached script 
        // - This attached script will handle button functionality, and store relevant tournament data in TournamentDataManager script
        //   - On successful data store, proceed to the Tournament Details Panel

        yield return StartCoroutine(DestroyChooseTournamentGameButtonCoroutine());

        for (int i = 0; i < Manager.instance.gameDataManager.userGameIdList.Count; i++)
        {
            //Debug.Log("Spawnning Choose Tournament Game Template Buttons");
            
            GameObject chooseTournamentGameButton = Instantiate(chooseTournamentGameButtonPrefab, chooseTournamentGameButtonContentGO.transform);
            int userGameTemplateIdInt = Manager.instance.gameDataManager.userGameTemplateIdList[i];



            chooseTournamentGameButton.GetComponent<ChooseTournamentGameButton>().chooseTournamentGameId = Manager.instance.gameDataManager.userGameIdList[i];
            chooseTournamentGameButton.GetComponent<Image>().sprite = Manager.instance.gameDataManager.gameTemplateImageList[userGameTemplateIdInt];
            chooseTournamentGameButton.GetComponentInChildren<TMP_Text>().text = Manager.instance.gameDataManager.userGameNameList[i];
        }

        //Debug.Log("Successfully Spawnned Choose Tournament Game Template Buttons");

    }

    public IEnumerator DestroyChooseTournamentGameButtonCoroutine()
    {
        foreach (Transform child in chooseTournamentGameButtonContentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }
        yield return null;
    }


    public void OnChooseTournamentGameTemplateButtonClicked()
    {

        chooseTournamentGamePanelGO.SetActive(false);
        tournamentDetailsPanelGO.SetActive(true);
        prizeDetailsPanelGO.SetActive(false);
        confirmationPanelGO.SetActive(false);

    }

    public void OnCreateTournamentGameTemplateBackClicked()
    {
        createPanelHomeGO.SetActive(true);
        createGamePanelGO.SetActive(false);
        createTournamentPanelGO.SetActive(false);
        chooseTournamentGamePanelGO.SetActive(false);
    }

    public void OnEnterStartTimeButtonClicked()
    {
        enterStartTimePanelGO.SetActive(true);
    }

    public void OnEnterEndTimeButtonClicked()
    {
        enterEndTimePanelGO.SetActive(true);
    }

    //Set the Start Time Text via Time Picker Pop-up
    public void OnEnterStartTimeSubmitClicked()
    {
        startTimeText.text = chooseStartTimeHourInputField.text + ":" + chooseStartTimeMinuteInputField.text;
        CheckTournamentDetailsInputFields();
        enterStartTimePanelGO.SetActive(false);

    }

    //Set the End Time Text via Time Picker Pop-up
    public void OnEnterEndTimeSubmitClicked()
    {
        endTimeText.text = chooseEndTimeHourInputField.text + ":" + chooseEndTimeMinuteInputField.text;
        CheckTournamentDetailsInputFields();
        enterEndTimePanelGO.SetActive(false);
    }

    //code to enable the Next button and change button text color
    //mostly by some events and stuff as done above
    void CheckTournamentDetailsInputFields()
    {
        // Check if all input fields are not empty
        if (!string.IsNullOrWhiteSpace(tournamentNameInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(hostNameInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(socialLinkInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(playerJoiningFeeInputFieldLegacy.text) &&
            !string.IsNullOrWhiteSpace(startDateInputField.text) &&
            !string.IsNullOrWhiteSpace(endDateInputField.text) &&
            !string.IsNullOrWhiteSpace(startTimeText.text) &&
            !string.IsNullOrWhiteSpace(endTimeText.text))
        {
            tournamentsDetailsNextButtonText.color = inputFieldFilledButtonTextColor;
            tournamentsDetailsNextButton.interactable = true; // Enable the button
        }
        else
        {
            tournamentsDetailsNextButtonText.color = Color.white;
            tournamentsDetailsNextButton.interactable = false; // Disable the button
        }
    }


    public void OnCreateTournamentDetailsBackClicked()
    {
        chooseTournamentGamePanelGO.SetActive(true);
        tournamentDetailsPanelGO.SetActive(false);
    }

    public void OnTournamentDetailsNextClicked()
    {

        Manager.instance.tournamentDataManager.tournamentName = tournamentNameInputFieldLegacy.text;
        Manager.instance.tournamentDataManager.tournamentHostName = hostNameInputFieldLegacy.text;
        Manager.instance.tournamentDataManager.socialLink = socialLinkInputFieldLegacy.text;

        if (float.TryParse(playerJoiningFeeInputFieldLegacy.text, out float playerJoiningFeeFloat))
        {
            Manager.instance.tournamentDataManager.playerJoiningFee = playerJoiningFeeFloat;
        }
        Manager.instance.tournamentDataManager.startDate = ConvertDateToInt(startDateInputField.text, startTimeText.text);
        Manager.instance.tournamentDataManager.endDate = ConvertDateToInt(endDateInputField.text, endTimeText.text);
        Manager.instance.tournamentDataManager.startTime = ConvertTimeToInt(startDateInputField.text, startTimeText.text);
        Manager.instance.tournamentDataManager.endTime = ConvertTimeToInt(endDateInputField.text, endTimeText.text);

        //code to save tournament name, host name, social link, start date, start time, end dat, end time data in TournamentDataManager

        tournamentDetailsPanelGO.SetActive(false);
        prizeDetailsPanelGO.SetActive(true);
    }


    public int ConvertDateToInt(string dateString, string timeString)
    {
        // Define the formats of the date and time strings
        string dateFormat = "dd-MM-yy";
        string timeFormat = "H:m";

        // Parse the date and time into a DateTime object in the user's local time zone
        DateTime localDate = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
        DateTime localTime = DateTime.ParseExact(timeString, timeFormat, CultureInfo.InvariantCulture);

        // Combine the parsed date and time into one DateTime object
        DateTime localDateTime = new DateTime(localDate.Year, localDate.Month, localDate.Day, localTime.Hour, localTime.Minute, 0);

        // Use the user's local time zone
        TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

        // Convert the local time to UTC
        DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, localTimeZone);

        // Convert the UTC DateTime to an integer format: yyyyMMdd
        int dateAsInt = utcDateTime.Year * 10000 + utcDateTime.Month * 100 + utcDateTime.Day;

        return dateAsInt;
    }

    public int ConvertTimeToInt(string dateString, string timeString)
    {
        // Define the formats of the date and time strings
        string dateFormat = "dd-MM-yy";
        string timeFormat = "H:m";

        // Parse the date and time into a DateTime object in the user's local time zone
        DateTime localDate = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
        DateTime localTime = DateTime.ParseExact(timeString, timeFormat, CultureInfo.InvariantCulture);

        // Combine the parsed date and time into one DateTime object
        DateTime localDateTime = new DateTime(localDate.Year, localDate.Month, localDate.Day, localTime.Hour, localTime.Minute, 0);

        // Use the user's local time zone
        TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

        // Convert the local time to UTC
        DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, localTimeZone);

        // Convert the UTC DateTime to an integer format: HHmm
        int timeAsInt = utcDateTime.Hour * 100 + utcDateTime.Minute;

        return timeAsInt;
    }


    void CheckPrizeDetailsInputField()
    {
        // Check if all input fields are not empty
        if (!string.IsNullOrWhiteSpace(prizePoolAmountInputFieldLegacy.text))
        {

            if (float.TryParse(prizePoolAmountInputFieldLegacy.text, out float prizePool))
            {
                // Calculate prize distribution
                float firstPrize = prizePool * 0.50f;  // 50%
                float secondPrize = prizePool * 0.30f; // 30%
                float thirdPrize = prizePool * 0.20f;  // 20%

                winnerAmountInputFieldLegacy.text = firstPrize.ToString();
                runnerUpAmountInputFieldLegacy.text = secondPrize.ToString();
                secondRunnerUpAmountInputFieldLegacy.text = thirdPrize.ToString();
            }
            else
            {
                Debug.LogError("Invalid prize pool input. Please enter a valid number.");
                Manager.instance.canvasManager.ShowErrorPopup("Invalid prize pool input. Please enter a valid number.");

            }

            prizeDetailsNextButtonText.color = inputFieldFilledButtonTextColor;
            prizeDetailsNextButton.interactable = true; // Enable the button


        }
        else
        {
            prizeDetailsNextButtonText.color = Color.white;
            prizeDetailsNextButton.interactable = false; // Disable the button
        }
    }

    public void OnCreateTournamentPrizeDetailsBackClicked()
    {
        tournamentDetailsPanelGO.SetActive(true);
        prizeDetailsPanelGO.SetActive(false);
    }


    public void OnPrizeDetailsNextClicked()
    {
        Manager.instance.tournamentDataManager.prizePool = float.Parse(prizePoolAmountInputFieldLegacy.text);

        confirmationPanelGO.SetActive(true);

        confirmationPanelWalletBalanceText.text = TruncateToTwoDecimalPlaces(Manager.instance.userInfoManager.walletBalanceFloat) + " SOL";

        confirmationPanelConfirmationText.text = "You will be transferring <b><color=#42EFEC>" + TruncateToTwoDecimalPlaces(Manager.instance.tournamentDataManager.prizePool) + " SOL</color></b> to Tournament Contract & winners will receive the prizes after tournament ends!";    
    
    }

    public void OnCreateTournamentConfirmationPanelBackClicked()
    {
        confirmationPanelGO.SetActive(false);
    }

    public void OnCreateTournamentCreateTournamentClicked()
    {
        // store data on server
        StartCoroutine(Manager.instance.webManager.StoreTournamentData(Manager.instance.userInfoManager.userId,
                                                                        Manager.instance.tournamentDataManager.gameId, 
                                                                        Manager.instance.tournamentDataManager.tournamentName, 
                                                                        Manager.instance.tournamentDataManager.tournamentHostName, 
                                                                        Manager.instance.tournamentDataManager.socialLink, 
                                                                        Manager.instance.tournamentDataManager.playerJoiningFee,
                                                                        Manager.instance.tournamentDataManager.startDate,
                                                                        Manager.instance.tournamentDataManager.startTime, 
                                                                        Manager.instance.tournamentDataManager.endDate, 
                                                                        Manager.instance.tournamentDataManager.endTime, 
                                                                        Manager.instance.tournamentDataManager.prizePool, 
                                                                        0, 0, 0));
        
        //  - after successful data store, create Aptos smart contract 
        //    - checked what is returned on successful tournament creaton on Aptos, save it in the tournaments table on server
        // Disable Prize Details & Confirmation Panel GOs
        // Go to Profile Panel (but after populating the panel with games & tournament data)

    }


    public IEnumerator CreateOnChainTournament()
    {

        Debug.Log("Creating On Chain Tournament");


        yield return StartCoroutine(Manager.instance.solanaWalletManager.CreateTournamentCoroutine());



        //yield return StartCoroutine(Manager.instance.walletManager.ExecuteStartNewTournament(Manager.instance.tournamentDataManager.tournamentId.ToString(),
        //                                                         (Manager.instance.tournamentDataManager.prizePool * 100000000).ToString("F0"),
        //                                                         (Manager.instance.tournamentDataManager.playerJoiningFee * 100000000).ToString("F0"),
        //                                                         Manager.instance.tournamentDataManager.startDate.ToString(),
        //                                                         Manager.instance.tournamentDataManager.endDate.ToString(),
        //                                                         Manager.instance.tournamentDataManager.startTime.ToString(),
        //                                                         Manager.instance.tournamentDataManager.endTime.ToString()));



        Debug.Log("Finished Creating On Chain Tournament");



        // - Get list of all tournaments
        // - Categorize all tournaments into past, live, & upcoming lists         
        // - From the live tournaments, show the one which has highest prize pool                        
        yield return StartCoroutine(Manager.instance.webManager.GetAllTournaments());

        yield return StartCoroutine(CategorizeAllTournamentData());




        //switch to Profile Panel
        //Show tournament Data in Profile Panel

        //reset prize details panel
        prizePoolAmountInputFieldLegacy.text = null;

        //disable prize details panel and confirmation panel 
        prizeDetailsPanelGO.SetActive(false);
        confirmationPanelGO.SetActive(false);







        ////Get user game data from server
        //Debug.Log("Get user game data from server");
        //yield return StartCoroutine(GetTournamentUserGameDataFromServer());

        ////Populate My Games Panel in Profile
        //Debug.Log("Populate My Games Panel in Profile");
        //yield return StartCoroutine(PopulateTopMyGamesProfilePanel());

        ////Populate Tournament Button in Profile
        //Debug.Log("Populate Tournament Button in Profile");
        //PopulateRecentTournamentProfilePanel();

        //enable profile toggle & panel
        profileToggle.isOn = true;
        OnProfileToggleChanged();


    }


    public IEnumerator PopulateTopMyGamesProfilePanel()
    {

        //Parse userGameIdList
        //Show only top 3 most recent games
        //Replace the image and text on the top3 button in MY GAMES Profile section

        foreach (GameObject buttonGO in profileMyGamesButtonGOList)
        {
            buttonGO.SetActive(false);
        }

        // Get the count of games, but limit to a maximum of 3
        int maxGames = Mathf.Min(Manager.instance.gameDataManager.userGameIdList.Count, 3);

        if(maxGames == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                profileMyGamesButtonGOList[i].SetActive(false);
                profileMyGamesEmptyButton.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < maxGames; i++)
            {
                profileMyGamesEmptyButton.SetActive(false);

                int userGameTemplateIdInt = Manager.instance.gameDataManager.userGameTemplateIdList[i];
                profileMyGamesButtonGOList[i].GetComponent<Image>().sprite = Manager.instance.gameDataManager.gameTemplateImageList[userGameTemplateIdInt];
                profileMyGamesButtonGOList[i].GetComponentInChildren<TMP_Text>().text = Manager.instance.gameDataManager.userGameNameList[i];

                profileMyGamesButtonGOList[i].SetActive(true);

            }

            yield return null;
        }


    }




    public void OnProfileViewAllMyGamesClicked()
    {
        StartCoroutine(OnProfileViewAllMyGamesClickedCoroutine());
    }

    public IEnumerator OnProfileViewAllMyGamesClickedCoroutine()
    {
        profilePanelMyGamesPanelGO.SetActive(true);

        //Get Games
        yield return StartCoroutine(Manager.instance.webManager.GetUserGames(Manager.instance.userInfoManager.userId));

        //Populate them in parent by spawnning my game prefabs

        if(Manager.instance.gameDataManager.userGameIdList.Count != 0)
        {
            foreach (Transform child in profilePanelMyGamesButtonParentGO.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Manager.instance.gameDataManager.userGameIdList.Count; i++)
            {
               
                GameObject chooseTournamentGameButton = Instantiate(profilePanelMyGamesButtonPrefab, profilePanelMyGamesButtonParentGO.transform);
                int userGameTemplateIdInt = Manager.instance.gameDataManager.userGameTemplateIdList[i];
                chooseTournamentGameButton.GetComponent<Image>().sprite = Manager.instance.gameDataManager.gameTemplateImageList[userGameTemplateIdInt];
                chooseTournamentGameButton.GetComponentInChildren<TMP_Text>().text = Manager.instance.gameDataManager.userGameNameList[i];
            }
        } 

    }

    public void OnProfileViewAllMyGamesBackClicked()
    {
        profilePanelMyGamesPanelGO.SetActive(false);
    }



    public void OnProfileViewAllMyTournamentsClicked()
    {
        StartCoroutine(OnProfileViewAllMyTournamentsClickedCoroutine());
    }

    public IEnumerator OnProfileViewAllMyTournamentsClickedCoroutine()
    {

        profilePanelMyTournamentsPanelGO.SetActive(true);

        //Get User Tournament Data
        yield return StartCoroutine(Manager.instance.webManager.GetUserTournaments(Manager.instance.userInfoManager.userId));


        if (Manager.instance.tournamentDataManager.userTournamentIdList.Count != 0)
        {

            foreach (Transform child in profilePanelMyTournamentsButtonParentGO.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Manager.instance.tournamentDataManager.userTournamentIdList.Count; i++)
            {
                //spawn My tournament button
                GameObject myTournamentButton = Instantiate(profilePanelMyTournamentsButtonPrefab, profilePanelMyTournamentsButtonParentGO.transform);

                //store tournament info
                myTournamentButton.GetComponent<MyTournamentButton>().tournamentId = Manager.instance.tournamentDataManager.userTournamentIdList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().gameId = Manager.instance.tournamentDataManager.userGameIdList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().tournamentName = Manager.instance.tournamentDataManager.userTournamentNameList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().tournamentHostName = Manager.instance.tournamentDataManager.userTournamentHostNameList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().socialLink = Manager.instance.tournamentDataManager.userSocialLinkList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().playerJoiningFee = Manager.instance.tournamentDataManager.userPlayerJoiningFeeList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().startDate = Manager.instance.tournamentDataManager.userStartDateList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().startTime = Manager.instance.tournamentDataManager.userStartTimeList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().endDate = Manager.instance.tournamentDataManager.userEndDateList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().endTime = Manager.instance.tournamentDataManager.userEndTimeList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().prizePool = Manager.instance.tournamentDataManager.userPrizePoolList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().status = Manager.instance.tournamentDataManager.userStatusList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().playCount = Manager.instance.tournamentDataManager.userPlayCountList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().userCount = Manager.instance.tournamentDataManager.userUserCountList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().winnerId = Manager.instance.tournamentDataManager.userWinnerIdList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().runnerUpId = Manager.instance.tournamentDataManager.userRunnerUpIdList[i];
                myTournamentButton.GetComponent<MyTournamentButton>().secondRunnerUpId = Manager.instance.tournamentDataManager.userSecondRunnerUpIdList[i];

                //populate My tournament button data
                StartCoroutine(myTournamentButton.GetComponent<MyTournamentButton>().PopulateTournamentData());

            }

        }


    }

    public void OnProfileViewAllMyTournamentsBackClicked()
    {
        profilePanelMyTournamentsPanelGO.SetActive(false);
    }

    public void OnTopMyGamesButton1Clicked()
    {
        //launch 1st game

    }

    public void OnTopMyGamesButton2Clicked()
    {
        //launch 2nd game

    }

    public void OnTopMyGamesButton3Clicked()
    {
        //launch 3rd game

    }


    public IEnumerator PopulateRecentTournamentProfilePanel()
    {

        profileTournamentButtonGO.SetActive(false);


        //Get User Tournament Data
        yield return StartCoroutine(Manager.instance.webManager.GetUserTournaments(Manager.instance.userInfoManager.userId));


        int tournamentDataListLength = Manager.instance.tournamentDataManager.userTournamentNameList.Count;

        if(tournamentDataListLength != 0)
        {

            profileTournamentEmptyButtonGO.SetActive(false);
            profileTournamentButtonGO.SetActive(true);


            profileTournamentButtonTitleText.text = Manager.instance.tournamentDataManager.userTournamentNameList[tournamentDataListLength - 1];
            profileTournamentButtonPrizePoolText.text = TruncateToTwoDecimalPlaces(Manager.instance.tournamentDataManager.userPrizePoolList[tournamentDataListLength - 1]) + " SOL";


            string startDate = Manager.instance.tournamentDataManager.userStartDateList[tournamentDataListLength - 1].ToString();
            string startTime = Manager.instance.tournamentDataManager.userStartTimeList[tournamentDataListLength - 1].ToString();
            string endDate = Manager.instance.tournamentDataManager.userEndDateList[tournamentDataListLength - 1].ToString();
            string endTime = Manager.instance.tournamentDataManager.userEndTimeList[tournamentDataListLength - 1].ToString();

            profileTournamentButtonTimeReaminingText.text = GetEventStatus(startDate, startTime, endDate, endTime);



            //Get Tournament Image
            // - same as game template image
            // - get game id for tournament 
            // - get corr game template id for the game id from the userGameList
            // - set the image from gameTemplateImageList


            int gameId = Manager.instance.tournamentDataManager.userGameIdList[tournamentDataListLength - 1];

            StartCoroutine(Manager.instance.webManager.GetGameTemplateId(gameId));
            
            profileTournamentImage.sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.tournamentDataManager.gameTemplateId];
            profileTournamentButtonImage.color = Manager.instance.gameDataManager.gameTemplateColorList[Manager.instance.tournamentDataManager.gameTemplateId];
        }
        else
        {
            profileTournamentButtonGO.SetActive(false);
            profileTournamentEmptyButtonGO.SetActive(true);
        }



    }

    public void OnProfileEmptyCreateGameButtonClicked()
    {
        profilePanelGO.SetActive(false);
        profilePanelHomeGO.SetActive(false);

        profileToggleOnLabelGO.SetActive(false);
        profileToggleOffLabelGO.SetActive(true);

        createToggle.isOn = true;
        OnCreateToggleChanged();

        OnCreateGameButtonClicked();

    }

    public void OnProfileEmptyCreateTournamentButtonClicked()
    {
        profilePanelGO.SetActive(false);
        profilePanelHomeGO.SetActive(false);

        profileToggleOnLabelGO.SetActive(false);
        profileToggleOffLabelGO.SetActive(true);

        createToggle.isOn = true;
        OnCreateToggleChanged();

        OnCreateTournamentButtonClicked();        

    }


    // Method to calculate the event status and remaining time
    public string GetEventStatus(string startDate, string startTime, string endDate, string endTime)
    {
        try
        {
            // Ensure startTime and endTime are at least 4 digits by padding with leading zeros
            startTime = startTime.PadLeft(4, '0'); // e.g., "1" becomes "0001"
            endTime = endTime.PadLeft(4, '0');     // e.g., "359" becomes "0359"

            // Parse startDate (YYYYMMDD) and startTime (HHmm) into a DateTime object in UTC
            string startDateTimeString = $"{startDate} {startTime}";
            DateTime startDateTimeUtc = DateTime.SpecifyKind(DateTime.ParseExact(startDateTimeString, "yyyyMMdd HHmm", null), DateTimeKind.Utc);

            // Parse endDate (YYYYMMDD) and endTime (HHmm) into a DateTime object in UTC
            string endDateTimeString = $"{endDate} {endTime}";
            DateTime endDateTimeUtc = DateTime.SpecifyKind(DateTime.ParseExact(endDateTimeString, "yyyyMMdd HHmm", null), DateTimeKind.Utc);

            // Convert both start and end DateTimes from UTC to the user's local time zone
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime startDateTimeLocal = TimeZoneInfo.ConvertTimeFromUtc(startDateTimeUtc, localTimeZone);
            DateTime endDateTimeLocal = TimeZoneInfo.ConvertTimeFromUtc(endDateTimeUtc, localTimeZone);

            // Get the current local time
            DateTime currentTime = DateTime.Now;

            // Case 1: Event hasn't started yet (current time is before startDateTimeLocal)
            if (currentTime < startDateTimeLocal)
            {
                TimeSpan timeUntilStart = startDateTimeLocal - currentTime;
                string timeUntilStartString = string.Format("{0:D2}", (int)timeUntilStart.TotalHours);
                return "Starts in " + timeUntilStartString + " Hrs";
            }

            // Case 2: Event has started but hasn't ended (current time is between startDateTimeLocal and endDateTimeLocal)
            if (currentTime >= startDateTimeLocal && currentTime < endDateTimeLocal)
            {
                TimeSpan timeUntilEnd = endDateTimeLocal - currentTime;
                string timeUntilEndString = string.Format("{0:D2}", (int)timeUntilEnd.TotalHours);
                return "Ends in " + timeUntilEndString + " Hrs";
            }

            // Case 3: Event has ended (current time is after endDateTimeLocal)
            if (currentTime >= endDateTimeLocal)
            {
                return "Tournament Ended";
            }

            return "Invalid state"; // This case should never be reached.
        }
        catch (Exception ex)
        {
            Debug.LogError("Error calculating event status: " + ex.Message);

            return "Invalid input";
        }
    }


    // Method to truncate the float to two decimal places
    public string TruncateToTwoDecimalPlaces(float value)
    {

        // Check if the float is a whole number
        if (value == Mathf.Floor(value))
        {
            // Return the integer part if it's a whole number
            return ((int)value).ToString();
        }
        else
        {
            // Multiply by 100, cast to int to truncate, then divide by 100 again
            float truncatedValue = Mathf.Floor(value * 100f) / 100f;

            // Return the result as a string with 2 decimal places
            return truncatedValue.ToString("F2");
        }

    }

    public void ShowErrorPopup(string message)
    {
        errorPopupText.text = message; 
        errorPopupGameObject.SetActive(true); 
        StartCoroutine(DisablePopupAfterDelay(5f)); 
    }

    private IEnumerator DisablePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        errorPopupGameObject.SetActive(false); 
    }

}
