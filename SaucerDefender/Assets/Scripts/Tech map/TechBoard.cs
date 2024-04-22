using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBoard : ManagerBehaviour
{
    [SerializeField] private InputMap inputMap;
    [Space]
    [SerializeField] private EmptyTechCell initialCell;
    [SerializeField] private Transform selection;

    private EmptyTechCell currentCell;


    private void Start()
    {
        currentCell = initialCell;
        selection.transform.position = initialCell.worldPosition;
        inputMap.InitializeBoardControls();
        inputMap.OnBoardNavigation += MoveSelection;
    }

    private void MoveSelection(Vector2 direction)
    {
        var cell = currentCell.GetConnectedCell(direction);

        if (cell != null)
        {
            SetCurrentCell(cell);
        }
    }

    public void SetCurrentCell(EmptyTechCell cell)
    {
        selection.transform.position = cell.worldPosition;
        currentCell = cell;
    }
}
