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


    private void Start()
    {
        attackWait = new WaitForSeconds(attackCooldown);
        radialMovement = GetComponent<RadialMovement>();
    }

    public void StopAndAttack(HealthModule campsiteHealth)
    {
        radialMovement.enabled = false;
        animator.SetBool("Stopped", true);
        StartCoroutine(Attack(campsiteHealth));
    }

    private IEnumerator Attack(HealthModule healthModule)
    {
        while(gameObject.activeSelf)
        {
            healthModule.CurrentHealth -= Damage;
            animator.SetTrigger("Attacking");

            yield return attackWait;
        }
    }
}
