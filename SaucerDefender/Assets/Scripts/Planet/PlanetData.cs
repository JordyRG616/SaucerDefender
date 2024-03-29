using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Planet")]
public class PlanetData : ScriptableObject
{
    public Sprite visual;
    public List<ResearchType> researchTypes;
    public List<PlanetStage> stages;
    [TextArea] public string planetDescription;


    public ResearchType GetRandomResearch()
    {
        var rdm = Random.Range(0, researchTypes.Count);
        return researchTypes[rdm];
    }
}
