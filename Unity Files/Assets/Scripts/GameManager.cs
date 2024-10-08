using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("Generic")]
    public int totalScore = 0;
    public int currentScore = 0;
    public bool isTournament;

  
    [Header("UI")]
    [Header("Start")]
    public GameObject[] uiStartPanelGOArray;
    public Image[] uiStartBGImageArray;
    public Image[] uiGameIconImageArray;
    public TMP_Text[] uiGameIconTitleTextArray;
    public TMP_Text[] uiTitleTotalScoreTextArray;

    [Header("OnGoing")]
    public GameObject[] uiOngoingPanelGOArray;
    public TMP_Text[] uiOngoingScoreTextArray;

    [Header("Game Over")]

    public GameObject[] uiGameOverPanelGOArray;
    public Image[] uiGameOverBGImageArray;
    public TMP_Text[] uiGameOverTotalScoreTextArray;
    public TMP_Text[] uiGameOverScoreTextArray;
    public GameObject[] uiGameOverPlayAgainButtonGOArray;
    public GameObject[] uiGameOverExitButtonGOArray;


    [Space(10)]
    [Header("Game 1")]
    public GameObject game1Parent;
    public GameObject game1PlayerGO;
    public Game1Player game1Player;
    public GameObject game1GameManagerGO;
    public Game1Manager game1Manager;
    public SpriteRenderer game1PlayerSpriteRenderer;
    public AudioSource game1PlayerAudioSource;
    public GameObject game1BackgroundGO;
    public SpriteRenderer game1BackgroundSpriteRenderer;
    public AudioSource game1BackgroundAudioSource;
    public Game1CameraMovement game1CameraMovement;
    public bool isGame1Over = false;

    [Header("Game 2")]
    public GameObject game2Parent;
    public GameObject game2PlayerGO;
    public Game2Player game2Player;
    public GameObject game2GameManagerGO;
    public Game2Manager game2Manager;
    public SpriteRenderer game2PlayerSpriteRenderer;
    public AudioSource game2PlayerAudioSource;
    public GameObject game2BackgroundParentGO;
    public Color[] gam2ObstacleColorArray;
    public GameObject game2ObstacleParentGO;
    public AudioSource game2BackgroundAudioSource;
    //public Game1CameraMovement game2CameraMovement;  //No Camera Movement in Game2
    public bool isGame2Over = false;


    [Header("Customizables")]
    public Sprite playerSprite;
    public Sprite backgroundSprite;
    public AudioClip playerJumpAudioClip;
    public AudioClip backgroundAudioClip;
    public AudioClip gameOverAudioClip;


    public void Start()
    {
        //game1Parent.SetActive(false);   
        
    }



    public void OnCanvasGameSelectedPlayClicked(bool tournament)
    {

        if (tournament)
        {
            isTournament = tournament;
            if (Manager.instance.gameDataManager.gameTemplateId == 0)
            {
                //Do Game1 Stuff
                SetGame1Customizable();
            }
            else if (Manager.instance.gameDataManager.gameTemplateId == 1)
            {
                //Do Game2 Stuff
                SetGame2Customizable();
            }
            else if (Manager.instance.gameDataManager.gameTemplateId == 2)
            {
                //Do Game3 Stuff
            }

        }
        else
        {
            isTournament = tournament;
            if (Manager.instance.gameDataManager.gameTemplateId == 0)
            {
                //Do Game1 Stuff
                SetGame1Customizable();
            }
            else if (Manager.instance.gameDataManager.gameTemplateId == 1)
            {
                //Do Game2 Stuff
                SetGame2Customizable();
            }
            else if (Manager.instance.gameDataManager.gameTemplateId == 2)
            {
                //Do Game3 Stuff
            }

        }
    }


    //Call when clicked to Play game from Canavas
    public void SetGame1Customizable()
    {

        uiGameIconImageArray[0].sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.gameDataManager.gameTemplateId];
        uiGameIconTitleTextArray[0].text = Manager.instance.gameDataManager.gameGameName;

        if (isTournament)
        {
            totalScore = Manager.instance.userInfoManager.tournamentScore;
        }
        else
        {
            totalScore = Manager.instance.userInfoManager.gameScore;
        }

        uiTitleTotalScoreTextArray[0].text = totalScore.ToString();

        playerSprite = Manager.instance.canvasManager.selectFaceSpriteArray[Manager.instance.gameDataManager.gameFaceId];
        backgroundSprite = Manager.instance.canvasManager.selectBackgroundSpriteArray[Manager.instance.gameDataManager.gameBackgroundId];
        playerJumpAudioClip = Manager.instance.canvasManager.selectJumpAudioClipArray[Manager.instance.gameDataManager.gameJumpAudioId];
        backgroundAudioClip = Manager.instance.canvasManager.selectBGAudioClipArray[Manager.instance.gameDataManager.gameBGAudioId];
        gameOverAudioClip = Manager.instance.canvasManager.selectGameOverAudioClipArray[Manager.instance.gameDataManager.gameGameOverAudioId];


        //Sprites
        game1PlayerSpriteRenderer.sprite = playerSprite;
        game1BackgroundSpriteRenderer.sprite = backgroundSprite;
        uiStartBGImageArray[0].sprite = backgroundSprite;
        uiGameOverBGImageArray[0].sprite = backgroundSprite;


        //Player Jump Audio
        game1Player.player1AudioSource = game1PlayerAudioSource;
        game1Player.player1JumpAudioClip = playerJumpAudioClip;

        //Background Audio
        game1BackgroundAudioSource.clip = backgroundAudioClip;

        game1Parent.SetActive(true);
        uiStartPanelGOArray[0].SetActive(true);
        uiOngoingPanelGOArray[0].SetActive(false);
        uiGameOverPanelGOArray[0].SetActive(false);

        isGame1Over = false;
        currentScore = 0;
    }

    public void SetGame2Customizable()
    {

        uiGameIconImageArray[1].sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.gameDataManager.gameTemplateId];
        uiGameIconTitleTextArray[1].text = Manager.instance.gameDataManager.gameGameName;

        if (isTournament)
        {
            totalScore = Manager.instance.userInfoManager.tournamentScore;
        }
        else
        {
            totalScore = Manager.instance.userInfoManager.gameScore;
        }

        uiTitleTotalScoreTextArray[1].text = totalScore.ToString();

        playerSprite = Manager.instance.canvasManager.selectFaceSpriteArray[Manager.instance.gameDataManager.gameFaceId];
        backgroundSprite = Manager.instance.canvasManager.selectBackgroundSpriteArray[Manager.instance.gameDataManager.gameBackgroundId];
        playerJumpAudioClip = Manager.instance.canvasManager.selectJumpAudioClipArray[Manager.instance.gameDataManager.gameJumpAudioId];
        backgroundAudioClip = Manager.instance.canvasManager.selectBGAudioClipArray[Manager.instance.gameDataManager.gameBGAudioId];
        gameOverAudioClip = Manager.instance.canvasManager.selectGameOverAudioClipArray[Manager.instance.gameDataManager.gameGameOverAudioId];


        //Sprites
        game2PlayerSpriteRenderer.sprite = playerSprite;

        foreach (Transform childTransform in game2BackgroundParentGO.transform)
        {
            childTransform.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        }

        foreach (Transform childTransform in game2ObstacleParentGO.transform)
        {
            childTransform.gameObject.GetComponent<SpriteRenderer>().color = gam2ObstacleColorArray[Manager.instance.gameDataManager.gameBackgroundId];
        }

        uiStartBGImageArray[1].sprite = backgroundSprite;
        uiGameOverBGImageArray[1].sprite = backgroundSprite;


        //Player Jump Audio
        game2Player.player2AudioSource = game2PlayerAudioSource;
        game2Player.player2JumpAudioClip = playerJumpAudioClip;

        //Background Audio
        game2BackgroundAudioSource.clip = backgroundAudioClip;

        game2Parent.SetActive(true);
        uiStartPanelGOArray[1].SetActive(true);
        uiOngoingPanelGOArray[1].SetActive(false);
        uiGameOverPanelGOArray[1].SetActive(false);

        isGame2Over = false;
        currentScore = 0;
    }


    public void OnGame1PlayClicked()
    {

        if (isTournament)
        {

            //Increment tournament play count
            StartCoroutine(Manager.instance.webManager.IncrementTournamentPlayCountCoroutine(Manager.instance.tournamentDataManager.tournamentId));
           
        }
        else
        {
            //Increment game play count
            StartCoroutine(Manager.instance.webManager.IncrementGamePlayCountCoroutine(Manager.instance.gameDataManager.gameId));
        }

        
        game1Player.transform.position = new Vector3(0, -2.53f, 0);
        
        game1Manager.DestroyAllBrickGameObjects();

        uiStartPanelGOArray[0].SetActive(false);
        uiOngoingPanelGOArray[0].SetActive(true);

        currentScore = 0;
        uiOngoingScoreTextArray[0].text = currentScore.ToString();


        game1Player.transform.position = new Vector3(0, -2.53f, 0);
        game1CameraMovement.transform.position = game1CameraMovement.initialCameraPosition;
        game1BackgroundGO.transform.position = new Vector3(0, 0, 0);
        ResetGame1CameraPosition();



        game1BackgroundAudioSource.loop = true;
        game1BackgroundAudioSource.Play();

        StartCoroutine(WaitForXSecCoroutine(2));

        game1Manager.SpawnInitialBlock();

        game1Player.canJump = true;
        game1Manager.canSpawn = true;


        
    }

    public void OnGame2PlayClicked()
    {

        if (isTournament)
        {

            //Increment tournament play count
            StartCoroutine(Manager.instance.webManager.IncrementTournamentPlayCountCoroutine(Manager.instance.tournamentDataManager.tournamentId));

        }
        else
        {
            //Increment game play count
            StartCoroutine(Manager.instance.webManager.IncrementGamePlayCountCoroutine(Manager.instance.gameDataManager.gameId));
        }


        game2Player.transform.position = new Vector3(-1.25f, 0.35f, 0);      
        game2Manager.DestroyAllHoopGameObjects();

        uiStartPanelGOArray[1].SetActive(false);
        uiOngoingPanelGOArray[1].SetActive(true);

        currentScore = 0;
        uiOngoingScoreTextArray[1].text = currentScore.ToString();


        game2BackgroundAudioSource.loop = true;
        game2BackgroundAudioSource.Play();

        StartCoroutine(WaitForXSecCoroutine(2));


        game2Player.canFlap = true;
        game2Manager.canSpawn = true;
        game2Player.rb.gravityScale = 1.5f;
        StartCoroutine(game2Manager.SpawnHoop());





    }

    public IEnumerator WaitForXSecCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


    public void ShowGameOver()
    {


        if(Manager.instance.gameDataManager.gameTemplateId == 0)
        {
            if (isGame1Over == false)
            {
                isGame1Over = true;

                StartCoroutine(ShowGame1OverCoroutine());

            }
        }
        else if (Manager.instance.gameDataManager.gameTemplateId == 1)
        {
            if (isGame2Over == false)
            {
                isGame2Over = true;

                StartCoroutine(ShowGame2OverCoroutine());

            }
        }

    }




    public IEnumerator ShowGame1OverCoroutine()
    {
        uiGameOverPlayAgainButtonGOArray[0].SetActive(false);
        uiGameOverExitButtonGOArray[0].SetActive(false);

        uiOngoingPanelGOArray[0].SetActive(false);
        uiGameOverPanelGOArray[0].SetActive(true);


        Debug.Log("-------------------------------------------------------------> Game OVER!!!");



        game1Player.canJump = false;
        game1Manager.canSpawn = false;

        if (isTournament)
        {
            uiGameOverTotalScoreTextArray[0].text = (Manager.instance.userInfoManager.tournamentScore + currentScore).ToString();

        }
        else
        {
            uiGameOverTotalScoreTextArray[0].text = (Manager.instance.userInfoManager.gameScore + currentScore).ToString();

        }


        uiGameOverScoreTextArray[0].text = currentScore.ToString();

        game1BackgroundAudioSource.Stop();
        game1BackgroundAudioSource.clip = gameOverAudioClip;
        game1BackgroundAudioSource.loop = false;
        game1BackgroundAudioSource.Play();

        if (isTournament)
        {
            //Store torunamentScore on Server & Smart contract
            //Storing tournament score for user
            yield return StartCoroutine(Manager.instance.webManager.StoreTournamentScoreCoroutine(Manager.instance.userInfoManager.userId,
                                                                                                  Manager.instance.tournamentDataManager.tournamentId,
                                                                                                  Manager.instance.userInfoManager.tournamentScore + currentScore));

            //yield return StartCoroutine(Manager.instance.walletManager.ExecuteRecordScore(Manager.instance.tournamentDataManager.tournamentId.ToString(), (Manager.instance.userInfoManager.tournamentScore + currentScore).ToString()));

            Manager.instance.userInfoManager.tournamentScore = Manager.instance.userInfoManager.tournamentScore + currentScore;

            //Submit Score
            yield return StartCoroutine(Manager.instance.solanaWalletManager.SubmitScoreCoroutine());
        
        }
        else
        {
            //Store Score on Server
            yield return StartCoroutine(Manager.instance.webManager.UpdateGameScoreCoroutine(Manager.instance.userInfoManager.userId, Manager.instance.gameDataManager.gameId, Manager.instance.userInfoManager.gameScore + currentScore));

        }

        uiGameOverPlayAgainButtonGOArray[0].SetActive(true);
        uiGameOverExitButtonGOArray[0].SetActive(true);






        game1Manager.DestroyAllBrickGameObjects();

        game1Player.transform.position = new Vector3(0, -2.53f, 0);
        game1CameraMovement.transform.position = game1CameraMovement.initialCameraPosition;
        game1BackgroundGO.transform.position = new Vector3(0, 0, 0);
    }


    public IEnumerator ShowGame2OverCoroutine()
    {
        uiGameOverPlayAgainButtonGOArray[1].SetActive(false);
        uiGameOverExitButtonGOArray[1].SetActive(false);

        uiOngoingPanelGOArray[1].SetActive(false);
        uiGameOverPanelGOArray[1].SetActive(true);


        Debug.Log("-------------------------------------------------------------> Game OVER!!!");



        game2Player.canFlap = false;
        game2Manager.canSpawn = false;
        game2Player.rb.gravityScale = 0;


        if (isTournament)
        {
            uiGameOverTotalScoreTextArray[1].text = (Manager.instance.userInfoManager.tournamentScore + currentScore).ToString();

        }
        else
        {
            uiGameOverTotalScoreTextArray[1].text = (Manager.instance.userInfoManager.gameScore + currentScore).ToString();

        }


        uiGameOverScoreTextArray[1].text = currentScore.ToString();

        game2BackgroundAudioSource.Stop();
        game2BackgroundAudioSource.clip = gameOverAudioClip;
        game2BackgroundAudioSource.loop = false;
        game2BackgroundAudioSource.Play();

        if (isTournament)
        {
            //Store torunamentScore on Server & Smart contract
            //Storing tournament score for user
            yield return StartCoroutine(Manager.instance.webManager.StoreTournamentScoreCoroutine(Manager.instance.userInfoManager.userId,
                                                                                                  Manager.instance.tournamentDataManager.tournamentId,
                                                                                                  Manager.instance.userInfoManager.tournamentScore + currentScore));

            //yield return StartCoroutine(Manager.instance.walletManager.ExecuteRecordScore(Manager.instance.tournamentDataManager.tournamentId.ToString(), (Manager.instance.userInfoManager.tournamentScore + currentScore).ToString()));

            Manager.instance.userInfoManager.tournamentScore = Manager.instance.userInfoManager.tournamentScore + currentScore;

            //Submit Score
            yield return StartCoroutine(Manager.instance.solanaWalletManager.SubmitScoreCoroutine());

        }
        else
        {
            //Store Score on Server
            yield return StartCoroutine(Manager.instance.webManager.UpdateGameScoreCoroutine(Manager.instance.userInfoManager.userId, Manager.instance.gameDataManager.gameId, Manager.instance.userInfoManager.gameScore + currentScore));

        }

        uiGameOverPlayAgainButtonGOArray[1].SetActive(true);
        uiGameOverExitButtonGOArray[1].SetActive(true);






        game2Manager.DestroyAllHoopGameObjects();

        game2Player.transform.position = new Vector3(-1.25f, 0.35f, 0);


    }


    public void OnGame1ReplayClicked()
    {

        if (isTournament)
        {

            //Increment tournament play count
            StartCoroutine(Manager.instance.webManager.IncrementTournamentPlayCountCoroutine(Manager.instance.tournamentDataManager.tournamentId));

        }
        else
        {
            //Increment game play count
            StartCoroutine(Manager.instance.webManager.IncrementGamePlayCountCoroutine(Manager.instance.gameDataManager.gameId));
        }



        //Get user score for game
        // - Store user game score
        //yield return StartCoroutine(Manager.instance.webManager.GetOrCreateGameScoreCoroutine(Manager.instance.userInfoManager.userId, Manager.instance.gameDataManager.gameId));

        game1Player.transform.position = new Vector3(0, -2.53f, 0);
        game1CameraMovement.transform.position = game1CameraMovement.initialCameraPosition;
        game1BackgroundGO.transform.position = new Vector3(0, 0, 0);



        if (isTournament)
        {
            totalScore = Manager.instance.userInfoManager.tournamentScore;
        }
        else
        {
            totalScore = Manager.instance.userInfoManager.gameScore;
        }

        uiTitleTotalScoreTextArray[0].text = totalScore.ToString();

        currentScore = 0;
        uiOngoingScoreTextArray[0].text = currentScore.ToString();

        //Fetch total GameScore based on GameId
        //Set titleTotalScoreText

        //Reset Camera Position
        ResetGame1CameraPosition();

        uiGameOverPanelGOArray[0].SetActive(false);

        game1BackgroundAudioSource.clip = backgroundAudioClip;
        game1BackgroundAudioSource.loop = true;
        game1BackgroundAudioSource.Play();

        StartCoroutine(WaitForXSecCoroutine(2));


        game1Player.canJump = true;
        game1Manager.canSpawn = true;

        isGame1Over = false;

        game1Manager.SpawnInitialBlock();
        uiStartPanelGOArray[0].SetActive(false);
        uiOngoingPanelGOArray[0].SetActive(true);


    }


    public void OnGame2ReplayClicked()
    {

        if (isTournament)
        {

            //Increment tournament play count
            StartCoroutine(Manager.instance.webManager.IncrementTournamentPlayCountCoroutine(Manager.instance.tournamentDataManager.tournamentId));

        }
        else
        {
            //Increment game play count
            StartCoroutine(Manager.instance.webManager.IncrementGamePlayCountCoroutine(Manager.instance.gameDataManager.gameId));
        }



        //Get user score for game
        // - Store user game score
        //yield return StartCoroutine(Manager.instance.webManager.GetOrCreateGameScoreCoroutine(Manager.instance.userInfoManager.userId, Manager.instance.gameDataManager.gameId));

        game2Player.transform.position = new Vector3(-1.25f, 0.35f, 0);


        if (isTournament)
        {
            totalScore = Manager.instance.userInfoManager.tournamentScore;
        }
        else
        {
            totalScore = Manager.instance.userInfoManager.gameScore;
        }

        uiTitleTotalScoreTextArray[1].text = totalScore.ToString();

        currentScore = 0;
        uiOngoingScoreTextArray[1].text = currentScore.ToString();

        //Fetch total GameScore based on GameId
        //Set titleTotalScoreText



        uiGameOverPanelGOArray[1].SetActive(false);

        game2BackgroundAudioSource.clip = backgroundAudioClip;
        game2BackgroundAudioSource.loop = true;
        game2BackgroundAudioSource.Play();

        StartCoroutine(WaitForXSecCoroutine(2));


        game2Player.canFlap = true;
        game2Manager.canSpawn = true;
        game2Player.rb.gravityScale = 1.5f;


        isGame2Over = false;

        StartCoroutine(game2Manager.SpawnHoop());
        uiStartPanelGOArray[1].SetActive(false);
        uiOngoingPanelGOArray[1].SetActive(true);


    }


    public void ResetGame1CameraPosition()
    {
        game1CameraMovement.gameObject.transform.position = game1CameraMovement.initialCameraPosition;
        game1CameraMovement.backgroundTransform.position = game1CameraMovement.initialBackgroundTransformPosition;

        game1CameraMovement.cameraTargetPosition = game1CameraMovement.initialCameraPosition;
        game1CameraMovement.backgroundTargetPosition = game1CameraMovement.initialBackgroundTransformPosition;


        game1Manager.currentSpawnHeight = game1Manager.blockIinitialStartPosition.y;
    }

    public void OnGame1ExitClicked()
    {

        game1Player.transform.position = new Vector3(0, -2.53f, 0);
        game1CameraMovement.transform.position = game1CameraMovement.initialCameraPosition;
        game1BackgroundGO.transform.position = new Vector3(0, 0, 0);

        ResetGame1CameraPosition();


        uiStartPanelGOArray[0].SetActive(true);
        uiOngoingPanelGOArray[0].SetActive(false);
        uiGameOverPanelGOArray[0].SetActive(false);
        game1Parent.SetActive(false);


        if (isTournament)
        {
            Manager.instance.canvasManager.systemBarGO.SetActive(true);
            Manager.instance.canvasManager.homeProfileTournamentCreatePanelGO.SetActive(true);
            Manager.instance.canvasManager.tournamentPanelGO.SetActive(true);
            Manager.instance.canvasManager.tournamentPanelHomeGO.SetActive(true);
            Manager.instance.canvasManager.tournamentToggle.isOn = true;
        }
        else
        {
            Manager.instance.canvasManager.systemBarGO.SetActive(true);
            Manager.instance.canvasManager.homeProfileTournamentCreatePanelGO.SetActive(true);
            Manager.instance.canvasManager.homePanelGO.SetActive(true);
            Manager.instance.canvasManager.homePanelHomeGO.SetActive(true);
            Manager.instance.canvasManager.homeToggle.isOn = true;
        }

    }

    public void OnGame2ExitClicked()
    {

        game2Player.transform.position = new Vector3(-1.25f, 0.35f, 0);


        uiStartPanelGOArray[1].SetActive(true);
        uiOngoingPanelGOArray[1].SetActive(false);
        uiGameOverPanelGOArray[1].SetActive(false);
        game2Parent.SetActive(false);


        if (isTournament)
        {
            Manager.instance.canvasManager.systemBarGO.SetActive(true);
            Manager.instance.canvasManager.homeProfileTournamentCreatePanelGO.SetActive(true);
            Manager.instance.canvasManager.tournamentPanelGO.SetActive(true);
            Manager.instance.canvasManager.tournamentPanelHomeGO.SetActive(true);
            Manager.instance.canvasManager.tournamentToggle.isOn = true;

        }
        else
        {
            Manager.instance.canvasManager.systemBarGO.SetActive(true);
            Manager.instance.canvasManager.homeProfileTournamentCreatePanelGO.SetActive(true);
            Manager.instance.canvasManager.homePanelGO.SetActive(true);
            Manager.instance.canvasManager.homePanelHomeGO.SetActive(true);
            Manager.instance.canvasManager.homeToggle.isOn = true;

        }

    }



}
