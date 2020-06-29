using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemDisplay : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject me;
    public Image ItemImage;
    public Text ItemName;
    public Text ItemDescription;
    public Text FundsCost;

    public Item thisItem;

    public Image EquippedItem1;
    public Image EquippedItem2;

    public GameObject Highlight;
    public GameObject Dark;
    public bool Equipped = false;
  
    public void Refresh(Item item)
    {
        thisItem = item;
        ItemImage.sprite = thisItem.Art;
        ItemName.text = thisItem.name;
        ItemDescription.text = thisItem.itemDescription;
        FundsCost.text = ""+thisItem.FundsCost;
    }

    public void EquipMe(int index)
    {
    
        playerData.EquippedItems[index] = thisItem;
        Equipped = true;
        EquippedItem1.sprite = playerData.EquippedItems[0].Art;
        EquippedItem2.sprite = playerData.EquippedItems[1].Art;
    }
}
