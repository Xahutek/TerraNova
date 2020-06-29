using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class NobuIsWatchingYou : CardBehaviours
{
    
    public bool MonkOrVisionCheck;
    public bool check = true;
    public PlayerData playerData;
   
    public override void Execute()
    {
        if (MonkOrVisionCheck)
        {
            playerData.IsMonk = check;
        }
        else
        {
            playerData.HadVision = check;
        }
    }
        

        
    }

