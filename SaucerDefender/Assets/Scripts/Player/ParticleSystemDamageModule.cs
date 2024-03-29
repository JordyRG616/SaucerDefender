using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDamageModule : MonoBehaviour
{
    [SerializeField] private int damage;


    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<HealthModule>(out var healthModule))
        {
            healthModule.CurrentHealth -= damage;
        } 
    }
}
