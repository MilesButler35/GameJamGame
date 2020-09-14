using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  public GameObject[] enemyPrefabs;
  public GameObject bossPrefab;
  public float xSpawnRangeMin = 10;
  public float xSpawnRangeMax = 15;
  public float ySpawnRangeMin = -5;
  public float ySpawnRangeMax = 5;
  public int waveNumber = 1;
  public int amountToSpawn = 5;
  public int enemyCount;
  public GameManager gameManager;

  private void Awake()
  {
    SpawnEnemyWave(amountToSpawn * waveNumber);
  }

  // Update is called once per frame
  void Update()
  {
    enemyCount = FindObjectsOfType<EnemyAI>().Length;
    if (enemyCount == 0)
    {
      waveNumber++;

      SpawnEnemyWave(amountToSpawn * waveNumber);
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
      int randomID = Random.Range(0, enemyPrefabs.Length);
      Instantiate(enemyPrefabs[randomID], GenerateSpawnPosition(), enemyPrefabs[randomID].transform.rotation);
    }

    if(waveNumber % 3 == 0)
    {
      Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
    }
  }
}
