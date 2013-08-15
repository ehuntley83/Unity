using UnityEngine;
using System.Collections;

public class Matches : MonoBehaviour
{
  void OnTriggerEnter(Collider col)
  {
    if ("Player" == col.gameObject.tag)
    {
      col.gameObject.SendMessage("MatchPickup");
      Destroy(gameObject);
    }
  }
}
