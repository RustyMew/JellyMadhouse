using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentMicrogame = 0;
	public int lives;
	public GameObject[] livesIcons; 
	public TextMeshProUGUI microNumber;
	public TextMeshProUGUI timeLeft;
	public AudioSource audio;
	public AudioClip start;
	public AudioClip end;
	public AudioClip endBad;
	public AudioClip gameover;
	public GameObject betweenGames;
	public float gameTime;
	public static bool gameIsGoing;
	public static bool gameIsWon;
	public bool game1IsGoing;
	public static bool game2IsGoing;
	[SerializeField] private int selectedGame;
	[SerializeField] private TextMeshProUGUI commandText;
	
	public GameObject placeYourPets;
	public static int pets;	
	public GameObject SadSlime;
	
	public GameObject trivia;
	public TextMeshProUGUI ask;
	public static bool incorrect;
	public static bool isCorrect;
	public GameObject button;
	public int question;
	
	void Start()
	{
		lives = 4;
		StartCoroutine(NextGame());
		game1IsGoing = false;
	}
	
	void Update()
	{
		if(currentMicrogame < 10)
		{
		microNumber.text = "00" + currentMicrogame.ToString();
		}
		else if(currentMicrogame >= 10)
		{
		microNumber.text = "0" + currentMicrogame.ToString();
		}
		else if(currentMicrogame > 99)
		{
		microNumber.text = currentMicrogame.ToString();
		}
		
		if(lives == 3)
		{
			livesIcons[3].SetActive(false);
		}
		
		if(lives == 2)
		{
			livesIcons[2].SetActive(false);
		}
		
		if(lives == 1)
		{
			livesIcons[1].SetActive(false);
		}
		
		if(lives == 0)
		{
			livesIcons[0].SetActive(false);
		}
		
		if(game1IsGoing)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				pets++;
				Debug.Log("pets: " + pets);
			}
			
			if(pets >= 5)
			{
				gameIsWon = true;
				Debug.Log("You did it!");
			}
		}
		
		if(gameIsGoing && gameTime > 0.0f)
		{
			gameTime -= Time.deltaTime;
			timeLeft.text = gameTime.ToString("f2");
		}
		
		if(!game2IsGoing)
		{
			if(gameIsGoing && gameTime <= 0.0f && !gameIsWon)
			{
				StartCoroutine(EndGameFail());
			}
		
			if(gameIsGoing && gameTime <= 0.0f && gameIsWon)
			{
				StartCoroutine(EndGameWin());
			}
		}
		
		if(game2IsGoing)
		{
			if(gameTime <= 0.0f && isCorrect && !gameIsWon)
			{
				StartCoroutine(EndGameFail());
			}
		
			if(gameTime <= 0.0f && isCorrect && gameIsWon)
			{
				StartCoroutine(EndGameWin());
			}
		
			if(gameTime <= 0.0f && !isCorrect && incorrect)
			{
				StartCoroutine(EndGameFail());
			}
		
			if(gameTime <= 0.0f && !isCorrect && !incorrect)
			{
				StartCoroutine(EndGameWin());
			}
		}
	}
	
	public void Game2Answer()
	{
		incorrect = false;
		question = Random.Range(0, 5);
		
		if(question == 0)
		{
			ask.text = "Is Pluto still considered a planet?";
			isCorrect = false;
		}
		
		if(question == 1)
		{
			ask.text = "Did Peter Piper pick a peck of pickled peppers?";
			isCorrect = true;
		}
		
		if(question == 2)
		{
			ask.text = "Did you pay money for this game?";
			isCorrect = false;
		}
		
		if(question == 3)
		{
			ask.text = "Chimken Nuggt?";
			isCorrect = true;
		}
		
		if(question == 4)
		{
			ask.text = "Is this statement false?";
			isCorrect = false;
		}
	}
	
	public void answered()
	{
		button.SetActive(false);
		
		if(game2IsGoing)
		{
			if(isCorrect)
			{
				gameIsWon = true;
			}
			
			if(!isCorrect)
			{
				incorrect = true;
			}
		}
	}
	
	IEnumerator NextGame()
	{
		selectedGame = Random.Range(0, 2);
		
		if(selectedGame == 0)
		{
		commandText.text = "Pet!";
		currentMicrogame += 1;
		audio.PlayOneShot(start);
		yield return new WaitForSeconds(2);
		betweenGames.SetActive(false);
		placeYourPets.SetActive(true);
		gameTime = 5.0f;
		pets = 0;
		gameIsWon = false;
		game1IsGoing = true;
		gameIsGoing = true;
		commandText.text = "";
		}
		
		if(selectedGame == 1)
		{
		commandText.text = "Click if Yes!";
		currentMicrogame += 1;
		audio.PlayOneShot(start);
		yield return new WaitForSeconds(2);
		betweenGames.SetActive(false);
		trivia.SetActive(true);
		button.SetActive(true);
		gameTime = 5.0f;
		Game2Answer();
		gameIsWon = false;
		game2IsGoing = true;
		gameIsGoing = true;
		commandText.text = "";
		}
	}
	
	IEnumerator EndGameFail()
	{
		if(selectedGame == 0)
		{
			game1IsGoing = false;
			placeYourPets.SetActive(false);
		}
		
		if(selectedGame == 1)
		{
			game2IsGoing = false;
			trivia.SetActive(false);
		}
		
		gameIsGoing = false;
		betweenGames.SetActive(true);
		timeLeft.text = "";
		commandText.text = "Too bad...";
		audio.PlayOneShot(endBad);
		yield return new WaitForSeconds(1.8f);
		lives--;
		
		if(lives > 0)
		{
		StartCoroutine(NextGame());
		}
		else if(lives == 0)
		{
		StartCoroutine(GameOver());	
		}
	}
	
	IEnumerator EndGameWin()
	{
		if(selectedGame == 0)
		{
			game1IsGoing = false;
			placeYourPets.SetActive(false);
		}
		
		if(selectedGame == 1)
		{
			game2IsGoing = false;
			trivia.SetActive(false);
		}
		
		gameIsGoing = false;
		betweenGames.SetActive(true);
		timeLeft.text = "";
		commandText.text = "OK!";
		audio.PlayOneShot(end);
		yield return new WaitForSeconds(1.7f);
		StartCoroutine(NextGame());
	}
	
	IEnumerator GameOver()
	{
		commandText.text = "Game Over";
		audio.PlayOneShot(gameover);
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Menu");
	}
}
