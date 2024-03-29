using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]  private List<PlanetStage> stages;
    [Space]
    [SerializeField] private ScriptableSignal OnStaySignal;

    private ResearchManager researchManager;


    private IEnumerator Start()
    {
        researchManager = FractaMaster.GetManager<ResearchManager>();
        OnStaySignal.Register(RequestNextResearch);

        yield return new WaitForSeconds(.1f);
        researchManager.SetQueue(stages);
        RequestNextResearch();
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