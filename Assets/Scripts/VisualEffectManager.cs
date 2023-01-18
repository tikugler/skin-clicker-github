using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VisualEffectManager : MonoBehaviour
{

    [SerializeField] GameObject hit;
    [SerializeField] GameObject criticalHit;
    [SerializeField] Camera UICamera;
    [SerializeField]  Toggle VisualEffecToggle;




    private void OnEnable()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!Account.LoggedIn)
            {
                return;
            }
        }

        VisualEffecToggle.onValueChanged.AddListener(ToggleShowVisualEffects);
        VisualEffecToggle.isOn = SettingValues.isVisualEffectsOn;
        VisualEffecToggle.onValueChanged.Invoke(SettingValues.isVisualEffectsOn);


    }
    private void OnDisable()
    {
        VisualEffecToggle.onValueChanged.RemoveListener(ToggleShowVisualEffects);

    }


    /// <summary>
    /// instantiate pacticle effect for hit if visual effects are switched on
    /// Effects are centered on the camera
    /// </summary>
    public void HitVisualEffect()
    {
        if (SettingValues.isVisualEffectsOn)
        {
            Vector3 worldPosition = UICamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(hit, worldPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// instantiate pacticle effect for critical hit if visual effects are switched on
    /// Effects are centered on the camera
    /// </summary>
    public void CriticalHitVisualEffect()
    {
        if (SettingValues.isVisualEffectsOn)
        {
            Vector3 worldPosition = UICamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(criticalHit, worldPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// toggles 
    /// </summary>
    /// <param name="isSelected">the value whether the toggle is selected or deselected</param>
    public void ToggleShowVisualEffects(bool isSelected)
    {
        SettingValues.isVisualEffectsOn = isSelected;
    }
}
