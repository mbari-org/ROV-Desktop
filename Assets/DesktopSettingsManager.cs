using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
//using Unity.XR.OpenVR.SimpleJSON;//vr not needed?

//For ROV Desktop

public class DesktopSettingsManager : MonoBehaviour
{
    [Header("Settings UI")]
    public Canvas SettingsCanvas;
    public Button CloseButton;

    string saveFilePath;
    int currUserIdx = -1;
    bool prevEscBool = false;
    bool currEscBool = false;
    List<GameObject> overlayList;


    void Start()
    {
        CloseButton.onClick.AddListener(CloseCallback);

        // Close settings on startup
        SettingsCanvas.enabled = false;
    }


    void Update()
    {
        currEscBool = Input.GetKey(KeyCode.Escape);

        // Press Escape to open/close window
        if (currEscBool == true && prevEscBool == false)
        {
            SettingsCanvas.enabled = !SettingsCanvas.enabled;
        }

        prevEscBool = currEscBool;
    }

    void CloseCallback()
    {
        SettingsCanvas.enabled = false;
    }

}