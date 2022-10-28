using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValue : MonoBehaviour
{
    public enum ShowType
    {
        Int,
        Float,
        Percent
    }

    public Text textValue;
    [Space]
    public string prefix = "";
    public string suffix = "";
    [Space]
    public ShowType showType = ShowType.Float;

    void Start()
    {
        var slider = GetComponent<Slider>();
        OnValueChanged(slider.value);
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(float value)
    {
        string str;
        switch (showType)
        {
            case ShowType.Int:
                str = Mathf.FloorToInt(value).ToString();
                break;
            case ShowType.Percent:
                str = Mathf.FloorToInt(value * 100).ToString();
                break;
            default:
                str = value.ToString();
                break;
        }
        textValue.text = $"{prefix}{str}{suffix}";
    }
}
