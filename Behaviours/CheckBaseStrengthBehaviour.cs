using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class CheckBaseStrengthBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public int Value = 1;
    public List<CardBehaviours> Behaviours;
    public List<CardBehaviours> ElseBehaviours;

    public override void Execute()
    {

        if (playerData.BaseStrengthBonus>=Value)
        {
            foreach (CardBehaviours behav in Behaviours)
            {
                behav.Execute();
                playerData.UpdateManpowerBoni();
            }
        }
        else
        {
            foreach (CardBehaviours behav in ElseBehaviours)
            {
                behav.Execute();
            }
        }
    }
}
