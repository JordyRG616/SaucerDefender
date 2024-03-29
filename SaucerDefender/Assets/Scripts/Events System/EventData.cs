using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event")]
public class EventData : ScriptableObject
{
    [TextArea] public string eventDescription;
    public List<EventOption> options;
}

[System.Serializable]
public struct EventOption
{
    public string description;
    public string effect;
    [TextArea] public string conclusion;
}