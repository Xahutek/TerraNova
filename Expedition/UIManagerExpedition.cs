using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerExpedition : MonoBehaviour
{
    public PlayerData playerData;
    public Animator Anim;

    public GameObject pauseMenu;
    public GameObject Expedition;

    public List<GameObject> Backgrounds;
    public List<TutorialPage> Tutorial;
    public GameObject TutorialObject;
    public Text TutorialText;
    public GameObject Arrow;
    public int Slide = 0;

    public Text ManpowerText;
    public Text SuppliesText;
    public Text GoldText;
    public Text TrophyText;
    public Text PrestigeText;
    public Text LoreText;

    //ManpowerBonusInfo
    public Text ManpowerMuscle;
    public Text ManpowerCook;
    public Text ManpowerScolar;
    public Text ManpowerTracker;

    public Text Strength;
    public Text MealBonus;
    public Text LorePlusText;
    public Text HintTrackerText;


    public Text StepText;
    public Text BackConsumeTotal;

    //public Text Thrill;
    public Image ThrillMeter;

    public Image Objective;
    public Image HintMeter;

    public Animator ManpowerMenuAnim;

    int ManpowerSave;
    int SuppliesSave;
    int GoldSave;
    int PrestigeSave;
    int LoreSave;
    int TrophySave;
    int ThrillSave;
    int HintSave;

    //Diary
    public GameObject Diary;
    bool DiaryOpen = false;
    public GameObject DiaryActiveEffectsOBJ;
    public GameObject DiaryPassiveEffectsOBJ;
    public Text DiaryActiveEffects;
    public Text DiaryPassiveEffects;
    public List<Image> DiaryItems;
    public Text DiaryItemName;
    public Text DiaryItemDescription;
    public List<GameObject> KeywordSlides;
    public List<GameObject> ResourceSlides;
    public ScrollRect ScrollList;
    bool KeywordsOpen;

    public void InspectItem(int num)
    {
        if (num<playerData.EquippedItems.Count&&playerData.EquippedItems[num].ID!=0)
        {
           
            Item MyItem = playerData.EquippedItems[num];

            DiaryItemName.text = MyItem.name;
            DiaryItemDescription.text = MyItem.itemDescription;
            if ( MyItem.DescriptionActive != "" || MyItem.ActiveB != null || MyItem.ActiveR != null)
            {
                DiaryActiveEffectsOBJ.SetActive(true);
                DiaryActiveEffects.text = MyItem.DescriptionActive;
            }
            else
            {
      
                DiaryActiveEffectsOBJ.SetActive(false);
            }

            if (MyItem.DescriptionPassive != "" || MyItem.PassiveB != null || MyItem.PassiveR != null)
            {
                DiaryPassiveEffectsOBJ.SetActive(true);
                DiaryPassiveEffects.text = MyItem.DescriptionPassive;
            }
            else
            {
     

                DiaryPassiveEffectsOBJ.SetActive(false);
            }
        }
        else
        {
            DiaryItemName.text = "None";
            DiaryItemDescription.text = "This item slot is currently empty.";
            DiaryPassiveEffectsOBJ.SetActive(false);
            DiaryActiveEffectsOBJ.SetActive(false);
        }
    }

    public void OpenDiaryDictionary(bool Keywords)
    {
        if (Keywords!=KeywordsOpen)
        {
            ScrollList.verticalScrollbar.value = 1;
            if (Keywords)
            {
                KeywordsOpen = true;
                
                foreach (GameObject Slide in KeywordSlides)
                {
                    Slide.SetActive(true);
                }
                foreach (GameObject Slide in ResourceSlides)
                {
                    Slide.SetActive(false);
                }
            }
            else
            {
                KeywordsOpen = false;
                foreach (GameObject Slide in KeywordSlides)
                {
                    Slide.SetActive(false);
                }
                foreach (GameObject Slide in ResourceSlides)
                {
                    Slide.SetActive(true);
                }
            }
        }
    }
    public void OpenDiary()
    {
        if (DiaryOpen)
        {
            DiaryOpen = false;
            OpenDiaryDictionary(false);
            Diary.SetActive(DiaryOpen);

        }
        else
        {
            DiaryOpen = true;
            Diary.SetActive(DiaryOpen);
            OpenDiaryDictionary(false);
        }
    }
    

    public List<Image> Items = new List<Image>();
    public List<GameObject> ItemAmmo = new List<GameObject>();
    public List<Text> ItemAmmoText = new List<Text>();
    public bool ItemNeedUpdate=true;

    private void Start()
    {
        SetDefaults();
        RefreshResources();
        RefreshItems();
        CapitalismVibeCheck();
        ManageBackground(1, true);
        StepRefresh(0);
        if (!playerData.ExpeditionTutorial)
        {
            Arrow.gameObject.SetActive(true);
            InitiateTutorial();
            playerData.ExpeditionTutorial = true;
        }
        if (KeywordSlides[0].activeSelf)
        {
            KeywordsOpen = true;
        }
        if (Diary.activeSelf)
        {
            OpenDiary();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuTrigger();
        }
        RefreshResources();
        if (ItemNeedUpdate)
        {
            RefreshItems();
        }

    }
    public void PauseMenuTrigger()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Expedition.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
            Expedition.SetActive(false);
        }
    }
    
    public void RefreshItems()
    {
        int i = 0;
        foreach (var item in Items)
        {
            if (i<playerData.EquippedItems.Count)
            {
                if (playerData.EquippedItems[i].name != "None")
                {
                    item.color = Color.white;
                    item.sprite = playerData.EquippedItems[i].Art;
                    if (playerData.EquippedItems[i].MaxShots != 0)
                    {
                        ItemAmmo[i].SetActive(true);
                        ItemAmmoText[i].text = "" + (playerData.EquippedItems[i].MaxShots - playerData.EquippedItems[i].Shots);
                    }

                }
                else
                {
                    item.sprite = null;
                    item.color = Color.clear;
                    ItemAmmo[i].SetActive(false);
                }
            }
            else
            {
                item.sprite = null;
                item.color = Color.clear;
                ItemAmmo[i].SetActive(false);
            }
            i++;

        }
        i = 0;
        foreach (var item in DiaryItems)
        {
            if (i < playerData.EquippedItems.Count)
            {
                if (playerData.EquippedItems[i].name != "None")
                {
                    item.color = Color.white;
                    item.sprite = playerData.EquippedItems[i].Art;
                }
                else
                {
                    item.sprite = null;
                    item.color = Color.clear;
                }
            }
            else
            {
                item.sprite = null;
                item.color = Color.clear;
            }
            i++;

        }
        RefreshResources();
        ItemNeedUpdate = false;
    }
    public void StepRefresh(int num)
    {
        StepText.text = "" + num;

    }
    public void BackConsumeTotalRefresh(int ConsequenceCount)
    {
        if (ConsequenceCount%2==1)
        {
            ConsequenceCount += 1;
        }
        BackConsumeTotal.text = "" + (playerData.getManpower()*2*(ConsequenceCount/2));

    }
    private void RefreshResources()
    {
        ManpowerText.text = "" + playerData.getManpower();
        SuppliesText.text = "" + playerData.Supplies;
        GoldText.text = "" + playerData.Gold;
        PrestigeText.text = "+" + playerData.Convert();
        LoreText.text = "" + playerData.Lore+"(+" + playerData.ManpowerScolarBonus + ")";
        TrophyText.text = "" + playerData.Trophy;

        ManpowerMuscle.text = "" + playerData.Manpower[0];
        ManpowerCook.text = "" + playerData.Manpower[3];
        ManpowerScolar.text = "" + playerData.Manpower[1];
        ManpowerTracker.text = "" + playerData.Manpower[2];

            
        Strength.text = "" + (playerData.Manpower[0] + playerData.Manpower[2] + playerData.BaseStrengthBonus + playerData.StrengthBoost-1);
        MealBonus.text = "" + playerData.ManpowerCookBonus;
        LorePlusText.text = "+" + playerData.ManpowerScolarBonus;
        HintTrackerText.text = "" + (-75 + playerData.ManpowerTrackerBonus+playerData.ItemHintCutBoost);

        ThrillMeter.fillAmount = (float)playerData.Thrill * (float)0.1;
        HintMeter.fillAmount = (float)playerData.Hint * (float)0.01;

        PopUpResources();
    }
    void SetDefaults()
    {
        ManpowerSave=playerData.getManpower();
        SuppliesSave=playerData.Supplies;
        GoldSave=playerData.Gold;
        PrestigeSave=playerData.Prestige;
        LoreSave=playerData.Lore;
        TrophySave=playerData.Trophy;
        ThrillSave=playerData.Thrill;
        HintSave=playerData.Hint;
    }
    public void PopUpResources()
    {
        int num;
        if (ManpowerSave != playerData.getManpower())
        {
            num = playerData.getManpower() - ManpowerSave;
            if (num > 0)
            {
                Anim.SetTrigger("Manpower");
            }
            else
            {
                Anim.SetTrigger("!Manpower");
            }
            ManpowerSave = playerData.getManpower();

        }
   
        if (SuppliesSave != playerData.Supplies)
        {

            num = playerData.Supplies - SuppliesSave;
            if (num > 0)
            {
                Anim.SetTrigger("Supplies");
            }
            else
            {
                Anim.SetTrigger("!Supplies");
            }
            SuppliesSave = playerData.Supplies;


        }
    
        if (GoldSave != playerData.Gold)
        {
            num = playerData.Gold - GoldSave;
            if (num > 0)
            {
                Anim.SetTrigger("Gold");
            }
            else
            {
                Anim.SetTrigger("!Gold");
            }
            GoldSave = playerData.Gold;


        }
   
        if (TrophySave != playerData.Trophy)
        {
            num = playerData.Trophy - TrophySave;
            if (num > 0)
            {
                Anim.SetTrigger("Trophy");
            }
            else
            {
                Anim.SetTrigger("!Trophy");
            }

            TrophySave = playerData.Trophy;

        }
      
        if (ThrillSave != playerData.Thrill)
        {
            Anim.SetTrigger("Thrill");
            ThrillSave = playerData.Thrill;
        }
        if (LoreSave != playerData.Lore)
        {
            num = playerData.Lore - LoreSave;
            if (num > 0)
            {
                Anim.SetTrigger("Lore");
            }
            else
            {
                Anim.SetTrigger("!Lore");
            }

            LoreSave = playerData.Lore;

        }

        if (PrestigeSave != playerData.Prestige)
        {
            num = playerData.Prestige - PrestigeSave;
            if (num > 0)
            {
                Anim.SetTrigger("Prestige");
            }
            else
            {
                Anim.SetTrigger("!Prestige");
            }
            PrestigeSave = playerData.Prestige;


        }

        if (HintSave != playerData.Hint)
        {
            num = playerData.Hint - HintSave;
            if (num > 0)
            {
                Anim.SetTrigger("Hint");
            }
            else
            {
                Anim.SetTrigger("!Hint");
            }
            HintSave = playerData.Hint;

        }



    }

    bool ManpowerMenuIsOpen;
    public void ToggleManpowerMenu()
    {
        if (ManpowerMenuIsOpen)
        {
            ManpowerMenuAnim.SetTrigger("Close");
            ManpowerMenuIsOpen = false;
        }
        else
        {
            ManpowerMenuAnim.SetTrigger("Open");
            ManpowerMenuIsOpen = true;
        }
    }

    public void ManageBackground(int Thrill, bool Event)
    {
        if (Event)
        {
            ToggleScreen(-1);
        }
        else if (Thrill <= 1)
        {
            ToggleScreen(0);
        }
        else if (Thrill <= 4)
        {
            ToggleScreen(1);
        }
        else if (Thrill <= 7)
        {
            ToggleScreen(2);
        }
        else if (Thrill <= 9)
        {
            ToggleScreen(3);
        }
        else if (Thrill <= 10)
        {
            ToggleScreen(4);
        }
    }

    public void ToggleScreen(int num)//-1 - Event; 0 - base; 1++ Areas
    {
        int i = 0;
        foreach (var item in Backgrounds)
        {
            if (i == num + 1)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
            i++;
        }
    }

    public void InitiateTutorial()
    {
        Slide = 0;
        TutorialObject.SetActive(true);
        OpenTutorialSlide(Slide);
    }
    public void ChangeTutorialSlide(bool Direction)
    {
        if (Direction&&Slide>=Tutorial.Count-1)
        {
            Arrow.SetActive(false);
            TutorialObject.SetActive(false);
            Debug.Log("Tutorial Finished");
        }
        else if (!Direction && Slide != 0)
        {
            OpenTutorialSlide(Slide - 1);
            Debug.Log("Previous Slide");
        }
        else if (Direction && Slide < Tutorial.Count)
        {
            OpenTutorialSlide(Slide+1);
            Debug.Log("Next Slide");
        }
        
    }
    public void OpenTutorialSlide(int num)
    {
        Slide = num;
        Debug.Log("Open Slide "+num);
        TutorialPage Page = Tutorial[num];
        TutorialText.text = Page.text;
        Arrow.transform.localPosition = Page.ArrowPosition;
        Arrow.transform.rotation=Quaternion.identity;
        Arrow.transform.Rotate(Page.ArrowRotation);
    }

    //Capitalism
    public void CapitalismVibeCheck()
    {
        if (playerData.Convert() < playerData.ActiveContracts[0].Funds * 2)
        {
            playerData.ActiveContracts[0].Debt = true;
        }
        else
        {
            playerData.ActiveContracts[0].Debt = false;
        }
    }
}
