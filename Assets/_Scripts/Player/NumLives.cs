using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumLives : MonoBehaviour
{
    [SerializeField] 
    private int numLives;

    public void setLive(int newLive)
    {
        numLives = newLive;
    }

    public int getNumberLives()
    {
        return numLives;
    }
}
