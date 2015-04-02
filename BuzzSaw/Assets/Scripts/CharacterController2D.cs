using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float SkinWidth = 0.02f;
    public int TotalHorizontalRays = 8;
    public int TotalVerticalRays = 4;

    private static readonly float SlopeLimitTangent = Mathf.Tan(75.0f * Mathf.Deg2Rad);

    public LayerMask PlatformMask;
    public ControllerParameters2D DefaultParameters;

    public ControllerState2D State { get; private set; }
    public Vector2 Velocity { get; private set; }
    public bool CanJump { get { return false; } }

    #region Public Methods

    public void Awake()
    {
        State = new ControllerState2D();
    }

    public void AddForce(Vector2 force)
    {
        
    }

    public void SetForce(Vector2 force)
    {
        
    }

    public void SetHorizontalForce(float x)
    {
        
    }

    public void SetVerticalForce(float y)
    {
        
    }

    public void Jump()
    {
        
    }

    public void LateUpdate()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void OnTriggerExit2D(Collider2D other)
    {

    }

    #endregion

    #region Private Methods

    private void Move(Vector2 deltaMovement)
    {
        
    }

    private void HandlePlatforms()
    {
        
    }

    private void CalculateRayOrigins()
    {
        
    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        
    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        
    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        
    }

    private void HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {

    }

    #endregion
}
