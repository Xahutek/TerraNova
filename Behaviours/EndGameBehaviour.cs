using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "New Behaviour")]
public class EndGameBehaviour : CardBehaviours
{
    public LevelManager Manager;
    public override void Execute()
    {
        SceneManager.LoadScene("Credits");
    }
}
