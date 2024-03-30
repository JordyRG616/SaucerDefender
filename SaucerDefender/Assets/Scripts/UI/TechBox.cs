using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBox : MonoBehaviour
{
    [SerializeField] private List<CostBox> costs;
    [SerializeField] private ScriptableSignal upgradeSignal;


    private void Start()
    {
        costs.ForEach(x => x.OnPurchase += CheckPurchaseCompleted);
    }

    private void CheckPurchaseCompleted()
    {
        if(costs.TrueForAll(x => x.Purchased == true))
        {
            upgradeSignal.Fire();
            costs.ForEach(x => x.Clear());
        }

    }
}
