using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour
{
  private bool doorIsOpen = false;
  private float doorTimer = 0.0f;

  public float doorOpenTime = 3.0f;
  public AudioClip doorOpenSound;
  public AudioClip doorShutSound;

	void Start ()
	{
    doorTimer = 0.0f;
	}

	void Update ()
	{
	  if (doorIsOpen)
    {
      doorTimer += Time.deltaTime;

      if (doorTimer > doorOpenTime)
      {
        Door(doorShutSound, false, "doorshut");
        doorTimer = 0.0f;
      }
    }
	}

  void DoorCheck()
  {
    if (!doorIsOpen)
    {
      Door(doorOpenSound, true, "dooropen");
    }
  }

  void Door(AudioClip sound, bool openTheDoor, string animationName)
  {
    audio.PlayOneShot(sound);
    doorIsOpen = openTheDoor;

    transform.parent.gameObject.animation.Play(animationName);
  }
}
