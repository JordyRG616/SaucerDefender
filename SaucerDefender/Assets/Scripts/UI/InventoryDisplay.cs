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

    }

    private void Update()
    {
        if (researchManager == null) return;
        UpdateDisplay(researchManager.storedResearchs);
    }

    private void UpdateDisplay(List<ResearchType> list)
    {
        boxes.ForEach(x => x.amount = 0);

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