using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class GoliathBehaviour : CardBehaviours
{
    public PlayerData playerData;
    public Card Attack;
    public Card Follow;

    public int probabilityAttack=20;
    public int probabilityAttackPlus=40;
    public int ManpowerValue = 5;
    public override void Execute()
    {
        int rand = Random.Range(0,100);
        if (playerData.Strength>=ManpowerValue && rand <= probabilityAttackPlus)
        {
            QueueAttack();
        }
        else if (rand <= probabilityAttack)
        {
            QueueAttack();
        }
        else
        {
            GameObject.Find("ExpeditionManager").GetComponent<Manager>().Queue.Add(Follow);
            GameObject.Find("ExpeditionManager").GetComponent<Manager>().NoAnimals = true;
        }
    }

    private void QueueAttack()
    {
        Manager manager=GameObject.Find("ExpeditionManager").GetComponent<Manager>();
            manager.Queue.Clear();
        manager.OneChoice = true;
        manager.Queue.Add(Attack);
    }
}
