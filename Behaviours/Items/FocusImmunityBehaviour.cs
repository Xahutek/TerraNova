using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boost")]
public class FocusImmunityBehaviour : CardBehaviours
{

    public override void Execute()
    {
        Manager manager =GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        manager.FocusImmunity = true;

    }
}
