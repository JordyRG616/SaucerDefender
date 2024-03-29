using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CostBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Signals")]
    public Signal OnPurchase = new Signal();
    public Signal OnCashback = new Signal();

    [SerializeField] private ResearchType researchType;
    [SerializeField] private ImageColorSet backgroundColorSet;
    [SerializeField] private Image icon;

    private ResearchManager researchManager;
    public bool Purchased { get; private set; }
    private Material iconMat;


    private void Start()
    {
        researchManager = FractaMaster.GetManager<ResearchManager>();

        iconMat = new Material(icon.material);
        icon.material = iconMat;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Purchased) Purchase();
        else Cashback();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Purchased) return;

        var key = "Hover_";
        if (researchManager.HasResearchPoint(researchType)) key += "True";
        else key += "False";

        backgroundColorSet.Set(key);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Purchased) return;

        backgroundColorSet.Set("Default");
    }

    private void Purchase()
    {
        if (researchManager.HasResearchPoint(researchType))
        {
            researchManager.SpendResearchPoint(researchType);
            Purchased = true;

            backgroundColorSet.Set("Purchased");
            iconMat.SetFloat("_Blend", 1);

            OnPurchase.Fire();
        }
    }

    private void Cashback()
    {
        researchManager.ReceiveResearchPoint(researchType);
        Purchased = false;

        backgroundColorSet.Set("Default");
        iconMat.SetFloat("_Blend", 0);

        OnCashback.Fire();
    }

    public void Clear()
    {
        backgroundColorSet.Set("Default");
        iconMat.SetFloat("_Blend", 0);
    }
}
