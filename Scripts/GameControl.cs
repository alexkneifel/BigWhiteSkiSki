using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// this is where I need to make it so if it is the robot then continue playing
public class GameControl : MonoBehaviour
{
	public static GameControl instance = null;

	[SerializeField]
	GameObject restartButton;

	[SerializeField]
	GameObject mainMenuButton;

	[SerializeField]
	GameObject skinMenuButton;

	[SerializeField]
	GameObject submitButton;

	[SerializeField]
	GameObject nameForScore;
	
	[SerializeField]
	GameObject leaderboardButton;

	[SerializeField]
	Text highScoreText;

	[SerializeField]
	Text yourScoreText;

	[SerializeField]
	GameObject[] obstacles;

	[SerializeField]
	Transform spawnPointObstacle;

	[SerializeField]
	float spawnRate = 2f;
	float nextSpawn;

	[SerializeField]
	float timeToBoost = 5f;
	float nextBoost;

	int highScore = 0, yourScore = 0;

	public static bool gameStopped;

	float nextScoreIncrease = 0f;

	float spawnTranslation = 1f;

	public GameObject[] characterPrefabs;
	public Transform spawnPointChar;

	int selectedCharacter = 0;


	// Use this for initialization
	void Start()
	{

		selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
		GameObject prefab = characterPrefabs[selectedCharacter];
		GameObject clone = Instantiate(prefab, spawnPointChar.position, Quaternion.identity);

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		if (selectedCharacter == 3)
        {
			print("Robot selected");
        }


		leaderboardButton.SetActive(false);
		submitButton.SetActive(false);
		nameForScore.SetActive(false);
		restartButton.SetActive(false);
		mainMenuButton.SetActive(false);
		skinMenuButton.SetActive(false);
		yourScore = 0;
		gameStopped = false;
		Time.timeScale = 1f;
		highScore = PlayerPrefs.GetInt("highScore");
		nextSpawn = Time.time + spawnRate;
		nextBoost = Time.unscaledTime + timeToBoost;
	}

	// Update is called once per frame
	void Update()
	{
		if (!gameStopped)
			IncreaseYourScore();

		highScoreText.text = "High Score: " + highScore;
		yourScoreText.text = "Your Score: " + yourScore;

		//chooses at which time the obstacle will be spawned
		if (Time.time > nextSpawn)
			SpawnObstacle();

		//increases how fast the game is played,
		if (Time.unscaledTime > nextBoost && !gameStopped)
			BoostTime();
	}

	//called by obstacle script
	public void DinoHit()
	{
		//rewrites highscore to update
		if (yourScore > highScore)
			PlayerPrefs.SetInt("highScore", yourScore);
		Time.timeScale = 0;
		gameStopped = true;

		if (selectedCharacter != 3)
		{
			leaderboardButton.SetActive(true);
			submitButton.SetActive(true);
			nameForScore.SetActive(true);
			restartButton.SetActive(true);
			mainMenuButton.SetActive(true);
			skinMenuButton.SetActive(true);
		}
        else
        {
// Do I need to track the score I am getting?
			print("restarting game");
			this.RestartGame();
        }
	}

	void SpawnObstacle()
	{
		nextSpawn = Time.time + spawnRate;
		int randomObstacle = Random.Range(0, obstacles.Length);
        switch (randomObstacle)
        {
			case 0:
				spawnTranslation = 0.97f;
				break;
			case 1:
				spawnTranslation = 0.93f;
				break;
			default:
				spawnTranslation = 1f;
				break;

        }
		Instantiate(obstacles[randomObstacle], spawnPointObstacle.position * spawnTranslation, Quaternion.identity);
	}

	void BoostTime()
	{
		print("time scale increased");
		nextBoost = Time.unscaledTime + timeToBoost;
		Time.timeScale += 0.25f;
	}

	void IncreaseYourScore()
	{
		if (Time.unscaledTime > nextScoreIncrease)
		{
			yourScore += 1;
			nextScoreIncrease = Time.unscaledTime + 1;
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene("Game");
	}

	public float getTime()
    {
		print("time scale: " + (Time.timeScale));
		//print(Time.timeScale);
		return Time.timeScale;
    }
	public int getScore()
    {
		return yourScore;
    }

	public bool isGameStopped()
    {
		return gameStopped;
    }


}
