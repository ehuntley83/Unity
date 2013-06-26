using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
  public AudioClip lockedSound;
  public Light doorLoght;

	void OnTriggerEnter(Collider col)
	{
    if ("Player" == col.gameObject.tag)
    {
      if (4 == Inventory.charge)
      {
        transform.FindChild("door").SendMessage("DoorCheck");

        if (GameObject.Find("PowerGUI"))
        {
          Destroy(GameObject.Find("PowerGUI"));
          doorLoght.color = Color.green;
        }
      }
      else
      {
        if (null != lockedSound)
          transform.FindChild("door").audio.PlayOneShot(lockedSound);

        col.gameObject.SendMessage("HUDon");
      }
    }
	}
}
