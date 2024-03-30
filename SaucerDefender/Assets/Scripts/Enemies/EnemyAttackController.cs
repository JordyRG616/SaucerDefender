using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Animator animator;
    private RadialMovement radialMovement;
    private WaitForSeconds attackWait;
    private HealthModule selfHealth;


    private void Start()
    {
        attackWait = new WaitForSeconds(attackCooldown);
        radialMovement = GetComponent<RadialMovement>();
        selfHealth = GetComponent<HealthModule>();
    }

    public void StopAndAttack(HealthModule campsiteHealth)
    {
        radialMovement.enabled = false;
        animator.SetBool("Stopped", true);
        StartCoroutine(Attack(campsiteHealth));
    }

    private IEnumerator Attack(HealthModule healthModule)
    {
        var pulsoCinetico = healthModule.GetComponent<PulsoCinetico>();

        while(gameObject.activeSelf)
        {
            healthModule.CurrentHealth -= Damage;
            selfHealth.CurrentHealth -= pulsoCinetico.DamageInReturn;
            animator.SetTrigger("Attacking");

            yield return attackWait;
        }
    }
}
