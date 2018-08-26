using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWinner : MonoBehaviour {
    string winnerName;
    Sprite winnerPic;

	// Use this for initialization
	void Awake () {
        GameObject God = GameObject.Find("God");
        if (God != null)
        {
            winnerName = God.GetComponent<GameStateManager>().winnerName;
            winnerPic = God.GetComponent<GameStateManager>().winnerPic;
        }
        Text text = transform.Find("Text").GetComponent<Text>();
        Image image = transform.Find("Image").GetComponent<Image>();
        if (winnerName != null)
        {
           text.text = winnerName;
           image.sprite = winnerPic;
        }
        else
        {
            text.text = "Nobody (ur all losers lol)";
        }
	}
	
	// Update is called once per frame

}
