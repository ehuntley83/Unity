using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float JumpMagnitude = 20f;

    public void ControllerEnter2D(CharacterController2D controller)
    {
        controller.SetVerticalForce(JumpMagnitude);
    }
}
