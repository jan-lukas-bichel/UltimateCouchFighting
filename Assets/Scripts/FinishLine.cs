using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class FinishLine : MonoBehaviour
{
    [SerializeField] private UnityEvent _enteredGoal;

    void OnTriggerEnter2D()
    {
        _enteredGoal.Invoke();
    }
}
