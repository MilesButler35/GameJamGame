using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSoldierAI : MonoBehaviour
{
  public float MovementSpeed = 1;
  public float AttackRange = 1;

  private Rigidbody2D _rigidbody2D;

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    float distanceToTarget = float.MaxValue;
    Transform targetTransform =  FindClosesTarget(out distanceToTarget);
    if (targetTransform == null || distanceToTarget < AttackRange)
    {
      _rigidbody2D.velocity = Vector2.zero;
      return;
    }

    Vector2 movementDirection = (targetTransform.position - transform.position).normalized;
    _rigidbody2D.velocity = movementDirection * MovementSpeed;
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
