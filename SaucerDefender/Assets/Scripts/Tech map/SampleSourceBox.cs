using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SampleSourceBox : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ResearchType researchType;
    [SerializeField] private Image dragableModel;

    private RectTransform currentDragable;


    public void OnBeginDrag(PointerEventData eventData)
    {
        var instance = Instantiate(dragableModel, transform.root);

        instance.sprite = researchType.techPiece;

        currentDragable = instance.transform as RectTransform;
        currentDragable.anchoredPosition = FractaMouse.Position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentDragable.anchoredPosition = FractaMouse.Position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (FractaMouse.FindUnder<TechCreatorSlot>(eventData, out var slot))
        {
            slot.ReceiveSample(researchType);
        }

        Destroy(currentDragable.gameObject);
    }
}
