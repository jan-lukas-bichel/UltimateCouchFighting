// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName = "SOVariable / AudioClip")]
public class AudioClipVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline] public string DeveloperDescription = "";
#endif
    public AudioClip Value;

    public void SetValue(AudioClip value)
    {
        Value = value;
    }

    public void SetValue(AudioClipVariable value)
    {
        Value = value.Value;
    }
}