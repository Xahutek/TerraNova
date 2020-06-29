using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Behaviour")]
public class QueueRandomBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public int Amount = 1;
    public List<Card> Queue;
    public bool CheckForThrill = true;
    public bool RerollDuplicates = false;
    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        int save = -1;
        for (int i = 0; i < Amount; i++)
        {
            int rand = Random.Range(0,Queue.Count);
            if (RerollDuplicates)
            {
                if (rand == save)//1 time reroll to diversify
                {
                    rand = Random.Range(0, Queue.Count);
                }
                save = rand;
            }
            
            if (Queue[rand].Active && Queue[rand].ActiveThisExpedition)
            {
                if (!CheckForThrill || CheckForThrill && Queue[rand].Rarity[playerData.Thrill - 1])
                {
                    Manager.Queue.Add(Queue[rand]);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                i--;
            }
        }
    }
}
