using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMPolice : MonoBehaviour
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
    public Slider musicSlider;

    [Header("Sound-Volume")]
    public Slider soundSlider;




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
        Setsliders();
        SetCoins();
      



        if (MySoundManager.instance)
            soundmngr = MySoundManager.instance;

        if (soundmngr)
            soundmngr.SetPoliceBGM(true);
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
            soundmngr.PlayBusClickSound();
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
        exitPanel.SetActive(val);
    }
    public void SettingsPanel(bool val) 
    {
        settingsPanel.SetActive(val);
    }
    

    public void SelectedLvl(int i) 
    {
        ValStorage.selLevel = i;
        ButtonActivity("Garage");
        garage.SetActive(true);
    }
    
    public void SelectedMode(string S) 
    {
        switch (S)
        {
            case "Drive":
                CheckUnlocked(ValStorage.GetUnlockedPoliceDriveMode()); //ValStorage.GetUnlockedModeLevelDrive("police"));
                ValStorage.modeSel = "Drive";
                break;
            case "Parking":
                CheckUnlocked(ValStorage.GetUnlockedPoliceParkMode());
                ValStorage.modeSel = "Parking";

                break;
            default:
                break;
        }
        ButtonActivity("LvlSel");
    }


    public void SelectedCar()
    {
        ButtonActivity("Loading");
    }

    public void LoadNxtScene()
    {
        string S = ValStorage.modeSel;
        switch (S)
        {
            case "Drive":
                StartCoroutine(StartLoading("DriveModePolice"));
                break;
            case "Parking":
               StartCoroutine(StartLoading("Parking"));

                break;
            default:
                break;
        }
    }

    AsyncOperation asyncLoad;
    public IEnumerator StartLoading(string sceneName)
    {
        soundmngr?.PlaypoliceClickSound();
        ButtonActivity("Loading");
        loadingImage.fillAmount = 0f;
        PlayInterAD();
        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(true);
        GarageHndlr garagehandler = garagePanel.GetComponent<GarageHndlr>();
        ValStorage.SetCarNumber(garagehandler.GetCurrCarNumber());
        garage.SetActive(false);
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0, value => loadingImage.fillAmount = value, 1f, loadingDuration)
                .SetEase(Ease.Linear)
                .OnKill(() => OnLoadingComplete());
    }

    void OnLoadingComplete()
    {
        PlayRectBanner(false);
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
        SettngsActivity(isControl:true);
    }
    public void Soundbtn() 
    {
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
      
    }
    
    public void Setsliders()
    {
        musicSlider.value = ValStorage.GetMVolume();
        soundSlider.value = ValStorage.GetSVolume();
    }

    public void ControlsActivity(bool isSteer = false, bool isArrow = false)
    {
        if (cntrl_Steering_chk)
        {
            cntrl_Steering_chk.SetActive(isSteer);
        }
        if (cntrl_Arrw_chk)
        {
            cntrl_Arrw_chk.SetActive(isArrow);
        }
       
    }

    public void Cntrl_btn_activity(string s) 
    {
       
        soundmngr?.PlayBusClickSound();

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
           
            default:
                break;
        }
    }

   

    public void Musicchkbox() 
    {
        soundmngr?.PlaypoliceClickSound();
        if (music_chk.activeSelf)
        {
            soundmngr?.MusicMute(true);
            music_chk.SetActive(false);
        }
        else
        {
            soundmngr?.MusicMute(false);
            music_chk.SetActive(true);
        }
    } 
    
    public void Soundchkbox() 
    {
        if (sound_chk.activeSelf)
        {
            soundmngr?.PlaypoliceClickSound();
            soundmngr?.SoundMute(true);
            sound_chk.SetActive(false);
        }
        else
        {
            soundmngr?.SoundMute(false);
            sound_chk.SetActive(true);
        }
    }

    public void SetCoins()
    {
        foreach (Text txt in allCoinstxt)
        {
            txt.text = ValStorage.GetpoliceCoins().ToString();
        }
    }

    public void OnMSliderValueChanged(float value)
    {
        ValStorage.SetMVolume(value);
    }
    public void OnSSliderValueChanged(float value)
    {
        ValStorage.SetSVolume(value);
    }

    public void GoToPrevScene()
    {
      StartCoroutine(StartLoading("Splash"));
    }
    public void OnVolumeChanged(float value)
    {
        soundmngr?.musicValueChanged(value);
    }

    public void OnSVolChanged(float value)
    {
        soundmngr?.soundValueChanged(value);
    }

    public void PP()
    {
        Application.OpenURL("https://privacypolicyforgamesfact.blogspot.com/2023/09/privacy-policy-for-games-fact.html");
    }
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Games+Fact");
    }
    public void PlayRectBanner(bool val)
    {
      //  if (val)
           // AdsController.Instance?.ShowBannerAd_Admob(1);
        //
       // else
       // {
         //   AdsController.Instance?.HideBannerAd_Admob(1);
        //}
    }


    public void PlayInterAD()
    {
      //  AdsController.Instance?.ShowInterstitialAd_Admob();
    }
    void enablecheckboxes()
    {
        music_chk.SetActive(true);
        sound_chk.SetActive(true);
    }

}
