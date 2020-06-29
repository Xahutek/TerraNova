using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayCard : MonoBehaviour
{
    public Card currentCard;
    public PlayerData playerData;

    public Text Name;
    public Text Desciption;
    public GameObject[] Shade;
    public Image Artwork;
    public GameObject PanicHighlight;

    public List<GameObject> ResourceMarkers;
    public List<Image> ResourceIcons;
    public List<Image> ResourcePlus;
    public List<Image> ResourceMinus;

    public List<CardResources> TaintCardEffect;
    public List<CardBehaviours> TaintCardBehaviour;

    public List<GameObject> TaintMarker;
    public List<Image> TaintIcon;
    public List<Image> TaintPlus;
    public List<Image> TaintMinus;

    public GameObject PanicMarker;


    public GameObject TaintInvolved;
    public GameObject CypherInvolved;
    public GameObject ChanceInvolved;
    Card currentsave;
    private void Update()
    {
        if (currentsave!=currentCard)
        {

            currentsave = currentCard;
        }
       
    }

    public void SetTainted(CardResources TaintEffect,CardBehaviours TaintBehaviour,bool HideEffect)
    {

        if (TaintBehaviour != null)
        {
            if (TaintBehaviour.GetType() == typeof(PanicBehaviour))
            {
                PanicMarker.SetActive(true);
            }
            TaintCardBehaviour.Add(TaintBehaviour);

            if (!HideEffect)
            {        TaintInvolved.SetActive(true);

            }
        }
        if (TaintEffect!=null)
        {
            int i = 0;
            TaintCardEffect.Add(TaintEffect);
            foreach (var marker in TaintMarker)
            {
                if (TaintEffect == null)
                {
                    break;
                }
                if (!marker.activeSelf)
                {

                    marker.SetActive(true);
                    TaintIcon[i].sprite = TaintEffect.Icon;
                    if (!HideEffect)
                    {
                        if (TaintEffect.Value > 0) //Plus
                        {
                            TaintPlus[i].gameObject.SetActive(true);
                            TaintPlus[i].fillAmount = (1.0f / 3.0f) * (float)TaintEffect.power;
                            TaintInvolved.SetActive(true);
                        }
                        else //Minus
                        {
                            TaintMinus[i].gameObject.SetActive(true);
                            TaintMinus[i].fillAmount = (1.0f / 3.0f) * (float)TaintEffect.power;
                            TaintInvolved.SetActive(true);
                        }
                    }
                    
                    break;
                }
                i++;
            }
        }
    }


    public void SetCard(Card card)
    {
        currentCard = card;

        if (card.Multi&&card.MultiCardList!=null&&card.MultiCardList.Count>0)
        {

            List<Card> Options = new List<Card>();
            //currentCard = currentCard.MultiCardList[rand];
            foreach (var option in card.MultiCardList)
            {
                if (option!=null)
                {
                    Options.Add(option);
                }
            }
            int rand = Random.Range(0, Options.Count);
            if (Options[rand]!=null)
            {
                currentCard = Options[rand];
            }
            else
            {
                currentCard = card;
            }
            
            card.Multivalue = card.MultiCardList.IndexOf(currentCard);
        }

        ChanceInvolved.SetActive(false);
        CypherInvolved.SetActive(false);
        TaintInvolved.SetActive(false);


        TaintCardEffect.Clear();
        TaintCardBehaviour.Clear();
        Name.text = currentCard.name;
        if (currentCard.name == "")
        {
            Name.gameObject.SetActive(false);
            Shade[0].SetActive(false);

        }
        else
        {
            Name.gameObject.SetActive(true);
            Shade[0].SetActive(true);
        }

        Desciption.text = currentCard.description;
        if (currentCard.description=="")
        {
            Desciption.gameObject.SetActive(false);
            Shade[1].SetActive(false);

        }
        else
        {
            Desciption.gameObject.SetActive(true);
            Shade[1].SetActive(true);
        }
        Artwork.sprite =currentCard.CardArt;

        RefreshResources();
    }

    public void RefreshResources()
    {
        //Clean Up
        int i = 0;

            for (int x = 0; x < TaintMarker.Count; x++)
            {
                TaintMarker[x].SetActive(false);
                TaintPlus[x].gameObject.SetActive(false);
                TaintMinus[x].gameObject.SetActive(false);
            }

        PanicMarker.SetActive(false);
        foreach (var Marker in ResourceMarkers)
        {
            Marker.SetActive(false);
            ResourcePlus[i].gameObject.SetActive(false);
            ResourceMinus[i].gameObject.SetActive(false);
            i++;
        }



        if (currentCard.Type == CardType.Cypher)
        {
            CypherInvolved.SetActive(true);
        }
        if (currentCard.Type == CardType.Chance)
        {
            ChanceInvolved.SetActive(true);
        }

        //Set New
        i = 0;
        foreach (CardResources Boni in currentCard.Resources)
        {
            if (Boni.Hide)
            {
                continue;
            }
            if (i < ResourceMarkers.Count)
            {
                ResourceMarkers[i].SetActive(true);

                ResourceIcons[i].sprite = Boni.Icon;
                if (Boni.Value < 0 || Boni.ShowsFakeMinus)
                {
                    ResourceMinus[i].gameObject.SetActive(true);
                    ResourceMinus[i].fillAmount = (1.0f / 3.0f) * (float)Boni.power;
                }
                else
                {
                    ResourcePlus[i].gameObject.SetActive(true);
                    ResourcePlus[i].fillAmount = (1.0f / 3.0f) * (float)Boni.power;
                }
                i++;
            }
            else
            {
                break;
            }
           
        }
       
        foreach (CardResources Boni in currentCard.SpecialResources)
        {
            if (Boni.Hide)
            {
                continue;
            }
            if (i < ResourceMarkers.Count)
            {
                ResourceMarkers[i].SetActive(true);
                ResourceIcons[i].sprite = Boni.Icon;
                if (Boni.Value < 0 || Boni.ShowsFakeMinus)
                {
                    ResourceMinus[i].gameObject.SetActive(true);
                    ResourceMinus[i].fillAmount = (1.0f / 3.0f) * (float)Boni.power;
                }
                else
                {
                    ResourcePlus[i].gameObject.SetActive(true);
                    ResourcePlus[i].fillAmount = (1.0f / 3.0f) * (float)Boni.power;
                }
                i++;
            }
            else
            {
                break;
            }
        }

        if (currentCard.Panic )
        {
            PanicMarker.SetActive(true);
        }


        //if (currentCard.Resources2.Count != 0||currentCard.Chance)
        //{
        //    ChanceInvolved.gameObject.SetActive(true);
        //    foreach (var Boni in currentCard.Resources2)
        //    {
        //        ResourceMarkers[i].SetActive(true);
        //        ResourceIcons[i].sprite = Boni.Icon;
        //        if (Boni.Value < 0)
        //        {
        //            ResourceMinus[i].gameObject.SetActive(true);
        //            ResourceMinus[i].fillAmount = (1.0f / 3.0f) * (float)Boni.power;
        //        }
        //        else
        //        {
        //            ResourcePlus[i].gameObject.SetActive(true);
        //            ResourcePlus[i].fillAmount = (1.0f / 3.0f) * (float)Boni.power;
        //        }
        //        i++;
        //    }
        //}
    }
}

