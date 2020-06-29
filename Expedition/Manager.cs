using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public PlayerData playerData;
    public DataBase dataBase;
    public LevelManager LevelManager;
    public UIManagerExpedition UI;
    public AudioManager audioManager;

    public Animator CardAnimator;

    public List<DisplayCard> CardSlots;
    public List<Card> Queue;
    public List<Card> Log;

    public int StepCount = 0;
    public int StepOnThrillCount=0;
    public int HintPlus = 5;

    public bool Forward = true;
    public bool Dead = false;
    public bool FourChoice = false;
    public bool TwoChoice = false;
    public bool OneChoice = false;
    public bool Consume = true;
    public bool NoInteractions = false;
    public bool Panic = false;
    public bool UnderAttack = false;
    public bool Event = false;
    public bool FocusImmunity =false;
    public bool InVision = false;
    public bool BigOnScreen;
    public GameObject PanicFrame;
    public VisionManager visionManager;

    public int LoreBoost=0;
    public int StrengthBoost = 0;
    int ThrillSave = 0;

    public GameObject BackTrackTopButton;
    public GameObject BacktrackActive;
    public GameObject BackTrackLock;


    //public bool Testmode = false;
    public void Start()
    {
        //if (Testmode)
        //{
        //    playerData.ResetStatsPreGame();
        //    dataBase.ResetContracts();
        //    playerData.Supplies = 20;
        //}
        FocusImmunity = false;
        dataBase.ResetExpedition();
        StepCount = 0;

        visionManager.PlayerSave.Clear();
        foreach (var item in dataBase.Start)
        {
            Queue.Add(item);
        }
        Forwards();
        Queue.Clear();
        BackTrackTopButton.SetActive(false);
    }

    private void Update()
    {
        //if (Dead) //For Cheating
        //{
        //    Dead = false;
        //    TwoChoice = false;
        //    playerData.AddManpower(4, 0);
        //    playerData.AddSupplies(100);

        //    Queue.Clear();
        //    Consume = false;
        //    CardSlots[0].SetCard(dataBase.FailState);
        //    StartCoroutine(ChooseCard(CardSlots[0].currentCard, 0));
        //    playerData.Hint = 100;
        //    //if (Input.GetKeyDown(KeyCode.Tab))
        //    //{
        //    //    Forward = true;
        //    //    Dead = false;
        //    //    TwoChoice = false;
        //    //    playerData.AddManpower(4, 0);
        //    //    playerData.AddSupplies(100);

        //    //    Queue.Clear();
        //    //    Consume = false;
        //    //    Forwards();
        //    //}
        //}
    }
   
    public void PlayCardSlot(int num)
    {
        if (!NoInteractions && !CardSlots[num].currentCard.Blocked)
        {
            StartCoroutine(ChooseCard(CardSlots[num].currentCard, num));
            //Play(CardSlots[num].currentCard, num);
        }
    }
    IEnumerator ChooseCard(Card card, int Index)
    {
        if (Panic)
        {
            CardAnimator.SetTrigger("CHOOSEPANIC");
            Panic = false;
        }
        else
        {
            CardAnimator.SetTrigger("CHOOSE");
        }

        NoInteractions = true;
        playerData.AddThrillProbability(1);
        UI.ManageBackground(playerData.Thrill, card.Event);
        if (Forward && !card.Event)
        {
            StepCount++;
            StepOnThrillCount++;
            playerData.AddHint(HintPlus);

            UI.StepRefresh(StepCount);
            if (FocusImmunity && FourChoice)
            {
                FourChoice = false;
            }
        }
        if (card.Event)
        {
            Event = true;
            if (Forward)
            {
                BackTrackTopButton.SetActive(true);
                BackTrackLock.SetActive(true);
            }
        }
        else
        {
            Event = false;
            if (Forward)
            {
                BackTrackTopButton.SetActive(false);
                BackTrackLock.SetActive(false);
            }
        }

        if (Forward)
        {
            UI.BackConsumeTotalRefresh(Log.Count);
        }
        else
        {
            UI.BackConsumeTotalRefresh(Queue.Count);
        }
        card.Execute();
        if (card.Consequence != null)
        {
            Log.Add(card.Consequence);
        }
        foreach (var effect in CardSlots[Index].TaintCardEffect)
        {
            effect.Give();
        }
        foreach (var effect in CardSlots[Index].TaintCardBehaviour)
        {
            effect.Execute();
        }

        foreach (var slots in CardSlots)//Check Pursue
        {
            slots.PanicMarker.SetActive(false);
            if (slots.gameObject.activeSelf && slots.currentCard != card && slots.currentCard.Pursue)
            {
                Queue.Add(slots.currentCard);
            }
        }
        foreach (var con in playerData.ActiveContracts)
        {
            if (con.goal.IsReached(card))
            {
                Debug.Log(card.name + " has been found");
                con.GiveReward(playerData);
            }
        }
        if (card.unique)
        {
            card.ActiveThisExpedition = false;
        }
        if (InVision)
        {
            visionManager.Execute(this, card);
        }
        
        yield return new WaitForSeconds(0.666666f); //YIELD FOR ANIMATION
        StartCoroutine(ConsumeBetween(card));
        
    }
    IEnumerator ConsumeBetween(Card card)
    {
        if (Consume && !card.Event)
        {
            CardAnimator.SetTrigger("CONSUME");
            Debug.Log("Consume");

            yield return new WaitForSeconds(0.33333f); //YIELD FOR ANIMATION let effect happen at its heigth

            playerData.Consume();
            if (!Forward)
            {
                playerData.Consume();
            }//Again if Backtracking
            StartCoroutine(WaitConsume(card));
        }
        else
        {

            yield return new WaitForSeconds(0); //YIELD FOR ANIMATION let effect happen at its heigth
            Consume = true;
            StartCoroutine(NextStep(card));

        }

    }
    IEnumerator WaitConsume(Card card)
    {
        yield return new WaitForSeconds(0.33333f); //YIELD FOR ANIMATION let effect happen at its heigth
        Consume = true;
        StartCoroutine(NextStep(card));

    }
    IEnumerator NextStep(Card card)
    {
        if (Dead)
        {
            LevelManager.LoadMuseum();//Quasi break to the scene
        }
        if (playerData.CheckDeath()) { Panic = false; Death(); }
        else if (Forward)
        {
            Forwards();
        }
        else
        {
            Backwards();
        }

        if (!Dead && playerData.Thrill != ThrillSave) //MAP TRANSITION
        {
            Panic = false;
            StepOnThrillCount = 0;
            Queue.Clear();
            MapCheck(ThrillSave, playerData.Thrill);
        }



        if (card.Attack)
        {
            if (!UnderAttack)
            {
                audioManager.PauseAll();
                audioManager.PlayAttack(true);
            }
            UnderAttack = true;
        }
        else
        {
            if (UnderAttack)
            {
  UnderAttack = false;
                audioManager.PauseAll();
                audioManager.RefreshBackground(playerData.Thrill);
            }
        }
        playerData.StrengthBoost = 0;
        playerData.ManpowerScolarBonus -= LoreBoost;
        StrengthBoost = 0;
        LoreBoost = 0;
        foreach (Item item in playerData.EquippedItems)
        {
            item.Passive();
            playerData.UpdateManpowerBoni();
        }
        ThrillSave = playerData.Thrill;
        if (!Panic)
        {
            CardAnimator.SetTrigger("NEW");

            if (card.HasDelay)
            {
                yield return new WaitForSeconds(1.5f); //YIELD FOR ANIMATION
            }
            else
            {
                yield return new WaitForSeconds(0.333f); //YIELD FOR ANIMATION
            }
           
            NoInteractions = false;
        }
        else //if (Panic)
        {
            Debug.Log("DrawPanic");

            CardAnimator.SetTrigger("NEWPANIC");

            PanicFrame.SetActive(true);
            audioManager.Play("Panic");
            List<DisplayCard> active = new List<DisplayCard>();
            foreach (var slot in CardSlots)
            {
                if (slot.gameObject.activeSelf && !slot.currentCard.Blocked)
                {
                    active.Add(slot);
                }

            }
            int i = UnityEngine.Random.Range(0, active.Count);
            CardSlots[i].PanicMarker.SetActive(true);
            yield return new WaitForSeconds(2);
            StartCoroutine(ChooseCard(CardSlots[i].currentCard, i));

            PanicFrame.SetActive(false);
            NoInteractions = false;
        }//Panic is deactivated at the start to secure smooth animation
        
    }
 //   public void Play(Card card, int Index)
 //   {

 //       NoInteractions = true;
 //       playerData.AddThrillProbability(1);
 //       UI.ManageBackground(playerData.Thrill, card.Event);
 //       if (Forward && !card.Event)
 //       {
 //           StepCount++;
 //           StepOnThrillCount++;
 //           playerData.AddHint(HintPlus);

 //           UI.StepRefresh(StepCount);
 //           if (FocusImmunity&&FourChoice)
 //           {
 //               FourChoice = false;
 //           }
 //       }
 //       if (card.Event)
 //       {
 //           Event = true;
 //       }
 //       else
 //       {
 //           Event = false;
 //       }
        
 //       if (Forward)
 //       {
 //           UI.BackConsumeTotalRefresh(Log.Count);
 //       }
 //       else
 //       {
 //           UI.BackConsumeTotalRefresh(Queue.Count);
 //       }


 //       //Play Card effects
 //       card.Execute();
 //       if (card.Consequence != null)
 //       {
 //           Log.Add(card.Consequence);
 //       }
 //foreach (var effect in CardSlots[Index].TaintCardEffect)
 //       {
 //           effect.Give();
 //       }
 //       foreach (var effect in CardSlots[Index].TaintCardBehaviour)
 //       {
 //           effect.Execute();
 //       }

 //       foreach (var slots in CardSlots)//Check Pursue
 //       {
 //           slots.PanicMarker.SetActive(false);
 //           if (slots.gameObject.activeSelf && slots.currentCard != card && slots.currentCard.Pursue)
 //           {
 //               Queue.Add(slots.currentCard);
 //           }  
 //       }
 //       //Check Taint
       
 //       foreach (var con in playerData.ActiveContracts)
 //       {
 //           if (con.goal.IsReached(card))
 //           {
 //               Debug.Log(card.name+" has been found");
 //               con.GiveReward(playerData);
 //           }
 //       }
 //       if (card.unique)
 //       {
 //           card.ActiveThisExpedition = false;
 //       }

 //       if (Consume && !card.Event) {
 //           playerData.Consume();
 //           if (!Forward)
 //           {
 //               playerData.Consume();
 //           }//Again if Backtracking
 //           Debug.Log("CONSUME");
 //       }
 //       Consume = true;

 //       if (InVision)
 //       {
 //           visionManager.Execute(this, card);
 //       }

 //       if (Dead)
 //       {
 //           LevelManager.LoadMuseum();//Quasi break to the scene
 //       }
 //       if (playerData.CheckDeath()) {  Panic = false; Death(); }
 //       else if (Forward)
 //       {
 //           Forwards();
 //       }
 //       else
 //       {
 //           Backwards();
 //       }

 //       if (!Dead && playerData.Thrill != ThrillSave) //MAP TRANSITION
 //       {
 //           Panic = false;
 //           StepOnThrillCount = 0;
 //           Queue.Clear();
 //           MapCheck(ThrillSave,playerData.Thrill);
 //       }
 //       NoInteractions = false;
 //       if (Panic)
 //       {
 //           PanicFrame.SetActive(true);
 //           Panic = false;
 //           audioManager.Play("Panic");
 //           StartCoroutine(PanicRoutine());
 //       }
 //       else if (card.HasDelay)
 //       {
 //           StartCoroutine(DelayInteractions());
 //       }
        
 //       if (UnderAttack && !card.Attack)
 //       {
 //           UnderAttack = false;
 //           audioManager.RefreshBackground(playerData.Thrill);
 //       }
 //       if (card.Attack)
 //       {
 //           if (!UnderAttack)
 //           {
 //               audioManager.Play("Attack");
 //           }
 //           UnderAttack = true;
 //       }
 //       playerData.StrengthBoost = 0;
 //       playerData.ManpowerScolarBonus -= LoreBoost;
 //       StrengthBoost = 0;
 //       LoreBoost = 0;
 //       foreach (Item item in playerData.EquippedItems)
 //       {
 //           item.Passive();
 //           playerData.UpdateManpowerBoni();
 //       }
 //       ThrillSave = playerData.Thrill;
 //   }
 //   IEnumerator DelayInteractions()
 //   {
 //       NoInteractions = true;
 //       yield return new WaitForSeconds(1.5f);
 //       NoInteractions = false;
 //   }
    //IEnumerator PanicRoutine()
    //{
        
    //    NoInteractions = true;
    //    List<DisplayCard> active = new List<DisplayCard>();
    //    foreach (var slot in CardSlots)
    //    {
    //        if (slot.gameObject.activeSelf && !slot.currentCard.Blocked)
    //        {
    //            active.Add(slot);
    //        }

    //    }
    //    int i = UnityEngine.Random.Range(0, active.Count);
    //    CardSlots[i].PanicMarker.SetActive(true);
    //    yield return new WaitForSeconds(Array.Find(audioManager.sounds, sound => sound.name == "Panic").clip.length);

    //    PanicFrame.SetActive(false);
    //    NoInteractions = false;
    //}

    public Animator MapControl;
    public GameObject ExpeditionMarker;
    public List<GameObject> FogOfWar;
    public Vector2 Pos1;
    public Vector2 Pos2;
    public Vector2 Pos3;
    public Vector2 Pos4;
    public Vector2 Pos5;
    private void MapCheck(int ThrillSave, int Thrill)
    {
        int AreaStart = ThrillToArea(ThrillSave);
        int AreaEnd = ThrillToArea(Thrill);

        if (AreaStart!=AreaEnd)
        {
            StartCoroutine(EnterRegion( AreaEnd));
        }
    }
    IEnumerator EnterRegion(int AreaEnd)
    {
        NoInteractions = true;

        for (int x = 0; x < playerData.AreasVisited.Count; x++)
        {
            if (x < AreaEnd)
            {
                playerData.AreasVisited[x] = true;
            }
          
        }


        MapControl.SetTrigger("Open");
        MoveMarker(AreaEnd);
        int i = 1;
        foreach (GameObject cloud in FogOfWar)
        {
            cloud.SetActive(false);
            if (i<=FogOfWar.Count&&i==AreaEnd)
            {
                cloud.SetActive(true);
            }
            //if (playerData.AreasVisited.Count > i + 1 && !playerData.AreasVisited[i + 1])
            //{
            //    foreach (GameObject c in FogOfWar)
            //    {
            //        c.SetActive(false);
            //    }
            //    cloud.SetActive(true);
            //}
            i++;
        }

        Queue.Clear();

        audioManager.PauseAll();
        yield return new WaitForSeconds(3);
        audioManager.RefreshBackground(playerData.Thrill);
        MapControl.SetTrigger("Close");
        NoInteractions = false;

    }
    private void MoveMarker( int areaEnd)
    {
        Vector2 end = new Vector2();
        switch (areaEnd)
        {
            case 1: end = Pos1; break;
            case 2: end = Pos2; break;
            case 3: end = Pos3; break;
            case 4: end = Pos4; break;
            case 5: end = Pos5; break;
            default: end = Pos1; break;
        }
     
        ExpeditionMarker.transform.localPosition = end;
    }
    private int ThrillToArea(int Thrill)
    {
        switch (Thrill)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 2;
            case 3: return 2;
            case 4: return 2;
            case 5: return 3;
            case 6: return 3;
            case 7: return 3;
            case 8: return 4;
            case 9: return 4;
            case 10: return 5;
            default: return 1;
        }
    }

    public void Death()
    {
        
        playerData.Die();
        //Clean Up
        foreach (var DisplayCard in CardSlots)
        {
            DisplayCard.gameObject.SetActive(false);
        }
        Queue.Clear();
        //Queue Cards if necessary

        CardSlots[0].gameObject.SetActive(true);
        CardSlots[1].gameObject.SetActive(true);
        CardSlots[0].SetCard(dataBase.DrawDeathCard());
        CardSlots[1].SetCard(dataBase.DrawDeathCard());
        if (CardSlots[0].currentCard==CardSlots[1].currentCard)
        {
            CardSlots[1].gameObject.SetActive(false);
        }
        List<int> activeindex = new List<int>();//for Taint info
        bool isTainted = false;
        int i = 0;
        foreach (DisplayCard card in CardSlots)
        {
            if (card.gameObject.activeSelf)
            {
                if (card.currentCard.Taint)
                {
                    isTainted = true;
                }
                activeindex.Add(i);
                
            }
            i++;
        }
        if (isTainted)
        {
            Tainted(activeindex);
        }
        Dead = true;
    }
    

    public bool NoAnimals = false;
    public void Forwards()
    {
        //Clean Up
        foreach (var DisplayCard in CardSlots)
        {

            DisplayCard.gameObject.SetActive(false);
        }


        foreach (var item in Queue)
        {
            //Debug.Log(item.name);
        }
        if (Queue.Count==0)
        {
            //Debug.Log("EMPTY");
        }


        //Build new
        int NeededCards = 3;
        if (OneChoice)
        {
            NeededCards = 1;
            OneChoice = false;
        }
        else if (TwoChoice)
        {
            NeededCards = 2;
            TwoChoice = false;
        }
        else if (FourChoice)
        {
            NeededCards = 4;
            FourChoice = false;
        }
        //Queue Cards


        List<Card> SideStack = new List<Card>();

        Card TheCard;
            int MaxRounds = 20;
        for (int i = 0; i < NeededCards; i++)
        {
            MaxRounds--;

            TheCard = dataBase.DrawCard(playerData.Thrill, StepOnThrillCount);

            bool check = true;
            if (TheCard.Animal && NoAnimals)
            {
                check = false;
                
            }
            if (check)
            {
                foreach (var card in SideStack)
                {
                    if (card == TheCard)
                    {
                        check = false;
                        break;
                    }
                }
    
                for (int x = 0; x < Queue.Count; x++)
                {
                    if (Queue[x] == TheCard)
                    {
                        check = false;
                        break;
                    }
                    if (Queue[x].Animal && NoAnimals)
                    {
                        Queue.RemoveAt(x);
                    }
                }
            }//Check Duplicates

            if (!check && MaxRounds > 0)
            {
                i--;
            }
            else
            {
                SideStack.Add(TheCard);
            }

        }//Pull Unique Cards

        List<Card> Stack = new List<Card>();

        for (int i = 0; i < Queue.Count; i++)
        {
            Stack.Add(Queue[i]);
        }

        if (Queue.Count < NeededCards)
        {
            for (int i = 0; i < NeededCards-Queue.Count; i++)
            {
                Stack.Add(SideStack[i]);
            }
        }

        //Set Display Cards
        List<int> activeindex = new List<int>();//for Taint info
        bool BigBoiCominIn = false;
        foreach (var Card in Stack)
        {
            if (Card.BigCard)
            {
                CardSlots[4].gameObject.SetActive(true);
                CardSlots[4].SetCard(Card);
                //Debug.Log("CARD SET:"+Card.name);
                BigBoiCominIn = true;
                activeindex.Add(4);
                break;
            }
        }

        bool isTainted = false;
        if (!BigBoiCominIn)
        {

                for (int j = 0; j < NeededCards; j++)
                {
                    CardSlots[j].gameObject.SetActive(true);
                }
            
            int i = 0;
            foreach (var DisplayCard in CardSlots)
            {
                if (DisplayCard.gameObject.activeSelf)
                {
                    DisplayCard.SetCard(Stack[i]);
                    //Debug.Log("CARD SET:" + Stack[i].name);
                   
                    if (Stack[i].Taint)
                    {
                        isTainted = true;
                    }
                    activeindex.Add(i);
                   
                }
                i++;
            }
        }
       

        if (isTainted)
        {
            Tainted(activeindex);
        }
        Queue.Clear();
        NoAnimals = false;

    }

    bool firstBackwards = true;
    public void Backwards()
    {
        //Clean Up
        foreach (var DisplayCard in CardSlots)
        {
            DisplayCard.gameObject.SetActive(false);
        }

        //Queue Cards if necessary
        if (firstBackwards)
        {
            Queue.Clear();
            //if (Log.Count <= 0)
            //{
            //    LevelManager.LoadMuseum();
            //}
            foreach (var card in Log)
            {
                Queue.Insert(0,card);
            }
            if (Queue.Count%2==1)
            {
                Queue.Add(dataBase.FailState);
            }

            firstBackwards = false;
            TwoChoice = true;
            Log.Clear();
        }

        if (Queue.Count <= 0)
        {
            LevelManager.LoadMuseum();
        }
        else
        {
            //Set Display Cards
            List<int> activeindex = new List<int>();//for Taint info
            bool BlockedOne = false;
            CardSlots[0].gameObject.SetActive(true);
            CardSlots[1].gameObject.SetActive(true);
            int i = 0;
            bool isTainted = false;
            foreach (var DisplayCard in CardSlots)
            {
                if (DisplayCard.gameObject.activeSelf)
                {
                    if (Queue[0].Blocked)
                    {
                        if (BlockedOne)
                        {
                            DisplayCard.SetCard(dataBase.BlockedWayAround);

                            break;
                        }
                        BlockedOne = true;
                    }
                    DisplayCard.SetCard(Queue[0]);
                    if (Queue[0].Taint)
                    {
                        isTainted = true;
                    }
                    activeindex.Add(i);
                    Queue.RemoveAt(0);
                }
                i++;

            }
            TwoChoice = true;
            if (isTainted)
            {
            Tainted(activeindex);
            }
        }

        
    }

    public void Tainted(List<int> activeindex)
    {
        List<int> TaintedIndex = new List<int>();
        List<CardResources> TaintEffects = new List<CardResources>();
        List<CardBehaviours> TaintBehaviours = new List<CardBehaviours>();
        List<bool> TaintHideAllowance = new List<bool>();
        int Displayindex = 0;
        foreach (var Displaycard in CardSlots)
        {
            if (Displaycard.isActiveAndEnabled && Displaycard.currentCard.Taint)
            {
                TaintedIndex.Add(Displayindex);
                TaintHideAllowance.Add(Displaycard.currentCard.HideTaintEffect);

                if (Displaycard.currentCard.TaintEffect!=null)
                {
                    TaintEffects.Add(Displaycard.currentCard.TaintEffect);
                }
                else
                {
                    TaintEffects.Add(null);
                }
                if (Displaycard.currentCard.TaintBehaviour != null)
                {
                    TaintBehaviours.Add(Displaycard.currentCard.TaintBehaviour);
                }
                else
                {
                    TaintBehaviours.Add(null);
                }

            }
            Displayindex++;
        }
        if (TaintedIndex.Count > 0 && activeindex.Count >= 2)
        {
            int save;
            for (int i = 0; i < TaintedIndex.Count; i++)
            {
                save = TaintedIndex[i];
                activeindex.Remove(TaintedIndex[i]);
                int rand = UnityEngine.Random.Range(0, activeindex.Count);
                CardSlots[activeindex[rand]].SetTainted(TaintEffects[i],TaintBehaviours[i],TaintHideAllowance[i]);
                activeindex.Add(save);

            }

        }
    }
    public void InitiateBackwards()
    {
        if (Forward && !Event)
        {
            BackTrackTopButton.SetActive(true);
            BacktrackActive.SetActive(true);
            Debug.Log("Backwards we go");
            Forward = false;
            firstBackwards = true;
            Backwards();
        }
        else if (Event)
        {
            BackTrackTopButton.SetActive(true);
            BackTrackLock.SetActive(true);

            Debug.Log("Event is true");
        }
    }
}
