using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampsiteModule : MonoBehaviour
{
    [Header("Signals")]
    public Signal OnResearchSiteReached = new Signal();

    private HealthModule healthModule;
    private AiMovementController movementController;


    private void Awake()
    {
        healthModule = GetComponent<HealthModule>();
        movementController = GetComponent<AiMovementController>();
        movementController.OnTargetReached += OnResearchSiteReached.Fire;

        var radialMovement = GetComponent<RadialMovement>();
        radialMovement.SetAngle(90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAttackController>(out var damageController))
        {
            damageController.StopAndAttack(healthModule);
        }
    }

    public void SetSiteLocation(float location)
    {
        movementController.SetTarget(location);
    }
}
