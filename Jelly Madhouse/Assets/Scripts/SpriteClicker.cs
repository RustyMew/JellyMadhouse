using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteClicker : MonoBehaviour
{
    void OnMouseDown()
	{
		GameManager.pets++;
	}
}
