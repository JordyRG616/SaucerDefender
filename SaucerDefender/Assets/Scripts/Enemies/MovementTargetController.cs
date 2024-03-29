using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTargetController : MonoBehaviour
{
    private RadialMovement radialMovement;
    private AiMovementController movementController;

    private Transform campsite;


    private void Start()
    {
        campsite = FindObjectOfType<CampsiteModule>().transform;

        radialMovement = GetComponent<RadialMovement>();
        movementController = GetComponent<AiMovementController>();
    }

    private void Update()
    {
        var pos = radialMovement.GetAngleFromPosition(campsite.position);
        movementController.SetTarget(pos);
    }
}
