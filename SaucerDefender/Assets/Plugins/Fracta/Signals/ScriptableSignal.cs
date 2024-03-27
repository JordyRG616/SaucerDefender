using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Scriptable Signal")]
public class ScriptableSignal : ScriptableObject
{
    private Signal OnSignalFired;


    public void Fire()
    {
        if (OnSignalFired == null)
        {
            OnSignalFired = new Signal();
        }

        Debug.Log(name + " fired.");
        OnSignalFired.Fire();
    }

    public void Register(System.Action callback)
    {
        if (OnSignalFired == null)
        {
            OnSignalFired = new Signal();
        }

        OnSignalFired += callback;
    }
}
