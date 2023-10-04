using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] SceneSettings sceneSettings;

    GameObject playerResource;
    GameObject pauseMenuResource;

    GameObject player;
    GameObject pauseMenuObject;

    PlayerParent playerParent;
    
    Stopwatch time = new Stopwatch();

    public bool isRunning;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Time.timeScale = 1;
        Load();
        SpawnObjects();
        time.Start();
        isRunning = true;
    }

    void Load()
    {
        playerResource = Resources.Load<GameObject>("Player");
        pauseMenuResource = Resources.Load<GameObject>("PauseMenu");
    }

    void SpawnObjects()
    {
        player = Instantiate(playerResource);
        pauseMenuObject = Instantiate(pauseMenuResource);
        AlignPlayer();
        InitObjects();
    }

    void AlignPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }

    void InitObjects()
    {
        playerParent = player.GetComponent<PlayerParent>();
        playerParent.Initialize(this, sceneSettings);

        PlayerValues playerValues = playerParent.playerValues;
        playerValues.Initialize(sceneSettings);

        PauseMenu pauseMenuScript = pauseMenuObject.GetComponent<PauseMenu>();
        pauseMenuScript.Init(this);
    }

    void OnPause(InputValue value)
    {
        if(isRunning) PauseGame();
        else ResumeGame();
    }

    public void PauseGame()
    {
        isRunning = false;
        pauseMenuObject.SetActive(true);
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
}
