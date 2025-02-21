using DG.Tweening;
using Gley.PedestrianSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UIAnimatorCore;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TrucksTrailer
{
    public GameObject Truck;
    public GameObject Trailer;
    
}

[System.Serializable]
public class Level
{
    public PlayableDirector CS;
    public GameObject LVL;
    public GameObject tRUCK;
}
public class GM_Euro_Drive : MonoBehaviour
{

    [Header("Canvas")]
    [SerializeField] CanvasGroup Controls;
    [SerializeField] GameObject fadeanim;

    [SerializeField] GameObject IgnitionBtn;
    [SerializeField] GameObject LoadingPnl;
    [SerializeField] GameObject CompletePanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject LoadBar;
    [SerializeField] Image loadingBar;

    [SerializeField] Text CoinsEarnedlvltxt;
    [SerializeField] Text TotalCompltxt;
    [SerializeField] GameObject NxtBtnSccs;



    [Header("GPDATA")]
    [SerializeField] GameObject Gp;
    [SerializeField] ParticleSystem CollectbleConfetti;
    [SerializeField] ParticleSystem CollectbleCoin;
    [SerializeField] ParticleSystem ShowerConfti;
    [SerializeField] Level[] lvl_data;
    [SerializeField] TrucksTrailer[] trucksntrailers;

    [Header("Managers")]
    [SerializeField] PedestrianSystemComponent PedestrianMan;
    [SerializeField] GameObject Traffic;
    [SerializeField] RCC_Demo RccControls;



    [Header("CSDATA")]
    [SerializeField] GameObject CSStart;
    [SerializeField] GameObject CSHook;
    PlayableDirector playableDirector;

   
    
    [Header("Cameras")]
    [SerializeField] RCC_Camera rccCam;
    [SerializeField] Camera shakeCam;
    [SerializeField] RCC_CameraCarSelection CarselCam;


    //Cache 
    [SerializeField] int currlevel;
    RCC_CarControllerV3 truck;
    levelStats lvldata;
    bool stopAnimation;
    float elapsedTime = 0f;




    public static GM_Euro_Drive instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private IEnumerator Start()
    {
        currlevel = ValStorage.selLevel - 1;
        yield return new WaitForSeconds(2f);
        LoadingPnl.SetActive(false);
        StartCoroutine(startTimelines());
    } 


    IEnumerator startTimelines()
    {
        yield return null;
        playableDirector = lvl_data[currlevel].CS;
        CSStart.SetActive(true);
        playableDirector.gameObject.SetActive(true);
        if (playableDirector != null)
        {
            playableDirector.stopped += OnstartTLFinished;
        }
    }


    void OnstartTLFinished(PlayableDirector director) 
    {
        DeactivatePrevLvls();
        Gp.SetActive(true);
        lvl_data[currlevel].LVL.SetActive(true);
        playableDirector.gameObject.SetActive(false);
        CSStart.SetActive(false);
        SetTruck(currlevel);
        IgnitionBtn.SetActive(true);
        StartCoroutine(SetCam(3f, false));
    }


    void SetTruck(int lvl) 
    {
        truck = lvldata.Truck;
        if (truck) 
        {
            truck.enabled = true;
            truck.canControl = true;
        }
        rb = truck.GetComponent<Rigidbody>();
    }
    void DeactivatePrevLvls() 
    {
        for (int i = 0; i < currlevel; i++)
        {
            trucksntrailers[i].Truck.SetActive(false);
            trucksntrailers[i].Trailer.SetActive(false);
        }
    }
    public void HookTrailerTimeline()
    {
        StartCoroutine(PlayTimeline());
    }
    IEnumerator PlayTimeline()
    {

        yield return null;
        Contols(false);
        rccCam.gameObject.SetActive(false);
        Gp.SetActive(false);
        CSHook.SetActive(true);
        playableDirector = lvldata.csHook.GetComponent<PlayableDirector>();
        lvldata.csHook.SetActive(true);

        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineFinished;
        }
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        StartCoroutine(SetCam(3f, true));
        setpos(lvldata.hookPoint);
        lvldata.DummyTrailer.SetActive(false);
        lvldata.linerend.SetActive(true);
        lvldata.Trailer.SetActive(true);

        Transform Car = truck.transform;
        CarselCam.target = Car;
        CSHook.SetActive(false);
        Gp.SetActive(true);
        Contols(true);
        Controls.gameObject.SetActive(true);
        rccCam.gameObject.SetActive(true);
        PedestrianMan._player = Car;
        PedestrianMan.gameObject.SetActive(true);
        Traffic.SetActive(true);
    }
    Rigidbody rb;
    void setpos(Transform pos) 
    {
        rb.isKinematic = true;
        truck.transform.position = pos.position;
        truck.transform.rotation = pos.rotation;
        rb.isKinematic = false;
    }

    public void CollectablePlay(bool isConfetti = false, bool isCoin = false)
    {
        if (isConfetti)
        {
            CollectbleConfetti.Play();
            //if (soundManager)
            //    soundManager.PlayCollectSound();
        }

        if (isCoin)
        {
            CollectbleCoin.Play();
            //if (soundManager)
            //    soundManager.PlayCollectCoin();
        }
    }


    void Contols(bool isTrue)
    {
        if (isTrue)
            Controls.alpha = 1f;
        else
            Controls.alpha = 0f;
        
        Controls.interactable = isTrue;
        Controls.blocksRaycasts = isTrue;
    }
    public void HandleCeleb(Transform pos)
    {
        //soundManager?.SetBGM(false);
        Contols(false);
        fadeanim.SetActive(true);
        StartCoroutine(delCeleb(pos));
    }
    
    IEnumerator delCeleb(Transform pos)
    {
        yield return new WaitForSeconds(1.7f);
        truck.transform.SetPositionAndRotation(pos.position, pos.rotation);
        yield return new WaitForSeconds(0.3f);
        fadeanim.SetActive(false);
        rccCam.enabled = false;
        CarselCam.enabled = true;
        ShowerConfti.Play();
        yield return new WaitForSeconds(12f);
        Contols(false);
        StartCoroutine(completePanel());
    }
    IEnumerator completePanel()
    {
        UnlckNxtLvl();
        PlayInterAD();
        yield return new WaitForSeconds(0.1f);
        delComp();
    }

    void UnlckNxtLvl()
    {
        PlayInterAD();
        int currlvl = ValStorage.selLevel;
        int unlockdlvls = ValStorage.GetUnlockedModeLevelDrive("truck");
        if (currlvl == unlockdlvls && currlvl < 5)
        {
            ValStorage.SetUnlockedModeLevelDrive("truck", unlockdlvls + 1);
        }

        if (currlvl == 5)
        {
            NxtBtnSccs?.SetActive(false);
        }
    }
    public void NextLvlBtn()
    {
       // soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        LoadingPnl.SetActive(true);
        LoadBar.SetActive(true);

        StopCoinAnimation();
        if (currlevel < 5)
        {
            ValStorage.selLevel += 1;
            StartCoroutine(LoadAsyncScene("EuroTruckMode"));
        }
    }
    void delComp()
    {
        PlayRectBanner(true);
        CompletePanel.SetActive(true);
        SetCoinsinPanel();
    }
    public void Pause()
    {
       // soundManager?.PauseSounds();
        PlayInterAD();
        PlayRectBanner(true);
      //  soundManager.PlaypoliceClickSound();


      //  if (soundManager)
          //  soundManager.PlayButtonClickSound();

        CarSound(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    void CarSound(bool IsActive)
    {
        Transform child = truck.transform.Find("All Audio Sources");
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
       // soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        Time.timeScale = 1f;
        StopCoinAnimation();
        LoadingPnl.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("MM"));
    }
    public void Restart()
    {
       // soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        Time.timeScale = 1f;
        StopCoinAnimation();
        LoadingPnl.SetActive(true);
        LoadBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("EuroTruckMode"));
    }
    public void Resume()
    {
       // soundManager?.PlaypoliceClickSound();
       // soundManager?.ResumeSounds();
        PlayRectBanner(false);
       // soundManager?.PlayPoliceSiren(true);
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
        ValStorage.SetCoins("truck", totalcoins);
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

        //if (soundManager)
        //    soundManager.PlaycoinSound();

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

        //// Stop sound if available
        //if (soundManager)
        //    soundManager.StopcoinSound();
    }
    IEnumerator SetCam(float delay,bool IsTrailer=false) 
    {
        yield return new WaitForSeconds(delay);

        CamCustom truckCamSet = truck.gameObject.GetComponent<CamCustom>();
        if (IsTrailer) 
        {
            truckCamSet.setcamTrailer();
        }
        else 
        {
            truckCamSet.setcamTruck();
        }
    }

    public void SetData(levelStats leveldata) 
    {

        lvldata = leveldata;
    }


    public void Shakecam()
    {
        IgnitionBtn.SetActive(false);
        shakeCam.DOShakePosition(0.5f, 0.5f, 10, 90f).OnKill(() => OnShakeComplete());
    }
    void OnShakeComplete()
    {
        
        Contols(true);
        rb.isKinematic = false;

        UIAnimator uianim = Controls.GetComponent<UIAnimator>();
        uianim.PlayAnimation(AnimSetupType.Intro);
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

    void SkipCS()
    {
        CSStart.SetActive(false);
        //CarSel.SetActive(true);
        //PlayObj();
    }

    public void ChangeControl()
    {
      //  soundManager?.PlaypoliceClickSound();
        PlayInterAD();
        int currentind = ValStorage.GetControls();
        currentind = (currentind + 1) % 3;
        RccControls.SetMobileController(currentind);
        ValStorage.SetControls(currentind);
    }
    public void PlayHorn()
    {
        //if (soundManager)
        //{
        //    soundManager.SetBGM(true);
        //}
    }
    public void CarFellOcean()
    {
        rccCam.cameraTarget = null;
        rccCam.enabled = false;
        Contols(false);

        //soundManager?.SetBGM(false);
        //soundManager?.PlayPoliceSiren(false);
        //soundManager?.SplashSound();


        CarSound(false);
        Invoke(nameof(DelFail), 4f);
    }

    void DelFail()
    {
        truck.gameObject.SetActive(false);
        failPanel.SetActive(true);
    }
}
