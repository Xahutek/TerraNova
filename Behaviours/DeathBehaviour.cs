using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death")]
public class DeathBehaviour : CardBehaviours
{
    public override void Execute()
    {
        GameObject.Find("ExpeditionManager").GetComponent<Manager>().Dead=true;
        
    }
}
