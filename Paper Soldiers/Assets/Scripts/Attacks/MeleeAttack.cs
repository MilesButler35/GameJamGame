using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EntityAttack
{
  public float AttackRadius;

  public override void Attack(Vector2 attackPosition)
  {
    var hits = Physics2D.OverlapCircleAll(transform.position, AttackRadius);

    foreach (var hit in hits)
    {
      EntityHealth targetHealth = hit.GetComponent<EntityHealth>();
      if(targetHealth != null)
      {
        targetHealth.ApplyDamage(Damage);
      }
    }
  }
}
