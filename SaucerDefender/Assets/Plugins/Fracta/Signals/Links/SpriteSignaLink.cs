using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Signal Links/Sprite Link")]
public class SpriteSignaLink : MonoBehaviour
{
    public SignalReference<Sprite> signalReference;
    [Space]
    public UnityEvent<Sprite> linkedCallback;


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

    private void InvokeCallbacks(Sprite value)
    {
        linkedCallback?.Invoke(value);
    }
}
