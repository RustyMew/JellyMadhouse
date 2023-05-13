using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCursor : MonoBehaviour
{
    public Sprite[] cursorSprite;
	public GameObject rice;
	
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private int faceIndex;
	
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		float randomX = Random.Range(32f, 1050f);
		float randomY = Random.Range(33f, 480f);
		Vector3 newPosition = new Vector3(randomX, randomY, rice.transform.position.z);
		rice.transform.position = newPosition;
	}
	
    void Update()
    {
        if(GameManager.game3IsGoing)
		{
			Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = cursorPosition;
		}
		
		if(GameManager.game3IsGoing && !GameManager.gameIsWon)
		{
			rice.SetActive(true);
		}
		
		if(GameManager.gameIsWon)
		{
			faceIndex = 1;
		}
		else
		{
			faceIndex = 0;
		}
		
		spriteRenderer.sprite = cursorSprite[faceIndex];
    }
	
	public void eaten()
	{
		rice.SetActive(false);
		GameManager.gameIsWon = true;
	}
}
