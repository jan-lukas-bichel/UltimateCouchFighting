using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour {

    private bool _mirrored = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void WalkingAnimation(float inputValue)
    {
        Mirror(inputValue);
        _animator.SetFloat("MovementSpeed", Mathf.Abs(inputValue));
    }

    void Mirror(float direction)
    {
        if (direction < 0 && !_mirrored)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            _mirrored = true;
        }

        else if (direction > 0 && _mirrored)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            _mirrored = false;
        }
    }
}
