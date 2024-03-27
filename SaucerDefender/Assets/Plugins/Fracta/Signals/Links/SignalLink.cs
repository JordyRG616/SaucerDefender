using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Signal Links/Default Link")]
public class SignalLink : MonoBehaviour
{
    public SignalReference signalReference;
    [Space]
    public UnityEvent linkedCallback;
    [SerializeField] private bool inactive;


    private void Start()
    {
        if (signalReference.CreateLink())
        {
            signalReference.Signal += InvokeCallbacks;
        }
        else
        {
        }
    }

    private void InvokeCallbacks()
    {
        if (inactive) return;

        linkedCallback?.Invoke();
    }

    public void SetActive(bool active)
    {
        inactive = !active;
    }
}
