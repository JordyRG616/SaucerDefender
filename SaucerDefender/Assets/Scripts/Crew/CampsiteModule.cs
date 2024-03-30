using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampsiteModule : MonoBehaviour
{
    [Header("Signals")]
    public Signal OnResearchSiteReached = new Signal();

    [SerializeField] private Animator animator;
    [Header("Upgrades")]
    [SerializeField] private ScriptableSignal healthRegen;
    [SerializeField] private int regenIncrease;
    [Space]
    [SerializeField] private ScriptableSignal maxHealth;
    [SerializeField] private int healthIncrease;

    public int EnemiesInRange => coll.GetContacts(contacts);

    private ContactPoint2D[] contacts = new ContactPoint2D[99];
    private AiMovementController movementController;
    private HealthModule healthModule;
    private Collider2D coll;
    private int regen;


    private void Awake()
    {
        OnResearchSiteReached += SetIdleState;

        coll = GetComponent<Collider2D>();

        healthModule = GetComponent<HealthModule>();
        healthModule.OnDeath += GameOver;

        movementController = GetComponent<AiMovementController>();
        movementController.OnTargetReached += OnResearchSiteReached.Fire;

        var radialMovement = GetComponent<RadialMovement>();
        radialMovement.SetAngle(90);

        healthRegen.Register(IncreaseHealthRegen);
        maxHealth.Register(IncreaseMaxHealth);
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

        healthModule.CurrentHealth += regen;
    }

    private void SetIdleState()
    {
        animator.SetBool("Moving", false);
    }

    private void IncreaseHealthRegen()
    {
        regen += regenIncrease;
    }

    private void IncreaseMaxHealth()
    {
        healthModule.RaiseMaxHealth(healthIncrease);
    }

    private void GameOver(GameObject obj)
    {
        FractaMaster.GetManager<EndgameManager>().DoGameOver();
    }
}
