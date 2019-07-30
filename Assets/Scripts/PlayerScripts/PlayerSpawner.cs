using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [System.Serializable]
    class PlayerData
    {
        public InputSource InputSource;
        public IntVariable PlayerScore;
        public Color PlayerColor;
        public string ColorName;
        public Transform StartPosition;
    }

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private IntVariable _playerAmount;
    [SerializeField] private StringVariable _winningPlayer;
    [SerializeField] private PlayerData[] _playerData;

    public void StartGame()
    {
        if (_playerAmount.Value > _playerData.Length)
        {
            _playerAmount.Value = _playerData.Length;
        }

        else if (_playerAmount.Value < 1)
        {
            _playerAmount.Value = 1;
        }

        for (int i = 0; i < _playerAmount.Value; i++)
        {
            SpawnPlayer(_playerData[i]);
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetWinningPlayerVariable()
    {
        _winningPlayer.Value = CheckWinner();
    }

    //ToDo: In anderes Skript auslagern
    private string CheckWinner()
    {
        int winningScore = 0;
        string winningName = "";

        foreach (PlayerData player in _playerData)
        {
            if (player.PlayerScore.Value > winningScore)
            {
                winningScore = player.PlayerScore.Value;
                winningName = player.ColorName;
            }
        }

        if (winningName != "")
        {
            return winningName;
        }
        else
        {
            return "nobody wins? something went wrong.";
        }
    }

    void SpawnPlayer(PlayerData playerData)
    {
        GameObject newPlayer = Instantiate(_playerPrefab, playerData.StartPosition.position, Quaternion.identity);
        InjectInputSource(newPlayer, playerData.InputSource);
        InjectPlayerScore(newPlayer, playerData.PlayerScore);
        InjectPlayerColor(newPlayer, playerData.PlayerColor);
    }

    void InjectInputSource(GameObject parentGameObject, InputSource inputSource)
    {
        InputDistributor[] inputDistributors = parentGameObject.GetComponentsInChildren<InputDistributor>(true);

        foreach (InputDistributor inputDistributor in inputDistributors)
        {
            inputDistributor.InputSource = inputSource;
        }
    }

    void InjectPlayerScore(GameObject parentGameObject, IntVariable playerScore)
    {
        PlayerScoreManager[] playerScoresManager = parentGameObject.GetComponentsInChildren<PlayerScoreManager>(true);

        foreach (PlayerScoreManager playerScoreManager in playerScoresManager)
        {
            playerScoreManager.PlayerScoreVariable = playerScore;
        }
    }

    void InjectPlayerColor(GameObject parentGameObject, Color playerColor)
    {
        SpriteRenderer[] playerRenderers = parentGameObject.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer playerRenderer in playerRenderers)
        {
            playerRenderer.color = playerColor;
        }
    }
}
