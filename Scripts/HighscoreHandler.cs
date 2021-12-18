using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.UI;

public class HighscoreHandler : MonoBehaviour
{
    private const string highscoreURL = "https://skiskipro.000webhostapp.com/highscore.php";
    private const int textWindow = 30;

    public Text changingText;
    public void RetrieveScores()
    {
        List<Score> scores = new List<Score>();
        StartCoroutine(DoRetrieveScores(scores));
    }

    public void PostScores(string name, int score)
    {
        StartCoroutine(DoPostScores(name, score));
    }

    IEnumerator DoRetrieveScores(List<Score> scores)
    {
        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            entry.score = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }
                        scores.Add(entry);
                    }
                }
            }
        }
        Debug.Log("Size of scores in Ienumerator: " + scores.Count);
        //could do something for each extra digit subtract a space, one digit subtract no spaces
        for (int i = 0; i < scores.Count; i++)
        {
            changingText.text += "\n" + (i + 1) + ". " + scores[i].name;
            for (int j = 0; j < textWindow - (scores[i].name.Length + scores[i].score.ToString().Length); j++)
            {
                changingText.text += " ";
            }
            changingText.text += scores[i].score;
        }
    }

    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }
}

public struct Score
{
    public string name;
    public int score;
}