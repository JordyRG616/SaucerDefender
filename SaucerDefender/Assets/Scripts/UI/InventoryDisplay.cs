using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private List<InventoryBox> boxes;
    private ResearchManager researchManager;


    private void Start()
    {
        researchManager = FractaMaster.GetManager<ResearchManager>();
        researchManager.OnResearchGained += GainResearch;
        researchManager.OnResearchSpended += SpendResearch;
    }

    private void GainResearch(ResearchType researchType)
    {
        var box = boxes.Find(x => x.researchType == researchType);

        box.amount++;
        box.display.text = box.amount.ToString();
    }

    private void SpendResearch(ResearchType researchType)
    {
        var box = boxes.Find(x => x.researchType == researchType);

        box.amount--;
        box.display.text = box.amount.ToString();
    }
}

[System.Serializable]
public class InventoryBox
{
    public ResearchType researchType;
    public int amount;
    public TextMeshProUGUI display;
}