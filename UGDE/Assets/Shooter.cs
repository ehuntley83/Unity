using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
  public Rigidbody bullet;
  public float power = 1500f;
  public float moveSpeed = 2f;

  void Update ()
  {
    float h = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    float v = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

    transform.Translate(h, v, 0f);

    if (Input.GetButtonUp("Fire1"))
    {
      Rigidbody instance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
      Vector3 fwd = transform.TransformDirection(Vector3.forward);
      instance.AddForce(fwd * power);
    }
  }
}