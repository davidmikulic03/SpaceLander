using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    GameObject playerResource;
    GameObject player;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        LoadResources();
        SpawnObjects();
    }

    void LoadResources()
    {
        playerResource = Resources.Load<GameObject>("Player");
    }

    void SpawnObjects()
    {
        PlayerSpawn();
    }

    void PlayerSpawn()
    {
        player = Instantiate(playerResource);
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        player.GetComponent<PlayerParent>().Initialize(this);
    }
}
