using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Settings")]
    public GameObject SettingsButton;
    public GameObject SettingsPopUp;

    public Toggle MusicToggle;
    public Slider MusicSlider;

    public Toggle EffectToggle;
    public Slider EffectSlider;

    [Header("Sounds")]
    public AudioSource MusicAudio;
    public AudioSource EffectAudio;

    //  if effect sounds muted or unmuted
    bool isEffectSoundOn;
    //  the volume of effect sounds
    float effectSoundVolume;


    // Start is called before the first frame update
    void Start()
    {
        InitAudios();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitAudios()
    {
        UpdateMusicSoundVolume();
        UpdateEffectSoundVolume();
        UpdateMusicSoundPlay();
        UpdateEffectSoundPlay();
    }

    public void ShowSettingsPopUp()
    {
        SettingsPopUp.SetActive(true);
    }

    public void CloseSettingsPopUp()
    {
        SettingsPopUp.SetActive(false);
    }

    /// <summary>
    /// if toggle for music in settings pop-up is selected, play music
    /// otherwise stop the music
    /// </summary>
    public void UpdateMusicSoundPlay()
    {
        if (MusicToggle.isOn)
        {
            MusicAudio.Play();
        }
        else
        {
            if (MusicAudio.isPlaying)
            {
                MusicAudio.Stop();
            }
        }
    }

    // <summary>
    /// if toggle for effect in settings pop-up is selected, play effect sounds
    /// otherwise stop the effect sounds
    /// 
    /// Note: it plays an audio for testing
    /// in the future, it will be used isEffectSoundOn and effectSoundVolume,
    /// if user causes some effects by interacting with game 
    /// </summary>
    public void UpdateEffectSoundPlay()
    {
        if (EffectToggle.isOn)
        {
            EffectAudio.Play();
            effectSoundVolume = EffectSlider.value;
        }
        else
        {
            if (EffectAudio.isPlaying)
            {
                EffectAudio.Stop();
                isEffectSoundOn = false;
            }
        }
    }

    /// <summary>
    /// determines the volume of MusicAudio by using Slider in the settings
    /// </summary>
    public void UpdateMusicSoundVolume()
    {
        MusicAudio.volume = MusicSlider.value;
    }

    /// <summary>
    /// determines the volume of EffectAudio by using Slider in the settings
    /// </summary>
    public void UpdateEffectSoundVolume()
    {
        EffectAudio.volume = EffectSlider.value;

        effectSoundVolume = EffectSlider.value;
        
    }
}
