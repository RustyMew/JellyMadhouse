using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContestant : MonoBehaviour
{
    public Sprite[] expressions;
	
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private int faceIndex;
	
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update()
	{
		if(!GameManager.gameIsWon && !GameManager.incorrect)
		{
			faceIndex = 0;
		}
		else if(GameManager.gameIsWon)
		{
			faceIndex = 1;
		}
		
		else if(GameManager.incorrect)
		{
			faceIndex = 2;
		}
		
		spriteRenderer.sprite = expressions[faceIndex];
	}
}
