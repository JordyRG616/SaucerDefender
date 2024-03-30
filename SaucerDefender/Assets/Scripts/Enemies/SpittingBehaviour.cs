using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingBehaviour : MonoBehaviour
{
    [SerializeField] private AiMovementController spitBall;
    [SerializeField] private Vector2 timeRange;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private RadialMovement movement;
    [SerializeField] private AiMovementController movementController;
    [SerializeField] private float distanceToStop;



    private void Start()
    {
        StartCoroutine(HandleAttack());
    }

    private IEnumerator HandleAttack()
    {
        while (gameObject.activeSelf)
        {
            var timeToNextAttack = Random.Range(timeRange.x, timeRange.y);

            yield return new WaitForSeconds(timeToNextAttack);

            movement.SpeedModifier = 0;
            animator.SetTrigger("Attacking");
        }
    }

    private void Update()
    {
        if (movementController.GetDistanceFromTarget() < distanceToStop)
        {
            animator.SetBool("Stopped", true);
            movement.enabled = false;
        }
    }

    private void OnDisable()
    {
        movement.enabled = true;
    }

    public void SpawnBall()
    {
        var ball = Instantiate(spitBall, spawnPosition.position, Quaternion.identity);
        ball.Initialize();

        var move = ball.GetComponent<RadialMovement>();
        var angle = move.GetAngleFromPosition(spawnPosition.position);
        move.SetAngle(angle);
    }

    public void Walk()
    {
        movement.SpeedModifier = 1;
    }
}
