using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechPieceSegment : MonoBehaviour
{
    [field:SerializeField] public int index { get; private set; }
    [SerializeField] private SpriteRenderer iconRenderer;

    public ResearchType storedSample { get; private set; }


    private void Start()
    {
    }

    public bool CanBePlaced()
    {
        var hit = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("TechBoard"));

        if (hit == null)
        {
            return false;
        }

        if(hit.TryGetComponent<EmptyTechCell>(out var boardCell))
        {
            if (boardCell.Occupied == false)
            {
                return true;
            }
        }

        return false;
    }

    public void SetOnCell()
    {
        var hit = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("TechBoard"));
        var cell = hit.GetComponent<EmptyTechCell>();
        cell.ReceiveTechSegment(this);
    }

    public void ReceiveSample(ResearchType sample)
    {
        storedSample = sample;
        iconRenderer.sprite = sample.techPiece;
        iconRenderer.color = Color.white;
    }

    private void Update()
    {
        iconRenderer.transform.rotation = Quaternion.identity;
    }
}
