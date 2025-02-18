using Gley.PedestrianSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;


[System.Serializable]
public class ETData
{
    public GameObject Truck;
    public GameObject Trailer;
    public GameObject TrailerDummy;
    public Transform transform;
    public GameObject[] Off;
    public GameObject lineRender ;
}
public class GM_Euro_Drive : MonoBehaviour
{

    [Header("Canvas")]
    [SerializeField] GameObject fadeanim;
    [SerializeField] CanvasGroup Controls;


    [Header("GPDATA")]
    [SerializeField] GameObject HookPoint;
    [SerializeField] GameObject Gp;
    [SerializeField] ETData[] truckdata;
    [SerializeField] GameObject[] Levels;
    [SerializeField] GameObject[] DummyTrailers;
    [SerializeField] GameObject[] DummyTrucks;
    [SerializeField] ParticleSystem CollectbleConfetti;
    [SerializeField] ParticleSystem CollectbleCoin;
    [SerializeField] ParticleSystem ShowerConfti;
    [SerializeField] PedestrianSystemComponent PedestrianMan;
    [SerializeField] GameObject Traffic;



    [Header("CSDATA")]
    [SerializeField] GameObject CS;
    [SerializeField] CSData[] csdata;
    PlayableDirector playableDirector;


    [SerializeField] int currlevel;
    [SerializeField] RCC_Camera rccCam;
    [SerializeField] RCC_CameraCarSelection CarselCam;
    [HideInInspector] int indexTruck;
    public static GM_Euro_Drive instance;
    RCC_CarControllerV3 truck;

    RCC_CameraConfig camset;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private IEnumerator Start()
    {
        SetTruck(currlevel);
        DeactivatePrevLvls();
        Levels[currlevel].SetActive(true);

        yield return new WaitForSeconds(3f);
        StartCoroutine(SetCam(3f,false));
    }


    void SetTruck(int lvl) 
    {
        truck = truckdata[lvl].Truck.GetComponent<RCC_CarControllerV3>();
        truck.enabled = true;
        truck.canControl= true;
    }
    void DeactivatePrevLvls() 
    {
        for (int i = 0; i < currlevel; i++)
        {
            DummyTrailers[i].SetActive(false);
            DummyTrucks[i].SetActive(false);
        }
    }
    public void HookTrailerTimeline()// int index) 
    {
        StartCoroutine(PlayTimeline());
    }
    IEnumerator PlayTimeline()
    {
        yield return null;
        Contols(false);
        rccCam.gameObject.SetActive(false);
        Gp.SetActive(false);
        CS.SetActive(true);
        csdata[currlevel].CSLevel.SetActive(true);
        playableDirector = csdata[currlevel].playable;

        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineFinished;
        }
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        foreach (GameObject g in truckdata[currlevel].Off) 
        {
            g.SetActive(false);
        }
        StartCoroutine(SetCam(3f, true));
        setpos(truckdata[currlevel].transform);
        truckdata[currlevel].TrailerDummy.gameObject.SetActive(false);
        truckdata[currlevel].lineRender.SetActive(true);
        truckdata[currlevel].Trailer.gameObject.SetActive(true);
        Transform Car= truckdata[currlevel].Truck.transform;
        CarselCam.target = Car;
        CS.SetActive(false);
        Gp.SetActive(true);
        Contols(true);
        
        rccCam.gameObject.SetActive(true);
        PedestrianMan._player = Car;
        PedestrianMan.gameObject.SetActive(true);
        Traffic.SetActive(true);
    }

    void setpos(Transform pos) 
    {
        Rigidbody rb = truckdata[currlevel].Truck.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        truckdata[currlevel].Truck.transform.position = pos.position;
        truckdata[currlevel].Truck.transform.rotation = pos.rotation;
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
        truckdata[currlevel].Truck.transform.SetPositionAndRotation(pos.position, pos.rotation);
        yield return new WaitForSeconds(0.3f);
        fadeanim.SetActive(false);
        rccCam.enabled = false;
        CarselCam.enabled = true;
        ShowerConfti.Play();
        yield return new WaitForSeconds(12f);
    }

    IEnumerator SetCam(float delay,bool IsTrailer=false) 
    {
        yield return new WaitForSeconds(delay);
       
        if (IsTrailer) 
        {
            truck.gameObject.GetComponent<CamCustom>().setcamTrailer();
        }
        else 
        {
            truck.gameObject.GetComponent<CamCustom>().setcamTruck();
        }
    }

}
