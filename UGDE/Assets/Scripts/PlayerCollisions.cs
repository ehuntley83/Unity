using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{
  private bool doorIsOpen = false;
  private float doorTimer = 0.0f;
  private GameObject currentDoor;

  public float doorOpenTime = 3.0f;
  public AudioClip doorOpenSound;
  public AudioClip doorShutSound;

  // Update is called once per frame
  void Update ()
  {
    if (doorIsOpen)
    {
      doorTimer += Time.deltaTime;

      if (doorTimer > doorOpenTime)
      {
        Door(doorShutSound, false, "doorshut", currentDoor);
        doorTimer = 0.0f;
      }
    }
  }

  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.gameObject.tag == "playerDoor" &&
        doorIsOpen == false)
    {
      currentDoor = hit.gameObject;
      Door(doorOpenSound, true, "dooropen", currentDoor);
    }
  }

  void Door(AudioClip clip, bool isDoorOpening, string animation, GameObject thisDoor)
  {
    if (null != clip)
      thisDoor.audio.PlayOneShot(clip);

    doorIsOpen = isDoorOpening;
    thisDoor.transform.parent.animation.Play(animation);
  }
}
