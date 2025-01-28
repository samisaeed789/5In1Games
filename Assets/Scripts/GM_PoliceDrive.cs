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
    public TimelineAsset timelines;
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
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] CSData[] CSdata;



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
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineFinished;
        }
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

        ValStorage.SetCarUnLocked(CarType.Swat);
        ValStorage.SetCarUnLocked(CarType.Regular);
    }

    public void PlayTimeline(int index)
    {
        CS.SetActive(true);
        CSdata[index].CSLevel.SetActive(true);
        if (index >= 0 && index < CSdata.Length)
        {
            playableDirector.playableAsset = CSdata[index].timelines;//  timelines[index];
            playableDirector.Play();
        }
        else
        {
            Debug.LogError("Timeline index out of range");
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
        GP.SetActive(true);
        CarSel.SetActive(false);
        map.gameObject.SetActive(true);
        EnemyCars[currLvl].SetActive(true);
        CarType car = GetCurrentCarType();
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
        yield return new WaitForSeconds(5f);
        soundManager?.PlayChatterSound(false);
        completePanel.SetActive(true);
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
}
