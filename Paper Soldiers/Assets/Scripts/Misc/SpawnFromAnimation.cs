using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromAnimation : MonoBehaviour
{
  public GameObject PureSoldierPrefab;

  public void Spawn()
  {
    Instantiate(PureSoldierPrefab, transform.position, Quaternion.identity);
  }
}
