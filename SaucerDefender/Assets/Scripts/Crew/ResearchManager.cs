using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : ManagerBehaviour
{
    [Header("Signals")]
    public Signal<float> OnResearchProgress = new Signal<float>();
    public Signal OnResearchCompleted = new Signal();
    public Signal OnResearchStarted = new Signal();

    [SerializeField] private float researchSpeed;

    private List<ResearchType> storedResearchs = new();

    private ResearchType currentResearchType;
    private float currentRequiredResearch;
    private float accumulatedResearch;
    private bool researching;
    private CampsiteModule campsite;


    private void Start()
    {
        campsite = FindObjectOfType<CampsiteModule>();
        campsite.OnResearchSiteReached += StartResearch;
    }

    public void SetResearch(ResearchType type, float siteLocation)
    {
        currentRequiredResearch = type.requiredAmount;
        currentResearchType = type;
        campsite.SetSiteLocation(siteLocation);

        OnResearchStarted.Fire();
    }

    private void StartResearch()
    {
        researching = true;
    }

    private void Update()
    {
        if (researching)
        {
            accumulatedResearch += researchSpeed * Time.deltaTime;
            OnResearchProgress.Fire(accumulatedResearch / currentRequiredResearch);

            if(accumulatedResearch >= currentRequiredResearch)
            {
                EndResearch();
            }
        }
    }

    private void EndResearch()
    {
        accumulatedResearch = 0;
        researching = false;

        storedResearchs.Add(currentResearchType);
        OnResearchCompleted.Fire();
    }
}
