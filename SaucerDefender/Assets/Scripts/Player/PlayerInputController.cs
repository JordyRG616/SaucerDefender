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
    [SerializeField] private SFXPlayer laserSFX;
    [SerializeField] private SFXPlayer shipMoveSFX;
    [Header("Upgrades")]
    [SerializeField] private ScriptableSignal movementSpeed;
    [SerializeField] private float speedIncrease;
    [Space]
    [SerializeField] private ScriptableSignal laserDamage;
    [SerializeField] private float damageIncrease;
    [Space]
    [SerializeField] private ScriptableSignal rotationLimit;
    [SerializeField] private int rotationIncrease;
    [Space]
    [SerializeField] private ScriptableSignal laserSpeed;
    [SerializeField] private float laserSpeedIncrease;

    private FMOD.Studio.EventInstance laserSfxInstance;
    private FMOD.Studio.EventInstance moveSfxInstance;
    private bool playingMove = false;

    private RadialMovement movement;
    private float shootingSpeed = .25f;
    private float angle;
    private Vector3 dir;
    private Camera cam;


    private void Start()
    {
        cam = Camera.main;

        movement = GetComponent<RadialMovement>();
        movement.SetAngle(90);
        movement.Frozen = false;

        inputMap.Initialize();

        inputMap.Planet_Fire.performed += FireBeam;
        inputMap.Planet_Fire.canceled += StopBeam;

        laserSfxInstance = laserSFX.GetInstance();
        moveSfxInstance = shipMoveSFX.GetInstance();

        movementSpeed.Register(IncreaseSpeed);
        laserDamage.Register(IncreaseDamage);
        rotationLimit.Register(IncreaseRotation);
        laserSpeed.Register(IncreaseLaserSpeed);
    }

    private void FireBeam(InputAction.CallbackContext obj)
    {
        beam.Play();
        movement.SpeedModifier = shootingSpeed;
        laserSfxInstance.start();
    }

    private void StopBeam(InputAction.CallbackContext obj)
    {
        beam.Stop();
        movement.SpeedModifier = 1f;
        laserSfxInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    private void Update()
    {
        var direction = inputMap.Planet_Move.ReadValue<float>();

        var abs = Mathf.Abs(direction);
        if(abs > .15f && !playingMove)
        {
            moveSfxInstance.start();
            playingMove = true;
        } else if(abs < .15f && playingMove)
        {
            moveSfxInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            playingMove = false;
        }

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

    private void IncreaseSpeed()
    {
        movement.RaiseSpeed(speedIncrease);
    }

    private void IncreaseRotation()
    {
        strafeLimit += rotationIncrease;
    }

    private void IncreaseDamage()
    {
        var damageDealer = beam.GetComponent<ParticleSystemDamageModule>();
        damageDealer.RaiseDamage(damageIncrease);
    }

    private void IncreaseLaserSpeed()
    {
        shootingSpeed += laserSpeedIncrease;
    }
}
