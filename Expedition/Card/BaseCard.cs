using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class BaseCard : Card
{
    public bool PlaySpecialToo = false;

    public override void Execute()
    {  foreach (var res in Resources)
        {
            res.Give();

        }
        foreach (var behav in Behaviours)
        {
            behav.Execute();
        }
      

        if (PlaySpecialToo)
        { foreach (var res in SpecialResources)
            {
                res.Give();
            }
            foreach (var behav in SpecialBehaviours)
            {
                behav.Execute();
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
            Manager.Queue.Insert(0,card);
        }
    }

}
