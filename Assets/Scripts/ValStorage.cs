using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValStorage 
{
    public static string modeSel;
    public static string gameSel;
    public static int selLevel;
    public static int TrnsparVal;
    public static bool IsSplashVidSeen;

    public static float timerforlane;


    public static bool IsSwatPurchased;
    public static bool IsFordPurchased;
    public static bool IsRegularPurchased;
    //public static int GetCoins() 
    //{
    //   return  PlayerPrefs.GetInt("Coins");
    //}



    // Create a static event that other classes can subscribe to
    public static event Action OnEnemyDestroyed;

    // Method to trigger the event
    public static void TriggerEnemyDestroyed()
    {
        OnEnemyDestroyed?.Invoke();
    }

    public static void SetCarNumber(int val)
    {
        PlayerPrefs.SetInt("SelectedCarNumber", val);
    }

    public static int GetCarNumber()
    {

        return PlayerPrefs.GetInt("SelectedCarNumber");
    }

    //public static void SetCoins(int coin)
    //{

    //    PlayerPrefs.SetInt("Coins",coin);
    //}

    public static void SetTransparency(int val) 
    {
       
        PlayerPrefs.SetInt("TransparentVal",val);
    }

    public static int GetTransparency()
    {
        if (!PlayerPrefs.HasKey("TransparentVal"))
        {
            PlayerPrefs.SetInt("TransparentVal", 5);
        }
        return  PlayerPrefs.GetInt("TransparentVal");
    }
    
    public static float GetSVolume()
    {
        if (!PlayerPrefs.HasKey("SoundVol"))
        {
            PlayerPrefs.SetFloat("SoundVol", 0.5f);
        }
        return  PlayerPrefs.GetFloat("SoundVol");
    }
    
    public static void SetSVolume(float val)
    {

        PlayerPrefs.SetFloat("SoundVol",val);
    }
    public static float GetMVolume()
    {

        if (!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 0.5f);
        }
        return  PlayerPrefs.GetFloat("MusicVol");
    }
    
    public static void SetMVolume(float val)
    {
         PlayerPrefs.SetFloat("MusicVol",val);
    }
    
    public static int GetControls()
    {
        return  PlayerPrefs.GetInt("Controls");
    }
   
    public static void SetControls(int val)
    {
        PlayerPrefs.SetInt("Controls", val);
    }
    public static int GetGQuality()
    {
        return  PlayerPrefs.GetInt("GQuality");
    }
   
    public static void SetGQuality(int val)
    {
        PlayerPrefs.SetInt("GQuality", val);
    }

    public static void SetUnlockedLevels(int val)
    {
        PlayerPrefs.SetInt("UnlockedLevels", val);
    }

    public static int GetUnlockedCarDriveMode()
    {
       return PlayerPrefs.GetInt("UnlockedCarDriveMode", 0);
    } 
    
    public static void SetUnlockedCarDriveMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedCarDriveMode", val);
    }
    
    public static int GetUnlockedCarParkMode()
    {
        if (!PlayerPrefs.HasKey("UnlockedCarParkMode"))
        {
            PlayerPrefs.SetInt("UnlockedCarParkMode", 1);
        }

        return PlayerPrefs.GetInt("UnlockedCarParkMode", 0);
    } 
    
    public static void SetUnlockedCarParkMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedCarParkMode", val);
    }

    public static int GetUnlockedEuroTruckDriveMode()
    {
        return PlayerPrefs.GetInt("UnlockedEuroTruckDriveMode", 0);
    }

    public static void SetUnlockedEuroTruckDriveMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedEuroTruckDriveMode", val);
    } 
    
    public static int GetUnlockedEuroTruckParkMode()
    {

        if (!PlayerPrefs.HasKey("UnlockedEuroTruckParkMode"))
        {
            PlayerPrefs.SetInt("UnlockedEuroTruckParkMode", 1);
        }
        return PlayerPrefs.GetInt("UnlockedEuroTruckParkMode", 0);
    }

    public static void SetUnlockedEuroTruckParkMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedEuroTruckParkMode", val);
    }


    public static int GetUnlockedJeepDriveMode()
    {
        

        return PlayerPrefs.GetInt("UnlockedJeepDriveMode", 0);
    }

    public static void SetUnlockedJeepDriveMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedJeepDriveMode", val);
    }

    public static int GetUnlockedPoliceDriveMode()
    {
        if (!PlayerPrefs.HasKey("UnlockedPoliceDriveMode"))
        {
            PlayerPrefs.SetInt("UnlockedPoliceDriveMode", 1);
        }
        return PlayerPrefs.GetInt("UnlockedPoliceDriveMode", 0);
    }

    public static void SetUnlockedPoliceDriveMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedPoliceDriveMode", val);
    }
    
    public static int GetUnlockedPoliceParkMode()
    {
        if (!PlayerPrefs.HasKey("UnlockedPoliceParkMode"))
        {
            PlayerPrefs.SetInt("UnlockedPoliceParkMode", 1);
        }
        return PlayerPrefs.GetInt("UnlockedPoliceParkMode", 0);
    }

    public static void SetUnlockedPoliceParkMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedPoliceParkMode", val);
    }


    public static int GetUnlockedJeepParkMode()
    {
        if (!PlayerPrefs.HasKey("UnlockedJeepParkMode"))
        {
            PlayerPrefs.SetInt("UnlockedJeepParkMode", 1);
        }
        return PlayerPrefs.GetInt("UnlockedJeepParkMode", 0);
    }

    public static void SetUnlockedJeepParkMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedJeepParkMode", val);
    }

    public static bool IsSoundMute()
    {
        int isMuted = PlayerPrefs.GetInt("SoundMute", 0);
       
        if (isMuted == 0) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public static void SetSoundMute(int val)
    {
        PlayerPrefs.SetInt("SoundMute", val);  
    }
    
    
    public static void SetMusicMute(int val)
    {
        PlayerPrefs.SetInt("MusicMute", val);  
    }
    public static bool IsMusicMute()
    {
        int isMuted = PlayerPrefs.GetInt("MusicMute", 0);

        if (isMuted == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    

    public static int GetUnlockedBusDriveMode()
    {
        return PlayerPrefs.GetInt("UnlockedBusDriveMode", 0);
    }

    public static void SetUnlockedBusDriveMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedBusDriveMode", val);
    }
    
    public static int GetUnlockedBusParkMode()
    {
        if (!PlayerPrefs.HasKey("UnlockedBusParkMode"))
        {
            PlayerPrefs.SetInt("UnlockedBusParkMode",1);
        }
        return PlayerPrefs.GetInt("UnlockedBusParkMode", 0);
    }

    public static void SetUnlockedBusParkMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedBusParkMode", val);
    }

    public static void SetGameSel(string val) 
    {
        PlayerPrefs.SetString("GameSelected",val);
    }

    public static string GetGameSel()
    {
        return PlayerPrefs.GetString("GameSelected");
    }


    public static void SetbusCoins(int coin)
    {

        PlayerPrefs.SetInt("BusCoins", coin);
    }
    
    public static int GetBusCoins()
    {

        return PlayerPrefs.GetInt("BusCoins");
    } 
    
    public static void SettruckCoins(int coin)
    {

        PlayerPrefs.SetInt("truckCoins", coin);
    } 
    
    public static int GettruckCoins()
    {

        return PlayerPrefs.GetInt("truckCoins");

    }

    public static void SetjeepCoins(int coin)
    {
      

        PlayerPrefs.SetInt("jeepCoins", coin);
    } 
    
    public static int GetjeepCoins()
    {

        return PlayerPrefs.GetInt("jeepCoins");
    }

    public static void SetcarCoins(int coin)
    {

        PlayerPrefs.SetInt("carCoins", coin);
    }
      public static int GetcarCoins()
    {

       return PlayerPrefs.GetInt("carCoins");
    }
    
    public static void SetpoliceCoins(int coin)
    {

        PlayerPrefs.SetInt("policeCoins", coin);
    }
    
    public static int GetpoliceCoins()
    {
       return PlayerPrefs.GetInt("policeCoins");
    }


    public static int GetCoins(string gameMode)
    {
        if (gameMode == "car")
        {
            return GetcarCoins();
        }
        else if (gameMode == "jeep")
        {
            return GetjeepCoins();
        }
        else if(gameMode == "truck")
        {
            return GettruckCoins();
        }
        
        else if(gameMode == "bus")
        {
            return GetBusCoins();
        }
        else if(gameMode == "police")
        {
            return GetpoliceCoins();
        }
        return 0;
    }

    public static void SetCoins(string gameMode, int val)
    {
        if (gameMode == "car")
        {
            SetcarCoins(val);
        }
        else if (gameMode == "jeep")
        {
            SetjeepCoins(val);
        }
        else if (gameMode == "truck")
        {
           SettruckCoins(val);
        }

        else if (gameMode == "bus")
        {
            SetbusCoins(val);
        }
        else if (gameMode == "police")
        {
            SetpoliceCoins(val);
        }
    }


    public static void SetUnlockedModeLevel(string gameMode, int val)
    {
        if (gameMode == "car")
        {
            SetUnlockedCarParkMode(val);
        }
        else if (gameMode == "jeep")
        {
            SetUnlockedJeepParkMode(val);
        }
        else if (gameMode == "truck")
        {
            SetUnlockedEuroTruckParkMode(val);
        }

        else if (gameMode == "bus")
        {
            SetUnlockedBusParkMode(val);
        }
        else if (gameMode == "police")
        {
            SetUnlockedPoliceParkMode(val);
        }
    }
    
    public static void SetUnlockedModeLevelDrive(string gameMode, int val)
    {
        if (gameMode == "car")
        {
            SetUnlockedCarDriveMode(val);
        }
        else if (gameMode == "jeep")
        {
            SetUnlockedJeepDriveMode(val);
        }
        else if (gameMode == "truck")
        {
            SetUnlockedEuroTruckDriveMode(val);
        }

        else if (gameMode == "bus")
        {
            SetUnlockedBusParkMode(val);
        }
        else if (gameMode == "police")
        {
            SetUnlockedPoliceDriveMode(val);
        }
    }
    
    public static int GetUnlockedModeLevel(string gameMode)
    {
        if (gameMode == "car")
        {
            return GetUnlockedCarParkMode();
        }
        else if (gameMode == "jeep")
        {
            return GetUnlockedJeepParkMode();

        }
        else if (gameMode == "truck")
        {
            return GetUnlockedEuroTruckParkMode();
        }
        else if (gameMode == "bus")
        {
            return GetUnlockedBusParkMode();

        }
        else if (gameMode == "police")
        {
            return GetUnlockedPoliceParkMode();

        }
        return 0;
    }
    
    public static int GetUnlockedModeLevelDrive(string gameMode)
    {
        if (gameMode == "car")
        {
            return GetUnlockedCarDriveMode();
        }
        else if (gameMode == "jeep")
        {
            return GetUnlockedJeepDriveMode();

        }
        else if (gameMode == "truck")
        {
            return GetUnlockedEuroTruckDriveMode();
        }
        else if (gameMode == "bus")
        {
            return GetUnlockedBusDriveMode();

        }
        else if (gameMode == "police")
        {
            return GetUnlockedPoliceDriveMode();

        }
        return 0;
    }

    public static bool GetCarUnLocked(CarType car)
    {
        if (car == CarType.Regular)
        {
            return IsRegularPurchased;
        }
        if (car == CarType.Ford)
        {
            return IsFordPurchased;
        }
        if (car == CarType.Swat)
        {
            return IsSwatPurchased;
        }

        return false;
    }

    public static void SetCarUnLocked(CarType car)
    {
        if (car == CarType.Regular)
        {
             IsRegularPurchased = true; 
        }
        if (car == CarType.Ford)
        {
             IsFordPurchased = true; 

        }
        if (car == CarType.Swat)
        {
             IsSwatPurchased=true;
        }
    }


}
