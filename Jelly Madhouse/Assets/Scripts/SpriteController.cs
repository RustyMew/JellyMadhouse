using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
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
		if(GameManager.pets < 5)
		{
			faceIndex = 0;
		}
		else if (GameManager.pets >= 5 && GameManager.pets < 10)
		{
			faceIndex = 1;
		}
		else if (GameManager.pets >= 10)
		{
			faceIndex = 2;
		}
		
		spriteRenderer.sprite = expressions[faceIndex];
	}
}
