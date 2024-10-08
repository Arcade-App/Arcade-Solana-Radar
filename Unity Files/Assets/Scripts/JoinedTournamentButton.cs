using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinedTournamentButton : MonoBehaviour
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
    public int score;

    [Space(10)]
    public TMP_Text joinedTournamentButtonTitleText;
    public TMP_Text joinedTournamentPrizePoolText;
    public TMP_Text joinedTournamentTimeRemainingText;
    public TMP_Text joinedTournamentPlayingText;
    public Image joinedTournamentImage;
    public Image joinedTournamentButtonImage;


    public string endingIn;
    public Sprite joinedTournamentSprite;
    public Color joinedTournamentColor;


    public IEnumerator PopulateTournamentData()
    {
        joinedTournamentButtonTitleText.text = tournamentName;
        joinedTournamentPrizePoolText.text = Manager.instance.canvasManager.TruncateToTwoDecimalPlaces(prizePool) + " SOL";

        string startDateString = startDate.ToString();
        string startTimeString = startTime.ToString();
        string endDateString = endDate.ToString();
        string endTimeString = endTime.ToString();

        endingIn = Manager.instance.canvasManager.GetEventStatus(startDateString, startTimeString, endDateString, endTimeString);
        
        joinedTournamentTimeRemainingText.text = endingIn;


        joinedTournamentPlayingText.text = userCount + " playing";

        ////Get Tournament Image
        //// - same as game template image
        //// - get game id for tournament 
        //// - get corr game template id for the game id from the userGameList
        //// - set the image from gameTemplateImageList


        yield return StartCoroutine(Manager.instance.webManager.GetGameTemplateId(gameId));
        

        //int gameIdIndex = Manager.instance.gameDataManager.userGameIdList.IndexOf(gameId);
        //int gameTemplateId = Manager.instance.gameDataManager.userGameTemplateIdList[gameIdIndex];
        joinedTournamentSprite = Manager.instance.gameDataManager.gameTemplateImageList[Manager.instance.tournamentDataManager.gameTemplateId];
        joinedTournamentImage.sprite = joinedTournamentSprite;

        joinedTournamentColor = Manager.instance.gameDataManager.gameTemplateColorList[Manager.instance.tournamentDataManager.gameTemplateId];
        joinedTournamentButtonImage.color = joinedTournamentColor;
    }


    public void OnJoinedTournamentButtonClicked()
    {

        //Do something 
        StartCoroutine(Manager.instance.canvasManager.OnJoinedTournamentCanvasButtonClicked(tournamentId, gameId, tournamentName, tournamentHostName, socialLink,
                                            playerJoiningFee, startDate, startTime, endDate, endTime, prizePool,
                                            status, playCount, userCount, winnerId, runnerUpId, secondRunnerUpId,
                                            joinedTournamentSprite, joinedTournamentColor, endingIn));


    }


}
