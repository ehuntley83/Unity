using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
  public GUITexture fader;

  // Use this for initialization
  void Start ()
  {
    Rect currentRes = new Rect(-Screen.width * 0.5f,
      -Screen.height * 0.5f,
      Screen.width,
      Screen.height);

    fader.pixelInset = currentRes;

    var fadeObj = Instantiate(fader);
    fadeObj.name = "fadeIn";

    Destroy(GameObject.Find("fadeIn"), 2.0f);
    Destroy(gameObject, 2.0f);
  }
}
