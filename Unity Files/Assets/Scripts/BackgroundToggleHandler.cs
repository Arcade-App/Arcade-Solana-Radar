using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundToggleHandler : MonoBehaviour
{

    public Toggle selectBackgroundToggle; //Assigned in this script via Start
    public Image selectBackgroundToggleImage; //Assigned in prefab via Editor
    public int selectBackgroundToggleIndex; //Assigned while spawning
    public UIManager uIManager; //Assigned while spawning

    // Start is called before the first frame update
    void Start()
    {
        selectBackgroundToggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to set player sprite and disable other enabled toggles 
    public void OnBackgroundToggleChange()
    {
        if (selectBackgroundToggle.isOn == true)
        {
            uIManager.backgroundSprite.sprite = selectBackgroundToggleImage.sprite;
            
            foreach (GameObject t in uIManager.selectBackgroundToggleList)
            {
                if (t.GetComponent<BackgroundToggleHandler>().selectBackgroundToggleIndex != selectBackgroundToggleIndex)
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
