using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEProjectile : AttackBase
{
  public GameObject ExplosionPrefab;

  protected override void OnImpact()
  {
    GameObject explosionObject = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

    AttackBase attackBase = explosionObject.GetComponent<AttackBase>();
    attackBase?.Initialize(this.gameObject, this.tag);
  }
}
