using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class ReturnToMuseumBehaviour : CardBehaviours
{


    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();

        Manager.LevelManager.LoadMuseum();
    }
}
