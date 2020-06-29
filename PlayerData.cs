using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
[Serializable]
public class PlayerData : ScriptableObject
{

    //Stuff for Game Progression
    public bool Intro = false;
    public bool ExpeditionTutorial = false;
    public bool MuseumTutorial = false;

    public List<bool> AreasVisited = new List<bool>() { false, false, false, false, false};

    //Nobus Judgement relevant
    //Lore
    public bool IsMonk = false;
    public bool HadVision = false;
    public int MeanToAluxCounter = 0;
    

    // Actual Resoruces
    public string PlayerName;
    public int Thrill;
    public int Prestige;
    public int[] Manpower=new int[4];//Muscle+Scolar+Tracker+Cook
    public int Supplies;
    public int Gold;
    public int Trophy;
    public int Lore;
    public int LoreSave;
    public int BaseStrengthBonus;
    public int StrengthBoost;
    public int Strength;

    public int Kudos;

    public int Funds;
    
    public int ManpowerScolarBonus;
    public int ManpowerTrackerBonus;
    public int ManpowerCookBonus;
    public int MealCounter;
    public int ItemHintCutBoost;

    public int Hint;
    public int PilgrimsPath=0;
    public int ThrillProbability=-24;

    public List<Item> EquippedItems;
    public List<Contract> ActiveContracts;
    public Contract LastFinished;

    public Item empty;
    public void UseItemActive(int num)
    {
        if (num < EquippedItems.Count)
        {
            EquippedItems[num].Execute();
            if (EquippedItems[num].MaxShots!=0&& EquippedItems[num].Shots>= EquippedItems[num].MaxShots)
            {
                EquippedItems[num] = empty;
            }
        }
        
    }

    public int getManpower()
    {
        int x = 0;
        foreach (var item in Manpower)
        {
            x += item;
        }
        return x;
    }

    public void AddThrill(int num)
    {

        if (Thrill + num > 10)
        {
            Thrill = 100;
        }
        else if (Thrill + num < 0)
        {
            Thrill = 0;
        }
        else
        {
            Thrill += num;
        }
        CutHint();
        ThrillProbability = 0;
    }
    public void AddPrestige(int num)
    {
        if ( Prestige + num > 0)
        {
            Prestige += num;
            
        }
        else
        {
            Prestige = 0;
        }
    }
    public void AddManpower(int num, int index)
    {
        if (Manpower==null)
        {
            Manpower = new int[4] { 1, 0, 0 ,0};
        }
        if (index >= 0)
        {
            if (Manpower[index] + num >= 0)
            {
                Manpower[index] += num;
            }
            else
            {
                Manpower[index] = 0;
            }
        }
        else
        {
            int rand = 0;
            if (num < 0)
            {
                for (int i = 0; i > num; i--)
                {
                    if (getManpower() > 0)
                    {
                        rand = UnityEngine.Random.Range(0, Manpower.Length);
                        if (Manpower[rand] > 0)
                        {
                            Manpower[rand]--;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else { break; }
                }
            }
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    rand = UnityEngine.Random.Range(0, Manpower.Length);
                    Manpower[rand]++;
                }
            }

        }
        UpdateManpowerBoni();
    }
    public void UpdateManpowerBoni()
    {
        Strength = Manpower[0] + Manpower[2]+BaseStrengthBonus+StrengthBoost-1;
        ManpowerScolarBonus = Manpower[1] * 10;
        ManpowerTrackerBonus = Manpower[2] * 10;
        ManpowerCookBonus = Manpower[3] * 10;
    }
    public bool CheckStrength(int num)
    {
        if (Strength >= num)
        {
            return true;
        }
        return false;
    }
    public void AddSupplies(int num)
    {
        if (Supplies + num > 0)
        {
            Supplies += num;
        }
        else
        {
            Supplies = 0;
        }
    }
    public void Consume()
    { int mealSuccess = 0;
        if (ManpowerCookBonus > 0)
        {
            //Other Men Feast
            for (int i = 0; i < (getManpower() - Manpower[3]); i++)
            {
                if (ManpowerCookBonus > 0 && MealCounter == 10 / ManpowerCookBonus)
                {
                    mealSuccess++;
                }
                MealCounter++;
                if (ManpowerCookBonus > 0 && MealCounter > 10 / ManpowerCookBonus)
                {
                    MealCounter = 0;
                }
            }
            //Cook Self Sustain
            int prob = 70;
            if (getManpower() - Manpower[3] <= 0)//harder if alone for balance
            {
                prob = 30;
            }
            for (int i = 0; i < Manpower[3]; i++)
            {

                if (UnityEngine.Random.Range(0, 100) <= prob)
                {
                    mealSuccess++;
                }
            }
        }
       



        int CutConsume = -(getManpower()) + mealSuccess;
        int CutHeads = Supplies + CutConsume;
        AddSupplies(CutConsume);
        if (CutHeads<0)
        {
            AddManpower(CutHeads,-1);
        }
    }
    public void AddGold(int num)
    {
        if (Gold + num > 0)
        {
            Gold += num;
        }
        else
        {
            Gold = 0;
        }
    }
    public void AddTrophy(int num)
    {
        if (Trophy + num > 0)
        {
            Trophy += num;
        }
        else
        {
            Trophy = 0;
        }
    }
    public void AddLore(int num)
    {
        if (Lore + num > 0)
        {
            Lore += num;
        }
        else
        {
            Lore = 0;
        }
    }
    public bool CheckLore(int cypher)
    {
        if (Lore+ManpowerScolarBonus>=cypher)
        {
            return true;
        }
        return false;
    }
    public void AddHint(int num)
    {
            if (Hint+num>100)
            {
                Hint = 100;
            }
            else if (Hint+num<0)
            {
                Hint = 0;
            }
            else
            {
                Hint += num;
            }

        
    }
    public void CutHint()
    {
    
            AddHint(-100 + ManpowerTrackerBonus+ItemHintCutBoost);
    }
    public void AddFunds(int num)
    {
        if (Funds + num > 0)
        {
            Funds += num;
        }
        else
        {
            Funds = 0;
        }
    }


    public void AddThrillProbability(int num)
    {

        if (ThrillProbability + num < 40 || ThrillProbability + num > 0)
        {
            ThrillProbability += num;
        }
        else if (Hint + num < 40)
        {
            ThrillProbability = 40;
        }
        else
        {
            ThrillProbability = 0;
        }
    }
    

    public void RefreshManpower()
    {
        Manpower = new int[] { Manpower[0], Manpower[1], Manpower[2] ,Manpower[3]};
    }
    public bool CheckDeath()
    {
        if (getManpower()<=0)
        {
            return true;
        }
        return false;
    }

    public void Die()
    {
        PilgrimsPath = 0;
        Manpower[0] = 0;
        Manpower[1] = 0;
        Manpower[2] = 0;
        Manpower[3] = 0;
        UpdateManpowerBoni();
        Supplies = 0;
        Gold = 0;
        Trophy = 0;
        Hint = 0;
        ThrillProbability = 0;
        MealCounter = 0;
    }
    public void Reset()
    {
        PilgrimsPath = 0;
        Manpower[0] = 1;
        Manpower[1] = 0;
        Manpower[2] = 0;
        Manpower[3] = 0;
        UpdateManpowerBoni();
        Supplies = 5;
        Gold = 0;
        Trophy = 0;
        Kudos = 0;
        Hint = 0;
        ThrillProbability = 0;
        MealCounter = 0;
    }
    public void ResetStatsPreGame()
    {
        PilgrimsPath = 0;
        Manpower = new int[4];
        Manpower[0] = 1;
        Manpower[1] = 0;
        Manpower[2] = 0;
        Manpower[3] = 0;
        Supplies = 0;
        ThrillProbability = 0;
        ManpowerScolarBonus = 0;
        ManpowerTrackerBonus = 0;
        ManpowerCookBonus = 0;
        Strength = 0;
        MealCounter = 0;
    }
    public void ResetAndConvert()
    {
        AddPrestige(Convert());
        Thrill = 0;
        Gold = 0;
        Trophy = 0;
        Kudos = 0;
        ThrillProbability = 0;
    }

    public int Convert()
    {
        return Mathf.CeilToInt(((float)Gold + (float)Trophy + (float)Kudos)*(1f+0.1f*(float)Thrill));
    }
    public void ItemCheck()
    {
        foreach (Contract con in ActiveContracts)
        {
            if (true)
            {

            }
        }
    }

    //public void ManageResources(int Th, int Pr, int Mp, int Su, int Go, int Tr, int Lo, int Hi)
    //{
    //    AddThrill(Th);
    //    AddPrestige(Pr);
    //    AddManpower(Mp);
    //    AddSupplies(Su);
    //    AddGold(Go);
    //    AddTrophy(Tr);
    //    AddLore(Lo);
    //    AddHint(Hi);
    //}


}
