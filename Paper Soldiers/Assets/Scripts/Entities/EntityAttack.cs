using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
  public GameObject ProjectilePrefab;
  public float AttackOriginOffset;

  private PaperSoldierAI _paperSoldierAI;
  private EnemyAI _enemyAI;

  private void Awake()
  {
    _paperSoldierAI = GetComponent<PaperSoldierAI>();
    _enemyAI = GetComponentInParent<EnemyAI>();
  }

  public void Attack()
  {
    Vector2 attackPosition = transform.position;
    if(_paperSoldierAI != null)
    {
      attackPosition = _paperSoldierAI.AttackPosition;
    }

    if(_enemyAI != null)
    {
      attackPosition = _enemyAI.AttackPosition;
    }

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
