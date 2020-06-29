using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject Expedition;
    public void OpenOptionsMenu()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
        if (Expedition!=null)
        {
        Expedition.SetActive(true);

        }
    }
    public void QuitToTitle()
    {
        playerData.Reset();
        //SaveLoad.Save();
        
        SceneManager.LoadScene("MainMenu");
    }
}
