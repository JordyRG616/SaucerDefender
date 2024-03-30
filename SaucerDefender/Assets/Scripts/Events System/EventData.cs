using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event")]
public class EventData : ScriptableObject
{
    public Sprite queueIcon;
    [TextArea] public string eventDescription;
    public List<EventOption> options;
}

[System.Serializable]
public struct EventOption
{
    public Sprite portrait;
    public string description;
    public ScriptableSignal effectSignal;
    public ScriptableSignal delayedSignal;
    [TextArea] public string conclusion;
}