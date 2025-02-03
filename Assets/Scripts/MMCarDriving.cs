using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMCarDriving : MonoBehaviour
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
    public GameObject Garage;

    [Header("UI")]
    public Text[] allCoinstxt;
    



    [Header("Loading")]
    public Text loadingText; 
    public float loadingDuration = 5f; 
    public Text prcntLoading;
    public Orbit sphereanim;

    [Header("Settings")]
    public GameObject controlPnl;
    public GameObject soundPnl;
    public GameObject soundActvBtn;
    public GameObject cntrlActvBtn;
    public GameObject cntrl_Steering_chk;
    public GameObject cntrl_Arrw_chk; 
    
    public GameObject music_chk;
    public GameObject sound_chk;


    [Header("Levels")]
    public Button[] LvlCards;


    MySoundManager soundmanager;
    private void Start()
    {
        SetControlsTTNGS();
        SetCoins();
        enablecheckboxes();


        if(MySoundManager.instance)
            soundmanager = MySoundManager.instance;

        soundmanager.SetBGM(true);

      
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
                Garage.SetActive(false);
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

        if (soundmanager && panelName== "ModeSel")
            soundmanager.PlayButtonClickSound(scifi:true);

        else
            soundmanager.PlayButtonClickSound();

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



    public void SelectedLvl(int i) 
    {
        ValStorage.selLevel = i;
        ButtonActivity("Garage");
        Garage.SetActive(true);

        if (soundmanager)
            soundmanager.PlayButtonClickSound();
    }
    
    public void SelectedMode(string S) 
    {

        AdsController.Instance?.ShowInterstitialAd_Admob();

        switch (S)
        {
            case "Drive":
                CheckUnlocked(0);//ValStorage.GetUnlockedCarDriveMode());
                break;
            case "Parking":
                CheckUnlocked(ValStorage.GetUnlockedCarParkMode());
                break;
            default:
                break;
        }
        ButtonActivity("LvlSel");


        if (soundmanager)
            soundmanager.PlayButtonClickSound();
    }


    public void SelectedCar()
    {
        ButtonActivity("Loading");

        if (soundmanager)
            soundmanager.PlayButtonClickSound();
    }

    public void LoadNxtScene(string scene)
    {
        if (soundmanager)
            soundmanager.PlayButtonClickSound(scifi: true);
       
        StartCoroutine(StartLoading(scene)); 
    }





    AsyncOperation asyncLoad;
    public IEnumerator StartLoading(string sceneName)
    {
        ButtonActivity("Loading");
        loadingText.text = 0f.ToString();
        AdsController.Instance?.ShowInterstitialAd_Admob();
        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(true);
        GarageHndlr garagehandler = garagePanel.GetComponent<GarageHndlr>();
        if(garagehandler!=null)
            ValStorage.SetCarNumber(garagehandler.GetCurrCarNumber());

        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0f, value => UpdateLoadingText(value), 100f, loadingDuration)
               .SetEase(Ease.Linear) 
               .OnKill(() => OnLoadingComplete()); 
    }

    void UpdateLoadingText(float value)
    {
      
        loadingText.text = $"{Mathf.FloorToInt(value)}%";
    }

    void OnLoadingComplete()
    {
        PlayRectBanner(false);
        sphereanim.enabled = false;
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
        soundmanager?.PlayButtonClickSound();
        SettngsActivity(isControl:true);
    }
    public void Soundbtn() 
    {
        soundmanager?.PlayButtonClickSound();
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
    
    public void Setmusicsound()
    {
        //if (ValStorage.IsMusicMute())
        //{
        //    music_chk.SetActive(false);
        //}
        //else 
        //{
        //    music_chk.SetActive(true);
        //}


        //if (ValStorage.IsSoundMute())
        //{
        //    sound_chk.SetActive(false);
        //}
        //else 
        //{
        //    sound_chk.SetActive(true);

        //}
        music_chk.SetActive(false);
        sound_chk.SetActive(true);
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

        if (soundmanager)
            soundmanager.PlayButtonClickSound();
    }

    public void Musicchkbox() 
    {
        if (soundmanager)
            soundmanager.PlayButtonClickSound();
        if (music_chk.activeSelf)
        {
            soundmanager?.MusicMute(true);
            music_chk.SetActive(false);
        }
        else 
        {
            soundmanager?.MusicMute(false);
            music_chk.SetActive(true);
        }
    } 
    
    public void Soundchkbox() 
    {
        if (soundmanager)
            soundmanager.PlayButtonClickSound();
        if (sound_chk.activeSelf)
        {
            //ValStorage.SetSoundMute(0);
            soundmanager?.SoundMute(true);
            sound_chk.SetActive(false);
        }
        else
        {
            //  ValStorage.SetSoundMute(1);
            soundmanager?.SoundMute(false);
            sound_chk.SetActive(true);
        }
    }

    public void SetCoins()
    {
        foreach (Text txt in allCoinstxt)
        {
            txt.text = ValStorage.GetcarCoins().ToString();
        }
    }

    public void LoadPrevScene() 
    {
        soundmanager?.PlayButtonClickSound();
        StartCoroutine(StartLoading("Splash")); 
    }

   
    public void PP()
    {
        soundmanager?.PlayButtonClickSound();
        Application.OpenURL("https://privacypolicyforgamesfact.blogspot.com/2023/09/privacy-policy-for-games-fact.html");
    }

    public void MoreGames()
    {
        soundmanager?.PlayButtonClickSound();
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Games+Fact");
    }
    

    public void PlayInterAD() 
    {
        AdsController.Instance?.ShowInterstitialAd_Admob();
    }
    public void PlayRectBanner(bool val) 
    {
        if(val)
            AdsController.Instance?.ShowBannerAd_Admob(1);

        else 
        {
            AdsController.Instance?.HideBannerAd_Admob(1);
        }
    }
    void enablecheckboxes()
    {
        music_chk.SetActive(true);
        sound_chk.SetActive(true);
    }
}
