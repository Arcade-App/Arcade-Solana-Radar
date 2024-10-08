using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [Header("Game Template List")]
    public List<string> gameTemplateNameList = new List<string>();
    public List<Sprite> gameTemplateImageNameList = new List<Sprite>();

    public List<Sprite> gameTemplateImageList = new List<Sprite>();
    public List<Color> gameTemplateColorList = new List<Color>();

    [Header("Game Data")]
    public int gameId;
    public int gameTemplateId;
    public int gameFaceId;
    public int gameBackgroundId;
    public int gameJumpAudioId;
    public int gameBGAudioId;
    public int gameGameOverAudioId;
    public string gameGameName;
    public int gamePlayCount;
    

    [Header("User Game Template Data")]
    public List<int> userGameIdList = new List<int>();
    public List<int> userGameTemplateIdList = new List<int>();
    public List<int> userGameFaceIdList = new List<int>();
    public List<int> userGameBackgroundIdList = new List<int>();
    public List<int> userGameJumpAudioIdList = new List<int>();
    public List<int> userGameBGAudioIdList = new List<int>();
    public List<int> userGameGameOverAudioIdList = new List<int>();
    public List<string> userGameNameList = new List<string>();
    public List<int> userGamePlayCountList = new List<int>();

    [Header("Top 30 Games")]
    public List<int> top30GameIdList = new List<int>();
    public List<int> top30UserIdList = new List<int>();
    public List<int> top30GameTemplateIdList = new List<int>();
    public List<int> top30GameFaceIdList = new List<int>();
    public List<int> top30GameBackgroundIdList = new List<int>();
    public List<int> top30GameJumpAudioIdList = new List<int>();
    public List<int> top30GameBGAudioIdList = new List<int>();
    public List<int> top30GameGameOverAudioIdList = new List<int>();
    public List<string> top30GameNameList = new List<string>();
    public List<int> top30GamePlayCountList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
