using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.UI;

public class PassThroughManager : MonoBehaviour
{
    public Camera mainCamera;
    public Button btnSwitch;

    private bool keepFlase = false;
    private bool nowState = false;

    private void Start()
    {
        btnSwitch.onClick.AddListener(() =>
        {
            if (nowState)
            {
                keepFlase = true;
                SetThroughSwitch(false);
            }
            else
            {
                keepFlase = false;
                SetThroughSwitch(true);
            }
        });

    }
    private void Update()
    {
        if (!keepFlase && !nowState) SetThroughSwitch(true);
    }

    private void OnApplicationPause(bool pause)
    {
        SetThroughSwitch(false);
    }

    public void SetThroughSwitch(bool flag)
    {
        PXR_Boundary.EnableSeeThroughManual(flag);
        nowState = flag;
    }

    public void SetSeeThroughPercent(float value)
    {
        var color = mainCamera.backgroundColor;
        color.a = value;
        mainCamera.backgroundColor = color;
    }

    public void SetBackgroundColor(Color color)
    {
        color.a = mainCamera.backgroundColor.a;
        mainCamera.backgroundColor = color;
    }
}
