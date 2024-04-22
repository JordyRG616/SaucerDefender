using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextAppearAnimation eventDescription;
    [SerializeField] private List<OptionBox> optionBoxes;
    [SerializeField] private GameObject lastBox;
    [SerializeField] private InputMap inputMap;
    [SerializeField] private ScriptableSignal OnLeave;

    private System.Action OnConclusion;
    private System.Action DelayedEffect;

    private void Start()
    {
        var eventManager = FractaMaster.GetManager<EventManager>();
        eventManager.OnEventReceived += ReceiveEvent;
        eventManager.OnOptionSelected += ReceiveConclusion;

        OnLeave.Register(DoDelayedEffect);
    }

    public void ReceiveEvent(EventData eventData)
    {
        inputMap.SetPlanetControls(false);
        inputMap.lockFireControls = true;
        lastBox.SetActive(false);
        panel.SetActive(true);

        eventTitle.text = eventData.name;
        eventDescription.SetText(eventData.eventDescription);
        optionBoxes.ForEach(x => x.box.SetActive(false));

        eventDescription.OnTextRevealed.Set(() => RevealOption(eventData, 0));
        eventDescription.Play();
    }

    private void RevealOption(EventData eventData, int index)
    {
        if (index >= eventData.options.Count) return;

        var box = optionBoxes[index];
        box.ReceiveOption(eventData.options[index]);
        box.optionDescription.OnTextRevealed.Set(() => RevealOption(eventData, index + 1));
    }

    public void ReceiveConclusion(EventOption option)
    {
        optionBoxes.ForEach(x => x.box.SetActive(false));
        eventDescription.SetText(option.conclusion);
        eventDescription.OnTextRevealed.Set(null);
        eventDescription.Play();
        lastBox.SetActive(true);

        OnConclusion = option.effectSignal.Fire;
        if (option.delayedSignal != null)
        {
            DelayedEffect += option.delayedSignal.Fire;
        }
    }

    public void Close()
    {
        inputMap.lockFireControls = false;
        inputMap.SetPlanetControls(true);
        panel.SetActive(false);
        OnConclusion.Invoke();
        OnConclusion = null;
    }

    private void DoDelayedEffect()
    {
        DelayedEffect?.Invoke();
        DelayedEffect = null;
    }
}

[System.Serializable]
public class OptionBox
{
    public GameObject box;
    public TextAppearAnimation optionDescription;
    [SerializeField] private TextMeshProUGUI optionEffect;
    [SerializeField] private Image characterPortrait;


    public void ReceiveOption(EventOption option)
    {
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
        optionDescription.SetText(option.description);
        optionDescription.Play();
    }
}