using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        inputMap.Board_Rotate.performed += RotateSelection;
        inputMap.Board_Flip.performed += FlipSelection;
        inputMap.Board_Place.performed += PlaceSelection;
    }

    public void ReceivePiece(TechPiece piece)
    {
        currentCell = initialCell;
        selection.transform.position = initialCell.worldPosition;

        piece.transform.SetParent(selection);
        piece.transform.localPosition = Vector3.zero;
        piece.gameObject.SetActive(true);
    }

    private void PlaceSelection(InputAction.CallbackContext obj)
    {
        if (HasSelection(out var piece))
        {
            if(piece.CanBePlaced(true))
            {
                piece.transform.SetParent(null);
            }
        }
    }

    private void FlipSelection(InputAction.CallbackContext obj)
    {
        var rotation = selection.eulerAngles;
        rotation.y += 180;

        selection.rotation = Quaternion.Euler(rotation);
    }

    private void RotateSelection(InputAction.CallbackContext obj)
    {
        var rotation = selection.eulerAngles;
        rotation.z += 90;

        selection.rotation = Quaternion.Euler(rotation);
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

    public bool HasSelection(out TechPiece piece)
    {
        piece = selection.GetComponentInChildren<TechPiece>();

        return piece != null;
    }
}
