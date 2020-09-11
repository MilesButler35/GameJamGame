using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public int Damage;

  private void OnTriggerEnter2D(Collider2D collider)
  {
    EntityHealth targetHealth = collider.GetComponent<EntityHealth>();
    if(targetHealth != null)
    {
      targetHealth.ApplyDamage(Damage);
    }
  }
}
