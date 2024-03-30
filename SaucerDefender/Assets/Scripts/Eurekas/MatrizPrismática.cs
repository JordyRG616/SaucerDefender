using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrizPrismática : MonoBehaviour
{
    [SerializeField] private Transform redBeam;
    [SerializeField] private Transform blueBeam;
    [Space]
    [SerializeField] private ScriptableSignal unlockedSignal;
    [SerializeField] private ScriptableSignal upgradeSignal;
    [SerializeField] private float angleIncrease;


    private void Start()
    {
        unlockedSignal.Register(Unlock);
        upgradeSignal.Register(IncreaseDispersion);
    }

    private void Unlock()
    {
        redBeam.gameObject.SetActive(true);
        blueBeam.gameObject.SetActive(true);
    }

    private void IncreaseDispersion()
    {
        var angle = redBeam.eulerAngles.z;
        angle -= angleIncrease;
        redBeam.localRotation = Quaternion.Euler(0, 0, angle);

        angle = blueBeam.eulerAngles.z;
        angle += angleIncrease;
        blueBeam.localRotation = Quaternion.Euler(0, 0, angle);
        
    }
}
