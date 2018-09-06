using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    List<PlayerHealth> players = new List<PlayerHealth>();
    GameStateManager attribs;
    public float maxEndTime = 4f;
    public Text game;
    float endTimer = 0f;
    bool gameOver = false;
    
	// Use this for initialization
	void Awake () {
        game.color = Color.clear;
        attribs = GameObject.Find("God").GetComponent<GameStateManager>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")){
            players.Add(obj.GetComponent<PlayerHealth>());
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
        {
            EndGame();
            return;
        }



		foreach (PlayerHealth player in players.ToArray())
        {
           
            if (player.NoLives())
            {
                players.Remove(player);
            }
        }
        if (players.Count == 1)
        {
            attribs.winnerName = players[0].gameObject.name;
            attribs.winnerPic = players[0].GetComponent<SpriteRenderer>().sprite;
            gameOver = true;

        } else if (players.Count < 1)
        {
            gameOver = true;
        }
	}

    void EndGame()
    {
        if (endTimer >= maxEndTime)
        {
            SetTimeScale(1f);
            SceneManager.LoadScene("Results");
            
        }
        SetTimeScale(0.3f);
        endTimer += Time.fixedUnscaledDeltaTime;
        game.color = Color.Lerp(Color.clear, Color.grey, 1.5f);

    }

    void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
        Time.fixedDeltaTime = timescale * 0.02f;
    }

}
