using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject credits;
	public bool creditsIsShown;
	
    void Start()
    {
		creditsIsShown = false;
    }
	
	public void BeginGame()
	{
		SceneManager.LoadScene("Game");
	}
	
	public void ShowCredits()
	{
		if(creditsIsShown)
		{
			credits.SetActive(false);
			creditsIsShown = false;
		}
		else
		{
			credits.SetActive(true);
			creditsIsShown = true;
		}
	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
}