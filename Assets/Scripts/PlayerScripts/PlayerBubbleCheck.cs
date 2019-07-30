using UnityEngine.Events;
using UnityEngine;

public class PlayerBubbleCheck : MonoBehaviour
{
    [SerializeField] private UnityEvent _onBubbledPlayer;

    void Update ()
	{
	    if (CheckForLeavingScreen())
	    {
	        _onBubbledPlayer.Invoke();
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
