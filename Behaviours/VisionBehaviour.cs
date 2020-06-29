using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour")]
public class VisionBehaviour : CardBehaviours
{
    public bool VisionSwitch = false;
    public override void Execute()
    {
        GameObject.Find("ExpeditionManager").GetComponent<Manager>().InVision=VisionSwitch;
    }
}
