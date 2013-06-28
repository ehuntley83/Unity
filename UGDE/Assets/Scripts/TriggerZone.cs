using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
  public AudioClip lockedSound;
  public Light doorLoght;
  public GUIText textHints;

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
      else if (Inventory.charge > 0 &&
               Inventory.charge < 4)
      {
        textHints.SendMessage("ShowHint", "This door won't budge... guess it needs more charging - maybe more power cells will help...");
        if (null != lockedSound)
          transform.FindChild("door").audio.PlayOneShot(lockedSound);
      }
      else
      {
        if (null != lockedSound)
          transform.FindChild("door").audio.PlayOneShot(lockedSound);

        col.gameObject.SendMessage("HUDon");
        textHints.SendMessage("ShowHint", "This door seems to be locked... maybe that generator needs power...");
      }
    }
	}
}
