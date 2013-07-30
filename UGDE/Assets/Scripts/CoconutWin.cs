using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CoconutWin : MonoBehaviour
{
  public static int targets = 0;
  public static bool haveWon = false;

  public AudioClip winSound;
  public GameObject cellPrefab;

  // Update is called once per frame
  void Update ()
  {
    if (3 == targets && false == haveWon)
    {
      targets = 0;
      audio.PlayOneShot(winSound);

      // Find the existing cell in the shack and move it out of its holder
      GameObject winCell = transform.Find("powerCell").gameObject;
      winCell.transform.Translate(-1f, 0f, 0f);

      // Create a new cell for the player to pickup
      Instantiate(cellPrefab, winCell.transform.position, transform.rotation);

      // Destroy the original cell
      Destroy(winCell);
      haveWon = true;
    }
  }
}
