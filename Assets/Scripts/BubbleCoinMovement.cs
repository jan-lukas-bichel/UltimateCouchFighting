using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BubbleCoinMovement : MonoBehaviour
{
    [SerializeField] private float _jumpCooldown = 1f;
    [SerializeField] private float _deceleration = 0.9f; //Horizontal Player Movement-Speed
    [SerializeField] private float _jumpForce = 1; //Impuls applied when jumping
    [SerializeField] private float _bounceToCenterForce = 1;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private float _jumpCooldownTimer;

    private void Start()
    {
        NewDirection();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }  

    public void MoveToScreenCenter()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 diffVector = screenCenter - screenPosition;
        Jump(diffVector.normalized * _bounceToCenterForce);
        NewDirection();
    }

    private void ApplyMovement()
    {
        _jumpCooldownTimer -= Time.deltaTime;

        if (CheckForLeavingScreen())
        {
            MoveToScreenCenter();
        }

        else if (_jumpCooldownTimer <= 0)
        {
            Jump(_moveDirection * _jumpForce);
        }

        _rigidbody.velocity = _rigidbody.velocity * _deceleration;
    }

    private void NewDirection()
    {
        _moveDirection = new Vector2(ReturnOneOfTwoValues(-1, 1), ReturnOneOfTwoValues(-1, 1));
    }

    private float ReturnOneOfTwoValues(float value1, float value2)
    {
        if (UnityEngine.Random.value > 0.5f)
        {
            return value1;
        }
        else
        {
            return value2;
        }
    }

    private void Jump(Vector2 direction)
    {
        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        _jumpCooldownTimer = _jumpCooldown;
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
}