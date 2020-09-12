using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
  public GameObject ProjectilePrefab;
  public float AttackOriginOffset;

  public void Attack(Vector2 attackPosition)
  {
    Vector2 attackOrigin = transform.position + transform.right * AttackOriginOffset;

    GameObject newProjectile = Instantiate(ProjectilePrefab, attackOrigin, Quaternion.identity);
    
    // RANGED
    AttackBase attackBase = newProjectile.GetComponent<AttackBase>();
    attackBase?.Initialize(this.gameObject, this.tag);

    // MELEE
    Explosion explosion = newProjectile.GetComponent<Explosion>();
    explosion?.Initialize(this.gameObject, this.tag);

    newProjectile.transform.LookAt(attackPosition, Vector3.up);

    Vector3 dir = (Vector3)attackPosition - newProjectile.transform.position;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    newProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }
}
