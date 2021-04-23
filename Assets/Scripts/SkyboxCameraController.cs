using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq; // For checking if items exist in array

public class SkyboxCameraController : MonoBehaviour
{
    // Gameobjects that need to be set in inspector menu
    public Dropdown skyBoxDropdown; 
    public Material skyBoxMaterial;
    public Texture skyBoxDefaultTexture;

    List<string> availableCameras;

    // Start is called before the first frame update
    void Start()
    {
        GetSavedSettings();
        GetAvailableWebCamDevices();
        UpdateSkyBoxDropdown();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetSavedSettings(){
        string cameraName = PlayerPrefs.GetString("skyboxcam", "Connect Camera");
        int dropdownIndex = skyBoxDropdown.options.FindIndex(option => option.text == cameraName);
        if (dropdownIndex != -1 && availableCameras.Contains(cameraName)){
            SetDisplayToCamera(cameraName);
            skyBoxDropdown.value = dropdownIndex;
        }

    }
    void GetAvailableWebCamDevices()
    {
        // Find Webcams
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("no camera detected");
        }
        else{
            availableCameras = devices.Select(device => device.name).ToList();
        }
    }

    void UpdateSkyBoxDropdown(){
        skyBoxDropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("No Camera");
        options.AddRange(availableCameras);
        skyBoxDropdown.AddOptions(options);
        skyBoxDropdown.onValueChanged.AddListener(delegate { DropdownTriggered();});
    }

    void SetDisplayToCamera(string cameraName){

        PlayerPrefs.SetString("skyboxcam", cameraName);
        Debug.Log("Setting skybox camera to " + cameraName);
        // RawImage rawImage = display.GetComponentInChildren(typeof(RawImage)) as RawImage;
        WebCamTexture webCamTexture = new WebCamTexture(cameraName, Screen.width, Screen.height);
        webCamTexture.Play();

        skyBoxDefaultTexture = webCamTexture;    
    }

    void DropdownTriggered(){
        SetDisplayToCamera(skyBoxDropdown.options[skyBoxDropdown.value].text);
    }
}
