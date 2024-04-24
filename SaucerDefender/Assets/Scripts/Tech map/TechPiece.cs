using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechPiece : MonoBehaviour
{
    [SerializeField] private List<TechPieceSegment> segments;

    private bool placed;

    public bool CanBePlaced(bool place = false)
    {
        bool result = true;

        foreach (var segment in segments)
        {
            if(segment.CanBePlaced() == false)
            {
                result = false;
            }
        }

        if(result == true && place)
        {
            segments.ForEach(x => x.SetOnCell());
            placed = true;
        }

        return result;
    }

    private void Update()
    {
        if (placed) return;

        CanBePlaced();
    }

    public void ReceiveSamples(List<TechCreatorSlot> slots)
    {
        foreach (var segment in segments)
        {
            var slot = slots.Find(x => x.index == segment.index);
            segment.ReceiveSample(slot.storedResearch);
        }
    }
}
