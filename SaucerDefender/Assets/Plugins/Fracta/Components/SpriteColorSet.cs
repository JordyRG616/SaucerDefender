using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/Color set (sprite)")]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorSet : MonoBehaviour
{
    [SerializeField] private List<ColorSetEntry> colorSet;

    private SpriteRenderer image;
    private Dictionary<string, Color> colorMatrix = new Dictionary<string, Color>();

    public void Start()
    {
        image = GetComponent<SpriteRenderer>();

        foreach (var entry in colorSet)
        {
            colorMatrix.Add(entry.key, entry.color);
        }
    }

    public void Set(int index)
    {
        image.color = colorSet[index].color;
    }

    public void Set(string key)
    {
        image.color = colorMatrix[key];
    }
}
