using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMuseum : MonoBehaviour
{
    public PlayerData playerData;
    public DataBase dataBase;


    public Animator SliderAnim;
    public GameObject Museum;
    public GameObject PostGameScreen;
    public ShopManager Shop;
    public GameObject pauseMenu;

    //PostGame
    public Text PostPrestige;
    public Text PostTrophy;
    public Text PostGold;
    public Text PostKudos;
    public Text PostThrillMultiplicator;
    public Text PostLore;
    public GameObject PostMessage;
    public Text PostMessageText;
    public Image PostContractor;


    //Profile
    public Image ProfilePic;
    public Text Title;
    public Text Prestige;
    public Text Lore;


    public List<ContractDisplay> ContractScrollContentReferences = new List<ContractDisplay>();
    public GameObject ContractList;

    public GameObject ItemFitter;
    public Image Item1;
    public Image Item2;
    public GameObject ItemSlot1;
    public GameObject ItemSlot2;
    public Text ContractDescription;
    public Text ContractDescriptionTitle;
    public Text FundsPlus;

    bool inPostGameScreen;
    bool inShop;

    //Tutorial
    public List<TutorialPage> Tutorial;
    public GameObject TutorialObject;
    public Text TutorialText;
    [SerializeField] int TutorialSlide = 0;

    public SaveLoad saveLoad;

    private void Start()
    {
        CheckItems();
        TutorialSlide = 0;
        inShop = false;
        if (playerData.Convert() > 0)
        {
            inPostGameScreen = true;
            Museum.SetActive(false);
            PostGameScreen.SetActive(true);

            //Present Screen
            Gold = playerData.Gold;
            Trophy = playerData.Trophy;
            ThrillMaxBonus = playerData.Thrill;
            Kudos = playerData.Kudos;
            PrestigeTotal = playerData.Prestige;

            PostPrestige.text = "" + PrestigeTotal;
            PostGold.text = "" + Gold;
            PostTrophy.text = "" + Trophy;
            PostThrillMultiplicator.text = "" + ThrillMaxBonus;
            PostKudos.text = "" + Kudos;
            PostLore.text = "" + playerData.Lore;


            if (playerData.Kudos > 0)
            {
                PostMessage.SetActive(true);
                PostMessageText.text = playerData.LastFinished.MessageWhenDone;
                PostContractor.sprite = playerData.LastFinished.contractor.image;
                playerData.LastFinished = null;
            }
            else
            {
                PostMessage.SetActive(false);
            }
        }//PostGameScreenStuff
        else
        {
            inPostGameScreen = false;
            Museum.SetActive(true);
            PostGameScreen.SetActive(false);
            RefreshMuseum();
        }
        if (!playerData.MuseumTutorial)
        {
            playerData.MuseumTutorial = true;

            InitiateTutorial();
            
        }//Tutorial Stuff

            Debug.Log("Saved");
            saveLoad.Save(playerData, dataBase);
        
    }
    public void OnDisable()
    {
        playerData.MuseumTutorial = true;

    }

    public void Slidin()
    {
        if (inShop)
        {
            Debug.Log("Slide to BlackBoard");
            SliderAnim.SetTrigger("SlideToCon");
            inShop = false;
        }
        else
        {
            Debug.Log("Slide to Shop");
            inShop = true;
            Shop.Initiate(playerData.ActiveContracts[0].Funds);
            SliderAnim.SetTrigger("SlideToShop");

        }
    }
    private void Update()
    {
        
        if (inPostGameScreen)
        {
            if (Input.anyKey)
            {
                inPostGameScreen = false;
                StartCoroutine(ConvertAndArtisticPause());
                RefreshMuseum();
            }
        }
      
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenuTrigger();
            }
        
    }
    public void PauseMenuTrigger()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }

    //Tutorial
    public void InitiateTutorial()
    {
        TutorialSlide = 0;
        TutorialObject.SetActive(true);
        OpenTutorialSlide(TutorialSlide);
    }
    public void ChangeTutorialSlide(bool Direction)
    {
        if (Direction && TutorialSlide + 1 >= Tutorial.Count)
        {
            
            TutorialObject.SetActive(false);
            Debug.Log("Tutorial Finished");
        }
        else if (!Direction && TutorialSlide != 0)
        {
            TutorialSlide--;
            OpenTutorialSlide(TutorialSlide);
            Debug.Log("Previous Slide");
        }
        else if (Direction && TutorialSlide < Tutorial.Count)
        {
            TutorialSlide++;
            OpenTutorialSlide(TutorialSlide);
            Debug.Log("Next Slide");
        }

    }
    public void OpenTutorialSlide(int num)
    {
        Debug.Log("Open Slide " + num);
        TutorialPage Page = Tutorial[num];
        TutorialText.text = Page.text;
 
    }
    public GameObject ContracterInfoSlide;
    public GameObject[] ContractorInfo;
    int currentslide=-1;

    public void OpenContracterInfo(int num)
    {
        if (currentslide != num)
        {
            ContracterInfoSlide.SetActive(true);
            for (int i = 0; i < ContractorInfo.Length; i++)
            {
                ContractorInfo[i].SetActive(false);
            }
            ContractorInfo[num].SetActive(true);
            currentslide = num;
        }
        else
        {
            currentslide = -1;
            ContracterInfoSlide.SetActive(false);
        }
        

    }

    //PostGameScreen
    public AudioSource Pling;

    int Gold;
    int Trophy;
    int ThrillMaxBonus;
    int Kudos;
    int PrestigeTotal;
    bool pling = false;

    IEnumerator ConvertAndArtisticPause()
    {
        playerData.ResetAndConvert();
        float Delay = 0.02f;
        float time = Delay * ((float)Mathf.Max(Mathf.Max(Gold, Trophy), Kudos));
        StartCoroutine(ConvertNumbers(Delay));
        yield return new WaitForSeconds(time + 2);
        PostGameScreen.SetActive(false);
        Museum.SetActive(true);
    }
    IEnumerator ConvertNumbers(float Delay)
    {
        while (Gold + Trophy + Kudos+ ThrillMaxBonus > 0)
        {
            yield return new WaitForSeconds(Delay);
            StartCoroutine(StepCalculate(Delay));
        }
        PrestigeTotal =playerData.Prestige;
        PostPrestige.text = "" + PrestigeTotal;
    }
    IEnumerator StepCalculate(float Delay)
    {
        pling = false;
        Debug.Log("StepCalculate");
        if (Gold > 0)
        {
            Gold--;
            PrestigeTotal++;
            PostGold.text = "" + Gold;
            pling = true;
        }
        if (Trophy > 0)
        {
            Trophy--;
            PrestigeTotal++;
            PostTrophy.text = "" + Trophy;
            pling = true;
        }
        if (Kudos > 0)
        {
            Kudos--;
            PrestigeTotal++;
            PostKudos.text = "" + Kudos;
            pling = true;
        }
      
            if (ThrillMaxBonus > 0)
            {
                ThrillMaxBonus--;
                PostThrillMultiplicator.text = "" + ThrillMaxBonus;
                pling = true;
                
            }
        
        if (pling)
        {
            if (Pling != null)
            {
                Pling.Play();
            }
        }
        PostPrestige.text = "" + PrestigeTotal;
        yield return new WaitForSeconds(Delay);
    }

    public bool CheckItems()
    {
        bool check=false;
        foreach (Contract con in playerData.ActiveContracts)
        {
            int i = 0;
            foreach (Item item in playerData.EquippedItems)
            {
                if (con.goal.IsReached(item))
                {
                    con.GiveReward(playerData);
                    playerData.EquippedItems[i] = dataBase.Items[0];
                    check = true;
                    break;
                }
                i++;
            }
            if (check)
            {
                break;
            }
        }

        return check;
        
    }

   

    public void RefreshMuseum()
    {
        RefreshContractList();
        if (ContractScrollContentReferences[0].isActiveAndEnabled)
        {
            ContractScrollContentReferences[0].Present();
        }
        RefreshProfileResources();
        SetTitle();
        playerData.Reset();
    }


    public void RefreshProfileResources()
    {
        Prestige.text = "" + playerData.Prestige;
        Lore.text = "" + playerData.Lore;
    }
    public void SetProfilePic(Sprite picture)
    {
        ProfilePic.sprite = picture;
    }
    public void SetTitle()
    {
        string Name = playerData.PlayerName;
        if (Name != "Dora" || Name != "Indiana Jones" || Name != "Jones" || Name != "Indiana")
        {
            Title.text = dataBase.GiveTitle(playerData.Prestige) + " " + playerData.PlayerName;
        }
        else
        {
            if (Name == "Dora")
            {
                Title.text = "Dora the Explorer";
            }
            if (Name == "Indiana Jones")
            {
                Title.text = Name;
            }
            if (Name == "Jones" || Name != "Indiana")
            {
                Title.text = dataBase.GiveTitle(playerData.Prestige) + " " + "Indiana Jones";
            }
        }
    }

    public void RefreshContractList()
    {
        int i = 0;
        bool check = false;
        foreach (var concard in ContractScrollContentReferences)
        {
            concard.me.SetActive(false);
        }
        foreach (var con in dataBase.Contracts)
        {
            if (con.PlotPoint>1 )
            {
                //update list
                if (con.goal.Done)
                {
                    dataBase.PlotPointsDone[con.PlotPoint - 1] = true;
                }
                else
                {
                    dataBase.PlotPointsDone[con.PlotPoint - 1] = false;
                }
                //see if one before is done
                if (dataBase.PlotPointsDone[con.PlotPoint - 2])
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
           
            }
            else
            {
                check = true;
            }
            if (con.ActivatePrestige <= playerData.Prestige && !con.goal.Done && check)
            {
                
                    ContractScrollContentReferences[i].me.SetActive(true);
                    ContractScrollContentReferences[i].
                    Refresh(
                    con, 
                     
                    ContractDescription, 
                    FundsPlus
                    );
                    i++;
            }
            //Debug.Log(con.Name+" is "+con.goal.Done);
        }
    }

}