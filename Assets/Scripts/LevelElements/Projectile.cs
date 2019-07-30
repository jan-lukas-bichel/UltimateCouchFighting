using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private Vector3 movementDirection = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        transform.position += movementDirection;

        if (CheckForLeavingScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool CheckForLeavingScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            return true;
        }
        return false;
    }
}
