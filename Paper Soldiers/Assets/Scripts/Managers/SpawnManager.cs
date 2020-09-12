using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float xSpawnRangeMin = 9;
    private float xSpawnRangeMax = 9;
    private float ySpawnRangeMin = 9;
    private float ySpawnRangeMax = 9    ;
    public int waveNumber = 1;
    public int enemyCount;
    public GameManager gameManager;

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

    Vector2 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(xSpawnRangeMin, xSpawnRangeMax);
        float spawnY = Random.Range(ySpawnRangeMin, ySpawnRangeMax);
        Vector2 randomPos = new Vector2(spawnX, spawnY);

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
