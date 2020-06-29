using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New ContractorProfile")]
[Serializable]
public class Contractor : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;

}
