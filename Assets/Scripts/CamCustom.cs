using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct CameraData
{
    public float height;   
    public float distance; 
}


public class CamCustom : MonoBehaviour
{



    [Header("TruckData")]
    public GameObject DummyTrailer;
    public GameObject Trailer;
    public GameObject linerend;
    public GameObject csHook;
    public Transform hookPoint;
    public RCC_CarControllerV3 Truck;


    public CameraData truck;
    public CameraData trailer;
    RCC_Camera cam; 

    private void Start()
    {
        cam = RCC_SceneManager.Instance.activePlayerCamera;
        ONtruckDataLoaded();
    }
    public void setcamTruck()
    {
        cam.TPSDistance = truck.distance;
        cam.TPSHeight = truck.height;
    }
    
    public void setcamTrailer()
    {
        cam.TPSDistance = trailer.distance;
        cam.TPSHeight = trailer.height;
    }

    void ONtruckDataLoaded() 
    {
        GM_Euro_Drive.instance.SetData(this);
    }
}
