using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class LeaveGroupBehaviour : CardBehaviours
{
    public PlayerData playerData;


    public override void Execute()
    {
        for (int i = 0; i < playerData.Manpower.Length; i++)
        {
            playerData.Manpower[i] = 0;
        }
        playerData.Manpower[0] = 1;
        playerData.Gold = 0;
        playerData.Trophy = 0;

        playerData.UpdateManpowerBoni();

        playerData.EquippedItems.Clear();
    }
}
