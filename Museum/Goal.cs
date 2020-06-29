using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Card")]
[Serializable]
public class Goal : ScriptableObject
{
    public bool Done=false;
    public Type GoalType;

    public Card Objective;
    public Card TrueObjective;
    public Item ItemObjective;

    public int checksNeeded;
    public int checksTotal;

    public bool IsReached()
    {
        if (GoalType==Type.Infinite)
        {
            Done = false;
            return false;
        }
        else if (checksTotal>=checksNeeded)
        {
            Done = true;

            return true;
        }

        return false;
    }

    public bool IsReached(Card card)
    {

      
        if (TrueObjective != null && card == TrueObjective)
        {
            checksTotal = checksNeeded;
            Done = true;
            return true;

        }
        if (TrueObjective == null && Objective != null && card == Objective)
        {
            checksTotal = checksNeeded;
            Done = true;
            

            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool IsReached(Item item)
    {
        if (Objective != null && item == ItemObjective)
        {
            checksTotal = checksNeeded;
            Done = true;

            return true;
        }
        else
        {
            return false;
        }

    }
}

public enum Type
{
    Infinite,
    FindCard,
    ReachThrill,
    collectResource,
}