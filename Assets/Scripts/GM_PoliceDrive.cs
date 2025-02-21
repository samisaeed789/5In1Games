using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MTAssets.EasyMinimapSystem;
using Gley.PedestrianSystem;
public enum CarType
{
    Regular,
    Ford,
    Swat,
    None,
}

[System.Serializable]
public class CSData
{
    public PlayableDirector playable;
    public GameObject CSLevel;
}
public class GM_PoliceDrive : MonoBehaviour
{

    [Header("PolicePanels")]
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject completePanel;
    [SerializeField] GameObject ObjPanel;
    [SerializeField] GameObject Loading;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject LoadBar;
    [SerializeField] GameObject UIBlocker;
    [SerializeField] Image loadingBar;
    [SerializeField] CanvasGroup Can;
    [SerializeField] CanvasGroup CarSelCan;
    [SerializeField] RCC_Demo Controls;
    [SerializeField] Text rewardedCoins;
    [SerializeField] GameObject nextbtncomp;
    [SerializeField] GameObject WaterSplashs;
    [SerializeField] RCC_Camera rcc_cam;
    [SerializeField] RCC_CameraCarSelection rcc_carselcam;


    [SerializeField] GameObject CarSel;
    [SerializeField] GameObject GP;
    [SerializeField] GameObject Finalpolice;
    [SerializeField] GameObject ThirdPersonCntrls;
    [SerializeField] GameObject Traffic;
    [SerializeField] MinimapRenderer mapMan;
    [SerializeField] MinimapRoutes mapRoutes;
    [SerializeField] PedestrianSystemComponent PedestrianMan;


    [Header("CSDATA")]
    [SerializeField] GameObject CS;
    [SerializeField] CSData[] csdata;
    [SerializeField] PlayableDirector playableDirector;



    [Header("LevelData")]
    [SerializeField] GameObject[] Cars;
    [SerializeField] GameObject[] EnemyCars;
    [SerializeField]public GameObject LockedBtn;
    [SerializeField]public GameObject UnLockedBtn;
    [SerializeField]public GameObject WatchVidSwat;
    [SerializeField]public GameObject WatchVidFord;
    [SerializeField]public GameObject Env;




    [Header("ObjPanels")]
    [SerializeField] string[] Objectives;
    [SerializeField] TypingEffect1 objtyping;
    [SerializeField] GameObject ok;

    [Header("CompPanel")]
    [SerializeField] Text CoinsEarnedlvltxt;
    [SerializeField] GameObject NxtBtnSccs;
    [SerializeField] Text TotalCompltxt;


    RCC_CarControllerV3 carcontroller;
    GameObject carController;
    bool stopAnimation;
    float elapsedTime = 0f;
    MySoundManager soundManager;
    public static GM_PoliceDrive instance;

    private CarType currentCarType = CarType.None;

    int currLvl;
    private void Awake()
    {
        if (instance == null)
            instance = this;

    }
    private void OnEnable()
    {
      
        if (objtyping != null)
        {
            objtyping.OnTypingFinished += typingfinished;
        }

        ValStorage.OnEnemyDestroyed += HandleEnemyDestroyed;
    }
    private void Start()
    {
        RCC_Settings.Instance.useAutomaticGear = true;
        RCC_Settings.Instance.autoReverse = true;
        CheckPurchasedCars();
        soundManager = MySoundManager.instance;
        currLvl = ValStorage.selLevel-1;
        StartCoroutine(PlayTimeline(currLvl)); 
        Contrls(false);
        ValStorage.SetCarUnLocked(CarType.Regular);
    }

    IEnumerator PlayTimeline(int index)
    {
        yield return new WaitForSeconds(4f);
        Env.SetActive(true);
        Loading.SetActive(false);
        CS.SetActive(true);
        csdata[index].CSLevel.SetActive(true);
        playableDirector= csdata[index].playable;
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineFinished;
        }
    }

    public void SetCarType(CarType carType)
    {
        currentCarType = carType;
    }

    public CarType GetCurrentCarType()
    {
        return currentCarType;
    }
    
    public void SetGP() 
    {
        soundManager?.PlaypoliceClickSound();

        GameObject Enemy = EnemyCars[currLvl];

        GP.SetActive(true);
        CarSel.SetActive(false);
        Enemy.SetActive(true);
        soundManager?.PlayPoliceSiren(true);
        soundManager?.SetBGM(true);
        CarType car = GetCurrentCarType();
        Contrls(true);
        switch (car)
        {
            case CarType.Regular:
                carController = Cars[0];
                break;
            case CarType.Ford:
                carController = Cars[2];
                break;
            case CarType.Swat:
                carController = Cars[1];
                break;
            default:
                break;
        }
        Traffic.SetActive(true);    
        carController.SetActive(true);
        carcontroller = carController.GetComponent<RCC_CarControllerV3>();
        rcc_carselcam.target = carController.transform;
        stering();
        PedestrianMan._player = carController.transform;
        PedestrianMan.gameObject.SetActive(true);
        mapRoutes.startingPoint = carController.transform;
        mapRoutes.destinationPoint = Enemy.transform;
        MinimapCamera playerMap = carController.GetComponent<MinimapCamera>();
        mapMan.minimapCameraToShow = playerMap;
    }

  

    public void FailLevel()
    {
        soundManager?.PlayLevelFailSound();

        Can.alpha = 0f;
        UIBlocker.SetActive(true);
        StartCoroutine(FailPanel());
    }

    IEnumerator FailPanel()
    {
        yield return null;
        Invoke(nameof(delFail), 0.2f);
    }


    void delFail()
    {
        PlayRectBanner(true);
        UIBlocker.SetActive(false);
        failPanel.SetActive(true);
    }

    void OnTimelineFinished(PlayableDirector director) 
    {
        CS.SetActive(false);
        CarSel.SetActive(true);
        PlayObj();
    }
    Canvas can;
    void PlayObj() 
    {
        can = Controls.GetComponent<Canvas>();
        can.sortingOrder = 1000;
        objtyping.fullText = Objectives[currLvl];
        ObjPanel.SetActive(true);
    }

    void typingfinished() 
    {
        soundManager?.PlayTypeSound(false);
        ok.SetActive(true);
    }

    public void OkObj() 
    {
        soundManager?.PlaypoliceClickSound();

        ObjPanel.SetActive(false);
        ThirdPersonCntrls.SetActive(true);
        soundManager?.SetBGM(true);
        can.sortingOrder = 1;

        CarSelCan.alpha = 1;
        CarSelCan.interactable = true;
        CarSelCan.blocksRaycasts = true;
    }

    void HandleEnemyDestroyed() 
    {
        soundManager?.SetBGM(false);
        StartCoroutine(deldestroyed());
    }
    IEnumerator deldestroyed() 
    {
        carcontroller.canControl = false;
        mapMan.gameObject.SetActive(false);
        mapRoutes.gameObject.SetActive(false);
        Traffic.SetActive(false);
        rcc_carselcam.enabled = true;
        rcc_cam.enabled = false;
        Rigidbody RB = carController.GetComponent<Rigidbody>();
        RB.isKinematic = true;
        yield return new WaitForSeconds(12f);
        soundManager?.PlayChatterSound(true);
        soundManager?.PlayPoliceSiren(true);
        GP.SetActive(false);
        rcc_cam.gameObject.SetActive(false);
        Contrls(false);
        Finalpolice.SetActive(true);
        StartCoroutine(CompletePanel());
    }
 
    IEnumerator CompletePanel()
    {
        UnlckNxtLvl();
        yield return new WaitForSeconds(10f);
        PlayInterAD();
        soundManager?.PlayChatterSound(false);
        soundManager?.PlayPoliceSiren(false);
        yield return new WaitForSeconds(0.1f);

        delComp();
    }
    void UnlckNxtLvl()
    {
        PlayInterAD();
        int currlvl = ValStorage.selLevel;
        int unlockdlvls = ValStorage.GetUnlockedModeLevelDrive("police");
        if (currlvl == unlockdlvls && currlvl < 5)
        {
            ValStorage.SetUnlockedModeLevelDrive("police", unlockdlvls + 1);
        }

        if (currlvl == 5)
        {
            NxtBtnSccs?.SetActive(false);
        }
    }

    void delComp()
    {
        PlayRectBanner(true);
        UIBlocker.SetActive(false);
        completePanel.SetActive(true);
        SetCoinsinPanel();
    }


    void SetCoinsinPanel()
    {
        CoinsEarnedlvltxt.text = 300.ToString();
        StartCoroutine(CounterAnimation(CalculateTotalCoins()));
        int alreadycoins = ValStorage.GetCoins("police");
        int totalcoins = alreadycoins + CalculateTotalCoins();
        ValStorage.SetCoins("police", totalcoins);
    }
    public void NextLvlBtn()
    {
        soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        Loading.SetActive(true);
        LoadBar.SetActive(true);

        StopCoinAnimation();
        if (currLvl < 5)
        {
            ValStorage.selLevel += 1;
            StartCoroutine(LoadAsyncScene("DriveModePolice"));
        }
    }


    public void Pause()
    {
        soundManager?.PauseSounds();
        PlayInterAD();
        PlayRectBanner(true);
        soundManager.PlaypoliceClickSound();

        if (soundManager)
            soundManager.PlayButtonClickSound();

        CarSound(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        soundManager?.PlaypoliceClickSound();
        soundManager?.ResumeSounds();
        PlayRectBanner(false);
        soundManager?.PlayPoliceSiren(true);
        CarSound(true);
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
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

    public void Home()
    {
        soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        Time.timeScale = 1f;
        StopCoinAnimation();
        Loading.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("MM"));
    }
    public void Restart()
    {
        soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        Time.timeScale = 1f;
        StopCoinAnimation();
        Loading.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("DriveModePolice"));
    }
    public void ChangeControl()
    {
        soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        int currentind = ValStorage.GetControls();
        currentind = (currentind + 1) % 3;
        Controls.SetMobileController(currentind);
        ValStorage.SetControls(currentind);
    }
    IEnumerator LoadAsyncScene(string sceneName)
    {
        loadingBar.fillAmount = 0f;
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
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        PlayRectBanner(false);
        asyncLoad.allowSceneActivation = true;
    }
    public void StopCoinAnimation()
    {
        stopAnimation = true;
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
        int duration = 3; 
        float elapsedTime = 0f; 
        int currentCoins = 0;

        if (soundManager)
            soundManager.PlaycoinSound();

        int coinsPerSecond = totalCoins / duration;

        while (elapsedTime < duration && !stopAnimation)
        {
            elapsedTime += Time.deltaTime; // Accumulate elapsed time
            currentCoins = Mathf.FloorToInt(coinsPerSecond * elapsedTime); // Increment coins

            currentCoins = Mathf.Min(currentCoins, totalCoins);

            if (TotalCompltxt != null)
                TotalCompltxt.text = currentCoins.ToString();

            yield return null; // Wait until the next frame
        }


        if (TotalCompltxt != null)
            TotalCompltxt.text = totalCoins.ToString();

        // Stop sound if available
        if (soundManager)
            soundManager.StopcoinSound();
    }
    private void OnDisable()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineFinished;
        }
        if (objtyping != null)
        {
            objtyping.OnTypingFinished -= typingfinished;
        }
        ValStorage.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }
    void Contrls(bool isOn) 
    {
        if (isOn) 
        {
            Can.alpha = 1;
        }
        else 
        {
            Can.alpha = 0;
        }
        Can.interactable = isOn;
        Can.blocksRaycasts = isOn;
    }
    public void PlayHorn()
    {
        if (soundManager)
        {
            soundManager.SetBGM(true);  
        }
    }

    public void OnButtonPressed()
    {

        if (!soundManager)
        {
            return;
        }
        soundManager.PlayHorn("Car");
    }

    public void OnButtonReleased()
    {
        if (soundManager)
        {
            soundManager.StopHorn();
        }
    }

    public void click() 
    {
        soundManager?.PlaypoliceClickSound();
    }
    
    public void CarFellOcean() 
    {
        rcc_cam.cameraTarget = null;
        rcc_cam.enabled = false;
        Contrls(false);

        soundManager?.SetBGM(false);
        soundManager?.PlayPoliceSiren(false);
        soundManager?.SplashSound();


        CarSound(false);
        Invoke(nameof(DelFail),4f);
    }
    void DelFail() 
    {
        carController.SetActive(false);
        failPanel.SetActive(true);
    }

    public void PlayRectBanner(bool val)
    {
        if (val)
            AdsController.Instance?.ShowBannerAd_Admob(1);

        else
        {
            AdsController.Instance?.HideBannerAd_Admob(1);
        }
    }
    public void PlayInterAD()
    {
        AdsController.Instance?.ShowInterstitialAd_Admob();
    }
    public void PlayRewardADSkip() 
    {
        AdsController.Instance.ShowRewardedInterstitialAd_Admob(SkipCS);
    }
    public void PlayRewardCarUnlck() 
    {
        AdsController.Instance.ShowRewardedInterstitialAd_Admob(UnlockCar);
    }

    public void UnlockCar()
    {
        CarType car = GetCurrentCarType();
        ValStorage.SetCarUnLocked(car);
        SetGP();
    }
    void SkipCS() 
    {
        CS.SetActive(false);
        CarSel.SetActive(true);
        PlayObj();
    }

    public void stering()
    {
        Invoke(nameof(delay), 2f);
    }
    private void delay()
    {
        carcontroller.steeringType = RCC_CarControllerV3.SteeringType.Simple;
        carcontroller.steeringSensitivityFactor = .65f;
    }
    void CheckPurchasedCars() 
    {
        if (ValStorage.GetCarUnLocked(CarType.Ford)) 
        {
            WatchVidFord.SetActive(false);
        }
        if (ValStorage.GetCarUnLocked(CarType.Swat)) 
        {
            WatchVidSwat.SetActive(false);
        }
    }
}
