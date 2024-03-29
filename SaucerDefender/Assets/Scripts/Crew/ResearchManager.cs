using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResearchManager : ManagerBehaviour
{
    [Header("Signals")]
    public Signal<float> OnResearchProgress = new Signal<float>();
    public Signal OnResearchCompleted = new Signal();
    public Signal OnResearchStarted = new Signal();
    public Signal<Sprite> OnResearchReceived = new Signal<Sprite>();

    [SerializeField] private List<QueueBox> researchQueue;
    [SerializeField] private float researchSpeed;

    private List<ResearchType> storedResearchs = new();

    private ResearchType currentResearchType;
    private float currentRequiredResearch;
    private float accumulatedResearch;
    private CampsiteModule campsite;
    private bool researching;
    private int queueIndex;


    private void Start()
    {
        campsite = FindObjectOfType<CampsiteModule>();
        campsite.OnResearchSiteReached += StartResearch;
    }

    public void SetQueue(List<PlanetStage> researchs)
    {
        researchQueue.ForEach(x => x.box.SetActive(false));

        for (int i = 0; i < researchs.Count; i++)
        {
            researchQueue[i].icon.sprite = researchs[i].GetIcon();
            researchQueue[i].box.SetActive(true); 
        }

        queueIndex = 0;
    }

    public void MoveQueue()
    {
        researchQueue[queueIndex].box.SetActive(false);
        queueIndex++;
    }

    public void SetResearch(ResearchType type, float siteLocation)
    {
        currentRequiredResearch = type.requiredAmount;
        currentResearchType = type;
        campsite.SetSiteLocation(siteLocation);

        OnResearchReceived.Fire(type.icon);
        OnResearchStarted.Fire();
    }

    private void StartResearch()
    {
        researching = true;
    }

    private void Update()
    {
        if (campsite.EnemiesInRange > 0) return;

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

    public bool HasResearchPoint(ResearchType research)
    {
        var res = storedResearchs.Find(x => x == research);
        return res != null;
    }

    public void SpendResearchPoint(ResearchType research)
    {
        var res = storedResearchs.Find(x => x == research);
        if (res != null)
        {
            storedResearchs.Remove(res);
        }
    }

    public void ReceiveResearchPoint(ResearchType research)
    {
        storedResearchs.Add(research);
    }
}

[System.Serializable]
public struct QueueBox
{
    public GameObject box;
    public Image icon;
}