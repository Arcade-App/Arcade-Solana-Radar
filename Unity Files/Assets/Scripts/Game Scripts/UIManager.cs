using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject playBackgroundPanelGO;
    public Button playButton;

    public GameObject gameOverBackgroundPanelGO;
    public Button restartButton;

    public TMP_Text scoreText;
    public int currentScore = 0;

    public Game1Manager gameManager;
    public Game1Player player;

    [Space(10)]
    [Header("Customization Panels References")]
    public GameObject customizationPanelGO;
    public GameObject selectPlayerGO;
    public GameObject selectBackgroundGO;
    public GameObject selectJumpAudioGO;
    public GameObject selectBackgroundAudioGO;
    public GameObject selectGameOverAudioGO;

    [Space(10)]
    [Header("Player Sprite Selection Toggle")]
    //Spawning Player Sprite Toggles
    public SpriteRenderer playerSprite;
    public Sprite[] playerSpritesArray;
    public GameObject selectPlayerTogglePrefab;
    public GameObject selectPlayerToggleParentGO;
    public List<GameObject> selectPlayerToggleList = new List<GameObject>();

    [Space(10)]
    [Header("Background Sprite Selection Toggle")]
    //Spawning Background Sprite Toggles
    public SpriteRenderer backgroundSprite;
    public Sprite[] backgroundSpritesArray;
    public GameObject selectBackgroundTogglePrefab;
    public GameObject selectBackgroundToggleParentGO;
    public List<GameObject> selectBackgroundToggleList = new List<GameObject>();

    [Space(10)]
    [Header("Jump Audio Selection Toggle")]
    //Spawning Jump Audio Toggles
    public AudioSource playerJumpAudioSource;
    public AudioClip[] playerJumpAudioClipArray;
    public GameObject selectJumpAudioTogglePrefab;
    public GameObject selectJumpAudioToggleParentGO;
    public List<GameObject> selectJumpAudioToggleList = new List<GameObject>();

    [Space(10)]
    [Header("Background Audio Selection Toggle")]
    //Spawning Background Audio Toggles
    public AudioSource backgroundAudioSource;
    public AudioClip backgroundAudioClip;
    public AudioClip[] backgroundAudioClipArray;
    public GameObject selectBackgroundAudioTogglePrefab;
    public GameObject selectBackgroundAudioToggleParentGO;
    public List<GameObject> selectBackgroundAudioToggleList = new List<GameObject>();

    [Space(10)]
    [Header("Game Over Audio Selection Toggle")]
    //Spawning Game Over Audio Toggles
    public AudioSource gameOverAudioSource;
    public AudioClip gameOverAudioClip;
    public AudioClip[] gameOverAudioClipArray;
    public GameObject selectGameOverAudioTogglePrefab;
    public GameObject selectGameOverAudioToggleParentGO;
    public List<GameObject> selectGameOverAudioToggleList = new List<GameObject>();


    public Game1CameraMovement game1CameraMovement;


    // Start is called before the first frame update
    void Start()
    {
        IncreaseScore(0);

        customizationPanelGO.SetActive(true);
        selectPlayerGO.SetActive(true);
        selectBackgroundGO.SetActive(false);
        selectJumpAudioGO.SetActive(false);
        selectBackgroundAudioGO.SetActive(false);
        selectGameOverAudioGO.SetActive(false);
        playBackgroundPanelGO.SetActive(false);
        gameOverBackgroundPanelGO.SetActive(false);

        SpawnPlayerToggles();
        //SpawnBackgroundToggles();
        //SpawnJumpAudioToggles();
        //SpawnBackgroundAudioToggles();
        //SpawnGameOverAudioToggles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectPlayerNextButtonClicked()
    {
        selectPlayerGO.SetActive(false);
        selectBackgroundGO.SetActive(true);
        selectJumpAudioGO.SetActive(false);
        selectBackgroundAudioGO.SetActive(false);
        selectGameOverAudioGO.SetActive(false);
        playBackgroundPanelGO.SetActive(false);
        gameOverBackgroundPanelGO.SetActive(false);

        //SpawnPlayerToggles();
        SpawnBackgroundToggles();
        //SpawnJumpAudioToggles();
        //SpawnBackgroundAudioToggles();
        //SpawnGameOverAudioToggles();
    }

    public void OnSelectBackgroundNextButtonClicked()
    {
        selectPlayerGO.SetActive(false);
        selectBackgroundGO.SetActive(false);
        selectJumpAudioGO.SetActive(true);
        selectBackgroundAudioGO.SetActive(false);
        selectGameOverAudioGO.SetActive(false);
        playBackgroundPanelGO.SetActive(false);
        gameOverBackgroundPanelGO.SetActive(false);

        //SpawnPlayerToggles();
        //SpawnBackgroundToggles();
        SpawnJumpAudioToggles();
        //SpawnBackgroundAudioToggles();
        //SpawnGameOverAudioToggles();
    }

    public void OnSelectJumpAudioNextButtonClicked()
    {
        selectPlayerGO.SetActive(false);
        selectBackgroundGO.SetActive(false);
        selectJumpAudioGO.SetActive(false);
        selectBackgroundAudioGO.SetActive(true);
        selectGameOverAudioGO.SetActive(false);
        playBackgroundPanelGO.SetActive(false);
        gameOverBackgroundPanelGO.SetActive(false);

        //SpawnPlayerToggles();
        //SpawnBackgroundToggles();
        //SpawnJumpAudioToggles();
        SpawnBackgroundAudioToggles();
        //SpawnGameOverAudioToggles();
    }

    public void OnSelectBackgroundAudioNextButtonClicked()
    {
        selectPlayerGO.SetActive(false);
        selectBackgroundGO.SetActive(false);
        selectJumpAudioGO.SetActive(false);
        selectBackgroundAudioGO.SetActive(false);
        selectGameOverAudioGO.SetActive(true);
        playBackgroundPanelGO.SetActive(false);
        gameOverBackgroundPanelGO.SetActive(false);

        //SpawnPlayerToggles();
        //SpawnBackgroundToggles();
        //SpawnJumpAudioToggles();
        //SpawnBackgroundAudioToggles();
        SpawnGameOverAudioToggles();
    }

    public void OnSelectGameOverAudioNextButtonClicked()
    {
        selectPlayerGO.SetActive(false);
        selectBackgroundGO.SetActive(false);
        selectJumpAudioGO.SetActive(false);
        selectBackgroundAudioGO.SetActive(false);
        selectGameOverAudioGO.SetActive(false);
        playBackgroundPanelGO.SetActive(true);
        gameOverBackgroundPanelGO.SetActive(false);

        //SpawnPlayerToggles();
        //SpawnBackgroundToggles();
        //SpawnJumpAudioToggles();
        //SpawnBackgroundAudioToggles();
        //SpawnGameOverAudioToggles();

        customizationPanelGO.SetActive(false);
        playBackgroundPanelGO.SetActive(true);


}

    public void SpawnPlayerToggles()
    {

        foreach (Transform child in selectPlayerToggleParentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerSpritesArray.Length; i++)
        {
            GameObject playerSpriteToggleGO = Instantiate(selectPlayerTogglePrefab, selectPlayerToggleParentGO.transform);
            playerSpriteToggleGO.GetComponent<PlayerToggleHandler>().selectPlayerToggleImage.sprite = playerSpritesArray[i];
            playerSpriteToggleGO.GetComponent<PlayerToggleHandler>().uIManager = gameObject.GetComponent<UIManager>();
            playerSpriteToggleGO.GetComponent<PlayerToggleHandler>().selectPlayerToggleIndex = i;
            selectPlayerToggleList.Add(playerSpriteToggleGO);
        }
 
    }

    public void SpawnBackgroundToggles()
    {

        foreach (Transform child in selectBackgroundToggleParentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        for (int i = 0; i < backgroundSpritesArray.Length; i++)
        {
            GameObject backgroundSpriteToggleGO = Instantiate(selectBackgroundTogglePrefab, selectBackgroundToggleParentGO.transform);
            backgroundSpriteToggleGO.GetComponent<BackgroundToggleHandler>().selectBackgroundToggleImage.sprite = backgroundSpritesArray[i];
            backgroundSpriteToggleGO.GetComponent<BackgroundToggleHandler>().uIManager = gameObject.GetComponent<UIManager>();
            backgroundSpriteToggleGO.GetComponent<BackgroundToggleHandler>().selectBackgroundToggleIndex = i;
            selectBackgroundToggleList.Add(backgroundSpriteToggleGO);
        }

    }

    public void SpawnJumpAudioToggles()
    {

        foreach (Transform child in selectJumpAudioToggleParentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerJumpAudioClipArray.Length; i++)
        {
            GameObject playerJumpAudioToggleGO = Instantiate(selectJumpAudioTogglePrefab, selectJumpAudioToggleParentGO.transform);
            playerJumpAudioToggleGO.GetComponent<JumpAudioToggleHandler>().selectJumpAudioToggleAudioClip = playerJumpAudioClipArray[i];
            playerJumpAudioToggleGO.GetComponent<JumpAudioToggleHandler>().selectJumpAudioToggleAudioClipNameText.text = playerJumpAudioClipArray[i].name;
            playerJumpAudioToggleGO.GetComponent<JumpAudioToggleHandler>().uIManager = gameObject.GetComponent<UIManager>();
            playerJumpAudioToggleGO.GetComponent<JumpAudioToggleHandler>().selectJumpAudioToggleIndex = i;
            selectJumpAudioToggleList.Add(playerJumpAudioToggleGO);
        }

    }

    public void SpawnBackgroundAudioToggles()
    {
        foreach (Transform child in selectBackgroundAudioToggleParentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        for (int i = 0; i < backgroundAudioClipArray.Length; i++)
        {
            GameObject playerBackgroundAudioToggleGO = Instantiate(selectBackgroundAudioTogglePrefab, selectBackgroundAudioToggleParentGO.transform);
            playerBackgroundAudioToggleGO.GetComponent<BackgroundAudioToggleHandler>().selectBackgroundAudioToggleAudioClip = backgroundAudioClipArray[i];
            playerBackgroundAudioToggleGO.GetComponent<BackgroundAudioToggleHandler>().selectBackgroundAudioToggleAudioClipNameText.text = backgroundAudioClipArray[i].name;
            playerBackgroundAudioToggleGO.GetComponent<BackgroundAudioToggleHandler>().uIManager = gameObject.GetComponent<UIManager>();
            playerBackgroundAudioToggleGO.GetComponent<BackgroundAudioToggleHandler>().selectBackgroundAudioToggleIndex = i;
            selectBackgroundAudioToggleList.Add(playerBackgroundAudioToggleGO);
        }

    }

    public void SpawnGameOverAudioToggles()
    {

        foreach (Transform child in selectGameOverAudioToggleParentGO.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        for (int i = 0; i < gameOverAudioClipArray.Length; i++)
        {
            GameObject playerGameOverAudioToggleGO = Instantiate(selectGameOverAudioTogglePrefab, selectGameOverAudioToggleParentGO.transform);
            playerGameOverAudioToggleGO.GetComponent<GameOverAudioToggleHandler>().selectGameOverAudioToggleAudioClip = gameOverAudioClipArray[i];
            playerGameOverAudioToggleGO.GetComponent<GameOverAudioToggleHandler>().selectGameOverAudioToggleAudioClipNameText.text = gameOverAudioClipArray[i].name;
            playerGameOverAudioToggleGO.GetComponent<GameOverAudioToggleHandler>().uIManager = gameObject.GetComponent<UIManager>();
            playerGameOverAudioToggleGO.GetComponent<GameOverAudioToggleHandler>().selectGameOverAudioToggleIndex = i;
            selectGameOverAudioToggleList.Add(playerGameOverAudioToggleGO);
        }

    }

    public void IncreaseScore(int score)
    {
        currentScore += score; 
        scoreText.text = "Score: " + currentScore;
    }

    public void OnPlayButtonClicked()
    {
        player.canJump = true;
        gameManager.canSpawn = true;

        //int playerSpriteRand = Random.Range(0, playerSpritesArray.Length);
        //playerSprite.sprite = playerSpritesArray[playerSpriteRand];

        //int backgroundSpriteRand = Random.Range(0, backgroundSpritesArray.Length);
        //backgroundSprite.sprite = backgroundSpritesArray[backgroundSpriteRand];

        backgroundAudioSource.clip = backgroundAudioClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();



        gameManager.SpawnNextBlock();
        playBackgroundPanelGO.SetActive(false);
    }

    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRetryButtonClicked()
    {



        currentScore = 0;
        scoreText.text = "Score: " + currentScore;

        game1CameraMovement.gameObject.transform.position = game1CameraMovement.initialCameraPosition;
        game1CameraMovement.backgroundTransform.position = game1CameraMovement.initialBackgroundTransformPosition;

        game1CameraMovement.cameraTargetPosition = game1CameraMovement.initialCameraPosition;
        game1CameraMovement.backgroundTargetPosition = game1CameraMovement.initialBackgroundTransformPosition;

        DestroyAllBrickObjects();

        player.transform.position = new Vector3(0, -3.25f, 0);
        gameManager.currentSpawnHeight = gameManager.blockIinitialStartPosition.y;



        
        backgroundAudioSource.clip = backgroundAudioClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();

        //Invoke("gameManager.SpawnNextBlock()", 1f);
        //gameManager.Invoke("SpawnNextBlock()", 1f);
        //gameManager.SpawnNextBlock();
        gameOverBackgroundPanelGO.SetActive(false);


        StartCoroutine(OneSecPause());

        player.canJump = true;
        gameManager.canSpawn = true;


    }

    IEnumerator OneSecPause()
    {
        yield return new WaitForSeconds(1f);
    }

    public void DestroyAllBrickObjects()
    {
        // Find all GameObjects with the tag "Brick"
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        // Iterate over each GameObject and destroy it
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }

    }

    public void ShowGameOver()
    {
        backgroundAudioSource.Stop();
        backgroundAudioSource.clip = gameOverAudioClip;
        backgroundAudioSource.loop = false;
        backgroundAudioSource.Play();
        gameOverBackgroundPanelGO.SetActive(true);
    }


}
