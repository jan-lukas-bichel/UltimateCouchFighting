using System;
using UnityEngine;
using UnityEngine.Events;

public class InputDistributor : MonoBehaviour
{
    //ON DISABLE DER COMPONENTE MUSS LAST INPUT VALUE IN BOOL INPUT DATA UND FLOAT INPUT DATA AUF FALSE / 0 GESETZT WERDEN

    public InputSource InputSource;

    [SerializeField] private BoolInput[] boolInputs;
    [SerializeField] private FloatInput[] floatInputs;

    public void Start()
    {
        //Bool und float input arrays zu einem array verbinden
        GenericInput[] inputs = new GenericInput[boolInputs.Length + floatInputs.Length];
        Array.Copy(boolInputs, inputs, boolInputs.Length);
        Array.Copy(floatInputs, 0, inputs, boolInputs.Length, floatInputs.Length);

        //Keybindings mit UnityEvents verbinden, über "Action" als Key
        foreach (GenericInput genericInput in inputs)
        {
            foreach (InputSource.ActionKeybindingPair actionKeybindingPair in InputSource.ActionKeybindingPairs)
            {
                if (genericInput.ActionName == actionKeybindingPair.ActionName)
                {
                    genericInput.Keybinding = actionKeybindingPair.Keybinding;
                }
            }
        }
    }

    public void Update()
    {
        foreach (var boolInput in boolInputs)
        {
            GetBinaryInputChange(boolInput.Event, boolInput.Keybinding, ref boolInput.LastInputValue);
        }

        foreach (var floatInput in floatInputs)
        {
            GetFloatInputChange(floatInput.Event, floatInput.Keybinding, ref floatInput.LastInputValue);
        }
    }

    private void GetBinaryInputChange(BoolUnityEvent eventToFire, string inputButtonName, ref bool lastInputValue)
    {
        bool currentInputValue = Input.GetButtonDown(inputButtonName);
        if (currentInputValue != lastInputValue)
        {
            lastInputValue = currentInputValue;

            if (eventToFire != null)
            {
                eventToFire.Invoke(true);
            }
        }
        else if (Input.GetButtonUp(inputButtonName))
        {
            if (eventToFire != null)
            {
                eventToFire.Invoke(false);
            }
        }
    }

    private void GetFloatInputChange(FloatUnityEvent eventToFire, string inputAxisName, ref float lastInputValue)
    {
        float currentInputValue = Input.GetAxis(inputAxisName);

        if (lastInputValue != currentInputValue)
        {
            lastInputValue = currentInputValue;

            if (eventToFire != null)
            {
                eventToFire.Invoke(currentInputValue);
            }
        }
    }

    private class GenericInput
    {
        public Action ActionName;
        [HideInInspector] public string Keybinding;
    }

    //Verknüpft ein Unity(Bool)Event mit einem Keybinding
    [System.Serializable]
    private class BoolInput : GenericInput
    {
        public BoolUnityEvent Event;
        [HideInInspector] public bool LastInputValue = false;
    }

    //Verknüpft ein Unity(Float)Event mit einem Keybinding und speichert den letzten gemessenen Input Wert.
    //So wird das Event nur getriggert, wenn sich der wert auch tatsächlich verändert.
    [System.Serializable]
    private class FloatInput : GenericInput
    {
        public FloatUnityEvent Event;
        [HideInInspector] public float LastInputValue = 0;
    }

}
