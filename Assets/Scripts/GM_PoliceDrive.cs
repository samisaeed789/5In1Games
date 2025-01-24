using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CarType
{
    Regular,
    Ford,
    Swat,
    None,
}
public class GM_PoliceDrive : MonoBehaviour
{

    [SerializeField] GameObject CarSel;
    [SerializeField] GameObject GP;
    [SerializeField] GameObject[] Cars;
    [SerializeField] MiniMapController map;
    [SerializeField]public GameObject LockedBtn;
    [SerializeField]public GameObject UnLockedBtn;


    public static GM_PoliceDrive instance;

    private CarType currentCarType = CarType.None;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        ValStorage.SetCarUnLocked(CarType.Swat);
        ValStorage.SetCarUnLocked(CarType.Regular);
    }
    public void SetCarType(CarType carType)
    {
        currentCarType = carType;

     
    }

    public CarType GetCurrentCarType()
    {
        return currentCarType;
    }
    public void SetGP() 
    {
        GP.SetActive(true);
        CarSel.SetActive(false);

        CarType car = GetCurrentCarType();
        switch (car)
        {
            case CarType.Regular:
                Cars[0].SetActive(true);
                map.target = Cars[0].transform;
                break;
            case CarType.Ford:
                Cars[2].SetActive(true);
                map.target = Cars[2].transform;
                break;
            case CarType.Swat:
                Cars[1].SetActive(true);
                map.target = Cars[1].transform;
                break;
            default:
                break;
        }
    }

    public void UnlockCar() 
    {
        CarType car = GetCurrentCarType();
        ValStorage.SetCarUnLocked(car);
        SetGP();
    }
}
