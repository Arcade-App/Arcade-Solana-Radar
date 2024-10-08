using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectGameOverAudioToggleHandler : MonoBehaviour
{

    public int selectGameOverAudioToggleId;


    public Toggle selectGameOverAudioToggle; //Assigned in this script via Start
    public AudioSource selectGameOverAudioToggleAudioSource; //Assigned in this script via Start
    public TMP_Text selectGameOverAudioToggleAudioClipNameText; //Assigned in prefab via Editor
    public AudioClip selectGameOverAudioToggleAudioClip; //Assigned while spawning
    public int selectGameOverAudioToggleIndex; //Assigned while spawning
    public CanvasManager canvasManager; //Assigned while spawning   



    // Start is called before the first frame update
    void Start()
    {
        selectGameOverAudioToggle = GetComponent<Toggle>();
        selectGameOverAudioToggleAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGameOverAudioToggleChange()
    {
        if (selectGameOverAudioToggle.isOn == true)
        {
            //uIManager.player.playerJumpAudioClip = selectJumpAudioToggleAudioClip;

            Manager.instance.gameDataManager.gameGameOverAudioId = selectGameOverAudioToggleId;
            canvasManager.selectGameOverAudioNextButton.interactable = true;
            canvasManager.selectGameOverAudioNextButtonText.color = canvasManager.inputFieldFilledButtonTextColor;

            selectGameOverAudioToggleAudioSource.PlayOneShot(selectGameOverAudioToggleAudioClip);

            foreach (GameObject t in canvasManager.selectGameOverAudioToggleList)
            {
                if (t.GetComponent<SelectGameOverAudioToggleHandler>().selectGameOverAudioToggleIndex != selectGameOverAudioToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.GetComponent<SelectGameOverAudioToggleHandler>().selectGameOverAudioToggleAudioSource.Stop();
                        tempToggle.isOn = false;
                    }
                }
            }

        }

    }


}
