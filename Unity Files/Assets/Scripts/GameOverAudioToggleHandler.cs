using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverAudioToggleHandler : MonoBehaviour
{

    public Toggle selectGameOverAudioToggle; //Assigned in this script via Start
    public AudioSource selectGameOverAudioToggleAudioSource; //Assigned in this script via Start
    public TMP_Text selectGameOverAudioToggleAudioClipNameText; //Assigned in prefab via Editor
    public AudioClip selectGameOverAudioToggleAudioClip; //Assigned while spawning
    public int selectGameOverAudioToggleIndex; //Assigned while spawning
    public UIManager uIManager; //Assigned while spawning

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
            uIManager.gameOverAudioClip = selectGameOverAudioToggleAudioClip;

            selectGameOverAudioToggleAudioSource.PlayOneShot(selectGameOverAudioToggleAudioClip);

            foreach (GameObject t in uIManager.selectGameOverAudioToggleList)
            {
                if (t.GetComponent<GameOverAudioToggleHandler>().selectGameOverAudioToggleIndex != selectGameOverAudioToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.GetComponent<GameOverAudioToggleHandler>().selectGameOverAudioToggleAudioSource.Stop();
                        tempToggle.isOn = false;

                    }
                }
            }

        }

    }

}
