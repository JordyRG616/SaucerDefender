using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [SerializeField] private List<OptionBox> optionBoxes;
    [SerializeField] private GameObject lastBox;


    private void Start()
    {
        var eventManager = FractaMaster.GetManager<EventManager>();
        eventManager.OnEventReceived += ReceiveEvent;
        eventManager.OnOptionSelected += ReceiveConclusion;
    }

    public void ReceiveEvent(EventData eventData)
    {
        eventTitle.text = eventData.name;
        eventDescription.text = eventData.eventDescription;

        optionBoxes.ForEach(x => x.box.SetActive(false));
        for (int i = 0; i < eventData.options.Count; i++)
        {
            optionBoxes[i].ReceiveOption(eventData.options[i]);
        }

        lastBox.SetActive(false);
        panel.SetActive(true);
    }

    public void ReceiveConclusion(EventOption option)
    {
        optionBoxes.ForEach(x => x.box.SetActive(false));
        eventDescription.text = option.conclusion;
        lastBox.SetActive(true);
    }
}

[System.Serializable]
public class OptionBox
{
    public GameObject box;
    [SerializeField] private TextMeshProUGUI optionDescription;
    [SerializeField] private TextMeshProUGUI optionEffect;
    [SerializeField] private Image characterPortrait;


    public void ReceiveOption(EventOption option)
    {
        optionDescription.text = option.description;
        optionEffect.text = option.effect;

        if (option.portrait != null)
        {
            characterPortrait.sprite = option.portrait;
            characterPortrait.gameObject.SetActive(true);
        }
        else
        {
            characterPortrait.gameObject.SetActive(false);
        }

        box.SetActive(true);
    }
}