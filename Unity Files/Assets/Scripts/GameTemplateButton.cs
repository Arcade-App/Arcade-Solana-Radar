using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTemplateButton : MonoBehaviour
{

    public int chooseGameTemplateId;


    public void OnChooseGameTemplateButtonClicked()
    {
        Manager.instance.gameDataManager.gameTemplateId = chooseGameTemplateId;
        Manager.instance.canvasManager.OnChooseGameTemplateButtonClicked();
    }
}
