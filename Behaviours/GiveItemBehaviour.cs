using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class GiveItemBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public DataBase dataBase;

    public Item Item;

    public override void Execute()
    {

        playerData.EquippedItems.Add(Item);
        GameObject.Find("ExpeditionManager").GetComponent<Manager>().UI.ItemNeedUpdate = true ;

    }
}
