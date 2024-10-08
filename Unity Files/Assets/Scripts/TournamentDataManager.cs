using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentDataManager : MonoBehaviour
{

    [Header("Tournament Manager Data")]
    public string tournamentManagerAddress;
    public string tournamentContractName;

    [Header("TournamentGameTemplateId")]
    public int gameTemplateId;

    [Header("Tournament Data")]
    public int tournamentId;
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

    [Header("User Tournaments Data")]
    public List<int> userTournamentIdList = new List<int>();
    public List<int> userGameIdList = new List<int>();
    public List<string> userTournamentNameList = new List<string>();
    public List<string> userTournamentHostNameList = new List<string>();
    public List<string> userSocialLinkList = new List<string>();
    public List<float> userPlayerJoiningFeeList = new List<float>();
    public List<int> userStartDateList = new List<int>();
    public List<int> userStartTimeList = new List<int>();
    public List<int> userEndDateList = new List<int>();
    public List<int> userEndTimeList = new List<int>();
    public List<float> userPrizePoolList = new List<float>();
    public List<int> userStatusList = new List<int>();
    public List<int> userPlayCountList = new List<int>();
    public List<int> userUserCountList = new List<int>();
    public List<float> userWinnerIdList = new List<float>();
    public List<float> userRunnerUpIdList = new List<float>();
    public List<float> userSecondRunnerUpIdList = new List<float>();

    [Header("All Tournaments Data")]
    public List<int> allTournamentIdList = new List<int>();
    public List<int> allUserIdList = new List<int>();
    public List<int> allGameIdList = new List<int>();
    public List<string> allTournamentNameList = new List<string>();
    public List<string> allTournamentHostNameList = new List<string>();
    public List<string> allSocialLinkList = new List<string>();
    public List<float> allPlayerJoiningFeeList = new List<float>();
    public List<int> allStartDateList = new List<int>();
    public List<int> allStartTimeList = new List<int>();
    public List<int> allEndDateList = new List<int>();
    public List<int> allEndTimeList = new List<int>();
    public List<float> allPrizePoolList = new List<float>();
    public List<int> allStatusList = new List<int>();
    public List<int> allPlayCountList = new List<int>();
    public List<int> allUserCountList = new List<int>();
    public List<float> allWinnerIdList = new List<float>();
    public List<float> allRunnerUpIdList = new List<float>();
    public List<float> allSecondRunnerUpIdList = new List<float>();

    [Header("Past Tournaments Data")]
    public List<int> pastTournamentIdList = new List<int>();
    public List<int> pastUserIdList = new List<int>();
    public List<int> pastGameIdList = new List<int>();
    public List<string> pastTournamentNameList = new List<string>();
    public List<string> pastTournamentHostNameList = new List<string>();
    public List<string> pastSocialLinkList = new List<string>();
    public List<float> pastPlayerJoiningFeeList = new List<float>();
    public List<int> pastStartDateList = new List<int>();
    public List<int> pastStartTimeList = new List<int>();
    public List<int> pastEndDateList = new List<int>();
    public List<int> pastEndTimeList = new List<int>();
    public List<float> pastPrizePoolList = new List<float>();
    public List<int> pastStatusList = new List<int>();
    public List<int> pastPlayCountList = new List<int>();
    public List<int> pastUserCountList = new List<int>();
    public List<float> pastWinnerIdList = new List<float>();
    public List<float> pastRunnerUpIdList = new List<float>();
    public List<float> pastSecondRunnerUpIdList = new List<float>();

    [Header("Upcoming Tournaments Data")]
    public List<int> upcomingTournamentIdList = new List<int>();
    public List<int> upcomingUserIdList = new List<int>();
    public List<int> upcomingGameIdList = new List<int>();
    public List<string> upcomingTournamentNameList = new List<string>();
    public List<string> upcomingTournamentHostNameList = new List<string>();
    public List<string> upcomingSocialLinkList = new List<string>();
    public List<float> upcomingPlayerJoiningFeeList = new List<float>();
    public List<int> upcomingStartDateList = new List<int>();
    public List<int> upcomingStartTimeList = new List<int>();
    public List<int> upcomingEndDateList = new List<int>();
    public List<int> upcomingEndTimeList = new List<int>();
    public List<float> upcomingPrizePoolList = new List<float>();
    public List<int> upcomingStatusList = new List<int>();
    public List<int> upcomingPlayCountList = new List<int>();
    public List<int> upcomingUserCountList = new List<int>();
    public List<float> upcomingWinnerIdList = new List<float>();
    public List<float> upcomingRunnerUpIdList = new List<float>();
    public List<float> upcomingSecondRunnerUpIdList = new List<float>();

    [Header("Live Tournaments Data")]
    public List<int> liveTournamentIdList = new List<int>();
    public List<int> liveUserIdList = new List<int>();
    public List<int> liveGameIdList = new List<int>();
    public List<string> liveTournamentNameList = new List<string>();
    public List<string> liveTournamentHostNameList = new List<string>();
    public List<string> liveSocialLinkList = new List<string>();
    public List<float> livePlayerJoiningFeeList = new List<float>();
    public List<int> liveStartDateList = new List<int>();
    public List<int> liveStartTimeList = new List<int>();
    public List<int> liveEndDateList = new List<int>();
    public List<int> liveEndTimeList = new List<int>();
    public List<float> livePrizePoolList = new List<float>();
    public List<int> liveStatusList = new List<int>();
    public List<int> livePlayCountList = new List<int>();
    public List<int> liveUserCountList = new List<int>();
    public List<float> liveWinnerIdList = new List<float>();
    public List<float> liveRunnerUpIdList = new List<float>();
    public List<float> liveSecondRunnerUpIdList = new List<float>();

    [Header("Joined Tournaments Data")]
    public List<int> joinedTournamentIdList = new List<int>();
    public List<int> joinedGameIdList = new List<int>();
    public List<string> joinedTournamentNameList = new List<string>();
    public List<string> joinedTournamentHostNameList = new List<string>();
    public List<string> joinedSocialLinkList = new List<string>();
    public List<float> joinedPlayerJoiningFeeList = new List<float>();
    public List<int> joinedStartDateList = new List<int>();
    public List<int> joinedStartTimeList = new List<int>();
    public List<int> joinedEndDateList = new List<int>();
    public List<int> joinedEndTimeList = new List<int>();
    public List<float> joinedPrizePoolList = new List<float>();
    public List<int> joinedStatusList = new List<int>();
    public List<int> joinedPlayCountList = new List<int>();
    public List<int> joinedUserCountList = new List<int>();
    public List<float> joinedWinnerIdList = new List<float>();
    public List<float> joinedRunnerUpIdList = new List<float>();
    public List<float> joinedSecondRunnerUpIdList = new List<float>();
    public List<int> joinedScoreList = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
