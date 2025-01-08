using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashJeep : MonoBehaviour
{
    [SerializeField] GameObject Splash;
    [SerializeField] GameObject Splash2;
    [SerializeField]bool IsJeep;


    [SerializeField] Image EuroTruckSplash;
    [SerializeField] bool isEuroTruck;
    AsyncOperation asyncLoad;
    IEnumerator Start()
    {

        if (IsJeep) 
        {
            Splash.SetActive(true);
            yield return new WaitForSeconds(4f);
            Splash2.SetActive(true);
            asyncLoad = SceneManager.LoadSceneAsync("MM");
            asyncLoad.allowSceneActivation = false;
            yield return new WaitForSeconds(5f);
            asyncLoad.allowSceneActivation = true;
        }

        if (isEuroTruck) 
        {
            Invoke(nameof(del),0.1f);
        }

    }

    void del() 
    {
        StartLoadingFill("MM");

    }
    public void StartLoadingFill(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0, value => EuroTruckSplash.fillAmount = value, 1f, 15f)
                .SetEase(Ease.Linear)
                .OnKill(() => asyncLoad.allowSceneActivation = true);
    }
    
}
