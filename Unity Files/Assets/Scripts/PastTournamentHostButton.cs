using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PastTournamentHostButton : MonoBehaviour
{

    public string pastTournamentHostUrl;

    public void OnPastTournamentHostButtonClicked()
    {
        Application.OpenURL(pastTournamentHostUrl);
    }

}
