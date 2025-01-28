using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
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

        ValStorage.selLevel = 1;
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
        soundManager = MySoundManager.instance;
        currLvl = ValStorage.selLevel-1;
        PlayTimeline(currLvl);
        Contrls(false);
        ValStorage.SetCarUnLocked(CarType.Swat);
        ValStorage.SetCarUnLocked(CarType.Regular);
    }

    public void PlayTimeline(int index)
    {
        CS.SetActive(true);
        csdata[index].CSLevel.SetActive(true);
        playableDirector= csdata[index].playable;
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineFinished;
        }
        //GameObject cutscenelvl = CSlvl[index];//.SetActive(true);
        //cutscenelvl.SetActive(true);
        //  playableDirector= cutscenelvl.transform.GetChild(0).GetComponent<>()
        //if (index >= 0 && index < CSdata.Length)
        //{
        //    playableDirector.playableAsset = CSdata[index].timelines;//  timelines[index];
        //    playableDirector.Play();
        //}
        //else
        //{
        //    Debug.LogError("Timeline index out of range");
        //}
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
        GP.SetActive(true);
        CarSel.SetActive(false);
        //map.gameObject.SetActive(true);
        EnemyCars[currLvl].SetActive(true);
        CarType car = GetCurrentCarType();
        Contrls(true);
        switch (car)
        {
            case CarType.Regular:
                Cars[0].SetActive(true);
                map.target = Cars[0].transform;
                break;
            case CarType.Ford:
                Cars[2].SetActive(true);
                map.target = Cars[2].transform;
                break;
            case CarType.Swat:
                Cars[1].SetActive(true);
                map.target = Cars[1].transform;
                break;
            default:
                break;
        }
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
    void PlayObj() 
    {
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
        ObjPanel.SetActive(false);
        ThirdPersonCntrls.SetActive(true);
        soundManager?.SetBGM(true);

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
        GP.SetActive(false);
        Finalpolice.SetActive(true);
        StartCoroutine(CompletePanel());
    }
    IEnumerator CompletePanel()
    {

        UnlckNxtLvl();
        yield return new WaitForSeconds(10f);
        soundManager?.PlayChatterSound(false);
        delComp();

        //if (soundManager)
        //{
        //    soundManager.PlayCompleteSound(false);
        //   // CarSound(false);
        //}

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
   
}
