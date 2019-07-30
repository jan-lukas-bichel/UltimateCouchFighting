using UnityEngine.Events;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public IntVariable PlayerScoreVariable;

    [SerializeField] private UnityEvent _onUpdateScore;
    [SerializeField] private GameObject _bubbledCoin;

    void Start()
    {
        PlayerScoreVariable.Value = 0;
        _onUpdateScore.Invoke();
    }

    public void ChangeScore(int value)
    {
        PlayerScoreVariable.Value += value;

        if (PlayerScoreVariable.Value < 0)
        {
            value -= PlayerScoreVariable.Value;
            PlayerScoreVariable.Value = 0;
        }

        if (value != 0)
        {
            _onUpdateScore.Invoke();

            if (value < 0)
            {
                SpawnCoins(Mathf.Abs(value));
            }
        }
    }

    private void SpawnCoins(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_bubbledCoin, transform.position, Quaternion.identity);
        }
    }
}
