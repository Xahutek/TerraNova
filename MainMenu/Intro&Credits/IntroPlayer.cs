using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IntroPlayer : MonoBehaviour
{

    public Animator Anim;
    public AudioManager Audio;
    public LevelManager LvL;
    public GameObject ClickToContinue;
    public Text StoryText;
    int Slide = 1;
    bool Blocked = true;

    private void Start()
    {
        Audio.Play("EpicDark");
        Audio.Play("EpicBright");
        Audio.Mute("EpicBright");
        Block();
    }
    private void Update()
    {
        if (!Blocked)
        {
            if (Input.anyKey)
            {
                Slide++;
                Debug.Log("ChangeSlide: "+Slide);
                Block();
            }
        }
        
    }
    void Block()
    {

        if (Slide==1)
        {
            StartCoroutine(Wait(1.5f));
            StoryText.text = "For centuries the Kingdoms lived in coexistence, their progress shaped by science, peace and war. Until the plague came... ";
        }
        else if (Slide==2)
        {
            StartCoroutine(Wait(5f));
            StoryText.text = "Civilizations on the Mainland broke down under its terror. Only the isle-Queendom of Felicita survived behind a blockade lasting 200 years. They called it the great Quarantine.";

        }
        else if (Slide==3)
        {
            Audio.Play("EpicBright");
            StartCoroutine(Wait(3.5f));
            StoryText.text = "When finally the blockade was dropped, it ushered in a new era of exploration! Bringing civilization back to the world, ships traveled even beyond the edge of the old maps.";

        }
        else if (Slide==4)
        {
            StartCoroutine(Wait(8f));
            StoryText.text = "Beyond the horizon they discovered another continent, isolated for aeons and untouched by the plague.										And they called this world...";
        }
        else if (Slide == 5)
        {
            Audio.PauseAll();
            StartCoroutine(Wait(1.2f));
        }

    }
    IEnumerator Wait(float num)
    {
        Blocked = true;
        if (Slide>1)
        {
        Anim.SetTrigger("GO");
        }
        ClickToContinue.SetActive(false);
        yield return new WaitForSeconds(num);
        if (Slide==5)
        {
            LvL.LoadMainMenu();
        }
        Blocked = false;
        ClickToContinue.SetActive(true);

    }

}

