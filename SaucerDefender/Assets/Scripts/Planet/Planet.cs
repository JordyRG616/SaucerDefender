using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private ScriptableSignal OnPlanetLoaded;
    [SerializeField] private SpriteRenderer planetVisual;
    [SerializeField] private ScriptableSignal OnStaySignal;
    [SerializeField] private PlanetSelectionPanel selectionPanel;

    private List<PlanetStage> stages;
    private ResearchManager researchManager;
    private PlanetData currentPlanet;


    private IEnumerator Start()
    {
        researchManager = FractaMaster.GetManager<ResearchManager>();
        OnStaySignal.Register(RequestNextResearch);
        selectionPanel.OnPlanetSelected += ReceivePlanet;

        yield return new WaitForSeconds(.1f);
        ReceivePlanet(selectionPanel.GetPlanetData(0));
        researchManager.SetQueue(stages);
        RequestNextResearch();
    }

    public void LoadPlanet()
    {
        researchManager.SetQueue(stages);
        RequestNextResearch();
    }

    public void ReceivePlanet(PlanetData planetData)
    {
        planetVisual.sprite = planetData.visual;

        stages = new(planetData.stages);

        foreach (var stage in stages)
        {
            if(stage.eventData == null)
            {
                stage.researchType = planetData.GetRandomResearch();
            }
        }

        currentPlanet = planetData;
        OnPlanetLoaded.Fire();
    }

    private void RequestNextResearch()
    {
        if (stages.Count > 0)
        {
            var stage = stages[0];
            
            if (stage.researchType != null)
            {
                var rdm = Random.Range(60, 120f);
                researchManager.SetResearch(stage.researchType, rdm);
            } else if (stage.eventData != null)
            {
                FractaMaster.GetManager<EventManager>().ReceiveEvent(stage.eventData);
            }

            researchManager.MoveQueue();
            stages.Remove(stage);
        }
    }
}

[System.Serializable]
public class PlanetStage
{
    public ResearchType researchType;
    public EventData eventData;

    public Sprite GetIcon()
    {
        if (researchType != null) return researchType.icon;
        else return eventData.queueIcon;
    }
}