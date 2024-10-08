using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTournamentGameButton : MonoBehaviour
{

    public int chooseTournamentGameId;



    public void OnChooseTournamentGameButtonClicked()
    {
        Manager.instance.tournamentDataManager.gameId = chooseTournamentGameId;
        Manager.instance.canvasManager.OnChooseTournamentGameTemplateButtonClicked();
    }


}
