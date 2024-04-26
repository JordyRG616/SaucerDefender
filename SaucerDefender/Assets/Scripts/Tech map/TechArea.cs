using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechArea : MonoBehaviour
{
    [SerializeField] private List<ResearchType> researchCost;
    private List<TechAreaCell> cells = new List<TechAreaCell>();

    private int _cost;
    public int Cost
    {
        get => _cost;
        set
        {
            _cost = value;

            if (_cost == 0)
            {
                UnlockTech();
            }
        }
    }

    private void UnlockTech()
    {
        Debug.Log("Unlocked");
    }

    private void Start()
    {
        cells = GetComponentsInChildren<TechAreaCell>().ToList();

        cells.ForEach(x => x.Setup(this));

        GenerateCost();
    }

    private void GenerateCost()
    {
        var container = new List<TechAreaCell>();

        foreach (var cost in researchCost)
        {
            var cell = cells[Random.Range(0, cells.Count)];
            cells.Remove(cell);
            container.Add(cell);

            cell.ReceiveResearchType(cost);
        }

        cells.AddRange(container);

        Cost = researchCost.Count;
    }
}
