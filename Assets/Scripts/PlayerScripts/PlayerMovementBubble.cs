using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementBubble : MonoBehaviour
{
    [SerializeField] private float _jumpCooldown = 0.1f;
    [SerializeField] private float _deceleration = 0.9f; //Horizontal Player Movement-Speed
    [SerializeField] private float _jumpForce = 1; //Impuls applied when jumping
    [SerializeField] private float _bounceToCenterForce = 1; 
    private Rigidbody2D _rigidbody;
    private float _horizontalInput;
    private float _verticalInput;
    private float _jumpCooldownTimer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }  

    //only gets called, when user input CHANGES
    public void SetHorizontalInput(float inputValue)
    {
        _horizontalInput = inputValue;
    }

    //only gets called, when user input CHANGES
    public void SetVerticalInput(float inputValue)
    {
        _verticalInput = inputValue;
    }

    private void MoveToScreenCenter()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 diffVector = screenCenter - screenPosition;
        Jump(diffVector.normalized * _bounceToCenterForce);
    }

    private bool CheckForLeavingScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = _rigidbody.velocity * _deceleration;
        _jumpCooldownTimer -= Time.deltaTime;

        if (CheckForLeavingScreen())
        {
            MoveToScreenCenter();
        }

        else if(_horizontalInput != 0 || _verticalInput != 0)
        {
            Vector2 moveDirection = new Vector2(_horizontalInput, _verticalInput);
            if (_jumpCooldownTimer <= 0)
            {
                Jump(moveDirection * _jumpForce);
            }
        }
    }
     


    private void Jump(Vector2 direction)
    {
        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        _jumpCooldownTimer = _jumpCooldown;
    }
}