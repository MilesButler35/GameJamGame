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
    attackBase?.Initialize(_owner, _teamTag);

    if(attackBase == null)
    {
      Explosion explosion = explosionObject.GetComponent<Explosion>();
      explosion?.Initialize(_owner, _teamTag);
    }
  }
}
