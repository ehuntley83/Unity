using UnityEngine;
using System.Collections;

public class ThrowTrigger : MonoBehaviour
{
  void OnTriggerEnter(Collider col)
  {
    if ("Player" == col.gameObject.tag)
      CoconutThrower.canThrow = true;
  }

  void OnTriggerExit(Collider col)
  {
    if ("Player" == col.gameObject.tag)
      CoconutThrower.canThrow = false;
  }
}
