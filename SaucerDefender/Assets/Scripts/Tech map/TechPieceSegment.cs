using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechPieceSegment : MonoBehaviour
{
    [field:SerializeField] public int index { get; private set; }
    [SerializeField] private SpriteRenderer iconRenderer;
    private SpriteRenderer spriteRenderer;

    public ResearchType storedSample { get; private set; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanBePlaced()
    {
        var hit = Physics2D.OverlapCircle(spriteRenderer.transform.position, .1f, LayerMask.GetMask("TechBoard"));

        if (hit == null)
        {
            spriteRenderer.color = Color.red;
            return false;
        }

        var cell = hit.GetComponent<EmptyTechCell>();

        if (cell.Occupied == false)
        {
            spriteRenderer.color = Color.green;

            return true;
        }

        spriteRenderer.color = Color.red;
        return false;
    }

    public void SetOnCell()
    {
        var hit = Physics2D.OverlapCircle(spriteRenderer.transform.position, .1f, LayerMask.GetMask("TechBoard"));
        var cell = hit.GetComponent<EmptyTechCell>();
        cell.ReceiveTechSegment(this);
        spriteRenderer.color = Color.green;
    }

    public void ReceiveSample(ResearchType sample)
    {
        storedSample = sample;
        iconRenderer.sprite = sample.icon;
        iconRenderer.color = Color.white;
    }

    private void Update()
    {
        iconRenderer.transform.rotation = Quaternion.identity;
    }
}
