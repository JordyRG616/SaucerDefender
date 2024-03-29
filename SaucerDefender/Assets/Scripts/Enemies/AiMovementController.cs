using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovementController : MonoBehaviour
{
    [Header("Signals")]
    public Signal OnTargetReached = new Signal();

    private RadialMovement movement;
    private float targetAngle;
    private bool onTarget = false;


    private void Awake()
    {
        movement = GetComponent<RadialMovement>();
    }

    public void Initialize()
    {
        targetAngle = 90f;

        movement.SetOnMaxAngle();
        movement.Frozen = false;

        movement.enabled = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        var direction = targetAngle - movement.CurrentAngle;
        direction = Mathf.Clamp(direction, -1, 1);

        if (Mathf.Abs(direction) <= 0.01f && !onTarget)
        {
            OnTargetReached.Fire();
            onTarget = true;
            movement.Frozen = true;
        } 

        movement.Move(direction * Time.deltaTime);
    }

    public void SetTarget(float target)
    {
        targetAngle = target;
        onTarget = false;
        movement.Frozen = false;
    }
}
