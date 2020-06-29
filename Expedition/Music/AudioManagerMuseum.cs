using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AudioManagerMuseum : MonoBehaviour
{
    public Sound sound;
    public Slider slider;
    public float prefferedVolume=0.5f;
    public bool forcesound = false;
    public int loopTimes = 0;
    // Start is called before the first frame update
    void Awake()
    {
       
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        
        if (forcesound)
        {
            slider.value = prefferedVolume;
        }
        else
        {
        slider.value = PlayerPrefs.GetFloat("Volume");
        }
        UpdateSlider();

        Play();
        if (loopTimes>=1)
        {
            StartCoroutine(StopLoop());
        }
    }

    
    public void UpdateSlider()
    {
        changeVolume (slider.value);
    }
    public void changeVolume(float num)
    {
        if (num >= 0 && num <= 1)
        {
            prefferedVolume = num;
            PlayerPrefs.SetFloat("Volume",num);
            
                sound.volume = num;
                sound.source.volume = num;
            
        }
       
    }
    IEnumerator StopLoop()
    {
        yield return new WaitForSeconds(sound.clip.length*(float)loopTimes-0.1f);
        Debug.Log("StopLoop");
        sound.source.Stop();
    }


    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Pause();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        Debug.Log("FadeIn");
        audioSource.Play();
        float startVolume = 0.05f;
        audioSource.volume = startVolume;

        while (audioSource.volume < prefferedVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = prefferedVolume;
    }


    public void Play()
    {
        StartCoroutine(FadeIn(sound.source,1.5f));

    }
    public void Pause()
    {
        StartCoroutine(FadeOut(sound.source, 1.5f));
    }
}
