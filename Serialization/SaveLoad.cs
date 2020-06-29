using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "SaveLoad")]
public class SaveLoad :ScriptableObject
{
    
    public void Save(PlayerData playerData,DataBase dataBase)
    {

        SaveFile save = new SaveFile(playerData, dataBase);

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/Save.Nobu"))
        {
            File.Delete(Application.persistentDataPath + "/Save.Nobu");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/Save.Nobu");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Has been Saved");
    }

    public bool Load(PlayerData playerData, DataBase dataBase)
    {
        if (File.Exists(Application.persistentDataPath + "/Save.Nobu"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Save.Nobu", FileMode.Open);
            file.Position = 0;
            SaveFile save = (SaveFile)bf.Deserialize(file);


            save.LoadFile(playerData, dataBase);
            
            file.Close();

            Debug.Log("Load Game...");
            return true;

        }
        else
        {
            Debug.Log("No Save File Available");
            return false;
        }
    }
}
[Serializable]
public class SaveFile
{
    //playerData Stuff
    public string PlayerName;
    public int Prestige;
    public int Lore;
    public int BaseStrengthBonus;
    public bool Intro;
    public bool ExpeditionTutorial;
    public bool MuseumTutorial;

    public List<bool> AreasVisited;

    public bool IsMonk;
    public bool HadVision;
    public int MeanToAluxCounter;

    //DataBaseStuff
    public bool[] PlotPointsDone;
    public List<bool> ContractsDone = new List<bool>();
    public List<int> ChecksTotal = new List<int>();
    public List<bool> ItemsActive = new List<bool>();


    public SaveFile(PlayerData playerData, DataBase dataBase)
    {
        PlayerName = playerData.PlayerName;
        Prestige = playerData.Prestige;
        Lore = playerData.Lore;
        BaseStrengthBonus = playerData.BaseStrengthBonus;
        Intro = playerData.Intro;
        ExpeditionTutorial = playerData.ExpeditionTutorial;
        MuseumTutorial = playerData.MuseumTutorial;
        AreasVisited = playerData.AreasVisited;
        IsMonk = playerData.IsMonk;
        HadVision = playerData.HadVision;
        MeanToAluxCounter = playerData.MeanToAluxCounter;

        PlotPointsDone = dataBase.PlotPointsDone;
        foreach (Contract con in dataBase.Contracts)
        {
            ContractsDone.Add(con.goal.Done);
            ChecksTotal.Add(con.goal.checksTotal);
        }
        foreach (Item item in dataBase.Items)
        {
            ItemsActive.Add(item.Active);
        }
    }

    public void LoadFile(PlayerData playerData, DataBase dataBase)
    {
        
        playerData.PlayerName = PlayerName;
        playerData.Prestige = Prestige;
        playerData.Lore = Lore;
        playerData.LoreSave = Lore;
        playerData.BaseStrengthBonus = BaseStrengthBonus;
        playerData.Intro = Intro;
        playerData.ExpeditionTutorial = ExpeditionTutorial;
        playerData.MuseumTutorial = MuseumTutorial;
        playerData.AreasVisited = AreasVisited;
        playerData.IsMonk = IsMonk;
        playerData.HadVision = HadVision;
        playerData.MeanToAluxCounter = MeanToAluxCounter;



        dataBase.PlotPointsDone = PlotPointsDone;
        int i = 0;
        foreach (Contract con in dataBase.Contracts)
        {
            if (i < ContractsDone.Count)
            {
                con.goal.Done = ContractsDone[i];
                con.goal.checksTotal = ChecksTotal[i];
            }
            i++;
        }
        i = 0;
        foreach (Item item in dataBase.Items)
        {
            item.Active = ItemsActive[i];
            i++;
        }
    }

}