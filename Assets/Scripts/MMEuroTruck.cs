using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMEuroTruck : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MMPanel;
    public GameObject modeSelPanel;
    public GameObject lvlSelPanel;
    public GameObject garagePanel;
    public GameObject loadingPanel;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject carPurhasedPanel;
    public GameObject notEnoughCoinsPanel;

    [Header("UI")]
    public Text[] allCoinstxt;



    [Header("Loading")]
    public Image loadingImage;
    public float loadingDuration = 5f;

    [Header("Settings")]
    public GameObject controlPnl;
    public GameObject soundPnl;
    public GameObject soundActvBtn;
    public GameObject cntrlActvBtn;
    public GameObject cntrl_Steering_chk;
    public GameObject cntrl_Arrw_chk; 
    public GameObject cntrl_Tilt_chk; 
    
    public GameObject music_chk;
    public GameObject sound_chk;

    [Header("Music-Volume")]
    public Image fillBar;
    private float volume = 0.5f; // Initial volume (full volume)
    private float volumeChangeAmount = 0.1f; // How much the volume changes with each 

    [Header("Sound-Volume")]
    public Image soundfillBar;
    private float soundvolume = 0.5f; // Initial volume (full volume)

    [Header("Sound-Volume")]
    public Image BothfillBar;

    [Header("Transparency")]
    private float transparencyChangeAmount = 0.1f; // A
    [SerializeField] private Text transparencyChange; // A



    [Header("Levels")]
    public Button[] LvlCards;

    public GameObject garage;

    MySoundManager soundmngr;

    private void Start()
    {
        SetControlsTTNGS();
        Setmusicsound();
        SetCoins();


        ValStorage.SetUnlockedEuroTruckDriveMode(2);
        ValStorage.SetUnlockedEuroTruckParkMode(2);


        if (MySoundManager.instance)
            soundmngr = MySoundManager.instance;

        if (soundmngr)
            soundmngr.SetEuroTruckBGM(true);


    }
    public void ButtonActivity(string panelName)
    {
        switch (panelName)
        {

            case "MM":
                PanelActivity(MM: true);
                break;
            case "ModeSel":
                PanelActivity(ModeSel: true);
                break;
            case "LvlSel":
                PanelActivity(LvlSel: true);
                garage.SetActive(false);
                
                break;
            case "Garage":
                PanelActivity(Garage: true);
                break;
            case "SettingsPnl":
                PanelActivity(SettingsPnl: true);
                break;
            case "exitPanel":
                PanelActivity(ExitPnl: true);
                break;
            
            case "Loading":
                PanelActivity(Loading: true);
                break;
            default:
                break;
        }

        if (soundmngr)
            soundmngr.PlayEuroClickSound();
    }

    public void PanelActivity(bool MM = false, bool ModeSel = false, bool LvlSel = false, bool ExitPnl = false, bool SettingsPnl = false, bool Garage = false, bool Loading = false)
    {
        if (MMPanel)
        {
            MMPanel.SetActive(MM);
        }
        if (modeSelPanel)
        {
            modeSelPanel.SetActive(ModeSel);
        }
        if (lvlSelPanel)
        {
            lvlSelPanel.SetActive(LvlSel);
        }
        if (garagePanel)
        {
            garagePanel.SetActive(Garage);
        }
        if (loadingPanel)
        {
            loadingPanel.SetActive(Loading);
        }
        if (settingsPanel)
        {
            settingsPanel.SetActive(SettingsPnl);
        } 
        if (exitPanel)
        {
            exitPanel.SetActive(ExitPnl);
        }
    }

    public void ExitPanel(bool val) 
    {
        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        exitPanel.SetActive(val);
    }
    public void SettingsPanel(bool val) 
    {
        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        settingsPanel.SetActive(val);
    }
    

    public void SelectedLvl(int i) 
    {
        ValStorage.selLevel = i;
        ButtonActivity("Garage");
        garage.SetActive(true);

        if (soundmngr)
            soundmngr.PlayEuroClickSound();
    }
    
    public void SelectedMode(string S) 
    {
        switch (S)
        {
            case "Drive":
                CheckUnlocked(ValStorage.GetUnlockedEuroTruckDriveMode());
                break;
            case "Parking":
                CheckUnlocked(ValStorage.GetUnlockedEuroTruckParkMode());
                break;
            default:
                break;
        }
        ButtonActivity("LvlSel");

        if (soundmngr)
            soundmngr.PlayEuroClickSound();
    }


    public void SelectedCar()
    {
        ButtonActivity("Loading");
    }

    public void LoadNxtScene()
    {

        if (soundmngr)
            soundmngr.PlayEuroClickSound();

        StartLoading("GPDriving");
    }

    AsyncOperation asyncLoad;
    public void StartLoading(string sceneName)
    {
        ButtonActivity("Loading");
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0, value => loadingImage.fillAmount = value, 1f, loadingDuration)
                .SetEase(Ease.Linear)
                .OnKill(() => OnLoadingComplete());
    }

    void OnLoadingComplete()
    {
        asyncLoad.allowSceneActivation = true;
    }



    void CheckUnlocked(int unlocledlvls)
    {
        int numUnlockedLevels = unlocledlvls;
        for (int i = 1; i <= LvlCards.Length; i++)
        {
            Button levelButton = LvlCards[i - 1];

            if (levelButton != null)
            {
                if (i <= numUnlockedLevels)
                {
                    levelButton.interactable = true;
                    levelButton.transform.GetChild(0).gameObject.SetActive(false);

                }
                else
                {
                    levelButton.interactable = false;
                    levelButton.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    public void Cntrlbtn() 
    {
        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        SettngsActivity(isControl:true);
    }
    public void Soundbtn() 
    {
        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        SettngsActivity(isSfx:true);
    }

    public void SettngsActivity(bool isSfx = false, bool isControl = false)
    {
        if (soundPnl)
        {
            soundPnl.SetActive(isSfx);
            soundActvBtn.SetActive(isSfx);
        }
        if (controlPnl)
        {
            controlPnl.SetActive(isControl);
            cntrlActvBtn.SetActive(isControl);
        }
    }

    public void SetControlsTTNGS()
    {
        if (ValStorage.GetControls() == 0)
        {
            ControlsActivity(isArrow: true);
        }
        if (ValStorage.GetControls() == 2)
        {
            ControlsActivity(isSteer: true);
        }
        if (ValStorage.GetControls() == 1)
        {
            ControlsActivity(isTilt: true);
        }
    }
    
    public void Setmusicsound()
    {
        if (ValStorage.IsMusicMute())
        {
            music_chk.SetActive(false);
        }
        else 
        {
            music_chk.SetActive(true);
        }


        if (ValStorage.IsSoundMute())
        {
            sound_chk.SetActive(false);
        }
        else 
        {
            sound_chk.SetActive(true);

        }
    }

    public void ControlsActivity(bool isSteer = false, bool isArrow = false,bool isTilt = false)
    {
        if (cntrl_Steering_chk)
        {
            cntrl_Steering_chk.SetActive(isSteer);
        }
        if (cntrl_Arrw_chk)
        {
            cntrl_Arrw_chk.SetActive(isArrow);
        }
        if (cntrl_Tilt_chk)
        {
            cntrl_Tilt_chk.SetActive(isTilt);
        }
    }

    public void Cntrl_btn_activity(string s) 
    {

        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        switch (s)
        {

            case "Steer":
                ValStorage.SetControls(2);
                ControlsActivity(isSteer:true);
                break;
            
            case "Arrow":
                ValStorage.SetControls(0);
                ControlsActivity(isArrow:true);
                break; 
            
            case "Tilt":
                ValStorage.SetControls(1);
                ControlsActivity(isTilt:true);
                break;
           
            default:
                break;
        }
    }

    public void Musicchkbox() 
    {
        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        if (music_chk.activeSelf)
        {
            ValStorage.SetMusicMute(0);
            music_chk.SetActive(false);
        }
        else 
        {
            ValStorage.SetMusicMute(1);
            music_chk.SetActive(true);
        }
    } 
    
    public void Soundchkbox() 
    {
        if (soundmngr)
            soundmngr.PlayEuroClickSound();
        if (sound_chk.activeSelf)
        {
            ValStorage.SetSoundMute(0);
            sound_chk.SetActive(false);
        }
        else
        {
            ValStorage.SetSoundMute(1);
            sound_chk.SetActive(true);
        }
    }

    public void SetCoins()
    {
        foreach (Text txt in allCoinstxt)
        {
            txt.text = ValStorage.GetCoins().ToString();
        }
    }

    public void AdjustVolume(float changeAmount, bool isMusic)
    {
        if (isMusic)
        {
            float vol = Mathf.Clamp(ValStorage.GetMVolume() + changeAmount, 0f, 1f);
            ValStorage.SetMVolume(vol);
        }
        else
        {
            float soundvol = Mathf.Clamp(ValStorage.GetSVolume() + changeAmount, 0f, 1f);
            ValStorage.SetSVolume(soundvol);
        }

        UpdateVolume();
    }

    private void UpdateVolume()
    {
        // Set the volume for music and sound effects
      //  soundmng.BGM.volume = ValStorage.GetMVolume(); // Music volume
      //  soundmng.Effectsource.volume = ValStorage.GetSVolume(); // Sound effect volume

        // Update the fill bars for both music and sound
        UpdateFillBar();
    }

    // This method updates the fill bars based on the current volume levels
    private void UpdateFillBar()
    {
        // Update fill bars
        if (fillBar != null)
        {
            fillBar.fillAmount = ValStorage.GetMVolume(); // Set music fill bar
        }

        if (soundfillBar != null)
        {
            soundfillBar.fillAmount = ValStorage.GetSVolume();  // Set sound fill bar
        }

        if (BothfillBar != null)
        {
            BothfillBar.fillAmount = Mathf.Max(ValStorage.GetMVolume(), ValStorage.GetSVolume()); // Set fill bar for both
        }

        // Play a button click sound (optional)
        //if (MySoundManager.instance != null)
        //{
        //    MySoundManager.instance.PlayButtonClickSound(1f);
        //}
    }

    // Public methods to handle button clicks
    public void DecreaseMusicVolume()
    {
        AdjustVolume(-volumeChangeAmount, true); // Decrease music volume
    }

    public void IncreaseMusicVolume()
    {
        AdjustVolume(volumeChangeAmount, true); // Increase music volume
    }

    public void DecreaseSoundVolume()
    {
        AdjustVolume(-volumeChangeAmount, false); // Decrease sound volume
    }

    public void IncreaseSoundVolume()
    {
        AdjustVolume(volumeChangeAmount, false); // Increase sound volume
    }

    public void DecreaseBothVolume()
    {
        DecreaseMusicVolume();
        DecreaseSoundVolume(); // Decrease both music and sound volume
    }

    public void IncreaseBothVolume()
    {
        IncreaseMusicVolume();
        IncreaseSoundVolume(); // Increase both music and sound volume
    }

    public void GoToPrevScene() 
    {
        StartLoading("Splash");
    }


}
