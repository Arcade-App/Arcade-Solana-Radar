using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveTournamentButton : MonoBehaviour
{

    [Header("Tournament Data")]
    public int tournamentId;
    public int userId;
    public int gameId;
    public string tournamentName;
    public string tournamentHostName;
    public string socialLink;
    public float playerJoiningFee;
    public int startDate;
    public int startTime;
    public int endDate;
    public int endTime;
    public float prizePool;
    public int status;
    public int playCount;
    public int userCount;
    public float winnerId;
    public float runnerUpId;
    public float secondRunnerUpId;

    [Space(10)]
    public TMP_Text liveTournamentButtonTitleText;
    public TMP_Text liveTournamentPrizePoolText;
    public TMP_Text liveTournamentTimeRemainingText;
    public TMP_Text liveTournamentPlayingText;
    public Image liveTournamentImage;
    public Image liveTournamentButtonImage;

    public string endingIn;
    public Sprite liveTournamentSprite;
    public Color liveTournamentColor;

    public IEnumerator PopulateTournamentData()
    {
        liveTournamentButtonTitleText.text = tournamentName;
        liveTournamentPrizePoolText.text = Manager.instance.canvasManager.TruncateToTwoDecimalPlaces(prizePool) + " SOL";

        string startDateString = startDate.ToString();
        string startTimeString = startTime.ToString();
        string endDateString = endDate.ToString();
        string endTimeString = endTime.ToString();

        endingIn = Manager.instance.canvasManager.GetEventStatus(startDateString, startTimeString, endDateString, endTimeString);

        liveTournamentTimeRemainingText.text = endingIn;

        liveTournamentPlayingText.text = userCount + " playing";

        ////Get Tournament Image
        //// - same as game template image
        //// - get game id for tournament 
        //// - get corr game template id for the game id from the userGameList
        //// - set the image from gameTemplateImageList

        yield return StartCoroutine(Manager.instance.webManager.GetGameTemplateId(gameId));
        


        //int gameIdIndex = Manager.instance.gameDataManager.userGameIdList.IndexOf(gameId);
        //int gameTemplateId = Manager.instance.gameDataManager.userGameTemplateIdList[gameIdIndex];
        liveTournamentSprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.tournamentDataManager.gameTemplateId];
        liveTournamentImage.sprite = liveTournamentSprite;

        liveTournamentColor = Manager.instance.gameDataManager.gameTemplateColorList[Manager.instance.tournamentDataManager.gameTemplateId];
        liveTournamentButtonImage.color = liveTournamentColor;
    }


    public void OnLiveTournamentButtonClicked()
    {

        

        //Do something 
        StartCoroutine(Manager.instance.canvasManager.OnLiveTournamentCanvasButtonClicked(tournamentId, gameId, tournamentName, tournamentHostName, socialLink,
                                            playerJoiningFee, startDate, startTime, endDate, endTime, prizePool,
                                            status, playCount, userCount, winnerId, runnerUpId, secondRunnerUpId,
                                            liveTournamentSprite, liveTournamentColor, endingIn));

       


    }


}
