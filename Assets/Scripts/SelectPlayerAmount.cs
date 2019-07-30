using UnityEngine;
using UnityEngine.Events;

public class SelectPlayerAmount : MonoBehaviour
{
    [SerializeField] private IntVariable _playerAmount;

    public void SetPlayerAmount(string amount)
    {
        if (amount != null)
        {
            _playerAmount.Value = int.Parse(amount);
        }
        else
        {
            _playerAmount.Value = 0;
        }
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
