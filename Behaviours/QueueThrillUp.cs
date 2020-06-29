using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class QueueThrillUp : CardBehaviours
{

    public override void Execute()
    {
        Manager manager= GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        int Thrill = manager.playerData.Thrill;
        foreach (Card card in manager.dataBase.ThrillUp)
        {
            if (card.Rarity[Thrill-1])
            {
                manager.Queue.Add(card);
                break;
            }
        }

    }

}
