using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
   
    public void ChangeSceneGame()
    {
        SceneManager.LoadScene("TheGame");
    }
    
}
