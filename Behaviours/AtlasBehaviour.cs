using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class AtlasBehaviour : CardBehaviours
{
    public PlayerData p;
    public Card AtlasTurtle;
    public Contract AtlasContract;
    public int probmultiplier=10;
    public override void Execute()
    {
        if (p.ActiveContracts[0]== AtlasContract)
        {
            Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();

                Manager.Queue.Clear();
                Manager.Queue.Add(AtlasTurtle);
            
        }
    }
}
