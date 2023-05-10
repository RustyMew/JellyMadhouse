using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentMicrogame = 0;
	public TextMeshProUGUI microNumber;
	public TextMeshProUGUI timeLeft;
	public AudioSource audio;
	public AudioClip start;
	public AudioClip end;
	public AudioClip correct;
	public AudioClip wrong;
	public GameObject betweenGames;
	public float gameTime;
	public static bool gameIsGoing;
	public bool gameIsWon;
	public bool game1IsGoing;
	[SerializeField] private int selectedGame;
	[SerializeField] private TextMeshProUGUI commandText;
	
	public GameObject placeYourPets;
	public static int pets;	
	public GameObject SadSlime;
	public bool isHappy;
	
	void Start()
	{
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
		
		if(game1IsGoing)
		{
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
			StartCoroutine(EndGameFail());
		}
	}
	
	void OnMouseDown()
	{
		Debug.Log("Click");
	}
	
	IEnumerator NextGame()
	{
		selectedGame = 1;
		
		if(selectedGame == 1)
		{
		commandText.text = "Pet!";
		currentMicrogame += 1;
		audio.PlayOneShot(start);
		yield return new WaitForSeconds(2.5f);
		betweenGames.SetActive(false);
		placeYourPets.SetActive(true);
		gameTime = 5.0f;
		game1IsGoing = true;
		gameIsGoing = true;
		commandText.text = "";
		}
	}
	
	IEnumerator EndGameFail()
	{
		if(selectedGame == 1)
		{
			game1IsGoing = false;
			placeYourPets.SetActive(false);
		}
		
		gameIsGoing = false;
		betweenGames.SetActive(true);
		timeLeft.text = "";
		commandText.text = "Too bad...";
		audio.PlayOneShot(end);
		audio.PlayOneShot(wrong);
		yield return new WaitForSeconds(2);
		StartCoroutine(NextGame());
	}
}
