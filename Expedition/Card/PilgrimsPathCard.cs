using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class PilgrimsPathCard : Card
{
    public PlayerData playerData;
    public DataBase dataBase;
    public int WaystoneNumber;

    public Contract NobuGrave;

    public bool Else = false;

    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
     foreach (var res in Resources)
        {
            res.Give();

        }
        foreach (var behav in Behaviours)
        {
            behav.Execute();
        }


        if (WaystoneNumber == playerData.PilgrimsPath + 1 && playerData.Lore >= 10)
        {
            playerData.PilgrimsPath += 1;
            playerData.AddHint(10 * WaystoneNumber);
            if (WaystoneNumber == 8)
            {
                foreach (var Waystone in dataBase.Waystones)
                {
                    Waystone.Active = false;
                }
            }
            if (!NobuGrave.goal.Done && WaystoneNumber == NobuGrave.PlotPoint - 1)
            {
                Manager.Queue.Add(NobuGrave.goal.Objective);
            }
            else
            {
                dataBase.Waystones[playerData.PilgrimsPath].ActiveThisExpedition = true;
            }
            foreach (var res in SpecialResources)
            {
                res.Give();
            }
            foreach (var behav in SpecialBehaviours)
            {
                behav.Execute();
            }

        }
        else
        {
            playerData.PilgrimsPath = 0;
        }


        if (!Consume)
        {
            Manager.Consume = false;
        }
        if (Panic)
        {
            Manager.Panic = true;
        }
        if (FourChoice)
        {
            Manager.FourChoice = true;
        }
        if (TwoChoice)
        {
            Manager.TwoChoice = true;
        }
        if (OneChoice)
        {
            Manager.OneChoice = true;
        }
        foreach (var card in EventQueue)
        {
            Manager.Queue.Insert(0, card);
        }
    }
  
}
