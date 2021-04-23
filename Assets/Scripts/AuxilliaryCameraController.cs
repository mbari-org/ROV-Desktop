using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq; 

public class AuxCamDisplay{

    Dropdown dropdown;
    GameObject display;
    int index;
    List<string> availableCameras;
    bool active;
    Image settingsIcon;
    List<WebCamTexture> webCamTextureList;

    public AuxCamDisplay(int _index, GameObject _display, Dropdown _dropdown, 
    List<string> _availableCameras, Image _settingsIcon, List<WebCamTexture> _webCamTextureList){
        display = _display;
        dropdown = _dropdown;
        availableCameras = _availableCameras;
        index = _index;
        settingsIcon = _settingsIcon;
        webCamTextureList = _webCamTextureList;

        UpdateDropdown();
        GetSavedSettings();

    }
    void GetSavedSettings(){
        string cameraName = PlayerPrefs.GetString("auxcam"+index, "Connect Camera");
        int dropdownIndex = dropdown.options.FindIndex(option => option.text == cameraName);
        if (dropdownIndex != -1 && availableCameras.Contains(cameraName)){
            SetDisplayToCamera(cameraName);
            dropdown.value = dropdownIndex;
            DisplayIsShown(true);
        }
        else{
            DisplayIsShown(false);
        }
    }

    void UpdateDropdown(){
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("No Camera");
        options.AddRange(availableCameras);
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(delegate { DropdownTriggered();});
    }
    
    void SetDisplayToCamera(string cameraName){
    if (cameraName != "No Camera" && !active){
        DisplayIsShown(true);
    }
    else if (cameraName == "No Camera"){
        DisplayIsShown(false);
    }
    PlayerPrefs.SetString("auxcam"+index, cameraName);
    Debug.Log("Setting camera to " + cameraName);
    RawImage rawImage = display.GetComponentInChildren(typeof(RawImage)) as RawImage;
    webCamTextureList.Where(texture => texture.deviceName == cameraName).ToList()[0].Play();
    rawImage.texture = webCamTextureList.Where(texture => texture.deviceName == cameraName).ToList()[0];    
    }

    void DropdownTriggered(){
        SetDisplayToCamera(dropdown.options[dropdown.value].text);
    }

    void DisplayIsShown(bool _active){
        active = _active;
        display.SetActive(_active);
        if (_active){
            settingsIcon.color = new Color32(255,255,255,255);
        }else{
            settingsIcon.color = new Color32(155,155,155,255);
        }

    }

}



public class AuxilliaryCameraController : MonoBehaviour
{   
    AuxCamDisplay auxCamDisplay1;
    public GameObject auxCamDisplayGameObject1;
    public Dropdown auxCamDropDown1;
    public Image auxCamSettingsIcon1;

    AuxCamDisplay auxCamDisplay2;
    public GameObject auxCamDisplayGameObject2;
    public Dropdown auxCamDropDown2;
    public Image auxCamSettingsIcon2;


    AuxCamDisplay auxCamDisplay3;
    public GameObject auxCamDisplayGameObject3;
    public Dropdown auxCamDropDown3;
    public Image auxCamSettingsIcon3;
    
    AuxCamDisplay auxCamDisplay4;
    public GameObject auxCamDisplayGameObject4;
    public Dropdown auxCamDropDown4;
    public Image auxCamSettingsIcon4;
    
    AuxCamDisplay auxCamDisplay5;
    public GameObject auxCamDisplayGameObject5;
    public Dropdown auxCamDropDown5;
    public Image auxCamSettingsIcon5;
    
    AuxCamDisplay auxCamDisplay6;
    public GameObject auxCamDisplayGameObject6;
    public Dropdown auxCamDropDown6;
    public Image auxCamSettingsIcon6;

    private List<string> availableCameras = new List<string>();
    private List<WebCamTexture> webCamTextureList = new List<WebCamTexture>();

    // Start is called before the first frame update
    void Start()
    {
        GetAvailableWebCamDevices();
        auxCamDisplay1 = new AuxCamDisplay(1, auxCamDisplayGameObject1, auxCamDropDown1, availableCameras, auxCamSettingsIcon1,  webCamTextureList);
        auxCamDisplay2 = new AuxCamDisplay(2, auxCamDisplayGameObject2, auxCamDropDown2, availableCameras, auxCamSettingsIcon2,  webCamTextureList);
        auxCamDisplay3 = new AuxCamDisplay(3, auxCamDisplayGameObject3, auxCamDropDown3, availableCameras, auxCamSettingsIcon3,  webCamTextureList);
        auxCamDisplay4 = new AuxCamDisplay(4, auxCamDisplayGameObject4, auxCamDropDown4, availableCameras, auxCamSettingsIcon4,  webCamTextureList);
        auxCamDisplay5 = new AuxCamDisplay(5, auxCamDisplayGameObject5, auxCamDropDown5, availableCameras, auxCamSettingsIcon5,  webCamTextureList);
        auxCamDisplay6 = new AuxCamDisplay(6, auxCamDisplayGameObject6, auxCamDropDown6, availableCameras, auxCamSettingsIcon6, webCamTextureList);

    }

    // Update is called once per frame
    void Update(){
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
            webCamTextureList = devices.Select(device => new WebCamTexture(device.name, Screen.width, Screen.height)).ToList();

        }

    }
}
