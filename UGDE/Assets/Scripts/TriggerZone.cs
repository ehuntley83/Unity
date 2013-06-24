using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
    if ("Player" == col.gameObject.tag)
    {
      transform.FindChild("door").SendMessage("DoorCheck");
    }
	}
}
