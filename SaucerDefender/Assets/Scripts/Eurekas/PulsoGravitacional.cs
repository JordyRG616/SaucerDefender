using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PulsoGravitacional : MonoBehaviour
{
    [SerializeField] private InputMap inputMap;
    [SerializeField] private float jumpRange;
    [SerializeField] private float cooldown;
    [Space]
    [SerializeField] private ScriptableSignal unlockedSignal;
    [SerializeField] private ScriptableSignal upgradeSignal;
    [SerializeField] private float jumpIncrease;

    private RadialMovement movement;
    private bool onCooldown;


    private void Start()
    {
        unlockedSignal.Register(Unlock);
        upgradeSignal.Register(IncreaseJump);

        movement = GetComponent<RadialMovement>();
    }

    private void Unlock()
    {
        inputMap.Planet_Jump.performed += Jump;
        inputMap.Planet_Jump.Enable();
    }

    private IEnumerator DoCooldown()
    {
        yield return new WaitForSeconds(cooldown);

        onCooldown = false;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (onCooldown) return;

        var angle = movement.CurrentAngle;
        var direction = inputMap.Planet_Move.ReadValue<float>();

        movement.SetAngle(angle + (direction * jumpRange));

        onCooldown = true;
        StartCoroutine(DoCooldown());
    }

    private void IncreaseJump()
    {
        jumpRange += jumpIncrease;
    }
}
