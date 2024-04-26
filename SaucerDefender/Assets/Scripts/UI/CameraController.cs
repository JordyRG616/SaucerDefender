using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ManagerBehaviour
{
    [SerializeField] private InputMap inputMap;
    [Space]
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 minWorldBoundaries;
    [SerializeField] private Vector2 maxWorldBoundaries;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private Vector2 zoomRange;

    private Vector2 Boundaries
    {
        get
        {
            return Vector2.Lerp(minWorldBoundaries, maxWorldBoundaries, normalizedZoom);
        }
    }

    private Camera cam;
    private float zoom;
    private float normalizedZoom;
    private Vector3 initialPos;
    private Vector3 refSpeed;


    private void Start()
    {
        initialPos = transform.position;
        cam = GetComponent<Camera>();
        //DoZoom();
    }

    private Vector3 GetDirection()
    {
        var x = inputMap.CameraMap.HorizontalMove.ReadValue<float>();
        var y = inputMap.CameraMap.VerticalMove.ReadValue<float>();

        return new(x, y);
    }

    private void MoveCamera()
    {
        var pos = transform.position + (GetDirection().normalized * movementSpeed);
        pos.x = Mathf.Clamp(pos.x, -Boundaries.x, Boundaries.x);
        pos.y = Mathf.Clamp(pos.y, -Boundaries.y, Boundaries.y);

        pos = Vector3.SmoothDamp(transform.position, pos, ref refSpeed, .1f);
        transform.position = pos;
    }

    private void DoZoom()
    {
        if (inputMap.CameraMap.Zoom.enabled == false) return;

        var value = -inputMap.CameraMap.Zoom.ReadValue<Vector2>().normalized.y;
        zoom = cam.orthographicSize + (value * zoomSpeed);
        zoom = Mathf.Clamp(zoom, zoomRange.x, zoomRange.y);
        normalizedZoom = Mathf.InverseLerp(zoomRange.x, zoomRange.y, zoom);

        cam.orthographicSize = zoom;
    }

    private void LateUpdate()
    {
        MoveCamera();
        //DoZoom();
    }

    public void ToInitialPosition()
    {
        transform.position = initialPos;
    }
}
