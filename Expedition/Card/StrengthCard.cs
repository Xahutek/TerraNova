using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class StrengthCard : Card
{
    public PlayerData playerData;
    public int ValueStrength;

    public bool Else = false;

    public override void Execute()
    {
        playerData.UpdateManpowerBoni();
        if (playerData.CheckStrength(Mathf.CeilToInt( ValueStrength*2 )))
        {
foreach (var res in SpecialResources)
            {
                if (res.ManpowerMuscle && res.Value < 0 || res.ManpowerScolar && res.Value < 0 || res.ManpowerTrapper && res.Value < 0 || res.ManpowerCook&&res.Value<0)
                {
                    //do nothing
                }
                else
                {
                    res.Give();
                }
            }
            foreach (var behav in SpecialBehaviours)
            {
                behav.Execute();
            }
            
            
            
            if (!Else)
            {  foreach (var res in Resources)
                {
                    if (res.ManpowerMuscle && res.Value<0|| res.ManpowerScolar && res.Value < 0|| res.ManpowerTrapper && res.Value < 0|| res.Supplies && res.Value < 0)
                    {
                        //do nothing
                    }
                    else
                    {
                     res.Give();
                    }
                 
                }
                foreach (var behav in Behaviours)
                {
                    
                    behav.Execute();
                }
              
            }
        }
        else
        {
            if (playerData.CheckStrength(ValueStrength))
            { foreach (var res in SpecialResources)
                {
                    res.Give();
                }
                foreach (var behav in SpecialBehaviours)
                {
                    behav.Execute();
                }
               
            }
            else if (Else)
            { foreach (var res in Resources)
                {
                    res.Give();
                }
                foreach (var behav in Behaviours)
                {
                    behav.Execute();
                }
               
            }
            if (!Else)
            { foreach (var res in Resources)
                {
                    res.Give();
                }
                foreach (var behav in Behaviours)
                {
                    behav.Execute();
                }
               
            }
        }

        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
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
