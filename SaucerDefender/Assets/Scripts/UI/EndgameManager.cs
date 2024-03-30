using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameManager : ManagerBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject fullCanvas;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private ScriptableSignal OnTabletDiscovered;

    private bool won;
    private int counter;


    private void Start()
    {
        OnTabletDiscovered.Register(RaiseCounter);
    }

    public void DoGameOver()
    {
        won = counter == 3? true : false;
        fullCanvas.SetActive(true);
        transition.SetTrigger("Open");
    }

    public void ActivatePanel()
    {
        if (won) winPanel.SetActive(true);
        else GameOverPanel.SetActive(false);
    }

    private void RaiseCounter()
    {
        counter++;
    }
}
