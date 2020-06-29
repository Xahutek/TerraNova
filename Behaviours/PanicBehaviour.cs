using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class PanicBehaviour : CardBehaviours
{
   
    public override void Execute()
    {
        GameObject.Find("ExpeditionManager").GetComponent<Manager>().Panic = true;
        
    }
}
