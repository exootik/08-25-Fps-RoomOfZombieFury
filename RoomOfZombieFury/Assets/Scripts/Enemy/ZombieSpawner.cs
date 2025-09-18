using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Prefab zombie")]
    public GameObject[] zombiePrefabs;

    [Header("Points de spawn")]
    private Vector3[] spawnPoints = new Vector3[]
    {
        new Vector3( 90f, -2f,  40f),
        new Vector3( 90f, -2f, -40f),
        new Vector3( 90f, -2f,  30f),
        new Vector3( 90f, -2f, -30f),
        new Vector3( 90f, -2f,  20f),
        new Vector3( 90f, -2f, -20f),
        new Vector3( 90f, -2f,  10f),
        new Vector3( 90f, -2f, -10f),
        new Vector3( 90f, -2f,   0f),

        new Vector3(-90f, -2f,  40f),
        new Vector3(-90f, -2f, -40f),
        new Vector3(-90f, -2f,  30f),
        new Vector3(-90f, -2f, -30f),
        new Vector3(-90f, -2f,  20f),
        new Vector3(-90f, -2f, -20f),
        new Vector3(-90f, -2f,  10f),
        new Vector3(-90f, -2f, -10f),
        new Vector3(-90f, -2f,   0f),
    };

    [Header("Paramètres de vague")]
    public float waveInterval = 20f;
    public int initialSpawnZombieCount = 2;
    public int spawnZombieIncrease = 2;

    private int currentSpawnCount;

    void Start()
    {
        LoadSettings();
        currentSpawnCount = initialSpawnZombieCount;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < currentSpawnCount; i++)
            {
                int idxPoint = Random.Range(0, spawnPoints.Length);
                int idxZombie = Random.Range(0, zombiePrefabs.Length);
                Instantiate(zombiePrefabs[idxZombie], spawnPoints[idxPoint], Quaternion.identity);
            }

            currentSpawnCount += spawnZombieIncrease;

            yield return new WaitForSeconds(waveInterval);
        }
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("WaveInterval"))
        {
            waveInterval = PlayerPrefs.GetFloat("WaveInterval");
        }
        if (PlayerPrefs.HasKey("SpawnZombieIncrease"))
        {
            spawnZombieIncrease = PlayerPrefs.GetInt("SpawnZombieIncrease");
        }
    }
}
