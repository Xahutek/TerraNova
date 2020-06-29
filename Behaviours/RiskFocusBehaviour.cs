using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class RiskFocusBehaviour : CardBehaviours
{
    public bool Focus;
    public bool One;

    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        Manager.OneChoice = false;
        Manager.TwoChoice = false;
        Manager.FourChoice = false;
        if (One)
        {
            Manager.OneChoice = true;
        }
        else
        {
            if (Focus)
            {
                Manager.FourChoice = true;
            }
            else
            {
                Manager.TwoChoice = true;
            }
        }
       
       
    }
}
