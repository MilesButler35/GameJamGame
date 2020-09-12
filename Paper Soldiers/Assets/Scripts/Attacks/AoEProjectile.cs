using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEProjectile : AttackBase
{
  public GameObject ExplosionPrefab;
  

  protected override void OnImpact()
  {
    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
  }
}
