using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxRadius;
    [SerializeField] private Vector2 maxAngle;

    private Vector2 currentPosition;
    private float _angle;

    public bool Frozen { get; set; } = true;
    public float CurrentAngle
    {
        get => _angle;
        private set
        {
            _angle = Mathf.Clamp(value, maxAngle.x, maxAngle.y);
        }
    }


    private void Update()
    {
        if (Frozen) return;

        CalculatePosition(CurrentAngle, maxRadius);

        transform.position = currentPosition;
        transform.up = transform.position.normalized;
    }

    private void CalculatePosition(float angle, float radius)
    {
        currentPosition.x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        currentPosition.y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
    }

    public float GetAngleFromPosition(Vector2 position)
    {
        var angle = Mathf.Atan2(position.y, position.x);
        angle *= Mathf.Rad2Deg;
        return angle;
    }

    public void Move(float direction)
    {
        CurrentAngle += direction * speed;
    }

    public void SetAngle(float angle)
    {
        CurrentAngle = angle;
    }

    public void SetOnMaxAngle()
    {
        var pos = Random.value > .5f ? maxAngle.x : maxAngle.y;
        CurrentAngle = pos;
    }
}
