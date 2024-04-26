using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TechCreatorSlot : MonoBehaviour, IPointerClickHandler
{
    public Signal OnSamplePlaced = new Signal();
    public Signal OnSampleCleared = new Signal();

    [field:SerializeField] public int index { get; private set; }
    [SerializeField] private Image icon;

    public ResearchType storedResearch { get; private set; }


    public void ReceiveSample(ResearchType researchType)
    {
        storedResearch = researchType;

        icon.sprite = researchType.techPiece;
        icon.color = Color.white;

        OnSamplePlaced.Fire();
    }

    public void Clear()
    {
        storedResearch = null;

        icon.sprite = null;
        icon.color = Color.clear;

        OnSampleCleared.Fire();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Clear();
        }
    }
}
