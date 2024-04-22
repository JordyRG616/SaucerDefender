using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TechBox : MonoBehaviour
{
    [Header("Signals")]
    public Signal<float> OnCostFullfilled = new Signal<float>();
    public Signal OnTechCompleted = new Signal();

    [SerializeField] private List<CostBox> costs;
    [SerializeField] private ScriptableSignal upgradeSignal;
    [SerializeField] private SFXPlayer onCompleteSfx;
    [Space]
    [SerializeField] private TextMeshProUGUI levelDisplay;
    [SerializeField] private TextMeshProUGUI maxLevelDisplay;
    [SerializeField] private GameObject maxLevelPanel;

    private int level;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        costs.ForEach(x => x.OnPurchase += CheckPurchaseCompleted);
    }

    private void CheckPurchaseCompleted()
    {
        if(costs.TrueForAll(x => x.Purchased == true))
        {
            upgradeSignal.Fire();
            costs.ForEach(x => x.Clear());

            level++;
            levelDisplay.text = "lvl " + level;
            if (level == 3)
            {
                maxLevelDisplay.text = "Essa tecnologia alcançou seu ápice";
                maxLevelPanel.SetActive(true);
            }

            animator.SetTrigger("Completed");

            OnCostFullfilled.Fire(1);
            OnTechCompleted.Fire(); 
        } else
        {
            float count = costs.FindAll(x => x.Purchased == true).Count;
            count /= costs.Count;

            OnCostFullfilled.Fire(count);
        }

    }
}
