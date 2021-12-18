using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftScenes : MonoBehaviour
{
  
    public void skinMenu()
    {
        SceneManager.LoadScene("SkinMenu");
    }
    public void mainMenu()
    { 
        SceneManager.LoadScene("MainMenu");
    }

    public void leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void gameScene()
    {
        SceneManager.LoadScene("Game");
    }

}
