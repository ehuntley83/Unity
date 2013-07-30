using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TargetCollision : MonoBehaviour
{
  bool beenHit = false;
  Animation targetRoot;

  public AudioClip hitSound;
  public AudioClip resetSound;
  public float resetTime = 3.0f;

  void Start()
  {
    targetRoot = transform.parent.transform.parent.animation;
  }

  void OnCollisionEnter(Collision theObject)
  {
    if (false == beenHit &&
        "coconut" == theObject.gameObject.name)
    {
      StartCoroutine("targetHit");
    }
  }

  IEnumerator targetHit()
  {
    audio.PlayOneShot(hitSound);
    targetRoot.Play("down");
    beenHit = true;
    CoconutWin.targets++;

    yield return new WaitForSeconds(resetTime);

    audio.PlayOneShot(resetSound);
    targetRoot.Play("up");
    beenHit = false;
    CoconutWin.targets--;
  }
}
