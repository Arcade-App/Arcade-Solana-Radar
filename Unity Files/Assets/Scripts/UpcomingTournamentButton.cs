using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpcomingTournamentButton : MonoBehaviour
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
    public TMP_Text upcomingTournamentButtonTitleText;
    public TMP_Text upcomingTournamentPrizePoolText;
    public TMP_Text upcomingTournamentTimeRemainingText;
    public TMP_Text upcomingTournamentPlayingText;
    public Image upcomingTournamentImage;

    public IEnumerator PopulateTournamentData()
    {
        upcomingTournamentButtonTitleText.text = tournamentName;
        upcomingTournamentPrizePoolText.text = Manager.instance.canvasManager.TruncateToTwoDecimalPlaces(prizePool) + " SOL";

        string startDateString = startDate.ToString();
        string startTimeString = startTime.ToString();
        string endDateString = endDate.ToString();
        string endTimeString = endTime.ToString();

        upcomingTournamentTimeRemainingText.text = Manager.instance.canvasManager.GetEventStatus(startDateString, startTimeString, endDateString, endTimeString);

        upcomingTournamentPlayingText.text = userCount + " playing";

        ////Get Tournament Image
        //// - same as game template image
        //// - get game id for tournament 
        //// - get corr game template id for the game id from the userGameList
        //// - set the image from gameTemplateImageList

        yield return StartCoroutine(Manager.instance.webManager.GetGameTemplateId(gameId));
        

        //int gameIdIndex = Manager.instance.gameDataManager.userGameIdList.IndexOf(gameId);
        //int gameTemplateId = Manager.instance.gameDataManager.userGameTemplateIdList[gameIdIndex];
        upcomingTournamentImage.sprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.tournamentDataManager.gameTemplateId];
    }


    public void OnUpcomingTournamentButtonClicked()
    {
        //Do something 
    }


}
