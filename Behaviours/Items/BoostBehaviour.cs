using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boost")]
public class BoostBehaviour : CardBehaviours
{
    public int LoreBoost=0;
    public int StrengthBoost=0;
    public int HintBoost = 0;
    public PlayerData playerData;

    public override void Execute()
    {
        Manager manager =GameObject.Find("ExpeditionManager").GetComponent<Manager>();
        playerData.ManpowerScolarBonus += LoreBoost;
        playerData.StrengthBoost += StrengthBoost;
        playerData.Hint += HintBoost;

        manager.LoreBoost +=LoreBoost;
        manager.StrengthBoost += StrengthBoost;
        Debug.Log("Boosted Bambino: Scolarship: "+playerData.ManpowerScolarBonus+", StrengthBoost: "+playerData.StrengthBoost+", Hint: "+playerData.Hint);
    }
}
