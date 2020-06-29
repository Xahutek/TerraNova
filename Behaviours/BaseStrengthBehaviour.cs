using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class BaseStrengthBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public int Amount = 1;
    public bool SetHard = false;

    public override void Execute()
    {
        if (SetHard)
        {
            playerData.BaseStrengthBonus = Amount;

        }
        else
        {
            playerData.BaseStrengthBonus += Amount;

        }
    }
}
