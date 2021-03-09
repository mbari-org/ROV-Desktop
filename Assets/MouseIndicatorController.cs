using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649

public class MouseIndicatorController : MonoBehaviour
{
    [SerializeField]
    Image laserImage;
    [SerializeField]
    Image navigateImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            laserImage.color = new Color(1,1,1,1);
        }
        if (Input.GetMouseButtonUp(0)){
            laserImage.color = new Color(1,1,1,.5f);
        }
        if (Input.GetMouseButtonDown(1)){
            navigateImage.color = new Color(1,1,1,1);
        }
        if (Input.GetMouseButtonUp(1)){
            navigateImage.color = new Color(1,1,1,.5f);
        }
    }
}
