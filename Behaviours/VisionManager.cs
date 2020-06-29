using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vision Manager")]
public class VisionManager : ScriptableObject
{
    public List<Card> PlayerSave;
    public List<Card> Goal;
    public List<Card> GoalRedAndBlocked;
    public List<Card> RandomCards;
    public Card End;

    public void Execute(Manager manager, Card Try)
    {
        manager.Queue.Clear();
        manager.Consume = false;
        List<Card> ToBeQueued = new List<Card>();

        for (int i = 0; i <= PlayerSave.Count; i++)
        {
            if (i==PlayerSave.Count&&i<Goal.Count)
            {
                if (Try==Goal[i])
                {
                    ToBeQueued.Add(GoalRedAndBlocked[i]);
                    PlayerSave.Add(Try);
                    break;
                }
            }
            else if (PlayerSave[i]==Goal[i])
            {
                ToBeQueued.Add(GoalRedAndBlocked[i]);
            }
        }
        if (PlayerSave.Count==Goal.Count) //Vision Done -> Queue End
        {
            manager.OneChoice = true;
            manager.Queue.Add(End);
        }
        else //Manage Vision
        {
            manager.FourChoice = true;
            for (int i = 0; i < 4-PlayerSave.Count; i++)
            {
                ToBeQueued.Add(RandomCards[Random.Range(0,RandomCards.Count)]);
            }
            foreach (Card C in ToBeQueued)
            {
                manager.Queue.Add(C);
            }
        }


        //    bool VisionDone = false;
        //    for (int i = 0; i < PlayerSave.Count; i++)
        //    {
        //        if (PlayerSave[i]=Goal[i])
        //        {
        //            VisionDone = true;
        //        }
        //        else
        //        {
        //            VisionDone = false;
        //        }
        //    }
        //    if (VisionDone)
        //    {
        //        manager.OneChoice = true;
        //        manager.Queue.Add(End);
        //    }
        //    else
        //    {
        //        manager.FourChoice = true;
        //        for (int i = 0; i <= PlayerSave.Count; i++)
        //        {
        //            if (i == PlayerSave.Count)
        //            {
        //                if (Try == Goal[i])
        //                {
        //                    PlayerSave.Add(Try);
        //                    manager.Queue.Add(GoalRedAndBlocked[i]);
        //                }
        //            }
        //            else if (PlayerSave[i] == Goal[i])
        //            {
        //                manager.Queue.Add(GoalRedAndBlocked[i]);
        //            }
        //        }
        //        for (int i = 0; i < 4-PlayerSave.Count; i++)
        //        {
        //            manager.Queue.Add(RandomCards[Random.Range(0, RandomCards.Count)]);
        //        }
        //    }
        //}

    }
}
