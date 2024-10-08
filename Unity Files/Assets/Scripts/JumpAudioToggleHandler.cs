using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpAudioToggleHandler : MonoBehaviour
{

    public Toggle selectJumpAudioToggle; //Assigned in this script via Start
    public AudioSource selectJumpAudioToggleAudioSource; //Assigned in this script via Start
    public TMP_Text selectJumpAudioToggleAudioClipNameText; //Assigned in prefab via Editor
    public AudioClip selectJumpAudioToggleAudioClip; //Assigned while spawning
    public int selectJumpAudioToggleIndex; //Assigned while spawning
    public UIManager uIManager; //Assigned while spawning


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
            uIManager.player.player1JumpAudioClip = selectJumpAudioToggleAudioClip;

            selectJumpAudioToggleAudioSource.PlayOneShot(selectJumpAudioToggleAudioClip);

            foreach (GameObject t in uIManager.selectJumpAudioToggleList)
            {
                if (t.GetComponent<JumpAudioToggleHandler>().selectJumpAudioToggleIndex != selectJumpAudioToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.GetComponent<JumpAudioToggleHandler>().selectJumpAudioToggleAudioSource.Stop();
                        tempToggle.isOn = false;

                    }
                }
            }

        }

    }

}
