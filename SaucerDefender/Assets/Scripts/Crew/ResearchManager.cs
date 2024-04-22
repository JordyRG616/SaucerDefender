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
    public Signal<ResearchType> OnResearchGained = new Signal<ResearchType>();
    public Signal<ResearchType> OnResearchSpended = new Signal<ResearchType>();

    [SerializeField] private ScriptableSignal OnFinalWaveSelected;
    [SerializeField] private ScriptableSignal OnLeave;
    [Space]
    [SerializeField] private List<QueueBox> researchQueue;
    [SerializeField] private float researchSpeed;
    [SerializeField] private List<ResearchType> researchTypes;
    [Header("Upgrades")]
    [SerializeField] private ScriptableSignal researchSpeedUpgrade;
    [SerializeField] private float speedIncrease;
    [Space]
    [SerializeField] private ScriptableSignal extraResearch;
    [SerializeField] private float extraResearchIncrease;

    public List<ResearchType> storedResearchs { get; private set; }  = new();

    private float extraResearchChance = 0;
    private ResearchType currentResearchType;
    private float currentRequiredResearch;
    private float accumulatedResearch;
    private CampsiteModule campsite;
    private bool researching;
    private int queueIndex;
    private bool onFinalWave;


    private void Start()
    {
        campsite = FindObjectOfType<CampsiteModule>();
        campsite.OnResearchSiteReached += StartResearch;

        researchSpeedUpgrade.Register(IncreaseResearchSpeed);
        extraResearch.Register(IncreaseExtraChance);

        OnFinalWaveSelected.Register(SetFinalWave);
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

    public void SetFinalWave()
    {
        onFinalWave = true;
        researching = true;
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
        OnResearchCompleted.Fire();

        if (onFinalWave)
        {
            OnLeave.Fire();
            onFinalWave = false;
            return;
        }

        if (Random.value < extraResearchChance)
        {
            var rdm = Random.Range(0, researchTypes.Count);
            ReceiveResearchPoint(researchTypes[rdm]);
        }

        ReceiveResearchPoint(currentResearchType);
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

        OnResearchSpended.Fire(research);
    }

    public void ReceiveResearchPoint(ResearchType research)
    {
        storedResearchs.Add(research);

        OnResearchGained.Fire(research);
    }

    private void IncreaseResearchSpeed()
    {
        researchSpeed *= 1 + speedIncrease;
    }

    private void IncreaseExtraChance()
    {
        extraResearchChance += extraResearchIncrease;
    }
}

[System.Serializable]
public struct QueueBox
{
    public GameObject box;
    public Image icon;
}