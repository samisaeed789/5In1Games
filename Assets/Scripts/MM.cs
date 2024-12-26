using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM : MonoBehaviour
{
    [Header("SplashPanels")]
    public GameObject firstScreenPanel;
    public GameObject SecondScreenPanel;
    public GameObject carDrivingPanel;
    public GameObject euroTruckPanel;
    public GameObject offRoadJeepPanel;
    public GameObject cityBusPanel;
    public GameObject policeChasePanel;


    public void MoreDetails(string panelName)
    {
        SecondScreenPanel.SetActive(true);
        firstScreenPanel.SetActive(false);

        switch (panelName)
        {
            case "CarDrive":
                PanelActivity(isCarDrive: true);
                break;
            case "EuroTruck":
                PanelActivity(isEuroTruck: true);
                break;
            case "OffRoadJeep":
                PanelActivity(isoffRoadJeepPanel: true);
                break;
            case "CityBus":
                PanelActivity(isCityBusPanel: true);
                break;
            case "PoliceChase":
                PanelActivity(isPoliceChasePanel: true);
                break;
            default:
                break;
        }
    }

    public void PanelActivity(bool isCarDrive = false, bool isEuroTruck = false, bool isoffRoadJeepPanel = false, bool isCityBusPanel = false, bool isPoliceChasePanel = false)
    {
        if (carDrivingPanel)
        {
            carDrivingPanel.SetActive(isCarDrive);
        }
        if (euroTruckPanel)
        {
            euroTruckPanel.SetActive(isEuroTruck);
        }
        if (offRoadJeepPanel)
        {
            offRoadJeepPanel.SetActive(isoffRoadJeepPanel);
        }
        if (cityBusPanel)
        {
            cityBusPanel.SetActive(isCityBusPanel);
        }
        if (policeChasePanel)
        {
            policeChasePanel.SetActive(isPoliceChasePanel);
        }
       
    }

}
