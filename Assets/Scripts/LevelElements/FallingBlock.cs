using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    // ToDo: vielleicht einen delay einbauen, dass die plattform erst fällt, nachdem der spieler eine weile auf ihr stand?
    //ToDo: Zerstören wenn außer sicht

    [SerializeField] private float fallingSpeed = 1;
    private bool falling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovementRegular>() != null)
        {
            falling = true;
        }
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed, transform.position.z);

            if (CheckForLeavingScreen())
            {
                Destroy(gameObject);
            }
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
