using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private InputMap inputMap;
    [Space]
    [SerializeField] private Transform ship;
    [SerializeField] private float strafeLimit;
    [SerializeField] private float strafeSpeed;
    [Space]
    [SerializeField] private ParticleSystem beam;

    private RadialMovement movement;
    private Camera cam;
    private float refVelocity;
    private float angle;
    private Vector3 dir;


    private void Start()
    {
        cam = Camera.main;

        movement = GetComponent<RadialMovement>();
        movement.SetAngle(90);
        movement.Frozen = false;

        inputMap.Initialize();

        inputMap.Planet_Fire.performed += FireBeam;
        inputMap.Planet_Fire.canceled += StopBeam;
    }

    private void FireBeam(InputAction.CallbackContext obj)
    {
        beam.Play();
    }

    private void StopBeam(InputAction.CallbackContext obj)
    {
        beam.Stop();
    }


    private void Update()
    {
        var direction = inputMap.Planet_Move.ReadValue<float>();
        movement.Move(direction * Time.deltaTime);

        RotateWeapon();
    }

    private void RotateWeapon()
    {
        var pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        pos.z = 0;
        dir = pos - transform.position;

        angle = Vector2.SignedAngle(-transform.up, dir);
        angle = Mathf.Clamp(angle, -strafeLimit, strafeLimit);
        ship.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ship.position, dir);
    }
}
