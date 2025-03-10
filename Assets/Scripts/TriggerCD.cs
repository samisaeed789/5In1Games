using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCD : MonoBehaviour
{
    GM_CD gm;
    [SerializeField] bool IsIndi;
    [SerializeField] bool IsSpeedLimit;
    [SerializeField] bool IsCrossWalk;
    [SerializeField] bool IsPoliceCp;
    [SerializeField] bool IsTrafficSignal;


    public Pedestrian[] peds;
    public GameObject[] redSignals;
    public GameObject[] greenSignals;
    

    RCC_CarControllerV3 vehicle;


    private float timeInsideTrigger = 0f;
    public float timeToGreen = 5f;  

    [SerializeField] bool isPedestrianCrossing;
    private void Start()
    {
        gm = GM_CD.instance;

        StartCoroutine(WaitForVehicleAssignment());
    }

    private IEnumerator WaitForVehicleAssignment()
    {
        while (RCC_SceneManager.Instance.activePlayerVehicle == null)
        {
            yield return null;  // Wait for the next frame
        }

        vehicle = RCC_SceneManager.Instance.activePlayerVehicle;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (IsIndi) 
            {
                if (vehicle.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Right || vehicle.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Left)
                {
                    gm.PlayAppreciate("appreciate","Indicator Rule Followed");
                }
                else 
                {
                    gm.PlayAppreciate("discourage", "Rule Not Followed");
                }
            }

            if (IsSpeedLimit)
            {
                if (vehicle.speed <=40)
                {
                    gm.PlayAppreciate("appreciate", "Speed Limit Followed");
                }
                else
                {
                    gm.PlayAppreciate("discourage", "Speed Limit Not Followed");
                }
            }

            if (IsCrossWalk)
            {

                if (AllPedestriansCrossed() && IsPedRanOver()==false) 
                {
                     gm.PlayAppreciate("appreciate", "You Stopped For Zebra Crossing");

                }
                else 
                {
                    gm.PlayAppreciate("discourage", "You Violated Zebra Crossing");
                }
            }
            if (IsTrafficSignal) 
            {
                if (isGreenSignal) 
                {
                    gm.PlayAppreciate("appreciate", "You Followed Traffic Signal Rule");
                }
                else 
                {
                    gm.PlayAppreciate("discourage", "You Violated Rule");

                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsSpeedLimit)
            {
                gm.PlayAppreciate("objective", "Speed Check Ahead");
            }
            
            if (IsCrossWalk)
            {
                gm.PlayAppreciate("objective", "Zebra Crossing Ahead");
                StartPedestrianCrossing();
            }

            if (IsIndi)
            {
                gm.PlayAppreciate("objective", "Turn Indicator On");
            }
            if (IsPoliceCp)
            {
                gm.PlayAppreciate("objective", "Police Check Point Ahead");
            }
            
            if (IsTrafficSignal)
            {
                gm.PlayAppreciate("objective", "Traffic Signal Ahead");
                RedSignal(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsTrafficSignal)
            {
                timeInsideTrigger += Time.deltaTime;

                if (timeInsideTrigger >= timeToGreen)
                {
                   StartCoroutine( SetSignalToGreen());
                }
            }
        }
    }

    private void StartPedestrianCrossing()
    {
        foreach (var pedestrian in peds)
        {
            pedestrian.StartCrossing();  // Pass the target position for each pedestrian
        }

        Debug.Log("Pedestrians are crossing.");
    }

    private bool AllPedestriansCrossed()
    {
        foreach (var pedestrian in peds)
        {
            // If any pedestrian hasn't crossed, return false
            if (!pedestrian.HasCrossed()  && !pedestrian.HasranOver())
            {
                return false;
            }
        }
        return true; // All pedestrians have crossed
    }
    
    private bool IsPedRanOver()
    {
        foreach (var pedestrian in peds)
        {
            // If any pedestrian ran over, return true
            if (pedestrian.HasranOver())
            {
                return true;//some ran over 
            }
        }
        return false; // All pedestrians have crossed
    }

    private void RedSignal(bool State) 
    {
        foreach(GameObject g in redSignals) 
        {
            g.SetActive(State);
        }
    }
    bool isGreenSignal;
    private void GreenSignal(bool State) 
    {
        foreach(GameObject g in greenSignals) 
        {
            g.SetActive(State);
        }
        isGreenSignal = State;
    }

    private IEnumerator SetSignalToGreen() 
    {
        RedSignal(false);
        yield return new WaitForSeconds(2f);
        GreenSignal(true);
    }
}
