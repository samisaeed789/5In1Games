using DG.Tweening;
using Gley.PedestrianSystem;
using System.Collections;
using System.Collections.Generic;
using TurnTheGameOn.SimpleTrafficSystem;
using UIAnimatorCore;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM_CD : MonoBehaviour
{

    public static GM_CD instance;

    public GameObject Env;

    [Header("Canvas")]
    [SerializeField] CanvasGroup Controls;
    [SerializeField] GameObject Loading;
    [SerializeField] GameObject Belt;
    [SerializeField] GameObject Beltbtn;
    [SerializeField] GameObject NxtBtnSccs;
    
    [SerializeField] GameObject Appreciate;
    [SerializeField] Text Appreciatetxt;

    [SerializeField] GameObject Discourage;
    [SerializeField] Text Disctxt;
    
    [SerializeField] GameObject Obj;
    [SerializeField] Text Objtxt;

    [SerializeField]GameObject IgnitionBtn;
    [SerializeField]Image loadingBar;
    [SerializeField]GameObject LoadBar;
    [SerializeField]Text loadingText;
    [SerializeField]Orbit sphereanim;
    [SerializeField]GameObject LoadingPnl;
    [SerializeField]GameObject pausePanel;
    [SerializeField]GameObject CompletePnl;


    [SerializeField]Text CoinsEarnedlvltxt;
    [SerializeField]Text TotalCompltxt;



    [Header("GP")]
    [SerializeField] GameObject GP;
    [SerializeField] GameObject dancingchar;
    [SerializeField] ParticleSystem Conftti;
    [SerializeField] ParticleSystem CollectbleCoin;
    [SerializeField] ParticleSystem CollectbleCash;
    [SerializeField] GameObject[] Cars;
    [SerializeField] GameObject[] Levels;




    [Header("CSDATA")]
    [SerializeField] GameObject CsStart;
    [SerializeField] PlayableDirector[] cslvl;


    [Header("Managers")]
    [SerializeField] PedestrianSystemComponent PedestrianMan;
    [SerializeField] GameObject Pedestrianparent;
    [SerializeField] GameObject Traffic;
    [SerializeField] RCC_Demo RccControls;

    [Header("Cameras")]
    [SerializeField] RCC_Camera rccCam;
    [SerializeField] Camera shakeCam;
    [SerializeField] RCC_CameraCarSelection CarselCam;


    MySoundManager soundManager;
    Rigidbody rb;
    float elapsedTime;
    bool stopAnimation;
    int currlevel;
    int selCar;
    [SerializeField] bool Test;
    [SerializeField]int Testlevel;
    [SerializeField]int TestCar;
    levelStats lvldata;
    GameObject car;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (Test)
        {
            currlevel = Testlevel - 1;
            ValStorage.SetCarNumber(TestCar);
            
        }
        else
        {
            currlevel = ValStorage.selLevel - 1;
        }
    }

    IEnumerator Start()
    {
        soundManager = MySoundManager.instance;
        selCar = ValStorage.GetCarNumber();
        yield return new WaitForSeconds(2f);
        Env.SetActive(true);
        Loading.SetActive(false);
        StartCoroutine(startTimelines());
    }


    IEnumerator startTimelines()
    {
        yield return null;
        PlayableDirector playableDirector = cslvl[currlevel];
        CsStart.SetActive(true);
        playableDirector.gameObject.SetActive(true);
        soundManager?.SetBGM(true);

        if (playableDirector != null)
        {
            playableDirector.stopped += OnstartTLFinished;
        }
    }

    void OnstartTLFinished(PlayableDirector director)
    {
      StartCoroutine(SkipTL());
    }
    public void Skip() 
    {
        StartCoroutine(SkipTL());
    }

    public IEnumerator SkipTL()
    {
        LoadingPnl.SetActive(true);
        CsStart.SetActive(false);
        GP.SetActive(true);
        Levels[currlevel].SetActive(true);
        cslvl[currlevel].gameObject.SetActive(false);
        IgnitionBtn.SetActive(true);
        yield return new WaitForSeconds(2f);
        LoadingPnl.SetActive(false);
    }
    void SetLevel() 
    {
        Levels[currlevel].SetActive(true);
    }
    public void SetData(levelStats lvlstats) 
    {
        lvldata = lvlstats;
        dancingchar = lvldata.Dance;
        SetCar();
    }
    void SetCar() 
    {
        car = Cars[selCar- 1];
        car.transform.position = lvldata.SpawnPoint.position;
        car.transform.rotation = lvldata.SpawnPoint.rotation;
        car.SetActive(true);
        

        if (rccCam.TryGetComponent(out CarselCam))
        {
            CarselCam.target = car.transform;
        }

        if (car.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        stering();
        PedestrianMan._player = car.transform;
        Pedestrianparent.SetActive(true);
        PedestrianMan.gameObject.SetActive(true);
    }

    public void stering()
    {
        Invoke(nameof(delay), 2f);
    }
    private void delay()
    {
        RCC.SetBehavior(0);
        car.gameObject.layer = LayerMask.NameToLayer("Player");
    }




    public void PlayAppreciate(string action, string s)
    {
        int alreadyCoin = ValStorage.GetCoins("car");

        switch (action.ToLower()) 
        {
            case "appreciate":
               
                Appreciatetxt.text = s;
                Appreciate.SetActive(true);
                StartCoroutine(DelApprecOff("appreciate"));

                ValStorage.SetCoins("car", alreadyCoin + 15);
                break;

            case "objective":
               
                Objtxt.text = s;
                Obj.SetActive(true);
                StartCoroutine(DelApprecOff("objective"));
                break;

            case "discourage":
               
                Disctxt.text = s;
                Discourage.SetActive(true);
                StartCoroutine(DelApprecOff("discourage"));

                ValStorage.SetCoins("car", alreadyCoin - 15);
                break;

            default:
                Debug.LogWarning("Invalid action: " + action);
                break;
        }
    }

    IEnumerator DelApprecOff(string action)
    {
        yield return new WaitForSeconds(2.2f);

        switch (action.ToLower())
        {
            case "appreciate":
                Appreciate.SetActive(false);
                break;

            case "objective":
                Obj.SetActive(false);
                break;

            case "discourage":
                Discourage.SetActive(false);
                break;

            default:
                Debug.LogWarning("Invalid action passed to DelApprecOff: " + action);
                break;
        }
    }


    public void StartDance()
    {
        Transform dancechar = dancingchar.transform;
        foreach (Transform child in dancechar)
        {
            Animator animator = child.GetComponent<Animator>();

            if (animator != null)
            {
                animator.SetBool("Dance", true);
            }
        }
    }

    public void Contrls(bool isTrue)
    {
        if (isTrue)
            Controls.alpha = 1f;
        else
            Controls.alpha = 0f;

        Controls.interactable = isTrue;
        Controls.blocksRaycasts = isTrue;
    }
    public void Shakecam()
    {
        soundManager?.PlayEngineSound();
        IgnitionBtn.SetActive(false);
        shakeCam.DOShakePosition(0.5f, 0.5f, 10, 90f).OnKill(() => OnShakeComplete());
    }
    void OnShakeComplete()
    {
        Contrls(true);
       // rb.isKinematic = false;

        UIAnimator uianim = Controls.GetComponent<UIAnimator>();
        uianim.PlayAnimation(AnimSetupType.Intro);
        soundManager?.SetBGM(true);

    }

    public void ToggleSeatBelt()
    {

        soundManager?.PlayButtonClickSound();

        if (soundManager)
            soundManager.PlayButtonClickSound();

        Belt.SetActive(true);
        Beltbtn.SetActive(false);
        Invoke(nameof(delayoff), 1.05f);
    }

    void delayoff()
    {
        Belt.SetActive(false);
    }
    public void ChangeControl()
    {
        soundManager?.PlayButtonClickSound();

        //PlayInterAD();
        int currentind = ValStorage.GetControls();
        currentind = (currentind + 1) % 3;
        RccControls.SetMobileController(currentind);
        ValStorage.SetControls(currentind);
    }

    public void Pause()
    {
        soundManager?.PauseSounds();
       // PlayInterAD();
       // PlayRectBanner(true);


        soundManager?.PlayButtonClickSound();

        CarSound(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    void CarSound(bool IsActive)
    {
        Transform child = car.transform.Find("All Audio Sources");
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
        soundManager?.PlayButtonClickSound();
       // PlayInterAD();
        Time.timeScale = 1f;
        StopCoinAnimation();
        LoadingPnl.SetActive(true);
        LoadBar.SetActive(true);
        //StartCoroutine(LoadAsyncScene("MM"));
        StartCoroutine(StartLoading("MM"));
    }
    public void Restart()
    {
        soundManager?.PlayButtonClickSound();
      //  PlayInterAD();
        Time.timeScale = 1f;
        StopCoinAnimation();
        LoadingPnl.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(StartLoading("CarDriveSchool"));
    }
    public void Resume()
    {
        soundManager?.PlayButtonClickSound();
        soundManager?.ResumeSounds();
       // PlayRectBanner(false);
        CarSound(true);
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }


    void SetCoinsinPanel()
    {
        CoinsEarnedlvltxt.text = 300.ToString();
        StartCoroutine(CounterAnimation(CalculateTotalCoins()));
        int alreadycoins = ValStorage.GetCoins("truck");
        int totalcoins = alreadycoins + CalculateTotalCoins();
        ValStorage.SetCoins("car", totalcoins);
    }
    private int CalculateTotalCoins()
    {
        int coinsFromTime = Mathf.FloorToInt(elapsedTime * 2);

        int total = 300 + coinsFromTime;
        return total;
    }
    public void StopCoinAnimation()
    {
        stopAnimation = true;
    }
    private IEnumerator CounterAnimation(int totalCoins)
    {
        yield return new WaitForSeconds(1f);
        int duration = 3;
        float elapsedTime = 0f;
        int currentCoins = 0;

        // Play sound if available
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


   

    AsyncOperation asyncLoad;

    public IEnumerator StartLoading(string sceneName)
    {
      //  ButtonActivity("Loading");
        loadingText.text = 0f.ToString();
        //  AdsController.Instance?.ShowInterstitialAd_Admob();
        yield return null;// new WaitForSeconds(0.1f);
     //   PlayRectBanner(true);
       

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
      //  PlayRectBanner(false);
        sphereanim.enabled = false;
        asyncLoad.allowSceneActivation = true;
    }

    public void CarFellOcean()
    {
        rccCam.cameraTarget = null;
        rccCam.enabled = false;
        Contrls(false);


        soundManager?.SetBGM(false);
        soundManager?.SplashSound();
        CarSound(false);
        Invoke(nameof(DelFail), 4f);
    }

    void DelFail()
    {
        //truck.gameObject.SetActive(false);
        //failPanel.SetActive(true);
    }
    public void Celeb()
    {

        soundManager?.PlayCompleteSound(true);

        Contrls(false);
        CarSound(false);
        lvldata.linerend.SetActive(false);

        Traffic.SetActive(false);
        DisableAllCars();
        StartDance();
        Contrls(false);
        if (rccCam.TryGetComponent(out CarselCam))
        {
            CarselCam.enabled = true;
        }
        Conftti.Play();
    
        StartCoroutine(compltePanel());
    }

    IEnumerator compltePanel()
    {
        UnlckNxtLvl();
        yield return new WaitForSeconds(12f);
        delComp();
    }
    void delComp()
    {
        soundManager?.SetBGM(false);
        CompletePnl.SetActive(true);
        SetCoinsinPanel();
    }
    void UnlckNxtLvl()
    {
        int currlvl = ValStorage.selLevel;
        int unlockdlvls = ValStorage.GetUnlockedModeLevelDrive("car");
        if (currlvl == unlockdlvls && currlvl < 5)
        {
            ValStorage.SetUnlockedModeLevelDrive("car", unlockdlvls + 1);
        }

        if (currlvl == 5)
        {
            NxtBtnSccs?.SetActive(false);
        }
    }
    public void NextLvlBtn()
    {
        soundManager?.PlayButtonClickSound();

        LoadingPnl.SetActive(true);
        LoadBar.SetActive(true);

        StopCoinAnimation();
        if (currlevel < 5)
        {
            ValStorage.selLevel += 1;
            StartCoroutine(StartLoading("CarDriveSchool"));
        }
    }

    public void OnButtonPressed()
    {
        soundManager?.PlayHorn("Car");
    }

    public void OnButtonReleased()
    {
        soundManager?.StopHorn();

    }

    public void DisableAllCars()
    {
        AITrafficController.Instance?.DisableAll();
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
    
}
