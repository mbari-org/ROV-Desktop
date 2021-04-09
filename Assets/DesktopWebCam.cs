using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq; // For checking if items exist in array

public class DesktopWebCam : MonoBehaviour
{
    public static int numDisplays = 6;

    private bool camAvailable;

    // Lists used to update aspect ratios
    private List<int> waitingCamList = new List<int>();
    private List<int> waitingDisplayList = new List<int>();

    // List of webcam textures
    private List<WebCamTexture> camList = new List<WebCamTexture>();

    // Gameobjects that need to be set in inspector menu
    public Dropdown skyBoxDropdown; 
    public Material skyBoxMaterial;
    public Texture skyBoxDefaultTexture;
    public GameObject display1;
    public GameObject display2;
    public GameObject display3;
    public GameObject display4;
    public GameObject display5;
    public GameObject display6;
    public Toggle toggleCam1;
    public Toggle toggleCam2;
    public Toggle toggleCam3;
    public Toggle toggleCam4;
    public Toggle toggleCam5;
    public Toggle toggleCam6;

    // List of display gameobjects and their components
    private GameObject[] displayArray = new GameObject[numDisplays];
    private RawImage[] backgroundArray = new RawImage[numDisplays];
    private AspectRatioFitter[] aspectRatioArray = new AspectRatioFitter[numDisplays];
    private Dropdown[] dropdownArray = new Dropdown[numDisplays];
    //private Toggle[] toggleArray = new Toggle[numDisplays]; //is this needed? will it make the functions simpler?

    // List containing what camera index is displayed on each display
    // -1 if display is inactive
    private int[] displayCameraIdxs = new int[numDisplays];
    private int skyboxCameraIdx = -1; // Initialize skybox camera idx value

    // For dropdown menu
    List<string> cameraNameList = new List<string>();


    void Start()
    {
        // Save displays
        displayArray[0] = display1;
        displayArray[1] = display2;
        displayArray[2] = display3;
        displayArray[3] = display4;
        displayArray[4] = display5;
        displayArray[5] = display6;

        for (int i = 0; i < numDisplays; i++)
        {
            backgroundArray[i] = displayArray[i].GetComponentInChildren(typeof(RawImage)) as RawImage;
            aspectRatioArray[i] = displayArray[i].GetComponentInChildren(typeof(AspectRatioFitter)) as AspectRatioFitter;
            dropdownArray[i] = displayArray[i].GetComponentInChildren(typeof(Dropdown)) as Dropdown;

            // Initialize all displays to inactive state 
            displayCameraIdxs[i] = -1;
        }

        // Find Webcams
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("no camera detected");
            camAvailable = false;
            return;
        }

        // Update list of devices
        cameraNameList.Add("[Select Camera]");
        for (int i = 0; i < devices.Length; i++)
        {
            // Save camera name
            cameraNameList.Add((string)devices[i].name);

            // Create texture for each camera
            camList.Add(new WebCamTexture(devices[i].name, Screen.width, Screen.height));
        }

        // Update dropdown options
        skyBoxDropdown.ClearOptions();
        skyBoxDropdown.AddOptions(cameraNameList);
        for (int i = 0; i < numDisplays; i++)
        {
            dropdownArray[i].ClearOptions();
            dropdownArray[i].AddOptions(cameraNameList);
        }
        // TODO: Insert this in the above loop without things breaking
        skyBoxDropdown.onValueChanged.AddListener(delegate { DisplayCamera(skyBoxDropdown, (int)-1, devices); });
        dropdownArray[0].onValueChanged.AddListener(delegate { DisplayCamera(dropdownArray[0], (int)0, devices); });
        dropdownArray[1].onValueChanged.AddListener(delegate { DisplayCamera(dropdownArray[1], (int)1, devices); });
        dropdownArray[2].onValueChanged.AddListener(delegate { DisplayCamera(dropdownArray[2], (int)2, devices); });
        dropdownArray[3].onValueChanged.AddListener(delegate { DisplayCamera(dropdownArray[3], (int)3, devices); });
        dropdownArray[4].onValueChanged.AddListener(delegate { DisplayCamera(dropdownArray[4], (int)4, devices); });
        dropdownArray[5].onValueChanged.AddListener(delegate { DisplayCamera(dropdownArray[5], (int)5, devices); });

        //Initiate WebCam toggles
        toggleCam1.onValueChanged.AddListener(delegate
        { ToggleCallback1(toggleCam1); });
        toggleCam2.onValueChanged.AddListener(delegate
        { ToggleCallback2(toggleCam2); });
        toggleCam3.onValueChanged.AddListener(delegate
        { ToggleCallback3(toggleCam3); });
        toggleCam4.onValueChanged.AddListener(delegate
        { ToggleCallback4(toggleCam4); });
        toggleCam5.onValueChanged.AddListener(delegate
        { ToggleCallback5(toggleCam5); });
        toggleCam6.onValueChanged.AddListener(delegate
        { ToggleCallback6(toggleCam6); });

        camAvailable = true;
    }

    void DisplayCamera(Dropdown dropdown, int displayIdx, WebCamDevice[] devices)
    {
        // Reset display if [Select Camera] is chosen
        if (dropdown.value == 0)
        {
            int prevCamIdx;

            if (displayIdx == -1) // Skybox camera feed 
            {
                skyBoxMaterial.SetTexture(Shader.PropertyToID("_LTex"), skyBoxDefaultTexture);
                prevCamIdx = skyboxCameraIdx;
                skyboxCameraIdx = -1;
            } 
            else // Non-skybox camera feed
            {
                backgroundArray[displayIdx].texture = null;
                prevCamIdx = displayCameraIdxs[displayIdx];
                displayCameraIdxs[displayIdx] = -1;
            }
            // Turn off camera if no other displays are using it
            if (prevCamIdx != -1) // Only run the check for valid cameras
            {
                // Check if the camera index is not being used by either the skybox or the other displays
                if (skyboxCameraIdx != prevCamIdx && !displayCameraIdxs.Contains(prevCamIdx))
                {
                    if (camList[prevCamIdx].isPlaying)
                    {
                        camList[prevCamIdx].Stop();
                    }
                }
            }
        }

        // Update camera display if a camera is chosen
        else
        {
            int cameraIdx = dropdown.value - 1; // Skip [Select Camera] option

            if (displayIdx == -1) // Skybox camera feed
            {
                skyBoxMaterial.SetTexture(Shader.PropertyToID("_LTex"), camList[cameraIdx]);
                skyboxCameraIdx = cameraIdx;
            }
            else // Non-skybox camera feed
            {
                backgroundArray[displayIdx].texture = camList[cameraIdx];
                displayCameraIdxs[displayIdx] = cameraIdx;

                waitingCamList.Add(cameraIdx);
                waitingDisplayList.Add(displayIdx);
            }

            // Play camera if not already playing
            if (!camList[cameraIdx].isPlaying)
            {
                camList[cameraIdx].Play();
            }
        }
    }

    bool AdjustDisplayRatio(int displayIdx, int cameraIdx)
    {
        // Ensures display has correct aspect ratio and scaling
        // returns true if successful, false if not

        float width = camList[cameraIdx].width;
        float height = camList[cameraIdx].height;

        if (width > 100)
        {
            float ratio = width / height;
            aspectRatioArray[displayIdx].aspectRatio = ratio;
            return true;
        }
        else
        {
            // Invalid Ratio
            return false;
        }
        //float scaleY = camList[cameraIdx].videoVerticallyMirrored ? -1f : 1f;
        //backgroundArray[displayIdx].rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        //int orient = -camList[cameraIdx].videoRotationAngle;
        //backgroundArray[displayIdx].rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    void Update()
    {
        if (!camAvailable)
            return;

        // Update aspect ratios for un-set displays
        if (waitingCamList.Count != 0)
        {
            // Iterate backwards to ensure removal doesn't break indexing
            for (int i = waitingCamList.Count - 1; i >= 0; i--)
            {
                int displayIdx = waitingDisplayList[i];
                int cameraIdx = waitingCamList[i];

                bool success = AdjustDisplayRatio(displayIdx, cameraIdx);
                if (success)
                {
                    waitingDisplayList.RemoveAt(i);
                    waitingCamList.RemoveAt(i);
                }
            }
        }
    }
    void ToggleCallback1(Toggle toggle)
    {
        if (toggle.isOn)
            display1.SetActive(true);
        else
            display1.SetActive(false);
    }
    void ToggleCallback2(Toggle toggle)
    {
        if (toggle.isOn)
            display2.SetActive(true);
        else
            display2.SetActive(false);
    }
    void ToggleCallback3(Toggle toggle)
    {
        if (toggle.isOn)
            display3.SetActive(true);
        else
            display3.SetActive(false);
    }
    void ToggleCallback4(Toggle toggle)
    {
        if (toggle.isOn)
            display4.SetActive(true);
        else
            display4.SetActive(false);
    }
    void ToggleCallback5(Toggle toggle)
    {
        if (toggle.isOn)
        {
            display5.SetActive(true);
        }
        else
        {
            display5.SetActive(false);
        }
    }
    void ToggleCallback6(Toggle toggle)
    {
        if (toggle.isOn)
        {
            //print("turn on");
            display6.SetActive(true);
        }
        else
        {
            //print("turn off");
            display6.SetActive(false);
        }
    }
}