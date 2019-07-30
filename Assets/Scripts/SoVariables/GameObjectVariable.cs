// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName = "SOVariable / GameObject")]
public class GameObjectVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline] public string DeveloperDescription = "";
#endif
    public GameObject Value;

    public void SetValue(GameObject value)
    {
        Value = value;
    }

    public void SetValue(GameObjectVariable value)
    {
        Value = value.Value;
    }
}