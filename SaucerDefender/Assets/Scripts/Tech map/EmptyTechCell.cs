using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTechCell : MonoBehaviour
{
    private Dictionary<Vector2, EmptyTechCell> connectedCells = new Dictionary<Vector2, EmptyTechCell>();
    private Vector3[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    private TechAreaCell areaCell;
    private Collider2D coll;
    private TechBoard board;

    public Vector3 worldPosition => transform.position;
    public bool Occupied { get; private set; }


    private void Start()
    {
        var hit = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("TechArea"));

        if (hit != null && hit.TryGetComponent<TechAreaCell>(out var areaCell))
        {
            this.areaCell = areaCell;
        }

        coll = GetComponent<Collider2D>();
        board = FractaMaster.GetManager<TechBoard>();
        GetConnections();
    }

    private void GetConnections()
    {
        foreach (var direction in directions)
        {
            var cell = Physics2D.OverlapCircle(transform.position + direction, .1f);

            if (cell != null)
            {
                connectedCells.Add(direction, cell.GetComponent<EmptyTechCell>());
            }
        }
    }

    public EmptyTechCell GetConnectedCell(Vector2 direction)
    {
        if (connectedCells.ContainsKey(direction))
        {
            return connectedCells[direction];
        }

        return null;
    }

    private void OnMouseEnter()
    {
        board.SetCurrentCell(this);
    }

    public void ReceiveTechSegment(TechPieceSegment segment)
    {
        if (Occupied) return;

        if (areaCell != null)
        {
            areaCell.CheckSegmentResearch(segment);
        }

        Occupied = true;
    }

    private void OnBecameInvisible()
    {
        coll.enabled = false;
    }

    private void OnBecameVisible()
    {
        coll.enabled = true;
    }
}