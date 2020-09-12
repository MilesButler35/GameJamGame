using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveryourBelt : MonoBehaviour
{
  public GameObject[] AllWeapons;
  public float SpawnInterval = 2;

  private float _spawnTimer;
  private GameObject[] _spawnedWeapons;
  private Transform[] _spawnPoints;

  private void Awake()
  {
    _spawnPoints = new Transform[transform.childCount];
    _spawnedWeapons = new GameObject[transform.childCount];
    for (int i = 0; i < transform.childCount; i++)
    {
      _spawnPoints[i] = transform.GetChild(i);
    }

    _spawnTimer = SpawnInterval;
  }

  private void Update()
  {
    if(_spawnTimer > 0)
    {
      _spawnTimer -= Time.deltaTime;

      if(_spawnTimer <= 0)
      {
        SpawnWeapon();
        _spawnTimer = SpawnInterval;
      }
    }
  }

  private void SpawnWeapon()
  {
    for (int i = 0; i < _spawnedWeapons.Length; i++)
    {
      if(_spawnedWeapons[i] == null)
      {
        int randomID = Random.Range(0, AllWeapons.Length);
        GameObject newWeapon = AllWeapons[randomID];
        _spawnedWeapons[i] = Instantiate(newWeapon, _spawnPoints[i].position, Quaternion.identity);
        break;
      }
    }
  }
}
