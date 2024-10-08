using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoinedTournamentHostButton : MonoBehaviour
{
    public string joinedTournamentHostUrl;

    public void OnJoinedTournamentHostButtonClicked()
    {
        Application.OpenURL(joinedTournamentHostUrl);
    }
}
