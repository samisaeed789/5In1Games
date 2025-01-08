using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIAnimatorCore;

public class splash : MonoBehaviour
{
    MySoundManager soundManager;
    [SerializeField] GameObject startBtn;
    [SerializeField] GameObject DummyBus;
    [SerializeField] GameObject AnimBus;
    [SerializeField] GameObject LoadingPnl;
   
    
    Animator anim;
    
    
    void OnEnable()
    {
        anim = this.GetComponent<Animator>();

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        anim.Play(stateInfo.fullPathHash, 0, 0f); 

        startBtn.SetActive(true);

    }
    private void Start()
    {
        soundManager = MySoundManager.instance;

        soundManager.CarUnlock();
    }
    public void Ignition() 
    {
        soundManager.PlayEngineSound();
        startBtn.SetActive(false);
        Invoke("delAnim",1f);
    }
    void delAnim() 
    {
        anim.enabled = true;
    }
    void PlayBeep() 
    {
        soundManager.PlayBeepSound();
    }
    void Playrevv() 
    {
        soundManager.PlayRevvSound();
    }
    void Playrevv2() 
    {
        soundManager.PlayRevv1Sound();
    }

    void RunBus() 
    {
        if (DummyBus != null) 
        {
            DummyBus.SetActive(false);
        }
        
        if (AnimBus != null) 
        {
            AnimBus.SetActive(true);
        }
        anim.enabled = false;
        Invoke(nameof(Loading),1.2f);
    }

    
    void Loading() 
    {
        anim.enabled = false;
        LoadingPnl.SetActive(true);
    }
    
   

}
