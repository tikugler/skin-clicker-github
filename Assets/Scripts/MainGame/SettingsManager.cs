using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsButton;
    public GameObject closeSettingsPanel;
    public GameObject settingsPanel;

    public void SettingsButtonAction()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanelAction()
    {
        settingsPanel.SetActive(false);
    }
}
