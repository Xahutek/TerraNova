using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    public Animator FadeBlack;
    public float TransitionTimeMuseumToExpedition = 2;


    public void LoadMuseum()
    {
        StartCoroutine(LoadMuseumFade());


        //StartCoroutine(LoadAnimated(1));
    }
    public void LoadMuseumClean()
    {
        SceneManager.LoadScene("Museum");

    }
    public void LoadExpedition()
    {
        Debug.Log("Load Expedition");
        StartCoroutine(LoadExpeditionFade());
        //StartCoroutine(LoadAnimated(2));
    }
    public void LoadMainMenu()
    {
        Debug.Log("Load MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public AudioManagerMuseum audioMuseum;

    IEnumerator LoadMuseumFade()
    {
        if (audioMuseum != null)
        {
            audioMuseum.Pause();
        }

        yield return new WaitForSeconds(TransitionTimeMuseumToExpedition);
        SceneManager.LoadScene("Museum");
    }

    IEnumerator LoadExpeditionFade()
    {
        if (audioMuseum!=null)
        {
            audioMuseum.Pause();
        }
        if (FadeBlack != null)
        {
            FadeBlack.SetTrigger("SlideToBlack");
        }
        else
        {
            SceneManager.LoadScene("Expedition");
        }
       
        yield return new WaitForSeconds(TransitionTimeMuseumToExpedition);
        SceneManager.LoadScene("Expedition");
    }

}
