using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundAudioToggleHandler : MonoBehaviour
{

    public Toggle selectBackgroundAudioToggle; //Assigned in this script via Start
    public AudioSource selectBackgroundAudioToggleAudioSource; //Assigned in this script via Start
    public TMP_Text selectBackgroundAudioToggleAudioClipNameText; //Assigned in prefab via Editor
    public AudioClip selectBackgroundAudioToggleAudioClip; //Assigned while spawning
    public int selectBackgroundAudioToggleIndex; //Assigned while spawning
    public UIManager uIManager; //Assigned while spawning

    // Start is called before the first frame update
    void Start()
    {
        selectBackgroundAudioToggle = GetComponent<Toggle>();
        selectBackgroundAudioToggleAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackgroundAudioToggleChange()
    {
        if (selectBackgroundAudioToggle.isOn == true)
        {
            uIManager.backgroundAudioClip = selectBackgroundAudioToggleAudioClip;

            selectBackgroundAudioToggleAudioSource.PlayOneShot(selectBackgroundAudioToggleAudioClip);

            foreach (GameObject t in uIManager.selectBackgroundAudioToggleList)
            {
                if (t.GetComponent<BackgroundAudioToggleHandler>().selectBackgroundAudioToggleIndex != selectBackgroundAudioToggleIndex)
                {
                    Toggle tempToggle = t.GetComponent<Toggle>();
                    if (tempToggle.isOn)
                    {
                        tempToggle.GetComponent<BackgroundAudioToggleHandler>().selectBackgroundAudioToggleAudioSource.Stop();
                        tempToggle.isOn = false;

                    }
                }
            }

        }

    }
}
