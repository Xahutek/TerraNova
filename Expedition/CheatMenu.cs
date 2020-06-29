using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    public PlayerData playerData;
    public DataBase dataBase;

    public InputField Thrill;
    public InputField Supplies;
    public InputField Specialist;
    public InputField Manpower;
    public InputField Hint;
    public InputField Lore;
    public InputField Prestiege;
    public InputField Plotpoint;

    bool x = false;

    public GameObject Me;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleMe(true);
        }
    }
    public void ToggleMe(bool on)
    {
        Me.SetActive(on);
    }

    public void SetThrill()
    {
        playerData.Thrill = int.Parse(Thrill.text);
        StartCoroutine(Clear());
    }

    public void SetLore()
    {
        playerData.Lore = int.Parse(Lore.text);
        
        StartCoroutine(Clear());
    }

    public void SetPrestige()
    {
        playerData.Prestige = int.Parse(Prestiege.text);
        StartCoroutine(Clear());
    }

    public void SetPlotPoint()
    {
        int num = int.Parse(Plotpoint.text);
        if (num != 2 && num != 3)
        {
            foreach (Contract con in dataBase.Contracts)
            {
                if (con.PlotPoint > 0)
                {
                    if (con.PlotPoint < num)
                    {
                        con.goal.Done = true;
                        con.goal.checksTotal = con.goal.checksNeeded;
                    }
                    else if (con.PlotPoint >= num)
                    {
                        con.goal.Done = false;
                        con.goal.checksTotal = 0;
                        if (con.PlotPoint == num)
                        {
                            playerData.ActiveContracts[0] = con;
                            Debug.Log("Plotpoint Contract Nr." + num + " " + playerData.ActiveContracts[0].Name);
                        }
                    }

                }
            }
        }
       
        dataBase.ManageContracts();
        StartCoroutine(Clear());
    }

    public void SetSupplies()
    {
        playerData.Supplies = int.Parse(Supplies.text);
        StartCoroutine(Clear());
    }
    public void SetManpower()
    {
        if (int.Parse(Specialist.text) >= 0 && int.Parse(Specialist.text) < 4 && int.Parse(Manpower.text)>-1)
        {
            playerData.Manpower[int.Parse(Specialist.text)] = int.Parse(Manpower.text);
            playerData.UpdateManpowerBoni();
            StartCoroutine(Clear());

        }
    }
    public void SetHint()
    {
        playerData.Hint = 0;
        playerData.AddHint (int.Parse(Hint.text));
        StartCoroutine(Clear());
    }

    IEnumerator Clear()
    {
        if (x)
        {
            yield return new WaitForSeconds(0f);

        }
        else
        {
            x = true;
            yield return new WaitForSeconds(1.5f);

            Thrill.text = null;
            Supplies.text = null;
            Hint.text = null;
            Specialist.text = null;
            Manpower.text = null;
            Plotpoint.text = null;
            Lore.text = null;
            Prestiege.text = null;

            x = false;
        }
       
    }
}
