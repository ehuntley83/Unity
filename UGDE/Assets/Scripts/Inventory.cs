using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
  public static int charge = 0;

  public AudioClip collectSound;

  // HUD
  public Texture2D[] hudCharge;
  public GUITexture chargeHudGUI;

  // Generator
  public Texture2D[] meterCharge;
  public Renderer meter;

  // Use this for initialization
  void Start ()
  {
    charge = 0;
  }

  void CellPickup()
  {
    if (null != collectSound)
      AudioSource.PlayClipAtPoint(collectSound, transform.position);

    HUDon();

    charge++;
    chargeHudGUI.texture = hudCharge[charge];
    meter.material.mainTexture = meterCharge[charge];
  }

  void HUDon()
  {
    if (!chargeHudGUI.enabled)
      chargeHudGUI.enabled = true;
  }
}
