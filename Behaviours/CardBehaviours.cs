using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardBehaviours : ScriptableObject
{
    public virtual void Execute() { Debug.Log("No Behaviour"); }
    //public virtual bool Check(int Testvalue) { return true; }
}
