using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadcardrive : MonoBehaviour
{
    public float loadingDuration = 5f;
    public bool IsNextScene;


    [Header("Loading")]
    public Text loadingText;
    public Orbit sphereanim;
    public bool IsPercentage;



    [Header("Loading_Filler")]
    public Image loadingImage;
    public bool IsFiller;


    public GameObject SplashPnl;

    AsyncOperation asyncLoad;

    private void OnEnable()
    {
        if (IsNextScene)
        {
            if (IsFiller)
            {
                StartLoadingFill("MM");
            }
            else
            {
                StartLoading("MM");
            }
        }
        else
        {
            if (IsFiller)
            {
                StartLoadingFilltemp();
            }
            else
            {
                StartLoadingTemp();
            }
        }
    }


    public void StartLoadingTemp()
    {
        DOTween.To(() => 0f, value => UpdateLoadingText(value), 100f, loadingDuration)
               .SetEase(Ease.Linear)
               .OnKill(() => OnLoadingCompletetemp());
    }
    void OnLoadingCompletetemp()
    {
        sphereanim.enabled = false;
        SplashPnl.SetActive(true);
        UpdateLoadingText(0);
        this.gameObject.SetActive(false);
        IsNextScene = true;
    }


    public void StartLoadingFilltemp()
    {
        DOTween.To(() => 0, value => loadingImage.fillAmount = value, 1f, loadingDuration)
                .SetEase(Ease.Linear)
                .OnKill(() => OnLoadingCompleteTemp());
    }
    void OnLoadingCompleteTemp()
    {
        SplashPnl.SetActive(true);
        loadingImage.fillAmount = 0f;
        IsNextScene = true;
        this.gameObject.SetActive(false);
    }


    public void StartLoadingFill(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0, value => loadingImage.fillAmount = value, 1f, loadingDuration)
                .SetEase(Ease.Linear)
                .OnKill(() => OnLoadingCompleteFill());
    }
    void OnLoadingCompleteFill()
    {
        asyncLoad.allowSceneActivation = true;
    }

    public void StartLoading(string sceneName)
    {

        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        DOTween.To(() => 0f, value => UpdateLoadingText(value), 100f, loadingDuration)
               .SetEase(Ease.Linear)
               .OnKill(() => OnLoadingComplete());
    }
    
    
    void OnLoadingComplete()
    {
        sphereanim.enabled = false;
        asyncLoad.allowSceneActivation = true;
    }

    void UpdateLoadingText(float value)
    {

        loadingText.text = $"{Mathf.FloorToInt(value)}%";
    }

   
   
}
