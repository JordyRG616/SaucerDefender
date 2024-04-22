using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CostBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Signals")]
    public Signal OnPurchase = new Signal();
    public Signal OnCashback = new Signal();

    [SerializeField] private ResearchType researchType;
    [SerializeField] private ImageColorSet backgroundColorSet;
    [SerializeField] private Image icon;
    [Space]
    [SerializeField] private SFXPlayer hoverPos;
    [SerializeField] private SFXPlayer hoverNeg;
    [SerializeField] private SFXPlayer confimation;
    [SerializeField] private SFXPlayer denied;

    private ResearchManager researchManager;
    public bool Purchased { get; private set; }
    private Animator animator;
    private Material iconMat;


    private void Start()
    {
        researchManager = FractaMaster.GetManager<ResearchManager>();

        animator = GetComponent<Animator>();

        iconMat = new Material(icon.material);
        icon.material = iconMat;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Purchased) Purchase();
        else Cashback();

        animator.SetBool("Clicked", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Purchased) return;

        var key = "Hover_";
        if (researchManager.HasResearchPoint(researchType))
        {
            key += "True";
            hoverPos.Play();
        }
        else
        {
            key += "False";
            hoverNeg.Play();
        }

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

            confimation.Play();
            OnPurchase.Fire();
            animator.SetBool("Confirmed", true);
        }
        else
        {
            denied.Play();
            animator.SetBool("Confirmed", false);
        }
    }

    private void Cashback()
    {
        researchManager.ReceiveResearchPoint(researchType);
        Purchased = false;

        backgroundColorSet.Set("Default");
        iconMat.SetFloat("_Blend", 0);
        animator.SetBool("Confirmed", true);

        confimation.Play();
        OnCashback.Fire();
    }

    public void Clear()
    {
        backgroundColorSet.Set("Default");
        iconMat.SetFloat("_Blend", 0);
        Purchased = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetBool("Clicked", true);
    }
}
