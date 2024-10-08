using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTop3GameButton : MonoBehaviour
{


    [Header("Game Data")]
    public int gameId;
    public int userId;
    public int gameTemplateId;
    public int gameFaceId;
    public int gameBackgroundId;
    public int gameJumpAudioId;
    public int gameBGAudioId;
    public int gameGameOverAudioId;
    public string gameGameName;
    public int gamePlayCount;


    public void OnChooseTop3GameButtonClicked()
    {

        StartCoroutine(OnChooseTop3GameButtonClickedCoroutine());


    }


    public IEnumerator OnChooseTop3GameButtonClickedCoroutine()
    {


        Manager.instance.gameDataManager.gameId = gameId;
        Manager.instance.gameDataManager.gameTemplateId = gameTemplateId;
        Manager.instance.gameDataManager.gameFaceId = gameFaceId;
        Manager.instance.gameDataManager.gameBackgroundId = gameBackgroundId;
        Manager.instance.gameDataManager.gameJumpAudioId = gameJumpAudioId;
        Manager.instance.gameDataManager.gameBGAudioId = gameBGAudioId;
        Manager.instance.gameDataManager.gameGameOverAudioId = gameGameOverAudioId;
        Manager.instance.gameDataManager.gameGameName = gameGameName;
        Manager.instance.gameDataManager.gamePlayCount = gamePlayCount;

        //Get user score for game
        // - Store user game score
        yield return StartCoroutine(Manager.instance.webManager.GetOrCreateGameScoreCoroutine(Manager.instance.userInfoManager.userId, gameId));



        Manager.instance.gameManager.OnCanvasGameSelectedPlayClicked(false);

        //Disable CanvasRegisterSign-in Panel 
        //Disable HomeProfileTournamentreate Panel 
        Manager.instance.canvasManager.homePanelGO.SetActive(false);
        Manager.instance.canvasManager.homeProfileTournamentCreatePanelGO.SetActive(false);
        Manager.instance.canvasManager.systemBarGO.SetActive(false);
    }


}
