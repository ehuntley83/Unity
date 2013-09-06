using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
  public static int charge = 0;

  public AudioClip collectSound;

  // HUD
  public Texture2D[] hudCharge;
  public GUITexture chargeHudGUI;
  public GUIText textHints;

  // Generator
  public Texture2D[] meterCharge;
  public Renderer meter;

  // Matches
  private bool haveMatches = false;
  private bool fireIsLit = false;
  private GUITexture matchGUIInstance;
  public GUITexture matchGUIPrefab;

  // Win sequence
  public GameObject winObject;

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

  void MatchPickup()
  {
    haveMatches = true;

    AudioSource.PlayClipAtPoint(collectSound, transform.position);
    GUITexture matchHUD = Instantiate(matchGUIPrefab, new Vector3(0.15f, 0.1f, 0.0f), transform.rotation) as GUITexture;
    matchGUIInstance = matchHUD;
  }

  void OnControllerColliderHit(ControllerColliderHit col)
  {
    if ("campfire" == col.gameObject.name)
    {
      if (haveMatches && !fireIsLit)
      {
        LightFire(col.gameObject);
        Destroy(matchGUIInstance);
        haveMatches = false;
        fireIsLit = true;
        winObject.SendMessage("GameOver");
      }
      else if (!haveMatches && !fireIsLit)
      {
        textHints.SendMessage("ShowHint", "I could use this campfire to signal for help...\nIf only I could light it...");
      }
    }
  }

  void LightFire(GameObject campfire)
  {
    ParticleSystem[] fireEmitters = campfire.GetComponentsInChildren<ParticleSystem>();

    foreach (ParticleSystem emitter in fireEmitters)
    {
      emitter.Play();
    }

    campfire.audio.Play();
  }

  void HUDon()
  {
    if (!chargeHudGUI.enabled)
      chargeHudGUI.enabled = true;
  }
}
