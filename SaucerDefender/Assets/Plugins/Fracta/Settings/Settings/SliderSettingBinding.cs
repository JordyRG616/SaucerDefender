using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Settings/Slider Binding")]
public class SliderSettingBinding : SettingBindingBase
{
    public Signal<float> OnInitialized;
    public Signal<float> OnValueChanged;

    [Header("Limits")]
    [SerializeField] private float defaultValue;
    [field:SerializeField] public float min { get; private set; }
    [field:SerializeField] public float max { get; private set; }

    private float _value;
    public float Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, min, max);
            OnValueChanged.Fire(_value);
        }
    }

    private void OnValidate()
    {
        defaultValue = Mathf.Clamp(defaultValue, min, max);
    }

    public override void Initialize()
    {
    }
}
