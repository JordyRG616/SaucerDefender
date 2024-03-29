using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ManagerBehaviour
{
    [Header("Signals")]
    public Signal<EventData> OnEventReceived = new Signal<EventData>();


    public void ReceiveEvent(EventData eventData)
    {
        OnEventReceived.Fire(eventData);
    }
}
