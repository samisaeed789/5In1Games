using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Transform SpawnPoint;
    public GameObject Dance;

    private void Awake()
    {
       int  selcar = ValStorage.GetCarNumber();
       Transform SP = this.transform.GetChild(selcar - 1);
       SpawnPoint = SP;

       ONtruckDataLoaded();
    }
 
    void ONtruckDataLoaded()
    {
        if(SceneManager.GetActiveScene().name== "EuroTruckMode")
        {
            GM_Euro_Drive.instance.SetData(this);
        }

        else if (SceneManager.GetActiveScene().name == "CarDriveSchool") 
        {
            GM_CD.instance.SetData(this);
        }
    }

}
