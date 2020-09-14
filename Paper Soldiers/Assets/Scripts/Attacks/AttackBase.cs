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

  protected GameObject _owner;
  protected string _teamTag;

  private void Update()
  {
    transform.position += transform.right * Speed * Time.deltaTime;
  }

  public void Initialize(GameObject owner, string teamTag)
  {
    _owner = owner;
    _teamTag = teamTag;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if(collider.gameObject == _owner || collider.tag == _teamTag)
      return;

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
