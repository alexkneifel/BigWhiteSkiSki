using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadLeaderboardText : MonoBehaviour
{
    public HighscoreHandler accessScore;


    void Start()
    {
        accessScore.RetrieveScores();
    }


}
