using UnityEngine;
using System.Collections;

public class ThrowTrigger : MonoBehaviour
{
  public GUITexture crosshair;
  public GUIText textHints;

  void OnTriggerEnter(Collider col)
  {
    if ("Player" == col.gameObject.tag)
    {
      CoconutThrower.canThrow = true;
      crosshair.enabled = true;

      if (!CoconutWin.haveWon)
      {
        textHints.SendMessage("ShowHint",
          "\n\n\n\n\nThere's a power cell attached to this game, \n" +
          "maybe I'll win it if I can knock down all the targets...");
      }
    }
  }

  void OnTriggerExit(Collider col)
  {
    if ("Player" == col.gameObject.tag)
    {
      CoconutThrower.canThrow = false;
      crosshair.enabled = false;
    }
  }
}
