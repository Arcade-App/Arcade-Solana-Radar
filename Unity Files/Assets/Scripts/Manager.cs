using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public static Manager instance;

    public WebManager webManager;
    public UserInfoManager userInfoManager;
    public CanvasManager canvasManager;
    public WalletManager walletManager;
    public SolanaWalletManager solanaWalletManager;
    public GameDataManager gameDataManager;
    public TournamentDataManager tournamentDataManager;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        webManager = GetComponent<WebManager>();
        userInfoManager = GetComponent<UserInfoManager>();
        walletManager = GetComponent<WalletManager>();
        gameDataManager = GetComponent<GameDataManager>();
        tournamentDataManager = GetComponent<TournamentDataManager>();
        gameManager = GetComponent<GameManager>();
        solanaWalletManager = GetComponent<SolanaWalletManager>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
    }




}
