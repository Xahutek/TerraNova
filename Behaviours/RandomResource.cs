using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Resource")]
public class RandomResource : CardResources
{
    public List<CardResources> ResourcesList;
    public override int Give()
    {
        if (ManpowerMuscle&&ManpowerCook&&ManpowerScolar&&ManpowerTrapper)
        {
            playerData.AddManpower(Value,-1);
        }
        else
        {
            ResourcesList[Random.Range(0, ResourcesList.Count)].Give();
        }
        return 0;
    }
}
