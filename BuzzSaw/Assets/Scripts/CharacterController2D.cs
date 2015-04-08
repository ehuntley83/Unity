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
    public bool CanJump { get { return false; } }
    public bool HandleCollisions { get; set; }

    // Need to use an explicit backing field in order to modify components of a Vector2 as it is a value type and otherwise passed by value into methods
    private Vector2 _velocity;
    public Vector2 Velocity { get { return _velocity; } }

    private ControllerParameters2D _overrideParameters;
    public ControllerParameters2D Parameters { get { return _overrideParameters ?? DefaultParameters; } }

    private Transform _transform;
    private Vector3 _localScale;
    private BoxCollider2D _boxCollider;
    private float _verticalDistanceBetweenRays;
    private float _horizontalDistanceBetweenRays;
    private Vector3 _raycastTopLeft;
    private Vector3 _raycastBottomRight;
    private Vector3 _raycastBottomLeft;

    #region Public Methods

    public void Awake()
    {
        HandleCollisions = true;
        State = new ControllerState2D();
        _transform = transform;
        _localScale = transform.localScale;
        _boxCollider = GetComponent<BoxCollider2D>();

        var colliderWidth = (_boxCollider.size.x * Mathf.Abs(transform.localScale.x)) - (SkinWidth * 2);
        _horizontalDistanceBetweenRays = colliderWidth / (TotalVerticalRays - 1);

        var colliderHeight = (_boxCollider.size.y * Mathf.Abs(transform.localScale.y)) - (SkinWidth * 2);
        _verticalDistanceBetweenRays = colliderHeight / (TotalHorizontalRays - 1);
    }

    public void AddForce(Vector2 force)
    {
        _velocity += force;
    }

    public void SetForce(Vector2 force)
    {
        _velocity = force;
    }

    public void SetHorizontalForce(float x)
    {
        _velocity.x = x;
    }

    public void SetVerticalForce(float y)
    {
        _velocity.y = y;
    }

    public void Jump()
    {
        
    }

    public void LateUpdate()
    {
        Move(Velocity * Time.deltaTime);
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
        var wasGrounded = State.IsCollidingBelow;
        State.Reset();

        if (HandleCollisions)
        {
            HandlePlatforms();
            CalculateRayOrigins();

            if (deltaMovement.y < 0 && wasGrounded) // moving down
                HandleVerticalSlope(ref deltaMovement);

            if (Mathf.Abs(deltaMovement.x) > 0.001f) // moving horizontally
                MoveHorizontally(ref deltaMovement);

            MoveVertically(ref deltaMovement);
        }

        _transform.Translate(deltaMovement, Space.World);

        // TODO: addition moving platform code

        if (Time.deltaTime > 0)
            _velocity = deltaMovement / Time.deltaTime;

        // clamp velocity to Max and Min as set in Parameters
        _velocity.x = Mathf.Min(_velocity.x, Parameters.MaxVelocity.x);
        _velocity.y = Mathf.Min(_velocity.y, Parameters.MaxVelocity.y);

        if (State.IsMovingUpSlope)
            _velocity.y = 0;
    }

    private void HandlePlatforms()
    {
        
    }

    private void CalculateRayOrigins()
    {
        var size = new Vector2(_boxCollider.size.x * Mathf.Abs(_localScale.x), _boxCollider.size.y * Mathf.Abs(_localScale.y)) / 2;
        var center = new Vector2(_boxCollider.offset.x * _localScale.x, _boxCollider.offset.y * _localScale.y);

        _raycastTopLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y + size.y - SkinWidth);
        _raycastBottomRight = _transform.position + new Vector3(center.x + size.x - SkinWidth, center.y - size.y + SkinWidth);
        _raycastBottomLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y - size.y + SkinWidth);
    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        var isGoingRight = deltaMovement.x > 0;
        var rayDistance = Mathf.Abs(deltaMovement.x) + SkinWidth;
        var rayDirection = isGoingRight ? Vector2.right : -Vector2.right;
        var rayOrigin = isGoingRight ? _raycastBottomRight : _raycastBottomLeft;

        for (var i = 0; i < TotalHorizontalRays; i++)
        {
            var rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * _verticalDistanceBetweenRays));
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask);

            if (!rayCastHit)
                continue;

            if (i == 0 && HandleHorizontalSlope(ref deltaMovement, Vector2.Angle(rayCastHit.normal, Vector2.up), isGoingRight))
                break;

            // if we've hit and we're not on a slope, we set our corrected deltaMovement and reset our rayDistance
            // to the distance to the closest object we've hit because there's no need to cast farther than that
            // distance from now on
            deltaMovement.x = rayCastHit.point.x - rayVector.x;
            rayDistance = Mathf.Abs(deltaMovement.x);

            if (isGoingRight)
            {
                deltaMovement.x -= SkinWidth;
                State.IsCollidingRight = true;
            }
            else
            {
                deltaMovement.x += SkinWidth;
                State.IsCollidingLeft = true;
            }

            if (rayDistance < SkinWidth + 0.0001f)
                break;
        }
    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        
    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        
    }

    private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
        return false;
    }

    #endregion
}
