using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ManagerBehaviour
{
    [Header("Signals")]
    public Signal<EventData> OnEventReceived = new Signal<EventData>();
    public Signal<EventOption> OnOptionSelected = new Signal<EventOption>();

    private EventData currentEvent = null;


    public void ReceiveEvent(EventData eventData)
    {
        currentEvent = eventData;
        OnEventReceived.Fire(eventData);
    }

    public void ProcessChoice(int index)
    {
        var option = currentEvent.options[index];
        OnOptionSelected.Fire(option);
    }
}
