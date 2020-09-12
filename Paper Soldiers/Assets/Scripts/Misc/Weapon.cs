using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { None, Sword, Hat, Crossbow }

public class Weapon : MonoBehaviour
{
  public Sprite BlueEquivalent;
  public Sprite RedEquivalent;
  public ESoldierType TransformationType;
  public WeaponType WeaponType;

  public void TransformWeapon(Color newColor)
  {
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    switch (newColor)
    {
      case Color.Red:
        spriteRenderer.sprite = RedEquivalent;

        if (WeaponType == WeaponType.Sword)
          TransformationType = ESoldierType.RedWarrior;

        if (WeaponType == WeaponType.Hat)
          TransformationType = ESoldierType.RedMage;

        if (WeaponType == WeaponType.Crossbow)
          TransformationType = ESoldierType.RedArcher;

        break;
      case Color.Blue:
        spriteRenderer.sprite = BlueEquivalent;

        if (WeaponType == WeaponType.Sword)
          TransformationType = ESoldierType.BlueWarrior;

        if (WeaponType == WeaponType.Hat)
          TransformationType = ESoldierType.BlueMage;

        if (WeaponType == WeaponType.Crossbow)
          TransformationType = ESoldierType.BlueArcher;

        break;
    }
  }
}
