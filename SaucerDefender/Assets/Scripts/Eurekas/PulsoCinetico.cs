using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsoCinetico : MonoBehaviour
{
    [SerializeField] private int initialDamage;
    [Space]
    [SerializeField] private ScriptableSignal unlockedSignal;
    [SerializeField] private ScriptableSignal upgradeSignal;
    [SerializeField] private int damageIncrease;

    public int DamageInReturn { get; private set; }  = 0;


    private void Start()
    {
        unlockedSignal.Register(Unlock);
        upgradeSignal.Register(IncreaseDispersion);
    }

    private void Unlock()
    {
        DamageInReturn = initialDamage;
    }

    private void IncreaseDispersion()
    {
        DamageInReturn += damageIncrease;
    }
}
