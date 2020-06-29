using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class AlterChoiceBehaviour : CardBehaviours
{
    public int ChoiceCount=3;
    public override void Execute()
    {
        if (ChoiceCount == 4)
        {
            GameObject.Find("ExpeditionManager").GetComponent<Manager>().FourChoice = true;
        }
        else if (ChoiceCount == 2)
        {
            GameObject.Find("ExpeditionManager").GetComponent<Manager>().TwoChoice = true;
        }
        else if (ChoiceCount == 1)
        {
            GameObject.Find("ExpeditionManager").GetComponent<Manager>().OneChoice = true;
        }
        else
        {
            Manager M = GameObject.Find("ExpeditionManager").GetComponent<Manager>();

            M.FourChoice = false;
            M.OneChoice = false;
            M.TwoChoice = false;
        }
    }
}
