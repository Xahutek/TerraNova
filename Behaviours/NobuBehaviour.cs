using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class NobuBehaviour : CardBehaviours
{
    public bool Final;
    public bool MonkOrVisionCheck;
    public PlayerData playerData;
    public Card Harmony;
    public Card Madness;

    public Card isMonk;
    public Card isNoMonk;
    public Card HadVision;
    public Card HadNoVision;

    public override void Execute()
    {Manager manager = GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        if (Final)
        {
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

            manager.Queue.Clear();
            if (Pro > Contra)
            {
                manager.Queue.Add(Harmony);
            }
            else
            {
                manager.Queue.Add(Madness);
            }
        }
        else 
        {
            if (MonkOrVisionCheck)
            {
                if (playerData.IsMonk)
                {
                    manager.Queue.Add(isMonk);
                }
                else
                {
                    manager.Queue.Add(isNoMonk);
                }
            }
            else
            {
                if (playerData.HadVision)
                {
                    manager.Queue.Add(HadVision);
                }
                else
                {
                    manager.Queue.Add(HadNoVision);
                }
            }
        }
        

        
    }
}
