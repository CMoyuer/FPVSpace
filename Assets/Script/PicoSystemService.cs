using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.UI;

public class PicoSystemService : MonoBehaviour
{

    void Awake()
    {
        PXR_System.InitSystemService(gameObject.name);
        // PXR_System.BindSystemService();
    }

    void Start()
    {
        PXR_System.AppKeepAlive(Application.identifier, true, 0);
    }


    void Update()
    {

    }

    private void OnDestroy()
    {
        // PXR_System.UnBindSystemService();
    }


}
