using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class FolktaleBehaviour : CardBehaviours
{
    public PlayerData playerData;
    
    public override void Execute()
    {
        playerData.AddLore(playerData.Hint/5);
        playerData.Hint = 0;
    }
}
