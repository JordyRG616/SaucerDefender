using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [Header("Signals")]
    public Signal<HealthState> OnHealthChange = new Signal<HealthState>();
    public Signal<GameObject> OnDeath = new Signal<GameObject>();

    [SerializeField] private int maxHealth;

    private int _health;

    public int CurrentHealth
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);

            if (_health == 0) Die();
            else OnHealthChange.Fire(new HealthState(maxHealth, _health));
        }
    }


    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }

    public void Die()
    {
        OnDeath.Fire(gameObject);
    }
}

public struct HealthState
{
    public int max;
    public int current;
    public float percentage;


    public HealthState(int max, int current)
    {
        this.max = max;
        this.current = current;
        percentage = current / (float)max;
    }
}