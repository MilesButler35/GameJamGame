using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSoldierAI : MonoBehaviour
{
  public float AttackRange = 1;
  public float AttackInterval = 1;
  private float _attackTimer;

  private Rigidbody2D _rigidbody2D;
  private EntityData _entityData;
  private EntityAttack _entityAttack;

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _entityData = GetComponent<EntityData>();
    _entityAttack = GetComponent<EntityAttack>();
  }

  // Update is called once per frame
  void Update()
  {
    if(_attackTimer > 0)
    {
      _attackTimer -= Time.deltaTime;
    }

    float distanceToTarget = float.MaxValue;
    Transform targetTransform =  FindClosesTarget(out distanceToTarget);
    if (targetTransform == null)
    {
      _rigidbody2D.velocity = Vector2.zero;
      return;
    }

    if (distanceToTarget < AttackRange)
    {
      _rigidbody2D.velocity = Vector2.zero;

      if(_attackTimer <= 0)
      {
        _entityAttack.Attack(targetTransform.position);
        _attackTimer = AttackInterval;
      }

      return;
    }
    
    Vector2 movementDirection = (targetTransform.position - transform.position).normalized;
    _rigidbody2D.velocity = movementDirection * _entityData.MovementSpeed;
  }

  private Transform FindClosesTarget(out float distanceToTarget)
  {
    GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

    Transform targetTransform = null;
    float minDistance = float.MaxValue;
    foreach (var enemy in allEnemies)
    {
      float dist = Vector2.Distance(transform.position, enemy.transform.position);
      if(dist < minDistance)
      {
        minDistance = dist;
        targetTransform = enemy.transform;
      }
    }

    distanceToTarget = minDistance;
    return targetTransform;
  }
  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, AttackRange);
  }
}
