using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_PoliceDrive : MonoBehaviour
{

    [SerializeField] GameObject CarSel;
    [SerializeField] GameObject GP;
    [SerializeField] GameObject[] Cars;
    [SerializeField] MiniMapController map;


    public static GM_PoliceDrive instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        
    }

    public void SetGP(int selCar) 
    {
        GP.SetActive(true);
        CarSel.SetActive(false);
        map.target = Cars[selCar].transform;
        Cars[selCar].SetActive(true);
    }

}
