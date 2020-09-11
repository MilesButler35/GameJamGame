using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAttack : MonoBehaviour
{
  public int Damage;

  public abstract void Attack(Vector2 attackPosition);
}
