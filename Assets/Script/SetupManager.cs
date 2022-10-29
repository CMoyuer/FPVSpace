using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
    public PassThroughManager passThroughManager;
    [Space]
    public Slider sliderTransparency;
    [Space]
    public Slider sliderBackgroundColorR;
    public Slider sliderBackgroundColorG;
    public Slider sliderBackgroundColorB;

    private Color backgroundColor = Color.white;

    void Start()
    {
        // 透视率
        var seeThroughPercent = PlayerPrefs.GetFloat("SeeThroughPercent", 0.3f);
        passThroughManager.SetSeeThroughPercent(seeThroughPercent);
        sliderTransparency.value = 1 - seeThroughPercent;
        sliderTransparency.onValueChanged.AddListener((value) =>
        {
            value = 1 - value;
            passThroughManager.SetSeeThroughPercent(value);
            PlayerPrefs.SetFloat("SeeThroughPercent", value);
        });
        // 背景颜色
        var r = PlayerPrefs.GetFloat("BackgroundColorR", 0);
        var g = PlayerPrefs.GetFloat("BackgroundColorG", 0);
        var b = PlayerPrefs.GetFloat("BackgroundColorB", 0);
        backgroundColor = new Color(r, g, b);
        passThroughManager.SetBackgroundColor(backgroundColor);
        sliderBackgroundColorR.value = r;
        sliderBackgroundColorR.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("BackgroundColorR", value);
            backgroundColor.r = value;
            passThroughManager.SetBackgroundColor(backgroundColor);
        });
        sliderBackgroundColorG.value = g;
        sliderBackgroundColorG.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("BackgroundColorG", value);
            backgroundColor.g = value;
            passThroughManager.SetBackgroundColor(backgroundColor);
        });
        sliderBackgroundColorB.value = b;
        sliderBackgroundColorB.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("BackgroundColorB", value);
            backgroundColor.b = value;
            passThroughManager.SetBackgroundColor(backgroundColor);
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
