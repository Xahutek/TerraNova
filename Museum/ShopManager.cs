using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public DataBase dataBase;
    public PlayerData playerData;


    int ManpowerMuscleCost;
    int ManpowerScolarCost;
    int ManpowerTrackerCost;
    int ManpowerCookCost;
    int FundsLeft;
    int ItemWorked;

    //PreGame
    public Text Funds;

    public Text ManpowerStart;
    public Text SuppliesStart;
    public Text ManpowerMuscle;
    public Text ManpowerCook;
    public Text ManpowerScolar;
    public Text ManpowerTracker;
    public Text Strength;
    public Text Hintcut;
    public Text Meal;
    public Text BonusLore;

    public Image EquippedItem1;
    public Image EquippedItem2;
    public GameObject[] ItemHighlight = new GameObject[2];
    public List<ItemDisplay> ItemScrollContentReferences = new List<ItemDisplay>();
    public GameObject ItemShopBlocker;

    bool InItemShop = false;

    public void Initiate(int Funds)
    {        
        //GeneralReset
        foreach (GameObject slide in InfoSlides)
        {
            slide.SetActive(false);
        }
        if (playerData.ActiveContracts[0].Debt)
        {
            FundsLeft = Mathf.CeilToInt( Funds*0.66f );
        }
        else
        {
            FundsLeft = Funds;
        }
        InItemShop = false;
        ItemShopBlocker.SetActive(true);
        //Update Costs
        ManpowerMuscleCost = dataBase.ManpowerMuscleCost;
        ManpowerScolarCost = dataBase.ManpowerScolarCost;
        ManpowerTrackerCost = dataBase.ManpowerTrackerCost;
        ManpowerCookCost = dataBase.ManpowerCookCost;
        //Unequip all items and add 2 empties to override
        playerData.EquippedItems.Clear();
        playerData.EquippedItems.Add(dataBase.Items[0]);
        playerData.EquippedItems.Add(dataBase.Items[0]);
        //Refresh Shop
        ShopUpdate();
        EquippedItem1.sprite = playerData.EquippedItems[0].Art;
        EquippedItem2.sprite = playerData.EquippedItems[1].Art;
        RefreshItemList();
        //Debt feedback
        //if (!Debt)
        //{
        //    FundsLeft = playerData.Funds;
        //}
        //else
        //{
        //    FundsLeft = Mathf.CeilToInt(playerData.Funds / 2);
        //}

    }
    public void Close()
    {
        SellItem(0);
        SellItem(1);
    }

    public void ShopUpdate()
    {
        playerData.UpdateManpowerBoni();
        Funds.text = "" + FundsLeft;
        playerData.Supplies = FundsLeft;
        SuppliesStart.text = "" + playerData.Supplies;
        ManpowerStart.text = "" + playerData.getManpower();

        ManpowerMuscle.text = "" + playerData.Manpower[0];
        ManpowerCook.text = "" + playerData.Manpower[3];
        ManpowerScolar.text = "" + playerData.Manpower[1];
        ManpowerTracker.text = "" + playerData.Manpower[2];
        BonusLore.text = "" + playerData.ManpowerScolarBonus;
        Strength.text = "" + playerData.Strength;
        Meal.text = "" + playerData.ManpowerCookBonus;
        Hintcut.text = "" + (-75 + playerData.ManpowerTrackerBonus);
        
    }
    //Plus Manpower
    public void AddMuscle()
    {
        if (FundsLeft - ManpowerMuscleCost >= 0)
        {
            playerData.Manpower[0] += 1;
            FundsLeft -= ManpowerMuscleCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            NotEnoughFunds();
        }
    }
    public void AddScolar()
    {
        if (FundsLeft - ManpowerScolarCost >= 0)
        {
            playerData.Manpower[1] += 1;
            FundsLeft -= ManpowerScolarCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            NotEnoughFunds();
        }
    }
    public void AddTracker()
    {
        if (FundsLeft - ManpowerTrackerCost >= 0)
        {
            playerData.Manpower[2] += 1;
            FundsLeft -= ManpowerTrackerCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            NotEnoughFunds();
        }
    }
    public void AddCook()
    {
        if (FundsLeft - ManpowerCookCost >= 0)
        {
            if (playerData.Manpower[3]<=4) //Can't hire more than 4 cooks
            {
                playerData.Manpower[3] += 1;
                FundsLeft -= ManpowerCookCost;
                RefreshItemListEquippedStatus();
            }
        }
        else
        {
            NotEnoughFunds();
        }
    }
    //Minus Manpower
    public void MinusMuscle()
    {
        if (playerData.Manpower[0] > 1)
        {
            playerData.Manpower[0] -= 1;
            FundsLeft += ManpowerMuscleCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            //playAnimation
        }
    }
    public void MinusScolar()
    {
        if (playerData.Manpower[1] > 0)
        {
            playerData.Manpower[1] -= 1;
            FundsLeft += ManpowerScolarCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            //playAnimation
        }
    }
    public void MinusTracker()
    {
        if (playerData.Manpower[2] > 0)

        {
            playerData.Manpower[2] -= 1;
            FundsLeft += ManpowerTrackerCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            //playAnimation
        }
    }
    public void MinusCook()
    {
        if (playerData.Manpower[3] > 0)

        {
            playerData.Manpower[3] -= 1;
            FundsLeft += ManpowerCookCost;
            RefreshItemListEquippedStatus();
        }
        else
        {
            //playAnimation
        }
    }
    //Buy/Sell Items
    public void BuyItem(int _index)
    {
        ItemDisplay Temp = ItemScrollContentReferences[_index];
        if (Temp.thisItem.FundsCost <= FundsLeft) //can i afford?
        {
            //Refund the current item in that slot; pay for the new one and equip it
            FundsLeft += playerData.EquippedItems[ItemWorked].FundsCost;
            FundsLeft -= Temp.thisItem.FundsCost;
            Temp.EquipMe(ItemWorked);
            //Refresh Equipped Markers
            RefreshItemListEquippedStatus();

        }
        else //bitch is broke
        {
            NotEnoughFunds();
        }
    }
    public void SellItem(int index)
    {
        ItemDisplay Temp = ItemScrollContentReferences[index];
        if (index != 0)
        {
            if (Temp.Equipped)
            {
                int i = playerData.EquippedItems.IndexOf(Temp.thisItem); //Get the ID in Equipped items

                FundsLeft += Temp.thisItem.FundsCost; //Refund Item
                playerData.EquippedItems[i] = dataBase.Items[0]; //Replace it with empty slot
           
            }
        }
        else
        {
            //Buy empty nevertheless so that 
            //clicking on empty always clears your space 
            //and can be equipped double
            BuyItem(0);
        }
        //Refresh Equipped Markers
        RefreshItemListEquippedStatus();
    }

    //Update items
    public void RefreshItemList()
    {
        int i = 0;
        foreach (var itemcard in ItemScrollContentReferences)
        {
            itemcard.me.SetActive(false);

        }
        foreach (var item in dataBase.Items)
        {
            if (item.Active)
            {
                ItemScrollContentReferences[i].me.SetActive(true);
                ItemScrollContentReferences[i].Refresh(item);
                i++;
            }
        }
        RefreshItemListEquippedStatus();
    }
    public void RefreshItemListEquippedStatus()
    {

        foreach (var item in ItemScrollContentReferences)
        {
            if (item.gameObject.activeSelf)
            {
                if (item.thisItem == playerData.EquippedItems[0] || item.thisItem == playerData.EquippedItems[1])
                {
                    item.Equipped = true;
                }
                else
                {
                    item.Equipped = false;
                }
            }

        }
        RefreshItemListHighlights(true);
    }
    public void RefreshItemListHighlights(bool UpdateShop)
    {

        foreach (var item in ItemScrollContentReferences)
        {
            if (item.gameObject.activeSelf)
            {
                item.Dark.SetActive(false);
                item.Highlight.SetActive(false);
                if (item.thisItem.FundsCost > FundsLeft)
                {
                    item.Dark.SetActive(true);
                }
                if (item.Equipped)
                {
                    item.Highlight.SetActive(true);
                    item.Dark.SetActive(false);
                }
            }

        }
        RefreshSlotIcons();
        ShopUpdate();
    }
    public void RefreshSlotIcons()
    {
        EquippedItem1.sprite = playerData.EquippedItems[0].Art;
        EquippedItem2.sprite = playerData.EquippedItems[1].Art;
    }

    //ItemShop
    public void TriggerItemShop(int SlotID)
    {
        if (!InItemShop) //if no slot marked properly open slot
        {
            //Define Item Slot we shop for
            ItemWorked = SlotID;
            //MarkSlot with highlight
            foreach (var highlight in ItemHighlight)
            {
                highlight.SetActive(false);
            }
            ItemHighlight[ItemWorked].SetActive(true);
            //Unlock Shop
            ItemShopBlocker.SetActive(false);
            InItemShop = true;
        }
        else if (ItemWorked != SlotID) //if its the other slot dont close the shop just change the context
        {
            //If its the other one just change item
            ItemHighlight[ItemWorked].SetActive(false);
            ItemHighlight[SlotID].SetActive(true);
            ItemWorked = SlotID;
        }
        else //if its the same slot it deselects properly closing the shop
        {
            foreach (var highlight in ItemHighlight)
            {
                highlight.SetActive(false);
            }
            ItemShopBlocker.SetActive(true);
            InItemShop = false;
        }
       
        
    }

    //If Funding is not sufficient
    public void NotEnoughFunds()
    {
        //playAnimation
        Debug.Log("Not ENough Funds");
    }

    //Visual Stuff for cooks

    public List<GameObject> InfoSlides;

    public void ShowInfoSlide(int num)
    {
        if (num < InfoSlides.Count && num >= 0)
        {
            foreach (GameObject slide in InfoSlides)
            {
                slide.SetActive(false);
            }
            InfoSlides[num].SetActive(true);
        }
        
    }
   
}
