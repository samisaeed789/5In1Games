using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM : MonoBehaviour
{
    
    
    
    [Header("SplashPanels")]
    public GameObject carDrivinggame;
    public GameObject euroTruckgame;
    public GameObject offRoadJeepgame;
    public GameObject cityBusgame;
    public GameObject policeChasegame;



    private void Start()
    {
        SelectedGame();
    }

    void SelectedGame() 
    {
        Debug.Log("ValStorage.GetGameSel()      " + ValStorage.GetGameSel());

        switch (ValStorage.GetGameSel())
        {
            case "Car":
                GameActivity(isCarDriveGame: true);
              
                break;
            case "EuroTruck":
                GameActivity(isEuroTruckGame: true);
               

                break;
            case "Jeep":
                GameActivity(isoffRoadJeepGame: true);
            
                break;
            case "Bus":
                GameActivity(isCityGame: true);
               

                break;
            case "Police":
                GameActivity(isPoliceChaseGame: true);
              
                break;
            default:
                break;
        }
    }





    public void GameActivity(bool isCarDriveGame = false, bool isEuroTruckGame = false, bool isoffRoadJeepGame = false, bool isCityGame = false, bool isPoliceChaseGame = false)
    {
        if (carDrivinggame)
        {
            carDrivinggame.SetActive(isCarDriveGame);
        }
        if (euroTruckgame)
        {
            euroTruckgame.SetActive(isEuroTruckGame);
        }
        if (offRoadJeepgame)
        {
            offRoadJeepgame.SetActive(isoffRoadJeepGame);
        }
        if (cityBusgame)
        {
            cityBusgame.SetActive(isCityGame);
        }
        if (policeChasegame)
        {
            policeChasegame.SetActive(isPoliceChaseGame);
        }
        this.gameObject.SetActive(false);
    }



    public void PlayNow() 
    {
        this.gameObject.SetActive(false);
    }
}
