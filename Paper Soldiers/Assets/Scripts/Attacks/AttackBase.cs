using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackType { None, White, Red, Blue };

public class AttackBase : MonoBehaviour
{
  public float Speed;
  public int Damage;

  public EAttackType AttackType;
  public int RedStacksAmount;
  public int BlueStacksAmount;

  private void Update()
  {
    transform.position += transform.right * Speed * Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    EntityHealth targetHealth = collider.GetComponent<EntityHealth>();
    if (targetHealth != null)
    {
      targetHealth.ApplyDamage(Damage);

      HandleAttackType(collider.gameObject);

      OnImpact();

      Destroy(this.gameObject);
    }
  }

  protected virtual void OnImpact()
  {
  }

  protected virtual void HandleAttackType(GameObject target)
  {
    if(AttackType == EAttackType.Blue)
    {
      BlueDebuff targetBlueDebuff = target.GetComponent<BlueDebuff>();
      if(targetBlueDebuff != null)
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
