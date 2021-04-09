using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

#pragma warning disable 0649

public class InformationPanelController : MonoBehaviour
{
    [SerializeField]
    GameObject listenerGameObject;

    private LCMListener listener;

    [SerializeField]
    TextMeshProUGUI depth;
    [SerializeField]
    TextMeshProUGUI pressure;
    [SerializeField]
    TextMeshProUGUI roll;    
    [SerializeField]
    TextMeshProUGUI pitch;    
    [SerializeField]
    TextMeshProUGUI yaw;
    [SerializeField]
    TextMeshProUGUI turns;
    [SerializeField]
    TextMeshProUGUI rov_lat;
    [SerializeField]
    TextMeshProUGUI rov_lon;
    [SerializeField]
    TextMeshProUGUI clump_lat;
    [SerializeField]
    TextMeshProUGUI clump_lon;
    [SerializeField]
    TextMeshProUGUI clump_delta;
    // Start is called before the first frame update
    void Start()
    {
        listener = listenerGameObject.GetComponent<LCMListener>();
    }

    // Update is called once per frame
    void Update()
    {
        depth.SetText(Math.Round(listener.Depth, 3).ToString() + "m");
        pressure.SetText(Math.Round(listener.Pressure, 3).ToString());

        roll.SetText(Math.Round(listener.Roll, 3).ToString());
        pitch.SetText(Math.Round(listener.Pitch, 3).ToString());
        yaw.SetText(Math.Round(listener.Yaw, 3).ToString());

        turns.SetText(Math.Round(listener.Turns, 3).ToString());

        rov_lat.SetText(Math.Round(listener.ROVLat, 3).ToString());
        rov_lon.SetText(Math.Round(listener.ROVLon, 3).ToString());

        clump_lat.SetText(Math.Round(listener.ClumpLat, 3).ToString());
        clump_lon.SetText(Math.Round(listener.ClumpLon, 3).ToString());


        clump_lat.SetText(Math.Round(listener.ClumpLat, 3).ToString());
        clump_lon.SetText(Math.Round(listener.ClumpLon, 3).ToString());

        clump_delta.SetText(Math.Round(listener.ClumpDelta, 3).ToString());


    }
}