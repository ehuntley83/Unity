using UnityEngine;
using System.Collections;

public class TextHints : MonoBehaviour
{
  private float timer = 0.0f;

  void ShowHint(string message)
  {
    guiText.text = message;
    if (!guiText.enabled)
      guiText.enabled = true;
  }

  void Update()
  {
    if (guiText.enabled)
    {
      timer += Time.deltaTime;

      if (timer >= 4.0)
      {
        guiText.enabled = false;
        timer = 0.0f;
      }
    }
  }
}
