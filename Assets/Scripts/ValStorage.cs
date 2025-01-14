using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValStorage 
{
    public static string modeSel;
    public static string gameSel;
    public static int selLevel;
    public static int TrnsparVal;

    public static float timerforlane;

   public static int GetCoins() 
   {
      return  PlayerPrefs.GetInt("Coins");
   }





    public static void SetCarNumber(int val)
    {
        PlayerPrefs.SetInt("SelectedCarNumber", val);
    }

    public static int GetCarNumber()
    {

        return PlayerPrefs.GetInt("SelectedCarNumber");
    }

    public static void SetCoins(int coin)
    {

        PlayerPrefs.SetInt("Coins",coin);
    }

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
        return PlayerPrefs.GetInt("UnlockedPoliceDriveMode", 0);
    }

    public static void SetUnlockedPoliceDriveMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedPoliceDriveMode", val);
    }
    
    public static int GetUnlockedPoliceParkMode()
    {
        return PlayerPrefs.GetInt("UnlockedPoliceParkMode", 0);
    }

    public static void SetUnlockedPoliceParkMode(int val)
    {
        PlayerPrefs.SetInt("UnlockedPoliceParkMode", val);
    }


    public static int GetUnlockedJeepParkMode()
    {
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

}
