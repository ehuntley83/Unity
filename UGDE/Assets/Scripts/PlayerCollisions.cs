using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{
  private GameObject currentDoor;

  void Update()
  {
    RaycastHit hit;

    if (Physics.Raycast(transform.position, transform.forward, out hit, 3.0f))
    {
      if ("playerDoor" == hit.collider.gameObject.tag)
      {
        currentDoor = hit.collider.gameObject;

        currentDoor.SendMessage("DoorCheck");
      }
    }
  }
}
