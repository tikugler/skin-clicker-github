using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Settings")]
    public GameObject SettingsPopUp;

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

    /// <summary>
    /// First, slider and toggle ui elements with tag Sound in Canvas are
    /// searched, then onValueChange is invoked for each found ones,
    /// to set the sound level for music and effects.
    /// This method is called, only when the game starts.
    /// </summary>
    private void InitAudios()
    {
        GameObject canvas = GameObject.Find("Canvas");
        foreach (Slider slider in canvas.GetComponentsInChildren<Slider>(true))
        {
            if (slider.CompareTag("Sound"))
            {
                slider.onValueChanged.Invoke(slider.value);
            }
        }
        foreach (Toggle toggle in canvas.GetComponentsInChildren<Toggle>(true))
        {
            if (toggle.CompareTag("Sound"))
            {
                toggle.onValueChanged.Invoke(toggle.isOn);
            }
        }
    }

    /// <summary>
    /// opens settings pop-up window
    /// </summary>
    public void ShowSettingsPopUp()
    {
        SettingsPopUp.SetActive(true);
    }

    /// <summary>
    /// closes settings pop-up window
    /// </summary>
    public void CloseSettingsPopUp()
    {
        SettingsPopUp.SetActive(false);
    }

    /// <summary>
    /// if toggle for music in settings pop-up is selected, play music
    /// otherwise stop the music
    /// </summary>
    public void UpdateMusicSoundPlay(bool isChecked)
    {
        if (isChecked)
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
    public void UpdateEffectSoundPlay(bool isChecked)
    {
        if (isChecked)
        {
            EffectAudio.Play();
            isEffectSoundOn = true;
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
    public void UpdateMusicSoundVolume(float newVolume)
    {
        MusicAudio.volume = newVolume;
    }

    /// <summary>
    /// determines the volume of EffectAudio by using Slider in the settings
    /// </summary>
    public void UpdateEffectSoundVolume(float newVolume)
    {
        EffectAudio.volume = newVolume;
        effectSoundVolume = newVolume;

    }


}
