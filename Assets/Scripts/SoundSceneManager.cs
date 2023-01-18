using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSceneManager : MonoBehaviour
{

    [SerializeField] GameObject SoundSettingsPopUp; // Settings PopUp
    [SerializeField] Button OpenSoundSettingsButton; // button to open settings PopUp
    [SerializeField] Button CloseSoundSettingsButton; // button to close settings PopUp

    [SerializeField] Slider MusicSoundSlider; // slider to set up the volume of background music
    [SerializeField] Slider EffectSoundSlider; // slider to set up the volume of effect music

    [SerializeField] Toggle MusicSoundToggle; // toggle for background music
    [SerializeField] Toggle EffectSoundToggle; // toggle for effect sounds


    private static SoundManager soundManager;
    // this variable will be false if game started from GameScene, which is only in unity Edifor for development possible
    private static bool isUseable; 

    private static bool isFirstRun = true;


    /// <summary>
    /// adds multiple listeners
    /// sets up the Settings panel
    /// if user changed the scene, then all settings from older scene is loaded by saving values in the static class SettingValues 
    /// </summary>
    private void Start()
    {
      
        if (SceneManager.GetActiveScene().buildIndex == 1)
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

    /// <summary>
    /// removes all listener
    /// </summary>
    private void Destroy()
    {
        OpenSoundSettingsButton.onClick.RemoveListener(OpenSoundSettingsPopUp);
        CloseSoundSettingsButton.onClick.RemoveListener(CloseSoundSettingsPopUp);

        MusicSoundSlider.onValueChanged.RemoveListener(UpdateMusicSoundVolume);
        EffectSoundSlider.onValueChanged.RemoveListener(UpdateEffectSoundVolume);

        MusicSoundToggle.onValueChanged.RemoveListener(UpdateMusicSoundPlay);
        EffectSoundToggle.onValueChanged.RemoveListener(UpdateEffectSoundPlay);

    }

   

    // opens Settings PopUp
    public void OpenSoundSettingsPopUp()
    {
        SoundSettingsPopUp.SetActive(true);
    }

    // closes Settings PopUp
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


    // plays coin sound if isUsable is true
    public void PlayPayWithCoinsSound()
    {
        if(isUseable)
            soundManager.PlayPayWithCoinsSound();
    }

    // plays hit sound if isUsable is true
    public void PlayHitSound()
    {
        if (isUseable)
            soundManager.PlayHitSound();
    }


    // plays critical hit sound if isUsable is true
    public void PlayCriticalHitSound()
    {
        if (isUseable)
            soundManager.PlayCriticalHitSound();
    }

    // plays selection sound if isUsable is true
    public void PlaySelectSound()
    {
        if (isUseable)
            soundManager.PlaySelectSound();
    }

    // plays drinking sound if isUsable is true
    public void PlayDrinkSound()
    {
        if (isUseable)
            soundManager.PlayDrinkSound();
    }

}

