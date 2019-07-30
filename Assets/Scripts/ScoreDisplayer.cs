using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private IntVariable[] _playerScores;

    private Text _textComponent;

    void Awake()
    {
        _textComponent = GetComponent<Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        string newText = "";
        for (int i = 0; i < _playerScores.Length; i++)
        {
            newText += " Player" + (i + 1) + " Score: " + _playerScores[i].Value + "\n";
        }

        _textComponent.text = newText;
    }
}