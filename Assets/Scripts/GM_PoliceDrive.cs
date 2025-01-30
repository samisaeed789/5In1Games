using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

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
    [SerializeField] RCC_Demo Controls;
    [SerializeField] Text rewardedCoins;
    [SerializeField] GameObject nextbtncomp;
    [SerializeField] GameObject WaterSplashs;
    [SerializeField] RCC_Camera rcc_cam;


    [SerializeField] GameObject CarSel;
    [SerializeField] GameObject GP;
    [SerializeField] GameObject Finalpolice;
    [SerializeField] GameObject ThirdPersonCntrls;


    [Header("CSDATA")]
    [SerializeField] GameObject CS;
    [SerializeField] CSData[] csdata;
    [SerializeField] PlayableDirector playableDirector;



    [Header("LevelData")]
    [SerializeField] GameObject[] Cars;
    [SerializeField] GameObject[] EnemyCars;
    [SerializeField] MiniMapController map;
    [SerializeField]public GameObject LockedBtn;
    [SerializeField]public GameObject UnLockedBtn;




    [Header("ObjPanels")]
    [SerializeField] string[] Objectives;
    [SerializeField] TypingEffect objtyping;
    [SerializeField] GameObject ok;

    [Header("CompPanel")]
    [SerializeField] Text CoinsEarnedlvltxt;
    [SerializeField] GameObject NxtBtnSccs;
    [SerializeField] Text TotalCompltxt;

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
        ValStorage.selLevel = 1;
        soundManager = MySoundManager.instance;
        currLvl = ValStorage.selLevel-1;
        StartCoroutine(PlayTimeline(currLvl)); 
        Contrls(false);
        ValStorage.SetCarUnLocked(CarType.Swat);
        ValStorage.SetCarUnLocked(CarType.Regular);
    }

    IEnumerator PlayTimeline(int index)
    {
        yield return new WaitForSeconds(4f);
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

        GP.SetActive(true);
        CarSel.SetActive(false);
        EnemyCars[currLvl].SetActive(true);
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

        carController.SetActive(true);
        map.target = carController.transform;

    }

    public void UnlockCar() 
    {
        CarType car = GetCurrentCarType();
        ValStorage.SetCarUnLocked(car);
        SetGP();
    }

    public void FailLevel()
    {
            soundManager?.PlayLevelFailSound();

     //   CarSound(false);
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
    }

    void HandleEnemyDestroyed() 
    {
        soundManager?.SetBGM(false);
        StartCoroutine(deldestroyed());
    }
    IEnumerator deldestroyed() 
    {
        yield return new WaitForSeconds(8f);
        soundManager?.PlayChatterSound(true);
        soundManager?.PlayPoliceSiren(true);

        GP.SetActive(false);
        Contrls(false);
        Finalpolice.SetActive(true);
        StartCoroutine(CompletePanel());
    }
 
    IEnumerator CompletePanel()
    {
        UnlckNxtLvl();
        yield return new WaitForSeconds(10f);
        soundManager?.PlayChatterSound(false);
        soundManager?.PlayPoliceSiren(false);
        delComp();
    }
    void UnlckNxtLvl()
    {

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

        Time.timeScale = 1f;
        StopCoinAnimation();
        Loading.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("MM"));
    }
    public void Restart()
    {
        soundManager?.PlaypoliceClickSound();

        Time.timeScale = 1f;
        StopCoinAnimation();
        Loading.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("DriveModePolice"));
    }
    public void ChangeControl()
    {
        soundManager?.PlaypoliceClickSound();
        int currentind = ValStorage.GetControls();

        currentind = (currentind + 1) % 3;
        Controls.SetMobileController(currentind);
        ValStorage.SetControls(currentind);

    }
    IEnumerator LoadAsyncScene(string sceneName)
    {
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
            soundManager.SetBGM(true);  // Start playing background music
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
}
