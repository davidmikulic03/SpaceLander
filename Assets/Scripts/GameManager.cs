using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] SceneSettings sceneSettings;

    GameObject playerResource;
    GameObject pauseMenuResource;
    GameObject gameOverResource;

    GameObject player;
    GameObject pauseMenuObject;
    GameObject gameOverObject;

    PlayerParent playerParent;

    [HideInInspector] public bool isRunning;

    [HideInInspector] public int sceneIndex = 1;

    [HideInInspector] public HighScoreManager highScoreManager = new HighScoreManager();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Time.timeScale = 1;

        highScoreManager.ReadFile();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        Load();
        SpawnObjects();

        isRunning = true;
    }

    void Load()
    {
        playerResource = Resources.Load<GameObject>("Player");
        pauseMenuResource = Resources.Load<GameObject>("PauseMenu");
        gameOverResource = Resources.Load<GameObject>("GameOverScreen");
    }

    void SpawnObjects()
    {
        player = Instantiate(playerResource);
        pauseMenuObject = Instantiate(pauseMenuResource);
        gameOverObject = Instantiate(gameOverResource);
        AlignPlayer();
        InitObjects();
    }

    void AlignPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }
    public PlayerValues playerValues;
    void InitObjects()
    {
        playerParent = player.GetComponent<PlayerParent>();
        playerParent.Initialize(this, sceneSettings);

        playerValues = playerParent.playerValues;
        playerValues.Initialize(sceneSettings);

        PauseMenu pauseMenuScript = pauseMenuObject.GetComponent<PauseMenu>();
        pauseMenuScript.Init(this);

        GameOverScreen gameOverScreen = gameOverObject.GetComponent<GameOverScreen>();
        gameOverScreen.Init(this);
    }

    void OnPause(InputValue value)
    {
        if(isRunning) PauseWithMenu();
        else ResumeGame();
    }

    public void PauseWithMenu()
    {
        pauseMenuObject.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        isRunning = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isRunning = true;
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex + 1 < SceneManager.sceneCount)
        {
            SceneManager.LoadScene(buildIndex + 1);
        }
    }

    public void PreviousLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex > 1)
        {
            SceneManager.LoadScene(buildIndex - 1);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
