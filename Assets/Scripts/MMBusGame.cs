using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMBusGame : MonoBehaviour
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


    [Header("Loading")]
    public Image loadingImage;
    public float loadingDuration = 5f;


    [Header("Settings")]

   
    public GameObject cntrl_Steering_chk;
    public GameObject cntrl_Arrw_chk;
   
    public GameObject cntrl_Steering_actv;
    public GameObject cntrl_Arrw_actv;



    public GameObject music_actv;
    public GameObject sound_actv;

    [Header("UI")]
    public Text[] allCoinstxt;


    [Header("Levels")]
    public Button[] LvlCards;


    MySoundManager soundmngr;

    public GameObject garage;
    private void Start()
    {
        SetControlsTTNGS();
      //  Setmusicsound();
        SetCoins();
        enablecheckboxes();
    


        if (MySoundManager.instance)
            soundmngr = MySoundManager.instance;

        if(soundmngr)
            soundmngr.SetBusBGM(true);



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

    public void SelectedLvl(int i)
    {
        ValStorage.selLevel = i;
        ButtonActivity("Garage");
        garage.SetActive(true);

        if (soundmngr)
            soundmngr.PlayBusClickSound();
    }

    public void SelectedMode(string S)
    {
        switch (S)
        {
            case "Drive":
                CheckUnlocked(0);// CheckUnlocked(ValStorage.GetUnlockedBusDriveMode());
                break;
            case "Parking":
                CheckUnlocked(ValStorage.GetUnlockedModeLevel("bus"));
                break;
            default:
                break;
        }
        ButtonActivity("LvlSel");

        if (soundmngr)
            soundmngr.PlayBusClickSound();
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


    public void LoadNxtScene(string s)
    {
        StartCoroutine(StartLoading(s));
    }

    AsyncOperation asyncLoad;
    public IEnumerator StartLoading(string sceneName)
    {
        soundmngr?.PlayBusClickSound();
        ButtonActivity("Loading");
        loadingImage.fillAmount = 0f;
      //  AdsController.Instance?.ShowInterstitialAd_Admob();
        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(true);
        GarageHndlr garagehandler= garagePanel.GetComponent<GarageHndlr>();
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

    public void Soundchkbox()
    {
        if (sound_actv.activeSelf)
        {
            soundmngr?.PlayBusClickSound();
            soundmngr?.SoundMute(true);
            sound_actv.SetActive(false);
        }
        else
        {
            soundmngr?.SoundMute(false);
            sound_actv.SetActive(true);
        }
    }
    public void Musicchkbox()
    {
        soundmngr?.PlayBusClickSound();
        if (music_actv.activeSelf)
        {

            soundmngr?.MusicMute(true);
            music_actv.SetActive(false);
        }
        else
        {
            soundmngr?.MusicMute(false);
            music_actv.SetActive(true);
        }
    }
    public void Setmusicsound()
    {
        if (ValStorage.IsMusicMute())
        {
            music_actv.SetActive(false);
        }
        else
        {
            music_actv.SetActive(true);
        }


        if (ValStorage.IsSoundMute())
        {
            sound_actv.SetActive(false);
        }
        else
        {
            sound_actv.SetActive(true);

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
    public void ControlsActivity(bool isSteer = false, bool isArrow = false)
    {
        if (cntrl_Steering_actv)
        {
            cntrl_Steering_actv.SetActive(isSteer);
        }
        if (cntrl_Arrw_actv)
        {
            cntrl_Arrw_actv.SetActive(isArrow);
        }
    }

    public void Cntrl_btn_activity(string s)
    {

        if (soundmngr)
            soundmngr.PlayBusClickSound();
        switch (s)
        {

            case "Steer":
                ValStorage.SetControls(2);
                ControlsActivity(isSteer: true);
                break;

            case "Arrow":
                ValStorage.SetControls(0);
                ControlsActivity(isArrow: true);
                break;

            default:
                break;
        }
    }

 


    public void SetCoins()
    {
        foreach (Text txt in allCoinstxt)
        {
            txt.text = ValStorage.GetBusCoins().ToString();
        }
    }

    public void GotPrevScene() 
    {
        StartCoroutine(StartLoading("Splash"));
    }


    public void PP()
    {
        Application.OpenURL("https://privacypolicyforgamesfact.blogspot.com/2023/09/privacy-policy-for-games-fact.html");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Games+Fact");
    }
    public void PlayInterAD()
    {
       // AdsController.Instance?.ShowInterstitialAd_Admob();
    }
    public void PlayRectBanner(bool val)
    {
       // if (val)
           // AdsController.Instance?.ShowBannerAd_Admob(1);

       // else
        //{
            //AdsController.Instance?.HideBannerAd_Admob(1);
       // }
    }

    void enablecheckboxes() 
    {
        music_actv.SetActive(true);
        sound_actv.SetActive(true);
    }
}
