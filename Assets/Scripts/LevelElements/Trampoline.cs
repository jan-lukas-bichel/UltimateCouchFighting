using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private GameObject fireballPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMovementRegular playerMovement = other.gameObject.GetComponent<PlayerMovementRegular>();

        if (playerMovement != null)
        {
            playerMovement.Bounce(other, bounceForce);
            ShootFireballs();
        }
    }

    private void ShootFireballs()
    {
        //ToDo: mehrere Fireballs, mit korrekten rotationen schießen
        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
    }
}
