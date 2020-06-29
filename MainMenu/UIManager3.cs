using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager3 : MonoBehaviour
{
    public PlayerData playerData;
    public DataBase dataBase;

    public GameObject MainMenu;
    public GameObject OptionsMenu;

    public Animator Anim;

    public SaveLoad saveLoad;

    private void Awake()
    {
        playerData.Intro = true;
        reset = false;

        Debug.Log(Application.persistentDataPath);
      
        saveLoad.Save(playerData, dataBase);
    }



    public void OpenOptionsMenu()
    {
        StartCoroutine(OptionsOpen());
    }
    IEnumerator OptionsOpen()
    {
        Anim.SetTrigger("SWAP");
        yield return new WaitForSeconds(1);
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void OpenMainMenu()
    {
        StartCoroutine(MainOpen());
    }
    IEnumerator MainOpen()
    {
        Anim.SetTrigger("SWAP");
        yield return new WaitForSeconds(1);
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    bool reset = false;
    public void Reset()
    {
        reset = true;
        Application.Quit();

    }
    public void QuitGame()
    {
        if (!reset)
        {
            playerData.Intro = true;

            Debug.Log("Saved");
            saveLoad.Save(playerData, dataBase);
            StartCoroutine(Quit());
            

        }
        else
        {
            Application.Quit();

        }

    }
    IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
