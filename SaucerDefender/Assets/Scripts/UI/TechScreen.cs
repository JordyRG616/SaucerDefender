using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TechScreen : MonoBehaviour
{
    [SerializeField] private InputMap inputMap;
    [SerializeField] private List<TechTree> techOptions;
    [Space]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterRole;
    [SerializeField] private Image characterVisual;

    private TechTree currentTree;
    private int _index;
    private int treeIndex
    {
        get => _index;
        set
        {
            if (value < 0) _index = techOptions.Count - 1;
            else if (value >= techOptions.Count) _index = 0;
            else _index = value;
        }
    }


    private void Start()
    {
        currentTree = techOptions[0];
        ReceiveTree(currentTree);
        
        gameObject.SetActive(false);
    }

    private void ReceiveTree(TechTree tree)
    {
        characterName.text = tree.character.name;
        characterRole.text = tree.character.role;
        characterVisual.sprite = tree.character.visual;

        currentTree.techs.SetActive(false);
        tree.techs.SetActive(true);
        currentTree = tree;
    }

    private void Update()
    {
        inputMap.SetPlanetControls(false);
    }

    public void ChangeTree(int direction)
    {
        treeIndex -= direction;
        ReceiveTree(techOptions[treeIndex]);
    }
}

[System.Serializable]
public struct TechTree
{
    public CharacterData character;
    public GameObject techs;
}