using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase")]
public class DataBase : ScriptableObject
{
    public bool TestReset = false;

    //Contracts
    public List<Contract> Contracts = new List<Contract>();
    public bool[] PlotPointsDone = new bool[10] { false, false, false, false, false, false, false, false, false, false, };

    //Items
    public List<Item> Items = new List<Item>();

    //Titles - Prestige Check
    public List<string> Titles = new List<string>();

    public int ManpowerMuscleCost = 1;
    public int ManpowerScolarCost = 3;
    public int ManpowerTrackerCost = 2;
    public int ManpowerCookCost = 3;

    public PlayerData playerData;

    public int HintPercentageThreshholds = 25;
    public int HintRisePerThreshhold = 5;
    public List<Card> CardList;
    //-------------------------{-S---D---M---P---L--}
    public int[] Area1Rarity = new int[5] { 80, 7, 7, 3, 3 };
    public int[] Area2Rarity = new int[5] { 40, 15, 30, 10, 5 };
    public int[] Area3Rarity = new int[5] { 20, 40, 10, 15, 10 };
    public int[] Area4Rarity = new int[5] { 5, 20, 40, 20, 20 };
    public int[] Area5Rarity = new int[5] { 5, 30, 5, 50, 20 };

    public List<Card> Start;
    public List<Card> IntroShots;
    public List<Card> DeathNote;
    public List<Card> Objectives;
    public List<Card> ThrillUp;
    public List<Card> Waystones;
    public int WaystoneProbability = 100;
    public Card FailState;
    public Card BlockedWayAround;

    public int[] WildProbability = { 0, 0, 0, 0, 0, 0 };
    public void RefreshWild()
    {
        //Area1Rarity = new int[5] { 60, 10, 10, 10, 10 };
        //Area2Rarity = new int[5] { 40, 15, 30, 10, 5 };
        //Area3Rarity = new int[5] { 20, 40, 10, 15, 10 };
        //Area4Rarity = new int[5] { 5, 20, 40, 20, 20 };
        //Area5Rarity = new int[5] { 5, 30, 5, 50, 20 };
        //if (TestReset)
        //{
        //    for (int i = 0; i < 10; i++)//TestReset Objective Progress
        //    {
        //        ObjectivesFound[i] = false;
        //    }
        //    foreach (var item in Contracts)
        //    {
        //        item.IllegalReset();
        //    }
        //}
        int ID = 0;
        foreach (var item in Items)
        {
            item.ID = ID++;
        }
        ID = 0;
        foreach (var con in Contracts)
        {
            con.ID = ID++;
        }
        ID = 0;
        foreach (var card in CardList)
        {
            card.ID = ID++;
        }
        for (int i = 0; i < 6; i++)
        {
            WildProbability[i] = Random.Range(0, 100);
        }
    }
    public int HintConverter()
    {
        int Hint=playerData.Hint;
        return (int)(Mathf.FloorToInt(Hint / HintPercentageThreshholds) * HintRisePerThreshhold);
    }
    //Card Methods
    public Card DrawCard(int Thrill, int StepsOnThrill)
    {
        int rand = Random.Range(0, 100);
        if (playerData.Hint >= 75 && Random.Range(1, 10) >= 5 || StepsOnThrill >= 10 && rand <= HintConverter())
        {
            Card b = null;
            foreach (var card in Objectives)
            {
                if (card.Rarity[Thrill - 1] && card.Active && card.ActiveThisExpedition)
                {
                    return card;
                }
            }
            if (b == null)
            {
                return DrawNormalCard(Thrill);
            }
            return b;
        }
        else if (playerData.Hint >= 90 || StepsOnThrill >= 5 && rand - HintConverter() <= playerData.ThrillProbability - 20)
        {
            Card a = null;
            foreach (var card in ThrillUp)
            {
                if (card.Rarity[Thrill-1] && card.Active && card.ActiveThisExpedition)
                {
                    a = card;
                }
            }
            if (a == null)
            {
                return DrawNormalCard(Thrill);
            }
            return a;
        }
        else
        {
        return DrawNormalCard(Thrill);

        }
    }

    public Card DrawNormalCard(int Thrill)
    {
        List<Card> Temporary = CollectWithCriteria(Thrill);
        if (Temporary.Count>0)
        {
            return Temporary[Random.Range(0, Temporary.Count)];
        }
        return FailState;
    }

    public List<Card> CollectWithCriteria(int Thrill)
    {
        int ThisThrill=1;
        if (Thrill != 0)
        {
        ThisThrill = Thrill;
        }
      
        int rand = Random.Range(0, 100);

        List<Card> Temporary = new List<Card>();
        foreach (var card in CardList)
        {
            if (card.Rarity[ThisThrill - 1] && card.Active && card.ActiveThisExpedition)
            {
                if (ThisThrill < 2)//Area1
                {
                    if (rand <= Area1Rarity[0])
                    {

                        if (card.Supplies)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area1Rarity[0];

                    if (rand <= Area1Rarity[1])
                    {

                        if (card.Danger)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area1Rarity[1];

                    if (rand <= Area1Rarity[2])
                    {

                        if (card.Move)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area1Rarity[2];

                    if (rand <= Area1Rarity[3])
                    {

                        if (card.Prestige)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area1Rarity[3];

                    if (rand <= Area1Rarity[4])
                    {

                        if (card.Lore)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area1Rarity[4];

                }

                else if (ThisThrill < 5) //Area2
                {

                    if (rand <= Area2Rarity[0])
                    {
                        if (card.Supplies)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area2Rarity[0];
                    if (rand <= Area2Rarity[1])
                    {
                        if (card.Danger)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area2Rarity[1];
                    if (rand <= Area2Rarity[2])
                    {
                        if (card.Move)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area2Rarity[2];
                    if (rand <= Area2Rarity[3])
                    {
                        if (card.Prestige)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area2Rarity[3];
                    if (rand <= Area2Rarity[4])
                    {
                        if (card.Lore)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area2Rarity[4];

                }
                else if (ThisThrill < 8)//Area3
                {

                    if (rand <= Area3Rarity[0])
                    {
                        if (card.Supplies)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area3Rarity[0];
                    if (rand <= Area3Rarity[1])
                    {
                        if (card.Danger)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area3Rarity[1];
                    if (rand <= Area3Rarity[2])
                    {
                        if (card.Move)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area3Rarity[2];
                    if (rand <= Area3Rarity[3])
                    {
                        if (card.Prestige)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area3Rarity[3];
                    if (rand <= Area3Rarity[4])
                    {
                        if (card.Lore)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area3Rarity[4];

                }
                else if (ThisThrill < 10)//Area4
                {

                    if (rand <= Area4Rarity[0])
                    {
                        if (card.Supplies)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area4Rarity[0];
                    if (rand <= Area4Rarity[1])
                    {
                        if (card.Danger)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area4Rarity[1];
                    if (rand <= Area4Rarity[2])
                    {
                        if (card.Move)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area4Rarity[2];
                    if (rand <= Area4Rarity[3])
                    {
                        if (card.Prestige)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area4Rarity[3];
                    if (rand <= Area4Rarity[4])
                    {
                        if (card.Lore)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area4Rarity[4];

                }
                else if (ThisThrill == 10)//Area5
                {

                    if (rand <= Area5Rarity[0])
                    {
                        if (card.Supplies)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area5Rarity[0];

                    if (rand <= Area5Rarity[1])
                    {
                        if (card.Danger)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area5Rarity[1];
                    if (rand <= Area5Rarity[2])
                    {
                        if (card.Move)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area5Rarity[2];

                    if (rand <= Area5Rarity[3])
                    {
                        if (card.Prestige)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area5Rarity[3];

                    if (rand <= Area5Rarity[4])
                    {
                        if (card.Lore)
                        {
                            Temporary.Add(card);
                        }

                    }
                    rand -= Area5Rarity[4];

                }
            }
        }
        return Temporary;
    }
    public Card DrawDeathCard()
    {
        return DeathNote[Random.Range(0, DeathNote.Count)];
    }

    //Bonus Methods
    public void ResetExpedition()
    {
        playerData.LoreSave = playerData.Lore;
        playerData.MealCounter = 0 ;
        playerData.StrengthBoost = 0;
        playerData.UpdateManpowerBoni();
        playerData.Thrill = 0;
        //Pilgrims Path        
        playerData.PilgrimsPath = 0;
        playerData.Hint = 0;
        playerData.BaseStrengthBonus = 1;

        foreach (Card card in CardList)
        {
            card.ActiveThisExpedition = true;
        }
        foreach (Item item in playerData.EquippedItems)
        {
            item.Single();
        }
        foreach (Item item in Items)
        {
            item.Shots = 0;
        }
        ManageContracts();
        RefreshWild();
    }
    public void ManageContracts()
    {

        foreach (Contract con in Contracts)
        {
            if (con.PlotPoint == 2 || con.PlotPoint == 3)
            {
                con.goal.Done = true;
                con.goal.checksTotal = con.goal.checksNeeded;
                con.goal.Objective.ActiveThisExpedition = false;
                ThrillUp[con.PlotPoint - 1].ActiveThisExpedition = true;
            }
            else
            {
                if (con.PlotPoint > 0)
                {
                    con.goal.Objective.ActiveThisExpedition = false;

                    if (con.goal.Done)
                    {
                        ThrillUp[con.PlotPoint - 1].ActiveThisExpedition = true;

                        foreach (var activecon in playerData.ActiveContracts)
                        {
                            if (con == activecon)
                            {
                                con.goal.Objective.ActiveThisExpedition = false;
                            }
                        }
                    }
                    else
                    {
                        ThrillUp[con.PlotPoint - 1].ActiveThisExpedition = false;

                        foreach (var activecon in playerData.ActiveContracts)
                        {
                            if (con == activecon)
                            {
                                con.goal.Objective.ActiveThisExpedition = true;
                            }
                        }
                    }
                }
                if (con.PlotPoint == 7)
                {
                    con.goal.Objective.ActiveThisExpedition = true;
                }
            }
           
        }
    }
    public string GiveTitle(int prestige)
    {
        return "Lord";
        //if (prestige<100)
        //{
        //    return Titles[1];
        //}
        //return Titles[0];
    }
    public void SetContract()
    {
        foreach (var contract in playerData.ActiveContracts)
        {

            if (contract.Funds>0)
            {
                if (contract.Debt)
                {
                    playerData.Funds = Mathf.CeilToInt(contract.Funds * 0.66f);
                }
                else
                {
                    playerData.Funds = contract.Funds;
                }
                
            }
            if (contract.Item1 != null)
            {
                contract.Item1.Active = true;
            }
            if (contract.Item2 != null)
            {
                contract.Item2.Active = true;
            }
            break;
        }
    }

   
}
