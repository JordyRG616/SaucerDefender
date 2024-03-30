using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(menuName = "Data/SFX")]
public class SFXPlayer : ScriptableObject
{
    public EventReference sfx;
    public bool muted;

    public void Play()
    {
        if (muted) return;
        AudioManager.PlayOneshot(sfx);
    }

    public EventInstance GetInstance()
    {
        var instance = RuntimeManager.CreateInstance(sfx);
        return instance;
    }

    public void SetMute(bool mute)
    {
        muted = mute;
    }
}
