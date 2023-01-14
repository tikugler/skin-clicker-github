using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSceneManager : MonoBehaviour
{

    [SerializeField] GameObject SoundSettingsPopUp;
    [SerializeField] Button OpenSoundSettingsButton;
    [SerializeField] Button CloseSoundSettingsButton;

    [SerializeField] Slider MusicSoundSlider;
    [SerializeField] Slider EffectSoundSlider;

    [SerializeField] Toggle MusicSoundToggle;
    [SerializeField] Toggle EffectSoundToggle;


    private static SoundManager soundManager;
    private static bool isUseable;

    private static bool isFirstRun = true;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!Account.LoggedIn)
            {
                OpenSoundSettingsButton.interactable = false;
                isUseable = false;
                return;
            }
        }

        if (!isFirstRun)
        {
            MusicSoundSlider.value = SettingValues.musicSoundVolume;
            EffectSoundSlider.value = SettingValues.effectSoundVolume;
            MusicSoundToggle.isOn = SettingValues.isMusicSoundOn;
            EffectSoundToggle.isOn = SettingValues.isEffectSoundOn;
        }

        OpenSoundSettingsButton.onClick.AddListener(OpenSoundSettingsPopUp);
        CloseSoundSettingsButton.onClick.AddListener(CloseSoundSettingsPopUp);

        MusicSoundSlider.onValueChanged.AddListener(UpdateMusicSoundVolume);
        EffectSoundSlider.onValueChanged.AddListener(UpdateEffectSoundVolume);

        MusicSoundToggle.onValueChanged.AddListener(UpdateMusicSoundPlay);
        EffectSoundToggle.onValueChanged.AddListener(UpdateEffectSoundPlay);

        

        soundManager = GameObject.Find("UserInfoCanvas").GetComponent<SoundManager>();
        if (isFirstRun)
        {
            MusicSoundSlider.onValueChanged.Invoke(SettingValues.musicSoundVolume);
            EffectSoundSlider.onValueChanged.Invoke(SettingValues.effectSoundVolume);
            MusicSoundToggle.onValueChanged.Invoke(SettingValues.isMusicSoundOn);
            EffectSoundToggle.onValueChanged.Invoke(SettingValues.isEffectSoundOn);
        }

        isUseable = true;


        isFirstRun = false;

    }

    private void Destroy()
    {
        OpenSoundSettingsButton.onClick.RemoveListener(OpenSoundSettingsPopUp);
        CloseSoundSettingsButton.onClick.RemoveListener(CloseSoundSettingsPopUp);

        MusicSoundSlider.onValueChanged.RemoveListener(UpdateMusicSoundVolume);
        EffectSoundSlider.onValueChanged.RemoveListener(UpdateEffectSoundVolume);

        MusicSoundToggle.onValueChanged.RemoveListener(UpdateMusicSoundPlay);
        EffectSoundToggle.onValueChanged.RemoveListener(UpdateEffectSoundPlay);

    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSoundSettingsPopUp()
    {
        SoundSettingsPopUp.SetActive(true);
    }

    public void CloseSoundSettingsPopUp()
    {
        SoundSettingsPopUp.SetActive(false);
    }


    /// <summary>
    /// if toggle for music in settings pop-up is selected, play music
    /// otherwise stop the music
    /// </summary>
    public void UpdateMusicSoundPlay(bool isChecked)
    {
        soundManager.UpdateMusicSoundPlay(isChecked);
    }

    // <summary>
    /// if toggle for effect in settings pop-up is selected, play effect sounds
    /// otherwise stop the effect sounds
    /// 
    /// Note: it plays an audio for testing
    /// in the future, it will be used isEffectSoundOn and effectSoundVolume,
    /// if user causes some effects by interacting with game 
    /// </summary>
    public void UpdateEffectSoundPlay(bool isChecked)
    {

        soundManager.UpdateEffectSoundPlay(isChecked);

    }

    /// <summary>
    /// determines the volume of MusicAudio by using Slider in the settings
    /// </summary>
    public void UpdateMusicSoundVolume(float newVolume)
    {
        soundManager.UpdateMusicSoundVolume(newVolume);
    }

    /// <summary>
    /// determines the volume of EffectAudio by using Slider in the settings
    /// </summary>
    public void UpdateEffectSoundVolume(float newVolume)
    {
        soundManager.UpdateEffectSoundVolume(newVolume);
    }

    public void PlayPayWithCoinsSound()
    {
        if(isUseable)
            soundManager.PlayPayWithCoinsSound();
    }

    public void PlayHitSound()
    {
        if (isUseable)
            soundManager.PlayHitSound();
    }

    public void PlayCriticalHitSound()
    {
        if (isUseable)
            soundManager.PlayCriticalHitSound();
    }

    public void PlaySelectSound()
    {
        if (isUseable)
            soundManager.PlaySelectSound();
    }
    public void PlayDrinkSound()
    {
        if (isUseable)
            soundManager.PlayDrinkSound();
    }

}

