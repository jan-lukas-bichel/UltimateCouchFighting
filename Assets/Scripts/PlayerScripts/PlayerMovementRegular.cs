using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementRegular : MonoBehaviour
{
    //ToDo: manchmal wird der _grounded status nicht wieder auf true gesetzt, wahrscheinlich
    //ToDo: nachdem man auf andere spieler gesprungen ist (aber nicht immer) was verhindert, dass man springen kann.
    //ToDo: wird resettet nachdem man in der bubble war / irgendwo runtergefallen ist.

    [SerializeField] private float _groundDeceleration = 1; //Horizontal Player Movement-Speed
    [SerializeField] private float _maxGroundSpeed = 1; //Horizontal Player Movement-Speed
    [SerializeField] private float _groundAcceleration = 1; //Horizontal Player Movement-Speed
    [SerializeField] private float _maxAirSpeed = 1; //Horizontal Player Movement-Speed
    [SerializeField] private float _airAcceleration = 1; //Horizontal Player Movement-Speed
    [SerializeField] private float _jumpForce = 1; //Impuls applied when jumping
    [SerializeField] private float _headBounceUpForce = 1; //Upward impuls applied when bouncing on someones head
    [SerializeField] private float _headBounceDownForce = 1; //Downward impuls applied when someone bounces on your head
    [SerializeField] private float _wallJumpForceY = 1.5f; //Impuls applied when walljumping
    [SerializeField] private float _wallJumpForceX = 1; //Impuls applied when walljumping
    [SerializeField] private float _fastFallAmount = 1f; //Player falls faster down than he jumps by this factor
    [SerializeField] private float _lowJumpAmount = 1f; //Determines how fast the players jump decelerates, when not holding the jump-button anymore
    [SerializeField] private float _groundCheckRayLength = 0.6f; //Length of the ray, that checks, if the player has groundcontact
    [SerializeField] private FloatUnityEvent _onSpeedChange;
    [SerializeField] private UnityEvent _onLanded;
    [SerializeField] private UnityEvent _onJump;

    private float collisionTolerance = 0.2f; //Tolerance when determinating the position of two colliders relative to each other
    private int levelStructureLayerIndex = 8; //Layer Index of the level structure

    private Rigidbody2D _rigidbody;
    private Vector2 _newRigidbodySpeed;
    private float _oldVelocity;
    private float _horizontalInput;
    private bool _jumpInput;
    private bool _grounded; //true if the player has groundcontact
    private int _wallSlidedirection; //the direction of current wallslide: -1 if facing left, 1 if facing right, 0 if not wallsliding

    //Get References
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovementRegular>() != null)
        {
            Bounce(other, _headBounceUpForce, _headBounceDownForce);
        }

        else
        {
            CheckGroundStatus();
            CheckWallslideStatus();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        CheckGroundStatus();
        CheckWallslideStatus();
    }

    public void Bounce(Collision2D collision, float bounceForceUp = 0, float bounceForceDown = 0)
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();

        Vector2 minBoundsThis = collider.bounds.min;
        Vector2 maxBoundsThis = collider.bounds.max;

        Vector2 minBoundsOther = collision.collider.bounds.min;
        Vector2 maxBoundsOther = collision.collider.bounds.max;

        if (minBoundsThis.y + collisionTolerance >= maxBoundsOther.y)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, bounceForceUp);
        }
        else if (maxBoundsThis.y <= minBoundsOther.y + collisionTolerance)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, bounceForceDown);
        }
    }

    //Sets _wallSlidedirection to -1 (for left) or +1 (for right), if player is airborne, facing a wall and trying to move into it, back to 0 if not wallsliding
    private void CheckWallslideStatus()
    {
        if (_grounded)
        {
            _wallSlidedirection = 0;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, _groundCheckRayLength, 1 << levelStructureLayerIndex);

        if (hit.collider != null && _horizontalInput < 0)
        {
            _wallSlidedirection = -1;
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, _groundCheckRayLength, 1 << levelStructureLayerIndex);
            if (hit.collider != null && _horizontalInput > 0)
            {
                _wallSlidedirection = 1;
            }
            else
            {
                _wallSlidedirection = 0;
            }
        }
    }

    //only gets called, when user input CHANGES
    public void SetHorizontalInput(float inputValue)
    {
        _horizontalInput = inputValue;
    }

    //only gets called, when user input CHANGES
    public void SetJumpInput(bool inputValue)
    {
        _jumpInput = inputValue;
    }

    //applies movement when not in a bubble
    private void ApplyMovement()
    {
        //Air Movement
        if (!_grounded)
        {
            //Gravity
            AddGravity();

            if (!_jumpInput && _rigidbody.velocity.y > 0)
            {
                AddGravity(_lowJumpAmount);
            }
            else if (_rigidbody.velocity.y < 0)
            {
                AddGravity(_fastFallAmount);
            }

            //Walljump
            if (_wallSlidedirection != 0 && _jumpInput)
            {
                _rigidbody.velocity = new Vector2(-_wallSlidedirection * _wallJumpForceX, _wallJumpForceY);
                _onSpeedChange.Invoke(-_wallSlidedirection);
                _wallSlidedirection = 0;
            }

            //Horizontal Movement
            if (_horizontalInput != 0)
            {
                if (!(_rigidbody.velocity.x > _maxAirSpeed && _horizontalInput > 0)
                    &&
                    !(_rigidbody.velocity.x < -_maxAirSpeed && _horizontalInput < 0)
                    )
                {
                    _rigidbody.AddForce(new Vector2(_horizontalInput * _airAcceleration, 0), ForceMode2D.Force);
                }
            }
        }

        //Ground Movement
        if (_grounded)
        {
            //Decelerate when no input
            if (_horizontalInput == 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * _groundDeceleration, _rigidbody.velocity.y);
            }

            //Jump
            if (_jumpInput)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            }

            //Horizontal Movement
            if (_horizontalInput != 0)
            {
                if (!(_rigidbody.velocity.x > _maxGroundSpeed && _horizontalInput > 0)
                    &&
                    !(_rigidbody.velocity.x < -_maxGroundSpeed && _horizontalInput < 0))
                {
                    _rigidbody.AddForce(new Vector2(_horizontalInput * _groundAcceleration, 0), ForceMode2D.Force);
                }
            }
        }

        //Fire Event when Velocity change
        if (Math.Abs(_oldVelocity - _rigidbody.velocity.x) > 0.1f)
        {
            _onSpeedChange.Invoke(_horizontalInput);
            _oldVelocity = _rigidbody.velocity.x;
        }
    }

    //Sets _grounded to true if player is grounded / fires "OnLanded"-event, when player is grounded again
    private void CheckGroundStatus()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckRayLength, 1 << levelStructureLayerIndex);
        if (hit.collider == null)
        {
            _grounded = false;
        }

        else if (_grounded == false)
        {
            _grounded = true;

            if (_onLanded != null)
            {
                _onLanded.Invoke();
            }
        }
    }

    //Multiplies gravitational effect by multiplier
    private void AddGravity(float multiplier = 1)
    {
        _rigidbody.AddForce(Physics2D.gravity * multiplier);
    }
}