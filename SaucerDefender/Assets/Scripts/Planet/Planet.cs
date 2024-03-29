using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField, DataDropdown(typeof(ResearchType), "Assets/Resources/Data/Research types")] 
    private List<ResearchType> researchs;
    [SerializeField] private EventData finalEvent;
    [Space]
    [SerializeField] private ScriptableSignal OnStaySignal;

    private ResearchManager researchManager;
    private bool eventReached;


    private void Start()
    {
        researchManager = FractaMaster.GetManager<ResearchManager>();
        OnStaySignal.Register(RequestNextResearch);

        RequestNextResearch();
    }

    private void RequestNextResearch()
    {
        if (researchs.Count > 0)
        {
            var research = researchs[0];
            researchs.Remove(research);

            var rdm = Random.Range(60, 120f);
            researchManager.SetResearch(research, rdm);
        } else if (!eventReached)
        {
            FractaMaster.GetManager<EventManager>().ReceiveEvent(finalEvent);
            eventReached = true;
        }
    }
}
