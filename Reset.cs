using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public PlayerData playerData;
    public DataBase dataBase;

    public void ResetGame()
    {
        playerData.PlayerName ="Arthur Delayne";
        playerData.Prestige = 0;
        playerData.Lore = 0;
        playerData.LoreSave = 0;
        playerData.BaseStrengthBonus = 0;
        playerData.Intro = false;
        playerData.ExpeditionTutorial = false;
        playerData.MuseumTutorial = false;
        for (int x = 0; x < playerData.AreasVisited.Count; x++)
        {
            playerData.AreasVisited[x] = false;
        }
        playerData.IsMonk = false;
        playerData.HadVision = false;
        playerData.MeanToAluxCounter = 0;

        for (int c = 0; c < dataBase.PlotPointsDone.Length; c++)
        {
            dataBase.PlotPointsDone[c] = false;
        }
        int i = 0;
        foreach (Contract con in dataBase.Contracts)
        {
            if (i < dataBase.Contracts.Count)
            {
                con.goal.Done = false;
                con.goal.checksTotal = 0;
            }
            i++;
        }
        i = 0;
        foreach (Item item in dataBase.Items)
        {
            item.Active = false;
            i++;
        }
        //Save
        //SaveFile save = new SaveFile(playerData, dataBase);

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/Save.Nobu"))
        {
            File.Delete(Application.persistentDataPath + "/Save.Nobu");
        }
        //FileStream file = File.Create(Application.persistentDataPath + "/Save.Nobu");
        //bf.Serialize(file, save);
        //file.Close();
    }
}
