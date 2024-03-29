using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Signal Links/Health state Link")]
public class HealthStateLink : MonoBehaviour
{
    public SignalReference<HealthState> signalReference;
    [Space]
    public UnityEvent<int> currentHealthCallback;
    public UnityEvent<int> maxHealthCallback;
    public UnityEvent<float> healthPercentageCallback;


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

    private void InvokeCallbacks(HealthState state)
    {
        healthPercentageCallback?.Invoke(state.percentage);
        currentHealthCallback?.Invoke(state.current);
        maxHealthCallback?.Invoke(state.max);
    }
}
