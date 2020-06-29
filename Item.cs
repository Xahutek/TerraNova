using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item")]
[Serializable]
public class Item : ScriptableObject
{
    public new string name;
    public int ID;
    public bool Active=false;
    public int FundsCost=0;
    public int PrestigeReward = 0;
    public int MaxShots = 0;
    public int Shots = 0;
    public Sprite Art;
    public string itemDescription;
    public List<CardBehaviours> ActiveB;
    public List<CardResources> ActiveR;
    public string DescriptionActive;
    public List<CardBehaviours> PassiveB;
    public List<CardResources> PassiveR;
    public string DescriptionPassive;
    public List<CardBehaviours> SinglePassiveB;
    public List<CardResources> SinglePassiveR;

    public void Execute()
    {
        foreach (var item in ActiveB)
        {
            item.Execute();
        }
        foreach (var item in ActiveR)
        {
            item.Give();
        }
        if (MaxShots > 0)
        {
            Shots++;
        }
   
    }

    public void Passive()
    {
        foreach (var item in PassiveB)
        {
            item.Execute();
        }
        foreach (var item in PassiveR)
        {
            item.Give();
        }
    }

    public void Single()
    {
        foreach (var item in SinglePassiveB)
        {
            item.Execute();
        }
        foreach (var item in SinglePassiveR)
        {
            item.Give();
        }
    }

    public int Reward()
    {
        return PrestigeReward;
    }
}
