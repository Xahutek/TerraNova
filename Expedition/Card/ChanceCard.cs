using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class ChanceCard : Card
{
    public int ValueProbability;

    public bool Else = false;

    public override void Execute()
    {
        int rand = Random.Range(0,101);
        if (rand <= ValueProbability)
        {
            foreach (var behav in SpecialBehaviours)
            {
                behav.Execute();
            }
            foreach (var res in SpecialResources)
            {
                res.Give();
            }
        }
        else if (Else)
        {
            foreach (var behav in Behaviours)
            {
                behav.Execute();
            }
            foreach (var res in Resources)
            {
                res.Give();
            }
        }
        if (!Else)
        {
            foreach (var behav in Behaviours)
            {
                behav.Execute();
            }
            foreach (var res in Resources)
            {
                res.Give();
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
