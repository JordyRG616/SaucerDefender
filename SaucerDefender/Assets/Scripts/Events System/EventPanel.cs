using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [SerializeField] private List<OptionBox> optionBoxes;

    private OptionBox lastBox => optionBoxes[optionBoxes.Count - 1];


    private void Start()
    {
        var eventManager = FractaMaster.GetManager<EventManager>();
        eventManager.OnEventReceived += ReceiveEvent;
    }

    public void ReceiveEvent(EventData eventData)
    {
        eventTitle.text = eventData.name;
        eventDescription.text = eventData.eventDescription;

        for (int i = 0; i < eventData.options.Count; i++)
        {
            optionBoxes[i].ReceiveOption(eventData.options[i]);
        }

        panel.SetActive(true);
    }

    public void ReceiveConclusion(EventOption option)
    {
        eventDescription.text = option.conclusion;
        lastBox.SetAsExit();
    }
}

[System.Serializable]
public class OptionBox
{
    [SerializeField] private TextMeshProUGUI optionDescription;
    [SerializeField] private TextMeshProUGUI optionEffect;


    public void ReceiveOption(EventOption option)
    {
        optionDescription.text = option.description;
        optionEffect.text = option.effect;
    }

    public void SetAsExit()
    {
        optionDescription.text = "Continue.";
        optionEffect.text = "";
    }
}