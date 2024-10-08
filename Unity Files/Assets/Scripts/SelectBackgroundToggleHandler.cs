using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBackgroundToggleHandler : MonoBehaviour
{

    public int selectBackgroundToggleId;

    public Toggle selectBackgroundToggle; //Assigned in this script via Start
    public Image selectBackgroundToggleImage; //Assigned in prefab via Editor
    public int selectBackgroundToggleIndex; //Assigned while spawning
    public CanvasManager canvasManager; //Assigned while spawning 

    // Start is called before the first frame update
    void Start()
    {
        selectBackgroundToggle = GetComponent<Toggle>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectBackgroundToggleChange()
    {
        if (selectBackgroundToggle.isOn == true)
        {
            //uIManager.playerSprite.sprite = selectPlayerToggleImage.sprite;

            Manager.instance.gameDataManager.gameBackgroundId = selectBackgroundToggleId;
            canvasManager.selectBackgroundNextButton.interactable = true;
            canvasManager.selectBackgroundNextButtonText.color = canvasManager.inputFieldFilledButtonTextColor;

            foreach (GameObject t in canvasManager.selectBackgroundToggleList)
            {
                if (t.GetComponent<SelectBackgroundToggleHandler>().selectBackgroundToggleIndex != selectBackgroundToggleIndex)
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
