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
	public bool gameIsWon;
	public bool game1IsGoing;
	public static bool game2IsGoing;
	[SerializeField] private int selectedGame;
	[SerializeField] private TextMeshProUGUI commandText;
	
	public GameObject placeYourPets;
	public static int pets;	
	public GameObject SadSlime;
	public bool isHappy;
	
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
		if(gameIsGoing && gameTime <= 0.0f && !gameIsWon)
		{
			StartCoroutine(EndGameFail());
		}
		
		if(gameIsGoing && gameTime <= 0.0f && gameIsWon)
		{
			StartCoroutine(EndGameWin());
		}
	}
	
	IEnumerator NextGame()
	{
		selectedGame = 0;
		
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
		
		/* if(selectedGame == 1)
		{
		commandText.text = "Make 4!";
		currentMicrogame += 1;
		audio.PlayOneShot(start);
		yield return new WaitForSeconds(2);
		gameIsWon = false;
		game2IsGoing = true;
		gameIsGoing = true;
		commandText.text = "";
		SceneManager.LoadScene("JellyConnect4");
		} */
	}
	
	IEnumerator EndGameFail()
	{
		if(selectedGame == 0)
		{
			game1IsGoing = false;
			placeYourPets.SetActive(false);
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
