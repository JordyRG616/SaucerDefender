using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampsiteModule : MonoBehaviour
{
    [Header("Signals")]
    public Signal OnResearchSiteReached = new Signal();

    [SerializeField] private Animator animator;

    public int EnemiesInRange => coll.GetContacts(contacts);

    private ContactPoint2D[] contacts = new ContactPoint2D[99];
    private AiMovementController movementController;
    private HealthModule healthModule;
    private Collider2D coll;


    private void Awake()
    {
        OnResearchSiteReached += SetIdleState;

        coll = GetComponent<Collider2D>();

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
        animator.SetBool("Moving", true);
    }

    private void SetIdleState()
    {
        animator.SetBool("Moving", false);
    }
}
