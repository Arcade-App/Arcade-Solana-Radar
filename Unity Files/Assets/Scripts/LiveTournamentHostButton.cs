using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveTournamentHostButton : MonoBehaviour
{

    public string liveTournamentHostUrl;

    public void OnLiveTournamentHostButtonClicked()
    {
        Application.OpenURL(liveTournamentHostUrl);
    }

}
