using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelStats : MonoBehaviour
{


    [Header("LvlData")]
    public GameObject DummyTrailer;
    public GameObject Trailer;
    public GameObject linerend;
    public GameObject csHook;
    public Transform hookPoint;
    public RCC_CarControllerV3 Truck;


    private void Awake()
    {
        Debug.LogError("called");
        ONtruckDataLoaded();
    }

    void ONtruckDataLoaded()
    {
        GM_Euro_Drive.instance.SetData(this);
    }


}
