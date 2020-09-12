using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int waveNumber = 1;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {


        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // enemyCount = FindObjectsOfType<EnemyAI>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;

            SpawnEnemyWave(waveNumber);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnY = Random.Range(-spawnRange, spawnRange);
        Vector2 randomPos = new Vector3(spawnX, spawnY);

        return randomPos;


    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
