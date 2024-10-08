using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class JoinedTournamentLeaderbaordEntry : MonoBehaviour
{


    public TMP_Text usernameText;
    public TMP_Text scoreText;


    public void SetLeaverboardEntryData(string username, int score)
    {
        usernameText.text = username;
        scoreText.text = score.ToString();
    }

}
