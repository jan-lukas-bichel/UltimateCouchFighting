using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerScoreManager playerScoreManager = other.gameObject.GetComponent<PlayerScoreManager>();

        if (playerScoreManager != null)
        {
            playerScoreManager.ChangeScore(-damageAmount);
        }
    }
}
