using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppearAnimation : MonoBehaviour
{
    [Tooltip("Interval between the reveal of each letter.")]
    [SerializeField] private float deltaTime;
    [SerializeField] private bool skipable = false;
    [SerializeField] private TextMeshProUGUI textRenderer;

    private float ogDeltaTime;
    private Color textColor;
    public bool playing {get; private set;}

    void Awake()
    {
        ogDeltaTime = deltaTime;
        ClearText();
    }

    public void Play()
    {
        StartCoroutine(Reveal());
    }

    public Coroutine PlayCoroutine()
    {
        return StartCoroutine(Reveal());
    }

    public IEnumerator Reveal()
    {
        textRenderer.ForceMeshUpdate();
        var textInfo = textRenderer.textInfo;
        textColor = textRenderer.color;
        ClearText();
        playing = true;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible)
            {
                var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

                for (int j = 0; j < 4; j++)
                {
                    meshInfo.colors32[charInfo.vertexIndex + j] = textColor;
                }
            }

            for (int x = 0; x < textInfo.meshInfo.Length; ++x)
            {
                var meshInfo = textInfo.meshInfo[x];
                meshInfo.mesh.colors32 = meshInfo.colors32;
                textRenderer.UpdateGeometry(meshInfo.mesh, x);
            }

            yield return new WaitForSeconds(deltaTime);
        }

        deltaTime = ogDeltaTime;
        playing = false;
    }


    private void ClearText()
    {
        textRenderer.ForceMeshUpdate();
        var textInfo = textRenderer.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible)
            {
                var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];
                var verts = meshInfo.vertices;

                for (int j = 0; j < 4; j++)
                {
                    var vert = verts[charInfo.vertexIndex + j];
                    meshInfo.colors32[charInfo.vertexIndex + j] = Color.clear;
                }
            }
        }

        for (int x = 0; x < textInfo.meshInfo.Length; ++x)
        {
            var meshInfo = textInfo.meshInfo[x];
            meshInfo.mesh.colors32 = meshInfo.colors32;
            textRenderer.UpdateGeometry(meshInfo.mesh, x);
        }
    }

    public void SetText(string text)
    {
        ClearText();
        textRenderer.text = text;
    }

    void Update()
    {
        if(skipable && playing && Input.anyKeyDown)
        {
            deltaTime = 0;
        }
    }
}