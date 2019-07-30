// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public EventResponsePair[] EventResponsePairs;

    private void OnEnable()
    {
        foreach (var t in EventResponsePairs)
        {
            t.Event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (var t in EventResponsePairs)
        {
            t.Event.UnregisterListener(this);
        }
    }

    public void OnEventRaised(GameEvent gameEvent)
    {
        foreach (var t in EventResponsePairs)
        {
            if (t.Event == gameEvent)
            {
                t.Response.Invoke();
            }
        }
    }
}

[System.Serializable]
public class EventResponsePair
{
    public GameEvent Event;
    public UnityEvent Response;
}