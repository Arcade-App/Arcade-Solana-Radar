using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToggleHandler : MonoBehaviour
{

    public Toggle selectPlayerToggle; //Assigned in this script via Start
    public Image selectPlayerToggleImage; //Assigned in prefab via Editor
    public int selectPlayerToggleIndex; //Assigned while spawning
    public UIManager uIManager; //Assigned while spawning

    // Start is called before the first frame update
    void Start()
    {
        selectPlayerToggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Used to set player sprite and disable other enabled toggles 
    public void OnPlayerToggleChange()
    {
        if (selectPlayerToggle.isOn == true)
        {
            uIManager.playerSprite.sprite = selectPlayerToggleImage.sprite;

            foreach (GameObject t in uIManager.selectPlayerToggleList)
            {
                if (t.GetComponent<PlayerToggleHandler>().selectPlayerToggleIndex != selectPlayerToggleIndex)
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
