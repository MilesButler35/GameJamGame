using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float Speed;
  public int Damage;

  private void Update()
  {
    transform.position += transform.right * Speed * Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    EntityHealth targetHealth = collider.GetComponent<EntityHealth>();
    if(targetHealth != null)
    {
      targetHealth.ApplyDamage(Damage);
      OnImpact();
      Destroy(this.gameObject);
    }
  }

  protected virtual void OnImpact()
  {
  }
}
