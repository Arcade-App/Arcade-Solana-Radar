using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectFaceToggleHandler : MonoBehaviour
{

    public int selectFaceToggleId;

    public Toggle selectFaceToggle; //Assigned in this script via Start
    public Image selectFaceToggleImage; //Assigned in prefab via Editor
    public int selectFaceToggleIndex; //Assigned while spawning
    public CanvasManager canvasManager; //Assigned while spawning   

    // Start is called before the first frame update
    void Start()
    {
        selectFaceToggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectFaceToggleChange()
    {
        if (selectFaceToggle.isOn == true)
        {
            //uIManager.playerSprite.sprite = selectPlayerToggleImage.sprite;

            Manager.instance.gameDataManager.gameFaceId = selectFaceToggleId;
            canvasManager.selectFaceNextButton.interactable = true;
            canvasManager.selectFaceNextButtonText.color = canvasManager.inputFieldFilledButtonTextColor;

            foreach (GameObject t in canvasManager.selectFaceToggleList)
            {
                if (t.GetComponent<SelectFaceToggleHandler>().selectFaceToggleIndex != selectFaceToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.isOn = false;
                    }
                }
            }

        }

    }
}
