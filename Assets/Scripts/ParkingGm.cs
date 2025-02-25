using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UIAnimatorCore;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParkingGm : MonoBehaviour
{

    public GameObject busMode;
    public GameObject carsMode;
    [SerializeField] GameObject emojiPanel;
    [SerializeField] GameObject Env;


    [SerializeField] GameObject busPanel;
    [SerializeField] GameObject truckPanel;
    [SerializeField] GameObject carPanel;
    [SerializeField] GameObject jeepPanel;
    [SerializeField] GameObject policePanel;

    [Header("BusPanels")]
    [SerializeField] GameObject failPanelbus;
    [SerializeField] GameObject completePanelbus;
    [SerializeField] GameObject Loadingbus;
    [SerializeField] GameObject pausePanelbus;
    [SerializeField] GameObject envBGbus;
    [SerializeField] GameObject busIgnition;
    [SerializeField] GameObject LoadBarbus;
    [SerializeField] GameObject uiBlockerbus;
    [SerializeField] GameObject seatbeltbus;
    [SerializeField] Image loadingBarbus;
    [SerializeField] CanvasGroup busCan;
    [SerializeField] RCC_Demo ControlsBus;
    [SerializeField] Text rewardedCoinsbus;
    [SerializeField] Text CoinsTotalbus;
    [SerializeField] GameObject nextbtncompbus;





    [Header("TruckPanels")]
    [SerializeField] GameObject failPanelTruck;
    [SerializeField] GameObject completePanelTruck;
    [SerializeField] GameObject LoadingTruck;
    [SerializeField] GameObject pausePanelTruck;
    [SerializeField] GameObject lftindiTruck;
    [SerializeField] GameObject rghtindiTruck;
    [SerializeField] GameObject LoadBarTruck;
    [SerializeField] GameObject uiBlockerTruck;
    [SerializeField] Image loadingBarTruck;
    [SerializeField] CanvasGroup truckCan;
    [SerializeField] GameObject truckIgnition;
    [SerializeField] GameObject seatbelttruck;
    [SerializeField]  RCC_Demo ControlsTruck;
    [SerializeField]  Text rewardedCoinstruck;
    [SerializeField] Text CoinsTotaltruck;
    [SerializeField] GameObject nextbtncomptruck;






    [Header("CarPanels")]
    [SerializeField] GameObject failPanelCar;
    [SerializeField] GameObject completePanelCar;
    [SerializeField] GameObject LoadingCar;
    [SerializeField] GameObject pausePanelCar;
    [SerializeField] GameObject LoadBarBCar;
    [SerializeField] GameObject uiBlockerBCar;
    [SerializeField] Text loadingText;
    [SerializeField] GameObject lftindicar;
    [SerializeField] GameObject rghtindiacr;
    [SerializeField] CanvasGroup carCan;
    [SerializeField] GameObject carIgnition;
    [SerializeField] GameObject seatbeltcar;
    [SerializeField] RCC_Demo ControlsCar;
    [SerializeField] Text rewardedCoinsCar;
    [SerializeField] Text   CoinsTotalCar;
    [SerializeField] GameObject nextbtncompcar;



    [Header("JeepPanels")]
    [SerializeField] GameObject failPanelJeep;
    [SerializeField] GameObject completePanelJeep;
    [SerializeField] GameObject LoadingJeep;
    [SerializeField] GameObject pausePanelJeep;
    [SerializeField] GameObject LoadBarJeep;
    [SerializeField] GameObject gearupJeep;
    [SerializeField] GameObject geardownJeep;
    [SerializeField] Image loadingBarJeep;
    [SerializeField] GameObject lftindijeep;
    [SerializeField] GameObject rghtindijeep;
    [SerializeField] GameObject uiBlockerjeep;
    [SerializeField] CanvasGroup jeepCan;
    [SerializeField] GameObject jeepIgnition;
    [SerializeField] GameObject seatbeltjeep;
    [SerializeField] RCC_Demo ControlsJeep;
    [SerializeField] Text rewardedCoinsjeep;
    [SerializeField] Text CoinsTotaljeep;
    [SerializeField] GameObject nextbtncompjeep;



    [Header("Settings")]
    public GameObject cntrl_Steering_chk;
    public GameObject cntrl_Arrw_chk;
    public GameObject cntrl_Tilt_chk;


    [Header("policePanels")]
    [SerializeField] GameObject failPanelPolice;
    [SerializeField] GameObject completePanelPolice;
    [SerializeField] GameObject LoadingPolice;
    [SerializeField] GameObject pausePanelPolice;
    [SerializeField] GameObject LoadBarPolice;
    [SerializeField] Image loadingBarPolice;
    [SerializeField] GameObject lftindipolice;
    [SerializeField] GameObject rghtindipolice;
    [SerializeField] CanvasGroup policeCan;
    [SerializeField] GameObject policeIgnition;
    [SerializeField] GameObject seatbeltpolice;
    [SerializeField] RCC_Demo Controlspolice;
    [SerializeField] GameObject gearuppolice;
    [SerializeField] GameObject geardownpolice;
    [SerializeField] GameObject uiBlockerpolice;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider soundvolumeSlider;
    [SerializeField] Text coinstxtcomppolice;
    [SerializeField] Text timetxtcomppolice;
    [SerializeField] GameObject nextbtncomppolice;



    [Header("Panels")]
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject completePanel;
    [SerializeField] GameObject Loading;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject LoadBar;
    [SerializeField] GameObject LeftIndActv;
    [SerializeField] GameObject RightIndActv;
    [SerializeField] Image loadingBar;
    [SerializeField] GameObject Ignition;
    [SerializeField] CanvasGroup canvas;
    [SerializeField] GameObject UIBlocker;




    [Header("Others")]
    RCC_CarControllerV3 carController;
    Rigidbody carRb;
    [SerializeField] Camera Cam;
    [SerializeField] RCC_Camera rccCam;
    [SerializeField] ParticleSystem celebConftti;
    [SerializeField] RCC_UIController GasBtn;
    [SerializeField] RCC_UIController BrakeBtn;
    [SerializeField] GameObject celeb;
    [SerializeField] ParticleSystem CollectbleCash;
    [SerializeField] ParticleSystem CollectbleCoin;




    [Header("UI")]
  
    [SerializeField] Text percentageText;
    [SerializeField] Orbit sphere;
    [SerializeField] GameObject MusicOff;
    [SerializeField] GameObject Belt;
    [SerializeField] GameObject Beltbtn;
    [SerializeField] GameObject headLightActvbtn;
    [SerializeField] RCC_Demo Controls;
    [SerializeField] GameObject NxtBtnSccs;
    [SerializeField] Image[] UIgp;
    [SerializeField] GameObject gearup;
    [SerializeField] GameObject geardown;

    [Header("Text")]
    [SerializeField] Text timerText;
    [SerializeField] Text ComptimeText;
    [SerializeField] Text CoinsEarnedlvltxt;
    [SerializeField] Text TotalCompltxt;





    [Header("Bools")]
    bool IsTimerRunning;
    bool isBrakePressed;


    [Header("Levels")]
    [SerializeField]GameObject[] BusLevels;
    [SerializeField]GameObject[] CarsLevels;


    [Header("Vehicles")]
    [SerializeField] GameObject[] busVehicles;
    [SerializeField] GameObject[] carsVehicles;
    [SerializeField] GameObject[] truckVehicles;
    [SerializeField] GameObject[] jeepVehicles;
    [SerializeField] GameObject[] policeVehicles;

    [SerializeField] bool Test;
    [SerializeField] string GameSelected;
    [SerializeField] int levelnumber;
    [SerializeField] int Carnumber;


    [SerializeField]GameObject CarSel = null;


    [Header("VehicleRelated")]
   public GameObject headLight;



    string gameSel;
    LD_Park lvldata;
    ParticleSystem[] lvlconfti;
    Rigidbody rbCar;
    MySoundManager soundManager;
    GameObject taillights;
    float elapsedTime = 0f;
    bool stopAnimation;
    int currentlvl;

    public static ParkingGm instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }
    void Start()
    {

        soundManager = MySoundManager.instance;
        RCC_Settings.Instance.useAutomaticGear = false;
        RCC_Settings.Instance.autoReverse = false;
        // UpdateVolume();
        // SetButtonTransparency(ValStorage.GetTransparency());
        //  Controls.SetMobileController(ValStorage.GetControls());
        volumeSlider.value = ValStorage.GetMVolume();
        soundvolumeSlider.value = ValStorage.GetSVolume();


        if (Test) 
        {
            SetPanels(GameSelected);
            SelectedGame(GameSelected);
        }
        else 
        {
            SetPanels(ValStorage.GetGameSel());
            SelectedGame(ValStorage.GetGameSel());
        }

        gameSel = ValStorage.GetGameSel();
        SetControlsTTNGS();
    }



    public void SelectedGame(string selGame)
    {
       
        currentlvl = ValStorage.selLevel;
        int selCar;
        selCar = ValStorage.GetCarNumber();


        if (Test) 
        {
            currentlvl = levelnumber;
            selCar = Carnumber;
        }
        switch (selGame)
        {
            case "CarDrive":
                carsMode.SetActive(true);
                CarsLevels[currentlvl - 1].gameObject.SetActive(true);
                CarSel = carsVehicles[selCar - 1];
                break;
            case "EuroTruck":
                busMode.SetActive(true);
                BusLevels[currentlvl - 1].gameObject.SetActive(true);
                CarSel = truckVehicles[selCar - 1];
                break;
            case "Jeep":
                carsMode.SetActive(true);
                CarsLevels[currentlvl - 1].gameObject.SetActive(true);
                CarSel = jeepVehicles[selCar - 1];
                break;
            case "Bus":
                busMode.SetActive(true);
                BusLevels[currentlvl - 1].gameObject.SetActive(true);
                CarSel = busVehicles[selCar - 1];
                break;
            case "Police":
                carsMode.SetActive(true);
                CarsLevels[currentlvl - 1].gameObject.SetActive(true);
                CarSel = policeVehicles[selCar - 1];
                break;
            default:
                break;
        }
        carController = CarSel.GetComponent<RCC_CarControllerV3>();
        carController.canControl = false;
        carRb = carController.GetComponent<Rigidbody>();
        carRb.isKinematic = true;

        CarSel.SetActive(true);
        Carstats = CarSel.GetComponent<ObstacleColl>();
    }
    ObstacleColl Carstats;

    string GameMode;
    public void SetPanels(string selGame)
    {
        
        switch (selGame)
        {
            case "CarDrive":
                carPanel.SetActive(true);
                failPanel = failPanelCar;
                completePanel = completePanelCar;
                Loading = LoadingCar;
                pausePanel = pausePanelCar;
                LoadBar = LoadBarBCar;
                LeftIndActv = lftindicar;
                RightIndActv = rghtindiacr;
                canvas = carCan;
                Ignition = carIgnition;
                Beltbtn = seatbeltcar;
                Controls = ControlsCar;
                UIBlocker = uiBlockerBCar;
                CoinsEarnedlvltxt = rewardedCoinsCar;
                TotalCompltxt = CoinsTotalCar;
                NxtBtnSccs = nextbtncompcar;
                GameMode = "car";

                break;
            case "EuroTruck":
                truckPanel.SetActive(true);
                failPanel = failPanelTruck;
                completePanel = completePanelTruck;
                Loading = LoadingTruck;
                pausePanel = pausePanelTruck;
                loadingBar = loadingBarTruck;
                LoadBar = LoadBarTruck;
                LeftIndActv = lftindiTruck;
                RightIndActv = rghtindiTruck;
                canvas = truckCan;
                Ignition = truckIgnition;
                Beltbtn = seatbelttruck;
                Controls = ControlsTruck;
                UIBlocker = uiBlockerTruck;
                CoinsEarnedlvltxt = rewardedCoinstruck;
                TotalCompltxt = CoinsTotaltruck;
                NxtBtnSccs = nextbtncomptruck;
                ControlsJeep.SetMobileController(ValStorage.GetControls());
                GameMode = "truck";


                break;
           
            case "Jeep":
                jeepPanel.SetActive(true);
                failPanel = failPanelJeep;
                completePanel = completePanelJeep;
                Loading = LoadingJeep;
                pausePanel = pausePanelJeep;
                loadingBar = loadingBarJeep;
                LoadBar = LoadBarJeep;
                LeftIndActv = lftindijeep;
                RightIndActv = rghtindijeep;
                canvas = jeepCan;
                Ignition = jeepIgnition;
                Beltbtn = seatbeltjeep;
                gearup = gearupJeep;
                geardown = geardownJeep;
                ControlsJeep.SetMobileController(ValStorage.GetControls());
                UIBlocker = uiBlockerjeep;
                CoinsEarnedlvltxt = rewardedCoinsjeep;
                TotalCompltxt = CoinsTotaljeep;
                NxtBtnSccs = nextbtncompjeep;

                GameMode = "jeep";

                break;


            case "Bus":
                busPanel.SetActive(true);
                failPanel = failPanelbus;
                completePanel = completePanelbus;
                Loading = Loadingbus;
                pausePanel = pausePanelbus;
                loadingBar = loadingBarbus;
                LoadBar = LoadBarbus;
                canvas = busCan;
                Ignition = busIgnition;
                Beltbtn = seatbeltbus;
                ControlsBus.SetMobileController(0);
                UIBlocker = uiBlockerbus;
                CoinsEarnedlvltxt = rewardedCoinsbus;
                TotalCompltxt = CoinsTotalbus;
                NxtBtnSccs = nextbtncompbus;

                GameMode = "bus";

                break;
            case "Police":
                policePanel.SetActive(true);
                failPanel = failPanelPolice;
                completePanel = completePanelPolice;
                Loading = LoadingPolice;
                pausePanel = pausePanelPolice;
                loadingBar = loadingBarPolice;
                LoadBar = LoadBarPolice;
                LeftIndActv = lftindipolice;
                RightIndActv = rghtindipolice;
                canvas = policeCan;
                Ignition = policeIgnition;
                Beltbtn = seatbeltpolice;
                Controls = Controlspolice;
                gearup = gearuppolice;
                geardown = geardownpolice;
                UIBlocker = uiBlockerpolice;
                CoinsEarnedlvltxt = timetxtcomppolice;
                TotalCompltxt = coinstxtcomppolice;
                NxtBtnSccs = nextbtncomppolice;

                GameMode = "police";
               


              

                break;
            default:
                break;
        }

    }

    public void OnLevelStatsLoadedHandler(LD_Park lvlData) 
    {
        Env.SetActive(true);
        lvldata = lvlData;
        if (lvldata.SpawnPoint) 
        {
            carRb.transform.position = lvldata.SpawnPoint.position;
        }
        carRb.isKinematic = false;
        CarSel.SetActive(true);
        Ignition.SetActive(true);
        Loading.SetActive(false);
        RCC_CameraCarSelection carselcam = rccCam.gameObject.GetComponent<RCC_CameraCarSelection>();
        carselcam.target = carController.transform;
    }



    void SetLevel()
    {
        foreach (GameObject g in lvldata.OnObjets)
        {
            g.SetActive(true);
        }
       
        carController.canControl = true;    
    }


    public void IndiRight()
    {

        if (soundManager)
            soundManager.PlayButtonClickSound();

        if (LeftIndActv.activeSelf)
        {
            LeftIndActv.SetActive(false);
        }

        if (RightIndActv.activeSelf)
        {
            RightIndActv.SetActive(false);
        }
        else
        {
            RightIndActv.SetActive(true);
       
        }
    }

    public void IndiLft()
    {
        if (soundManager)
            soundManager.PlayButtonClickSound();
        if (RightIndActv.activeSelf)
        {
            RightIndActv.SetActive(false);
        }
        if (LeftIndActv.activeSelf)
        {
            LeftIndActv.SetActive(false);
        }
        else
        {
            LeftIndActv.SetActive(true);
        }
    }
    Transform GetCarspawn() 
    {
        string name = rbCar.gameObject.name;
        Transform SP;
        if (name == "Jeep")
        {
            SP = lvldata.SpawnPointJeep;
        }
        else if (name== "Mazda") 
        {
            SP = lvldata.SpawnPoint;
        }
        else if (name== "Audi_Etron") 
        {
            SP = lvldata.SpawnPointAudi ;
        }
        else if (name == "Bugatti")
        {
            SP = lvldata.SpawnPointBugatti ;
        }
        else 
        {
            return null;
        }

        return SP;
    }

   
   



    public void Collided() 
    {
        Cam.DOShakePosition(0.5f, 0.5f, 10, 90f);
    }
    
    public void CarFinalPark() 
    {
        carRb.isKinematic = true;
        canvas.alpha = 0f;
        UIBlocker.SetActive(true);

        if (soundManager)
        {
            soundManager.PlayCompleteSound(true);
        }
        Invoke(nameof(Celeb),0.5f);
    }
    void Celeb() 
    {
        PlayInterAD();
        RCC_CameraCarSelection celebCam = rccCam.gameObject.GetComponent<RCC_CameraCarSelection>();
        celebConftti.Play();
        celebCam.enabled = true;
        IsTimerRunning = false;
        StartCoroutine(CompletePanel());
    } 


    public void StartDance()
    {
        Transform dancechar = lvldata.DanceChar?.transform;
        foreach (Transform child in dancechar)
        {
            Animator animator = child.GetComponent<Animator>();

            if (animator != null)
            {
                animator.SetBool("Dance", true);
            }
        }
    }
    public void EngineRun()
    {
        if (soundManager)
            soundManager.PlayEngineSound();
        
        Shakecam();

        Ignition.SetActive(false);
        IsTimerRunning = true;

        if (soundManager)
            soundManager.SetBGM(true);
    }
    void Shakecam() 
    {
        Cam.DOShakePosition(0.5f, 0.5f, 10, 90f).OnKill(() => OnShakeComplete());
    }
    void OnShakeComplete() 
    {
        canvas.alpha = 1f;
        UIBlocker.SetActive(false);
        SetLevel();

         canvas.gameObject.GetComponent<UIAnimator>().PlayAnimation(AnimSetupType.Intro);
         headLight = Carstats.Headlight;
            soundManager?.SetBGM(true);
    
    }





    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Divide elapsed time by 60 to get minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Get the remainder for seconds

        string timeFormatted = string.Format("{0:D2}:{1:D2}", minutes, seconds);

        // Update the UI Text
        timerText.text = timeFormatted;
    }
    private bool HasBrakeStateChanged()
    {
        return BrakeBtn.pressing != isBrakePressed;
    }

    private void UpdateBrakeLightColor(bool isPressed)
    {
        taillights.SetActive(isPressed);
    }

    public void CollectablePlay(bool isCash = false, bool isCoin = false)
    {
        if (isCash)
        {
            CollectbleCash.Play();
            if (soundManager)
                soundManager.PlayCollectSound();

        }

        if (isCoin)
        {
            CollectbleCoin.Play();
            if (soundManager)
                soundManager.PlayCollectCoin(); 

        }
    }

    public void FailLevel() 
    {
        if (soundManager)
            soundManager.PlayLevelFailSound();

        CarSound(false);
        canvas.alpha = 0f;
        UIBlocker.SetActive(true);
        StartCoroutine(FailPanel());
    }

    IEnumerator FailPanel() 
    {
        yield return new WaitForSeconds(3f);
        emojiPanel.SetActive(true);
        yield return new WaitForSeconds(4f);

        Invoke(nameof(delFail),0.2f);
    }


    void delFail()
    {
        emojiPanel.SetActive(false);
        UIBlocker.SetActive(false);
        failPanel.SetActive(true);
        PlayRectBanner(true);
    }

    IEnumerator CompletePanel() 
    {

        UnlckNxtLvl();

        yield return new WaitForSeconds(7f);
        if (soundManager)
        {
            soundManager.PlayCompleteSound(false);
            CarSound(false);
        }

        delComp();
    }


    void delComp() 
    {
        UIBlocker.SetActive(false);
        completePanel.SetActive(true);
        PlayRectBanner(true);
        SetCoinsinPanel();
    }

    void GetComplPnl() 
    {

    }
    void UnlckNxtLvl()
    {

        int currlvl = ValStorage.selLevel;
        int unlockdlvls = ValStorage.GetUnlockedModeLevel(GameMode);

        if (currlvl == unlockdlvls && currentlvl<5)
        {
            ValStorage.SetUnlockedModeLevel(GameMode,unlockdlvls + 1);
        }

        if (currlvl == 5)
        {
            NxtBtnSccs?.SetActive(false);
        }
    }

    void SetCoinsinPanel()
    {
        CoinsEarnedlvltxt.text = 300.ToString();
        StartCoroutine(CounterAnimation(CalculateTotalCoins()));
        int alreadycoins = ValStorage.GetCoins(GameMode);
        int totalcoins = alreadycoins + CalculateTotalCoins();
        ValStorage.SetCoins(GameMode,totalcoins);
    }
    private int CalculateTotalCoins()
    {
        int coinsFromTime = Mathf.FloorToInt(elapsedTime * 2);

        int total = 300 + coinsFromTime;
        return total;
    }
    private IEnumerator CounterAnimation(int totalCoins)
    {
        yield return new WaitForSeconds(1f);
        int duration = 3; // Total duration for the animation
        float elapsedTime = 0f; // Time elapsed since the start of the animation
        int currentCoins = 0;

        // Play sound if available
        if (soundManager)
            soundManager.PlaycoinSound();

        // Calculate the number of coins per second
        int coinsPerSecond = totalCoins / duration;

        // Loop until the animation reaches the total coins
        while (elapsedTime < duration && !stopAnimation)
        {
            elapsedTime += Time.deltaTime; // Accumulate elapsed time
            currentCoins = Mathf.FloorToInt(coinsPerSecond * elapsedTime); // Increment coins

            // Make sure currentCoins does not exceed totalCoins
            currentCoins = Mathf.Min(currentCoins, totalCoins);

            // Update the UI or text with the current number of coins
            if (TotalCompltxt != null)
                TotalCompltxt.text = currentCoins.ToString();

            yield return null; // Wait until the next frame
        }

        // Ensure the final count is exactly totalCoins

        if (TotalCompltxt != null)
            TotalCompltxt.text = totalCoins.ToString();

        // Stop sound if available
        if (soundManager)
            soundManager.StopcoinSound();
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        StopCoinAnimation();
        Loading.SetActive(true);
        if (ValStorage.GetGameSel() == "CarDrive") 
        {
            LoadBar.SetActive(true);
            loadingText.text = $"{Mathf.FloorToInt(0)}%";
            StartCoroutine(StartLoading("Parking"));
        }
        else 
        {
            LoadBar.SetActive(true);
            //Invoke(nameof(delrestart), 0.2f);
            StartCoroutine(LoadAsyncScene("Parking"));
        }
    }

    public void LoadNxtScene()
    {
        StartLoading("Parking");
    }

    AsyncOperation asyncLoad;
    public IEnumerator StartLoading(string sceneName)
    {
        PlayInterAD();
        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(true);
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0f, value => UpdateLoadingText(value), 100f, 5f)
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

        asyncLoad.allowSceneActivation = true;
        sphere.enabled = false;
    }
    void delrestart() 
    {
        StartCoroutine(LoadAsyncScene("Parking"));

    }


    public void Home()
    {
        Time.timeScale = 1f;
        StopCoinAnimation();
        Loading.SetActive(true);
        LoadBar.SetActive(true);

        if (ValStorage.GetGameSel() == "CarDrive")
        {
            loadingText.text = 0f.ToString()+"%";
            LoadBar.SetActive(true);
            StartCoroutine(StartLoading("MM"));
        }
        else 
        {
            StartCoroutine(LoadAsyncScene("MM"));
        }
    }

    public void NextLvlBtn()
    {
        Loading.SetActive(true);
        LoadBar.SetActive(true);

        StopCoinAnimation();
        if (currentlvl < 5)
        {
            ValStorage.selLevel += 1;

            if (ValStorage.GetGameSel() == "CarDrive")
            {
                loadingText.text = 0f.ToString() + "%";
                LoadBar.SetActive(true);
                StartCoroutine(StartLoading("Parking"));
            }
            else
            {
                StartCoroutine(LoadAsyncScene("Parking"));
            }
        }
    }

    public void StopCoinAnimation()
    {
        stopAnimation = true;
    }
    public void Enablegearactv(string s)
    {
        if (s == "drive")
        {
            Gearactive(IsDrive: true);
        }
        if (s == "reverse")
        {
            Gearactive(IsReverse: true);
        }


        if (soundManager)
            soundManager.PlayButtonClickSound();
    }

    public void Gearactive(bool IsDrive = false, bool IsReverse = false)
    {
        if (gearup)
        {
            gearup.SetActive(IsDrive);
        }

        if (geardown)
        {
            geardown.SetActive(IsReverse);
        }
    }
    public void ChangeControl()
    {

        soundManager?.PlayButtonClickSound();

        PlayInterAD();
        int currentind = ValStorage.GetControls();

        currentind = (currentind + 1) % 3;
        Controls.SetMobileController(currentind);
        ValStorage.SetControls(currentind);
    }

    public void Pause()
    {
        soundManager?.PauseSounds();
        soundManager?.PlayButtonClickSound();
        PlayInterAD();
        CarSound(false);
        pausePanel.SetActive(true);
        PlayRectBanner(true);
        Time.timeScale = 0f;
    }
    void CarSound(bool IsActive)
    {
        Transform child = carController.transform.Find("All Audio Sources");
        if (child != null)
        {
            child.gameObject.SetActive(IsActive);
        }
        else
        {
            UnityEngine.Debug.LogError("Object not found!");
        }
    }


    public void Resume()
    {
        soundManager?.ResumeSounds();
        CarSound(true);
        PlayRectBanner(false);
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    void CheckMusis()
    {
        if (MusicOff.activeSelf)
        {

            if (soundManager)
            {
                soundManager.SetBGM(false);  // Start playing background music
            }
        }
        else
        {

            if (soundManager)
            {
                soundManager.SetBGM(true);  // Stop playing background music
            }
        }

    }

    public void PlayStopMusic(GameObject redBtn)
    {
        soundManager?.PlayButtonClickSound();
        if (redBtn.activeSelf) 
        {
            soundManager?.SetBGM(true);
            redBtn.SetActive(!redBtn.activeSelf);
        }
        else 
        {
            soundManager?.SetBGM(false);
            redBtn.SetActive(!redBtn.activeSelf);
        }
    }

    public void PlayHorn()
    {
        if (soundManager)
        {
            soundManager.SetBGM(true);  // Start playing background music
        }
    }

    public void OnButtonPressed()
    {

        if (!soundManager)
        {
            return;
        }
        
        switch (gameSel)
        {
            case "CarDrive":
                soundManager.PlayHorn("Car");
                break;
            case "EuroTruck":
                soundManager.PlayHorn("Bus");

                break;
            case "Jeep":
                soundManager.PlayHorn("Car");

                break;
            case "Bus":
                soundManager.PlayHorn("Bus");

                break;
            case "Police":
                soundManager.PlayHorn("Car");

                break;
            default:
                break;
        }

    }

    public void OnButtonReleased()
    {

        if (soundManager)
        {
            soundManager.StopHorn();
        }
    }

    public void ToggleSeatBelt()
    {
        if (soundManager)
            soundManager.PlayButtonClickSound();


        Belt.SetActive(true);
        Beltbtn.SetActive(false);
        Invoke(nameof(delayoff), 1.05f);
    }

    public void ToggleHeadlight()
    {

        if (soundManager)
            soundManager.PlayButtonClickSound();


        if (headLight == null)
        {
            headLight = Carstats.Headlight;

        }
       
             headLight?.SetActive(!headLight.activeSelf);
        

       
    }

    void delayoff()
    {
        Belt.SetActive(false);
    }
    IEnumerator LoadAsyncScene(string sceneName)
    {
        PlayInterAD();
        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(true);
        float timer = 0f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (timer < 5f)
        {
            if (timer < 5f)
            {
                timer += Time.deltaTime;
                float progress = Mathf.Clamp01(timer / 5f);  
                loadingBar.fillAmount = progress;
            }
            else
            {
                loadingBar.fillAmount = 1f;
                percentageText.text = "100%";
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(false);
        asyncLoad.allowSceneActivation = true;
    }

    public void PlayHIT()
    {
       soundManager?.PlayHitSound();
     
    }


    private void UpdateVolume()
    {
        if (soundManager) 
        {
            soundManager.BGM.volume = ValStorage.GetMVolume(); // Music volume
            soundManager.Effectsource.volume = ValStorage.GetSVolume(); // Sound effect volume
        }

    }

    public void SetButtonTransparency(int transval)
    {
        transval = Mathf.Clamp(transval, 1, 5);

        float alpha = Mathf.Lerp(0.2f, 1f, (transval - 1) / 4f);


        foreach (Image UI in UIgp)
        {
            Color buttonColor = UI.color;
            buttonColor.a = alpha;  // Set alpha based on the calculation
            UI.color = buttonColor;
        }
    }

   

    public void DualIndi(GameObject btn) 
    {
        if(btn!=null)
            btn.SetActive(!btn.activeSelf);
    }

    public void Gear(GameObject Drive,GameObject Reverse) 
    {
        if (Drive.activeSelf) 
        {
            Drive.SetActive(false);
            Reverse.SetActive(true);
        }
        else 
        {
            Drive.SetActive(true);
            Reverse.SetActive(false);
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
            
            case "Tilt":
                ValStorage.SetControls(0);
                ControlsActivity(isTilt: true);
                break;

            default:
                break;
        }
    }

    public void SetControlsTTNGS()
    {
        if (ValStorage.GetControls() == 0)
        {
            ControlsActivity(isArrow: true);
        }
        if (ValStorage.GetControls() == 1)
        {
            ControlsActivity(isTilt: true);

        }
        if (ValStorage.GetControls() == 2)
        {
            ControlsActivity(isSteer: true);

        }
    }
    public void OnVolumeChanged(float value)
    {
        soundManager?.musicValueChanged(value);
    }
    
    public void OnSVolChanged(float value)
    {
        soundManager?.soundValueChanged(value);
    }

    public void PlayRectBanner(bool val)
    {
       // if (val)
          //  AdsController.Instance?.ShowBannerAd_Admob(1);

       // else
       // {
           // AdsController.Instance?.HideBannerAd_Admob(1);
        //}
    }


    public void PlayInterAD()
    {
        //AdsController.Instance?.ShowInterstitialAd_Admob();
    }
}



