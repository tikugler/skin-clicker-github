using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sounds")]
    public AudioSource MusicAudio;
    public AudioSource EffectAudio;

    [Header("AudioClips")]
    [SerializeField] AudioClip CoinsDropSound;
    [SerializeField] AudioClip HitSound;
    [SerializeField] AudioClip CriticalSound;
    [SerializeField] AudioClip SelectSound;
    [SerializeField] AudioClip DrinkSound;




    /// <summary>
    /// if toggle for music in settings pop-up is selected, play music
    /// otherwise stop the music
    /// </summary>
    public void UpdateMusicSoundPlay(bool isChecked)
    {
        SettingValues.isMusicSoundOn = isChecked;
        if (SettingValues.isMusicSoundOn)
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
    /// otherwise sound effects will no be played
    /// </summary>
    public void UpdateEffectSoundPlay(bool isChecked)
    {
        SettingValues.isEffectSoundOn = isChecked;
       
    }

    /// <summary>
    /// determines the volume of MusicAudio by using Slider in the settings
    /// </summary>
    public void UpdateMusicSoundVolume(float newVolume)
    {
        SettingValues.musicSoundVolume = newVolume;
        MusicAudio.volume = SettingValues.musicSoundVolume;
    }

    /// <summary>
    /// determines the volume of EffectAudio by using Slider in the settings
    /// </summary>
    public void UpdateEffectSoundVolume(float newVolume)
    {
        SettingValues.effectSoundVolume = newVolume;
        EffectAudio.volume = SettingValues.effectSoundVolume;
    }

    public void PlayPayWithCoinsSound()
    {
        if (SettingValues.isEffectSoundOn)
        {
            EffectAudio.clip = CoinsDropSound;
            EffectAudio.Play();
        }  
    }

    public void PlayHitSound()
    {
        if (SettingValues.isEffectSoundOn)
        {
            EffectAudio.clip = HitSound;
            EffectAudio.Play();
        }   
    }

    public void PlayCriticalHitSound()
    {
        if (SettingValues.isEffectSoundOn)
        {
            EffectAudio.clip = CriticalSound;
            EffectAudio.Play();
        }       
    }

    public void PlaySelectSound()
    {
        if (SettingValues.isEffectSoundOn)
        {
            EffectAudio.clip = SelectSound;
            EffectAudio.Play();
        } 
    }

    public void PlayDrinkSound()
    {
        if (SettingValues.isEffectSoundOn)
        {
            EffectAudio.clip = DrinkSound;
            EffectAudio.Play();
        }
    }

}
