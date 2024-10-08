using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login1 : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(() => 
        {
            StartCoroutine(Main.instance.web.Login(usernameInput.text, passwordInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
