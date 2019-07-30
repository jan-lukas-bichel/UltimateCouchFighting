using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiser : MonoBehaviour {

    public void RaiseEvent(GameEvent gameEvent)
    {
        gameEvent.Raise();
    }
}
