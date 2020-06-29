using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class HighEldersBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public int ElderNum=0;

    public List<Card> HighEldersInMyOrder;
    public override void Execute()
    {
        Manager manager= GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        manager.OneChoice = true;
        if (HighEldersInMyOrder.Count >= ElderNum)
        {
            switch (ElderNum)
            {
                case 0:
                    //Determine final verdict
                    int Pro = playerData.Lore / 5;
                    int Contra = playerData.MeanToAluxCounter;

                    if (playerData.IsMonk)
                    {
                        Pro = Mathf.FloorToInt(Pro * 1.25f);
                    }
                    if (playerData.HadVision)
                    {
                        Pro = Mathf.FloorToInt(Pro * 1.25f);
                    }
                    string answer = "I see no hope foe you...";
                    if (Pro > Contra)
                    {
                        answer = "I feel you might be ready...";
                    }
                    //Set answers
                    HighEldersInMyOrder[0].description = "You came to us for Wisdom? Wisdom can't be given it must be found. We shall only give you knowledge...";
                    HighEldersInMyOrder[1].description = "We see that what drives you is glory. You accumulated " + playerData.Prestige.ToString() + " Prestige. It is nothing but dust that settels on ones spirit.";
                    HighEldersInMyOrder[2].description = "Is it worth it, we wonder? To live to be remembered in books but not in the hearts of people?";
                    HighEldersInMyOrder[3].description = "Remember that entropy is what gives beauty its meaning... or takes it away.";
                    HighEldersInMyOrder[4].description = "Next time you hurt one of ours, as you did " + playerData.MeanToAluxCounter.ToString() + " times already, remember these implications!";
                    HighEldersInMyOrder[5].description = "If only it would matter... You have nothing but " + playerData.Lore.ToString() + " Lore yet don't know the nature of the CIRCULAR SHAPE.";
                    HighEldersInMyOrder[6].description = "We see you are lead on a journey you yourself cannot comprehend... Right now " + answer;
                    manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;
                case 1: manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;
                case 2: manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;
                case 3: manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;
                case 4: manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;
                case 5: manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;
                case 6: manager.Queue.Add(HighEldersInMyOrder[ElderNum]); ElderNum++; break;

                case 7: ElderNum = 0; manager.OneChoice = false; break;
                default:
                    break;
            }
            
        }
    }
}
