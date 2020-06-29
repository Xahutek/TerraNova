using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boat Behaviour")]
public class BoatBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public List<Card> RiverCards;
    public int Power=0;
    public List<Card> Accidents;

    

    public override void Execute()
    {
        Manager Manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        int rand = Random.Range(0, 100) - (playerData.Thrill+Power)*4;

        if (Power > 0 && rand <= 8)
        {
            Manager.TwoChoice = true;
            rand = Random.Range(0, 100) + (playerData.Thrill - 2) * 10;

            if (rand <= 25)
            {
                Manager.Queue.Add(Accidents[1]);
            }
            else if (rand <= 60)
            {
                Manager.Queue.Add(Accidents[2]);
            }
            else if (rand <= 80)
            {
                Manager.Queue.Add(Accidents[3]);
            }
            else
            {
                Manager.Queue.Add(Accidents[4]);
            }
            Manager.Queue.Add(Accidents[0]);

        }
        else
        {
            rand = Random.Range(0, RiverCards.Count);
            Manager.Queue.Add(RiverCards[rand]);
        }






        //int rand2;
        //if (Power != 0 && rand1 <= 15 * Power)
        //{

        //    if (playerData.Thrill == 2 && Queue1 != null)
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            rand2 = Random.Range(0, Queue1.Count);
        //            Manager.Queue.Add(Queue1[rand2]);
        //        }
        //    }
        //    else if (playerData.Thrill == 3 && Queue2 != null)
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            rand2 = Random.Range(0, Queue2.Count);
        //            Manager.Queue.Add(Queue2[rand2]);
        //        }
        //    }
        //    else if (playerData.Thrill == 4 && Queue3 != null)
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            rand2 = Random.Range(0, Queue3.Count);
        //            Manager.Queue.Add(Queue3[rand2]);
        //        }
        //    }
        //}
        //else
        //{
        //    rand2 = Random.Range(0, RiverCards.Count);
        //    Manager.Queue.Add(RiverCards[rand2]);
        //}
    }
}
