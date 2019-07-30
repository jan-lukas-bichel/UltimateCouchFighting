using UnityEngine;
using UnityEngine.UI;

public class WinnerDisplayer : MonoBehaviour
{
    [SerializeField] private StringVariable _winnerName;

    public void DisplayWinner()
    {
        print("test");
        GetComponent<Text>().text = _winnerName.Value + " wins!";
    }
}
