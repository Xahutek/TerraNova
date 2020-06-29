using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class MeanToAluxBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public int value;
    public override void Execute()
    {
        playerData.MeanToAluxCounter += value;
    }
}

