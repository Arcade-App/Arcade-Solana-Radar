using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{


    [Header("User Info")]
    public int userId;
    public string username;
    public string email;
    public string password;
    public string walletMnemonics;
    public string walletAddress;
    //Balance in Lamports
    public int walletBalanceInt;
    //Simplifid Balance
    public float walletBalanceFloat;

    public int tournamentsJoined = 0;

    public int tournamentScore = 0;
    public int gameScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetUserData()
    {
        userId = -1;
        username = null;
        email = null;
        password = null;
        walletMnemonics = null;
        walletAddress = null;
        walletBalanceInt = -1;
        walletBalanceFloat = -1;

        tournamentsJoined = -1;

        tournamentScore = 0;

        gameScore = 0;
}
 
}
