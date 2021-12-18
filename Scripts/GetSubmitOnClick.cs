using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSubmitOnClick : MonoBehaviour
{
    public Button btnClick;

    public InputField inputUser;

    [SerializeField]
    GameObject submitButton;

    [SerializeField]
    GameObject nameForScore;

    /*need to reference these scripts in a different way
     */
    public GameControl game;
    public HighscoreHandler accessScore;


    private void Start()
    {
        btnClick.onClick.AddListener(GetInputonClickHandler);
    }
    /*
    private void Update()
    {
        //if game has restarted then reset
        if (game.isGameStopped())
        {
            alreadySubmitted = false;
        }
    }
    */
    public void GetInputonClickHandler()
    {
        //add a boolean where if already press submit can't press it again, until new game starts
        accessScore.PostScores(inputUser.text, game.getScore());
        Debug.Log("Log Input" +inputUser.text);
        inputUser.Select();
        inputUser.text = "";
        submitButton.SetActive(false);
        nameForScore.SetActive(false);
    }
}
