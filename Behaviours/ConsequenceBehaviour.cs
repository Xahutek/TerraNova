using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class ConsequenceBehaviour : CardBehaviours
{
    public List<Card> Consequence;
    public List<Card> ConsequenceElse;
    public int Chance = 100;
    public bool Else=false;

    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        int Rand = Random.Range(0,100);
        if (Rand<=Chance)
        {
            foreach (Card card in Consequence)
            {
                Manager.Log.Add(card);
            }

            if (!Else && ConsequenceElse.Count>0)
            {
                foreach (Card card in ConsequenceElse)
                {
                    Manager.Log.Add(card);
                }
            }
        }
        else if (Else&&ConsequenceElse.Count>0)
        {
            foreach (Card card in ConsequenceElse)
            {
                Manager.Log.Add(card);
            }
        }

       
    }
}
