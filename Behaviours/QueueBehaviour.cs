using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class QueueBehaviour : CardBehaviours
{
    public List<Card> Queue;
    public bool HardQueue;
    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        if (HardQueue)
        {
            Manager.Queue.Clear();
        }
        foreach (var card in Queue)
        {
            Manager.Queue.Add(card);
        }
    }
}
