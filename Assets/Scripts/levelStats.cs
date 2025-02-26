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

    public MeshRenderer[] brakeLight=null;
    public GameObject indiLeft=null;
    public GameObject indiRight=null;



    private void Awake()
    {
        ONtruckDataLoaded();
    }

    void ONtruckDataLoaded()
    {
        GM_Euro_Drive.instance.SetData(this);
    }


}
