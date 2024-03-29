using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlanetSelectionPanel : MonoBehaviour
{
    [Header("Signals")]
    public Signal<PlanetData> OnPlanetSelected = new Signal<PlanetData>();

    [SerializeField] private List<PlanetData> planets;
    [Space]
    [SerializeField] private TextMeshProUGUI planetName;
    [SerializeField] private TextMeshProUGUI planetDescription;
    [SerializeField] private Image planetVisual;
    [Space]
    [SerializeField] private List<ResearchTypeDisplay> researchTypeDisplays;

    private int _index;
    private int planetIndex
    {
        get => _index;
        set
        {
            if (value < 0) _index = planets.Count - 1;
            else if (value >= planets.Count) _index = 0;
            else _index = value;
        }
    }


    private void Start()
    {
        ReceivePlanetData(planetIndex);
    }

    private void ReceivePlanetData(int index)
    {
        var data = planets[index];

        planetName.text = data.name;
        planetDescription.text = data.planetDescription;
        planetVisual.sprite = data.visual;

        researchTypeDisplays.ForEach(x => x.box.SetActive(false));
        for (int i = 0; i < data.researchTypes.Count; i++)
        {
            var display = researchTypeDisplays[i];
            display.icon.sprite = data.researchTypes[i].icon;
            display.box.SetActive(true);
        }
    }

    public void ChangePlanet(int direction)
    {
        planetIndex += direction;
        ReceivePlanetData(planetIndex);
    }

    public void SelectPlanet()
    {
        OnPlanetSelected.Fire(planets[planetIndex]);
    }

    public PlanetData GetPlanetData(int index)
    {
        return planets[index];
    }
}

[System.Serializable]
public struct ResearchTypeDisplay
{
    public GameObject box;
    public Image icon;
}