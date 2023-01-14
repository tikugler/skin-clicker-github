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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitVisualEffect()
    {
        if (SettingValues.isVisualEffectsOn)
        {

            Vector3 worldPosition = UICamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(hit, worldPosition, Quaternion.identity);

        }

    }
    public void CriticalHitVisualEffect()
    {
        if (SettingValues.isVisualEffectsOn)
        {
            Vector3 worldPosition = UICamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(criticalHit, worldPosition, Quaternion.identity);

        }
    }

    public void ToggleShowVisualEffects(bool isSelected)
    {
        SettingValues.isVisualEffectsOn = isSelected;
    }
}
