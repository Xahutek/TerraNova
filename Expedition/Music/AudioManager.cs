using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Slider slider;
    public float prefferedVolume=0.5f;
    public bool forcesound = false;
    public bool playOcean = true;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.mute = s.mute;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
        if (forcesound)
        {
            slider.value = 0.5f;
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("Volume");
        }
        UpdateSlider();
        if (playOcean)
        {
        Play("Ocean");
        }
    }

    public void RefreshBackground(int Thrill)
    {
        

        if (Thrill <= 1)
        {
            Play("LightJungle");
        }
        else if (Thrill <= 4)
        {
            Play("RiverDelta");
        }
        else if (Thrill <= 7)
        {
            Play("DeepJungle");
        }
        else if (Thrill <= 9)
        {
            Play("MountainPlateaus");
        }
        else if (Thrill <= 10)
        {
            Play("AncientCity");
        }
    

    }
    public void PlayAttack(bool on)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Attack");
        if (!on && s.source.isPlaying)
        {
            StartCoroutine(FadeOut(s.source, 0.75f));
        }
        if (on&&!s.source.isPlaying)
        {
            StartCoroutine(FadeIn(s.source, 0.75f));
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
            foreach (Sound clip in sounds)
            {
                clip.volume = num;
                clip.source.volume = num;
            }
        }
       
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

        audioSource.Play();
        float startVolume = 0.1f;
        audioSource.volume = startVolume;

        while (audioSource.volume < prefferedVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = prefferedVolume;
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            return;
        }
        s.source.mute = true;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            return;
        }
         else if (name == "Panic")
        {
            s.source.Play();
        }
        else
        {

            if (s.Theme)
            {
                foreach (Sound x in sounds)
                {
                    if (x.Theme && x.source.isPlaying)
                    {
                        StartCoroutine(FadeOut(x.source, 1.5f));
                    }
                }
            }
            s.source.mute = false;
            StartCoroutine(FadeIn(s.source, 2));
            Debug.Log("Play "+name);
        }

    }
    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
                StartCoroutine(FadeOut(s.source, 1.5f));
            }
        }
    }
}
[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume=(float)0.5;
    [Range(.1f,3f)]
    public float pitch=1;
    public bool mute = false;
    public bool loop;
    public bool Theme;

    [HideInInspector]
    public AudioSource source;
}