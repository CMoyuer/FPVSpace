using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelQuickMove : MonoBehaviour
{
    public Transform panel;
    [Space]
    public float deadZone = 0.1f;
    [Space]
    public float zMoveSpeed = 1.0f;
    public float maxDistance = 5;
    public float minDistance = 1;
    [Space]
    public float yMoveSpeed = 0.25f;
    public float maxHeight = 2.5f;
    public float minHeight = 0.5f;
    [Space]
    public float scaleSpeed = 0.00003f;
    public float maxScale = 0.02f;
    public float minScale = 0.002f;

    private InputDevice[] controllers = new InputDevice[2];
    void Start()
    {
        var z = PlayerPrefs.GetFloat("PanelZ", 3);
        var y = PlayerPrefs.GetFloat("PanelY", 1.7f);
        panel.localPosition = new Vector3(0, y, z);

        var scale = PlayerPrefs.GetFloat("PanelScale", 0.005f);
        panel.transform.localScale = new Vector3(scale, scale, 1);
    }

    void Update()
    {
        UpdateController();
        foreach (var controller in controllers)
        {
            if (controller == null || !controller.isValid) continue;
            // 紧握键
            if (controller.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton) && gripButton)
            {
                // 摇杆
                if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxis))
                {
                    // 远近
                    if (Mathf.Abs(primary2DAxis.y) > deadZone)
                    {
                        var position = panel.localPosition;
                        position.z += primary2DAxis.y * Time.deltaTime * zMoveSpeed;
                        position.z = Mathf.Clamp(position.z, minDistance, maxDistance);
                        panel.localPosition = position;
                        PlayerPrefs.GetFloat("PanelZ", position.z);
                    }

                    // 缩放
                    if (Mathf.Abs(primary2DAxis.x) > deadZone)
                    {
                        var scale = panel.transform.localScale.x;
                        var val = primary2DAxis.x * Time.deltaTime * scaleSpeed;
                        scale = Mathf.Clamp(scale + val, minScale, maxScale);
                        panel.transform.localScale = new Vector3(scale, scale, 1);
                        PlayerPrefs.GetFloat("PanelScale", scale);
                    }
                }
                // YB键
                if (controller.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButton) && secondaryButton)
                {
                    var position = panel.localPosition;
                    position.y += Time.deltaTime * yMoveSpeed;
                    position.y = Mathf.Clamp(position.y, minHeight, maxHeight);
                    panel.localPosition = position;
                    PlayerPrefs.GetFloat("PanelY", position.y);
                }
                else if (controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton) && primaryButton)
                {
                    var position = panel.localPosition;
                    position.y -= Time.deltaTime * yMoveSpeed;
                    position.y = Mathf.Clamp(position.y, minHeight, maxHeight);
                    panel.localPosition = position;
                    PlayerPrefs.GetFloat("PanelY", position.y);
                }
            }
        }
    }
    private void UpdateController()
    {
        if (controllers[0] == null || !controllers[0].isValid)
        {
            var devices = new List<InputDevice>();
            var characteristics_left = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(characteristics_left, devices);
            if (devices.Count > 0) controllers[0] = devices[0];
        }

        if (controllers[1] == null || !controllers[1].isValid)
        {
            var devices = new List<InputDevice>();
            var characteristics_right = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(characteristics_right, devices);
            if (devices.Count > 0) controllers[1] = devices[0];
        }
    }
}