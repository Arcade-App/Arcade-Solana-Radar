using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectJumpAudioToggleHandler : MonoBehaviour
{

    public int selectJumpAudioToggleId;


    public Toggle selectJumpAudioToggle; //Assigned in this script via Start
    public AudioSource selectJumpAudioToggleAudioSource; //Assigned in this script via Start
    public TMP_Text selectJumpAudioToggleAudioClipNameText; //Assigned in prefab via Editor
    public AudioClip selectJumpAudioToggleAudioClip; //Assigned while spawning
    public int selectJumpAudioToggleIndex; //Assigned while spawning
    public CanvasManager canvasManager; //Assigned while spawning   



    // Start is called before the first frame update
    void Start()
    {
        selectJumpAudioToggle = GetComponent<Toggle>();
        selectJumpAudioToggleAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnJumpAudioToggleChange()
    {
        if (selectJumpAudioToggle.isOn == true)
        {
            //uIManager.player.playerJumpAudioClip = selectJumpAudioToggleAudioClip;

            Manager.instance.gameDataManager.gameJumpAudioId = selectJumpAudioToggleId;
            canvasManager.selectJumpAudioNextButton.interactable = true;
            canvasManager.selectJumpAudioNextButtonText.color = canvasManager.inputFieldFilledButtonTextColor;

            selectJumpAudioToggleAudioSource.PlayOneShot(selectJumpAudioToggleAudioClip);

            foreach (GameObject t in canvasManager.selectJumpAudioToggleList)
            {
                if (t.GetComponent<SelectJumpAudioToggleHandler>().selectJumpAudioToggleIndex != selectJumpAudioToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.GetComponent<SelectJumpAudioToggleHandler>().selectJumpAudioToggleAudioSource.Stop();
                        tempToggle.isOn = false;
                    }
                }
            }

        }

    }
}
