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
        researchManager.OnResearchInvetoryUpdated += UpdateDisplay;

    }

    private void UpdateDisplay(List<ResearchType> list)
    {
        foreach (var research in list)
        {
            var box = boxes.Find(x => x.researchType == research);
            box.amount++;
            box.display.text = box.amount.ToString();
        }
    }
}

[System.Serializable]
public struct InventoryBox
{
    public ResearchType researchType;
    public int amount;
    public TextMeshProUGUI display;
}