using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractDisplay : MonoBehaviour
{
    public GameObject me;
    public DataBase Data;
    public PlayerData playerData;

    public Contract MyContract;

    //ScrollCard
    public int ID;
    public int Prestige;
    public string Name;
    public Contractor contractor;

    public Text TName;
    public Text TContractor;
    public Text TPrestige;
    public Image TContractorImage;

    //Present
    public Item Item1;
    public Item Item2;
    public string Description;
    public int FundsPlus;

    public Text TDescriptionName;
    public Text TDescription;
    public Text TFunds;
    public Image TItem1;
    public Image TItem2;
    public GameObject ItemSlot1;
    public GameObject ItemSlot2;



    public void Refresh(Contract con, Text _ContractorDescription, Text _FundsText)
    {
        MyContract = con;
        ID = con.ID;
        Prestige = con.ActivatePrestige;
        Name = con.Name;
        contractor = con.contractor;
        TContractorImage.sprite = contractor.image;

        Item1 = con.Item1;
        Item2 = con.Item2;
        Description = con.Description;
        FundsPlus = con.Funds;

        //Visual
        TName.text = Name;
        TContractor.text = contractor.name;
        TPrestige.text = "" + Prestige;

        TDescription = _ContractorDescription;
        TFunds = _FundsText;
    }

    public void Present()
    {
        playerData.ActiveContracts.Clear();
        playerData.ActiveContracts.Add(MyContract);
        TDescription.text = Description;
        TDescriptionName.text = MyContract.Name;
        if (MyContract.Debt)
        {
            TFunds.text = "" +  Mathf.CeilToInt(FundsPlus * 0.66f);
        }
        else
        {
            TFunds.text = "" + FundsPlus;
        }
        TFunds.text = "" + FundsPlus;
        if (Item1!=null)
        {
            ItemSlot1.SetActive(true);
            TItem1.sprite = Item1.Art;
            Item1.Active = true;

        }
        else
        {
            ItemSlot1.SetActive(false);
        }
        if (Item2!=null)
        {
            ItemSlot2.SetActive(true);
            TItem2.sprite = Item2.Art;
            Item2.Active = true;
            
        }
        else
        {
            ItemSlot2.SetActive(false);
        }

    }
}