using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWinner : MonoBehaviour {
    GameObject winner;

	// Use this for initialization
	void Awake () {
        GameObject God = GameObject.Find("God");
        if (God != null)
        {
            winner = God.GetComponent<GameStateManager>().winner;
        }
        Text text = transform.Find("Text").GetComponent<Text>();
        Image image = transform.Find("Image").GetComponent<Image>();
        if (winner != null)
        {
           text.text = winner.name;
           image.sprite = winner.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            text.text = "Nobody (ur all losers lol)";
        }
	}
	
	// Update is called once per frame

}
