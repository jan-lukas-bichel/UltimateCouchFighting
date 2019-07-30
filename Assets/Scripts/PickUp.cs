using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour {

    [SerializeField] private int _scoreValue;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        PlayerScoreManager playerScoreManager = collider2D.GetComponent<PlayerScoreManager>();
        if (playerScoreManager != null)
        {
            playerScoreManager.ChangeScore(_scoreValue);
            Destroy(gameObject);
        }
    }
}
