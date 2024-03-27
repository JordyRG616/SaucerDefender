using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ScriptableLink : MonoBehaviour
{
    [SerializeField] private ScriptableSignal signal;
    [SerializeField] private UnityEvent OnSignalFired;

    private void Start()
    {
        signal.Register(PassSignal);
    }

    private void PassSignal()
    {
        OnSignalFired?.Invoke();
    }
}
