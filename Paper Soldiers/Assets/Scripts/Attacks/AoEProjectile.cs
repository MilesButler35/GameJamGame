using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEProjectile : Projectile
{
  public GameObject ExplosionPrefab;

  protected override void OnImpact()
  {
    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
  }
}
