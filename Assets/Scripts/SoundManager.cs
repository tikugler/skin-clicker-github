using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sounds")]
    public AudioSource MusicAudio;
    public AudioSource EffectAudio;

    [SerializeField] AudioClip CoinsDropSound;

    //  if effect sounds muted or unmuted
    public static bool isEffectSoundOn = false;
    //  the volume of effect sounds
    public static float effectSoundVolume = 0.5f;


    //  if effect sounds muted or unmuted
    public static bool isMusicSoundOn = false;
    //  the volume of effect sounds
    public static float musicSoundVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    /// <summary>
    /// if toggle for music in settings pop-up is selected, play music
    /// otherwise stop the music
    /// </summary>
    public void UpdateMusicSoundPlay(bool isChecked)
    {
        isMusicSoundOn = isChecked;
        if (isMusicSoundOn)
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
        isEffectSoundOn = isChecked;
        if (isEffectSoundOn)
        {
            EffectAudio.Play();
        }
        else
        {
            if (EffectAudio.isPlaying)
            {
                EffectAudio.Stop();
            }
        }
    }

    /// <summary>
    /// determines the volume of MusicAudio by using Slider in the settings
    /// </summary>
    public void UpdateMusicSoundVolume(float newVolume)
    {
        musicSoundVolume = newVolume;
        MusicAudio.volume = musicSoundVolume;
    }

    /// <summary>
    /// determines the volume of EffectAudio by using Slider in the settings
    /// </summary>
    public void UpdateEffectSoundVolume(float newVolume)
    {
        effectSoundVolume = newVolume;
        EffectAudio.volume = effectSoundVolume;

    }

    public void PlayPayWithCoinsSound()
    {
        EffectAudio.clip = CoinsDropSound;
        EffectAudio.Play();
    }

}
