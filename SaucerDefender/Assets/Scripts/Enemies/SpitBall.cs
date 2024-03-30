using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBall : MonoBehaviour
{
    [SerializeField] private int damage;


    private void Start()
    {
        GetComponent<HealthModule>().OnDeath += Destroy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CampsiteModule>(out var campsiteModule))
        {
            var health = campsiteModule.GetComponent<HealthModule>();
            health.CurrentHealth -= damage;
            Destroy(gameObject);
        }
    }
}
