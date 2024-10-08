using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectBGAudioToggleHandler : MonoBehaviour
{

    public int selectBGAudioToggleId;


    public Toggle selectBGAudioToggle; //Assigned in this script via Start
    public AudioSource selectBGAudioToggleAudioSource; //Assigned in this script via Start
    public TMP_Text selectBGAudioToggleAudioClipNameText; //Assigned in prefab via Editor
    public AudioClip selectBGAudioToggleAudioClip; //Assigned while spawning
    public int selectBGAudioToggleIndex; //Assigned while spawning
    public CanvasManager canvasManager; //Assigned while spawning   



    // Start is called before the first frame update
    void Start()
    {
        selectBGAudioToggle = GetComponent<Toggle>();
        selectBGAudioToggleAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBGAudioToggleChange()
    {
        if (selectBGAudioToggle.isOn == true)
        {
            //uIManager.player.playerJumpAudioClip = selectJumpAudioToggleAudioClip;

            Manager.instance.gameDataManager.gameBGAudioId = selectBGAudioToggleId;
            canvasManager.selectBGAudioNextButton.interactable = true;
            canvasManager.selectBGAudioNextButtonText.color = canvasManager.inputFieldFilledButtonTextColor;


            selectBGAudioToggleAudioSource.PlayOneShot(selectBGAudioToggleAudioClip);

            foreach (GameObject t in canvasManager.selectBGAudioToggleList)
            {
                if (t.GetComponent<SelectBGAudioToggleHandler>().selectBGAudioToggleIndex != selectBGAudioToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.GetComponent<SelectBGAudioToggleHandler>().selectBGAudioToggleAudioSource.Stop();
                        tempToggle.isOn = false;
                    }
                }
            }

        }

    }

}
