using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSel : MonoBehaviour
{
    [Header("GamePanels")]
    public GameObject firstScreenPanel;
    public GameObject SecondScreenPanel;
    public GameObject carDrivingPanel;
    public GameObject euroTruckPanel;
    public GameObject offRoadJeepPanel;
    public GameObject cityBusPanel;
    public GameObject policeChasePanel;


    [Header("SplashPanels")]
    public GameObject carDriving;
    public GameObject euroTruck;
    public GameObject offRoadJeep;
    public GameObject cityBus;
    public GameObject policeChase;


    MySoundManager soundmngr;

    private void Start()
    {
        if (MySoundManager.instance)
            soundmngr = MySoundManager.instance;

        soundmngr.SetBGM(true);
    }
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
        if (soundmngr)
            soundmngr.PlayButtonClickSound();

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


    public void PlayNow(string panelName)
    {
      
        switch (panelName)
        {
            case "CarDrive":
                GameActivity(isCarDriveGame: true);
                ValStorage.SetGameSel("CarDrive");
                break;
            case "EuroTruck":
                GameActivity(isEuroTruckGame: true);
                ValStorage.SetGameSel("EuroTruck");

                break;
            case "OffRoadJeep":
                GameActivity(isoffRoadJeepGame: true);
                ValStorage.SetGameSel("Jeep");

                break;
            case "CityBus":
                GameActivity(isCityGame: true);
                ValStorage.SetGameSel("Bus");

                break;
            case "PoliceChase":
                GameActivity(isPoliceChaseGame: true);
                ValStorage.SetGameSel("Police");

                break;
            default:
                break;
        }

        if (soundmngr)
            soundmngr.PlayButtonClickSound();
    }


    public void GameActivity(bool isCarDriveGame = false, bool isEuroTruckGame = false, bool isoffRoadJeepGame = false, bool isCityGame = false, bool isPoliceChaseGame = false)
    {
        if (carDriving)
        {
            carDriving.SetActive(isCarDriveGame);
        }
        if (euroTruck)
        {
            euroTruck.SetActive(isEuroTruckGame);
        }
        if (offRoadJeep)
        {
            offRoadJeep.SetActive(isoffRoadJeepGame);
        }
        if (cityBus)
        {
            cityBus.SetActive(isCityGame);
        }
        if (policeChase)
        {
            policeChase.SetActive(isPoliceChaseGame);
        }

        //if (soundmngr)
        //    soundmngr.SetBGM(false);

    }

    public void BackVBtn() 
    {
        PanelActivity();
        SecondScreenPanel.SetActive(false);
        firstScreenPanel.SetActive(true);

        if (soundmngr)
            soundmngr.PlayButtonClickSound();

    }


}
