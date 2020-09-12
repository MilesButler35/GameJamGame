using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public int Damage;
  
  public EAttackType AttackType;
  public int RedStacksAmount;
  public int BlueStacksAmount;

  private void OnTriggerEnter2D(Collider2D collider)
  {
    EntityHealth targetHealth = collider.GetComponent<EntityHealth>();
    if(targetHealth != null)
    {
      targetHealth.ApplyDamage(Damage);
      HandleAttackType(collider.gameObject);
    }
  }

  private void HandleAttackType(GameObject target)
  {
    if (AttackType == EAttackType.Blue)
    {
      BlueDebuff targetBlueDebuff = target.GetComponent<BlueDebuff>();
      if (targetBlueDebuff != null)
      {
        targetBlueDebuff.Apply(BlueStacksAmount);
      }
    }

    if (AttackType == EAttackType.Red)
    {
      RedDebuff targetRedDebuff = target.GetComponent<RedDebuff>();
      if (targetRedDebuff != null)
      {
        targetRedDebuff.Apply(RedStacksAmount);
      }
    }
  }
}
