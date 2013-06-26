using UnityEngine;
using System.Collections;

public class PowerCell : MonoBehaviour
{
  public float rotationSpeed = 100.0f;

  // Update is called once per frame
  void Update ()
  {
    transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime, 0.0f));
  }

  void OnTriggerEnter(Collider col)
  {
    if ("Player" == col.gameObject.tag)
    {
      col.gameObject.SendMessage("CellPickup");
      Destroy(gameObject);
    }
  }
}
