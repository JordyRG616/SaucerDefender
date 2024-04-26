using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechAreaCell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconRenderer;

    private ResearchType storedResearch;
    private TechArea owner;
    private TechPieceSegment segment;


    public void Setup(TechArea area)
    {
        owner = area;
    }

    public void ReceiveResearchType(ResearchType research)
    {
        storedResearch = research;

        iconRenderer.sprite = storedResearch.icon;
        iconRenderer.color = Color.white;
        iconRenderer.transform.rotation = Quaternion.identity;
    }

    public void CheckSegmentResearch(TechPieceSegment segment)
    {
        if (storedResearch != null && storedResearch == segment.storedSample)
        {
            owner.Cost--;
        }

        this.segment = segment;
    }
}
