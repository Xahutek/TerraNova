using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Contract")]
[Serializable]
public class Contract : ScriptableObject
{
    public Goal goal;
    public int PlotPoint = 0;

    public int Reward;
    public int ID;
    public int ActivatePrestige;

    public string Name;
    public Contractor contractor;
    public string Description;
    public string MessageWhenDone;

    public int Funds;
    public bool Debt;
    public Item Item1;
    public Item Item2;

    public void GiveReward(PlayerData player)
    {
        player.Kudos = Reward;
        player.LastFinished = this;
    }


}
