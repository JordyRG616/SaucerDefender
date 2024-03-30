using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(menuName ="Input Map")]
public class InputMap : ScriptableObject
{
    private PlayerControls controls;

    public InputAction Planet_Move { get; private set; }
    public InputAction Planet_Fire { get; private set; }
    public InputAction Planet_Strafe { get; private set; }
    public InputAction Planet_Jump { get; private set; }


    public void Initialize()
    {
        controls = new PlayerControls();

        Planet_Move = controls.Planet.Move;
        Planet_Move.Enable();

        Planet_Fire = controls.Planet.Fire;
        Planet_Fire.Enable();

        Planet_Strafe = controls.Planet.Strafe;
        Planet_Strafe.Enable();

        Planet_Jump = controls.Planet.Jump;
    }

    public void SetPlanetControls(bool enabled)
    {
        if(enabled)
        {
            controls.Planet.Enable();
        } else
        {
            controls.Planet.Disable();
        }
    }
}
