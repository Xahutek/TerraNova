using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollCredits : MonoBehaviour
{
    public Reset reset;
    
    void Start()
    {
        StartCoroutine(ROllTHEMCREDS());
    }
    IEnumerator ROllTHEMCREDS()
    {
        yield return new WaitForSeconds(55);
        reset.ResetGame();
        SceneManager.LoadScene("MainMenu");
    }
}
