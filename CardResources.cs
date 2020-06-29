using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardResources : ScriptableObject
{
    public PlayerData playerData;
    public int Value;
    public int power=1; //1-3
    public bool ShowsFakeMinus;
    public bool Hide = false;
    public Sprite Icon;
    public bool Thrill;
    public bool ManpowerMuscle;
    public bool ManpowerScolar;
    public bool ManpowerTrapper;
    public bool ManpowerCook;
    public bool Supplies;
    public bool Gold;
    public bool Trophy;
    public bool Lore;
    public bool Hint;
    public bool Prestige = false;
    public bool BaseStrength=false;


    public virtual int Give()
    {
       
            float ThrillScalingMultiplicator = 1;

            if (Thrill)
            {
                playerData.AddThrill(Value);
            }
            if (ManpowerMuscle)
            {
                playerData.AddManpower(Value, 0);
            }
            if (ManpowerScolar)
            {
                playerData.AddManpower(Value, 1);
            }
            if (ManpowerTrapper)
            {
                playerData.AddManpower(Value, 2);
            }
            if (ManpowerCook)
            {
                playerData.AddManpower(Value, 3);
            }
            if (Supplies)
            {
                if (playerData.Thrill >= 8)
                {
                    ThrillScalingMultiplicator = 0.5f;
                }
                playerData.AddSupplies(Mathf.CeilToInt(Value * ThrillScalingMultiplicator));
            }
            if (Gold)
            {
                ThrillScalingMultiplicator = Mathf.FloorToInt(playerData.Thrill - 1 / 3);
                playerData.AddGold(Value * (int)ThrillScalingMultiplicator);
            }
            if (Trophy)
            {
                ThrillScalingMultiplicator = Mathf.FloorToInt(playerData.Thrill - 1 / 3);
                playerData.AddTrophy(Value * (int)ThrillScalingMultiplicator);
            }
            if (Lore)
            {
                playerData.AddLore(Value);
            }
            if (Hint)
            {
                playerData.AddHint(Value);
            }
        if (BaseStrength)
        {
            playerData.BaseStrengthBonus+= Value;
        }
        if (Prestige)
        {
            playerData.AddPrestige(Value);
        }
        playerData.UpdateManpowerBoni();

        return 0;
    }
}
