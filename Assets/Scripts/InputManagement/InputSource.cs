using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input / InputSource")]
public class InputSource : ScriptableObject
{
    public ActionKeybindingPair[] ActionKeybindingPairs;

    [System.Serializable]
    public class ActionKeybindingPair
    {
        public Action ActionName;
        public string Keybinding;
    }

}
