using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card : ScriptableObject
{
    public CardType Type;
    public int ID;
    public bool[] Rarity = { false, false, false , false , false , false , false , false , false , false };//0-20
    public bool Supplies = false;
    public bool Danger = false;
    public bool Move = false;
    public bool Prestige = false;
    public bool Lore = false;
    public bool BigCard;


    public bool Active = true;
    public bool ActiveThisExpedition = true;
    public bool unique = false;
    public bool Animal = false;
    public bool Attack = false;

    public new string name="";
    public Sprite CardArt;
    public string description="";

    public List<CardBehaviours> Behaviours;
    public List<CardResources> Resources;
    public List<CardBehaviours> SpecialBehaviours;
    public List<CardResources> SpecialResources;

    public Card Consequence;
    public bool Event;
    public List<Card> EventQueue;

    public bool Consume=true;
    public bool HasDelay = false;
    public bool Panic = false;
    public bool FourChoice=false;
    public bool TwoChoice=false;
    public bool OneChoice = false;

    public bool Blocked = false;
    public bool Pursue = false;

    public bool Taint = false;
    public CardResources TaintEffect;
    public CardBehaviours TaintBehaviour;
    public bool HideTaintEffect = false;

    public bool Multi = false;
    public int Multivalue;
    public List<Card> MultiCardList;
    public virtual void Execute()
    {
        Debug.Log("None");
        
    }


}
[System.Serializable]
public enum CardType
{
    Base,Chance,Cypher,Attack
}
