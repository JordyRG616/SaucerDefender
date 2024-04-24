using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(menuName ="Input Map")]
public class InputMap : ScriptableObject
{
    public Signal<Vector2> OnBoardNavigation = new Signal<Vector2>();

    private PlayerControls controls;

    public InputAction Planet_Move { get; private set; }
    public InputAction Planet_Fire { get; private set; }
    public InputAction Planet_Strafe { get; private set; }
    public InputAction Planet_Jump { get; private set; }

    public InputAction Board_HorizontalNavigation { get; private set; }
    public InputAction Board_VerticalNavigation { get; private set; }
    public InputAction Board_Rotate { get; private set; }
    public InputAction Board_Flip { get; private set; }
    public InputAction Board_Place { get; private set; }

    public bool lockFireControls = false;

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

    public void SetFireControls(bool enabled)
    {
        if (lockFireControls) return;

        if (enabled)
        {
            Planet_Fire.Enable();
        }
        else
        {
            Planet_Fire.Disable();
        }
    }

    public void InitializeBoardControls()
    {
        controls = new PlayerControls();

        Board_HorizontalNavigation = controls.Board.Horizontal_Navigation;
        Board_HorizontalNavigation.performed += GetBoardDirection;
        Board_HorizontalNavigation.Enable();

        Board_VerticalNavigation = controls.Board.Vertical_Navigation;
        Board_VerticalNavigation.performed += GetBoardDirection;
        Board_VerticalNavigation.Enable();

        Board_Rotate = controls.Board.Rotate;
        Board_Rotate.Enable();

        Board_Flip = controls.Board.Flip;
        Board_Flip.Enable();

        Board_Place = controls.Board.Place;
        Board_Place.Enable();
    }

    public void GetBoardDirection(InputAction.CallbackContext callbackContext)
    {
        Vector2 direction = Vector2.zero;

        var horizontal = Mathf.RoundToInt(Board_HorizontalNavigation.ReadValue<float>());
        var vertical = Mathf.RoundToInt(Board_VerticalNavigation.ReadValue<float>());

        if(Mathf.Abs(horizontal) > 0)
        {
            direction = new Vector2(horizontal, 0);
        } else
        {
            direction = new Vector2(0, vertical);
        }

        OnBoardNavigation.Fire(direction);
    }
}
