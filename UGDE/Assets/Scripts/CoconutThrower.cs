using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class CoconutThrower : MonoBehaviour
{
  public AudioClip throwSound;
  public Rigidbody coconutPrefab;
  public float throwSpeed = 30.0f;

  public static bool canThrow = false;

  void Update ()
  {
    if (Input.GetButtonDown("Fire1") && canThrow)
    {
      if (null != throwSound)
        audio.PlayOneShot(throwSound);

      if (null != coconutPrefab)
      {
        Rigidbody newCoconut = Instantiate(coconutPrefab, transform.position, transform.rotation) as Rigidbody;
        newCoconut.name = "coconut";
        newCoconut.velocity = transform.forward * throwSpeed;
        Physics.IgnoreCollision(transform.root.collider, newCoconut.collider, true);
      }
    }
  }
}
