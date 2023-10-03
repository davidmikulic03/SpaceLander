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
        Load();
        SpawnObjects();
    }

    void Load()
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

        PlayerParent playerParent = player.GetComponent<PlayerParent>();
        playerParent.Initialize(this);

        PlayerValues playerValues = playerParent.playerValues;
    }
}
