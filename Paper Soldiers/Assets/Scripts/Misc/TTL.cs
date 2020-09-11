using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTL : MonoBehaviour
{
  public float Lifetime = 1;

  void Update()
  {
    Lifetime -= Time.deltaTime;
    if(Lifetime <= 0){
      Destroy(this.gameObject);
    }
  }
}
