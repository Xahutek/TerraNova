using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public PlayerData player;
    public DataBase data;
    public SaveLoad save;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Splash());
    }

    IEnumerator Splash()
    {
        save.Load(player,data);
        yield return new WaitForSeconds(3.5f);
        if (!player.Intro)
        {
            player.Intro = true;
            Debug.Log("Load Intro");
            SceneManager.LoadScene("Intro");
        }
        else
        {
            Debug.Log("Load MainMenu");
            SceneManager.LoadScene("MainMenu");
        }

    }
    
}
