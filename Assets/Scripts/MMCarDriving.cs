using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCarDriving : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MMPanel;
    public GameObject modeSelPanel;
    public GameObject lvlSelPanel;
    public GameObject garagePanel;
    public GameObject loadingPanel;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject carPurhasedPanel;
    public GameObject notEnoughCoinsPanel;
   


    public void ButtonActivity(string panelName)
    {
        switch (panelName)
        {

            case "MM":
                PanelActivity(MM: true);
                break;
            case "ModeSel":
                PanelActivity(ModeSel: true);
                break;
            case "LvlSel":
                PanelActivity(LvlSel: true);
                break;
            case "Garage":
                PanelActivity(Garage: true);
                break;
            case "SettingsPnl":
                PanelActivity(SettingsPnl: true);
                break;
            case "exitPanel":
                PanelActivity(ExitPnl: true);
                break;
            
            case "Loading":
                PanelActivity(Loading: true);
                break;
            default:
                break;
        }
    }

    public void PanelActivity(bool MM = false, bool ModeSel = false, bool LvlSel = false, bool ExitPnl = false, bool SettingsPnl = false, bool Garage = false, bool Loading = false)
    {
        if (MMPanel)
        {
            MMPanel.SetActive(MM);
        }
        if (modeSelPanel)
        {
            modeSelPanel.SetActive(ModeSel);
        }
        if (lvlSelPanel)
        {
            lvlSelPanel.SetActive(LvlSel);
        }
        if (garagePanel)
        {
            garagePanel.SetActive(Garage);
        }
        if (loadingPanel)
        {
            loadingPanel.SetActive(Loading);
        }
        if (settingsPanel)
        {
            settingsPanel.SetActive(SettingsPnl);
        } 
        if (exitPanel)
        {
            exitPanel.SetActive(ExitPnl);
        }
    }


    public void StartMM()
    {
        
    }
}
