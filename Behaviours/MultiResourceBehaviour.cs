using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class MultiResourceBehaviour : CardBehaviours
{
    public List<CardResources> Resources;
    public override void Execute()
    {
        foreach (var item in Resources)
        {
            item.Give();
        }
    }
}
