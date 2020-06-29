using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class ConsumeBehaviour : CardBehaviours
{
    public bool Consume = false;
    public override void Execute()
    {
        GameObject.Find("ExpeditionManager").GetComponent<Manager>().Consume = Consume;
        
    }
}
