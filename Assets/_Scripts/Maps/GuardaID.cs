using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaID : MonoBehaviour
{
    //public int ID { get; set; }

    
    public int ID;
    public void SetID(int ID)
    {
        this.ID = ID;
    }

    public int GetID()
    {
        return ID;
    }
    
}
