using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnter : MonoBehaviour
{
    GameObject LockedBtn;
    GameObject UnLockedBtn;
    GM_PoliceDrive gm;
   
    
    public CarType carType;

    private void Start()
    {
        gm = GM_PoliceDrive.instance;
        LockedBtn = gm.LockedBtn;
        UnLockedBtn = gm.UnLockedBtn;
        ValStorage.IsRegularPurchased = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        
            gm?.SetCarType(carType);

            if (ValStorage.GetCarUnLocked(carType)) 
            {
                LockedBtn.SetActive(false);
                UnLockedBtn.SetActive(true);
            }
            else 
            {
                LockedBtn.SetActive(true);
                UnLockedBtn.SetActive(false);
            }
        }
    } 
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        
            gm?.SetCarType(CarType.None);
            LockedBtn.SetActive(false);
            UnLockedBtn.SetActive(false);
           
        }
    }


}
    
