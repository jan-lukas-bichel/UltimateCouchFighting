using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    [SerializeField] private float _speed;
    private Vector3 _newPosition;
    private bool _moveCamera;

    public void ToggleCameraMovement()
    {
        _moveCamera = !_moveCamera;
    }

    void Start()
    {
        _newPosition = transform.position;
    }

	void FixedUpdate ()
	{
	    if (_moveCamera)
	    {
	        _newPosition.x += _speed * Time.deltaTime;
	        transform.position = _newPosition;

        }
    }
}
