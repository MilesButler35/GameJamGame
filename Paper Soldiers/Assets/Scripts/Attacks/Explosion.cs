using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public int Damage;
  
  public EAttackType AttackType;
  public int RedStacksAmount;
  public int BlueStacksAmount;

  private GameObject _owner;
  private string _teamTag;

  public void Initialize(GameObject owner, string teamTag)
  {
    _owner = owner;
    _teamTag = teamTag;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject == _owner || collider.tag == _teamTag)
      return;

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
