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
            attribs.winner = players[0].gameObject;
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
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Results");
            
        }
        Time.timeScale = 0.3f;
        endTimer += Time.fixedUnscaledDeltaTime;
        game.color = Color.Lerp(Color.clear, Color.grey, 1.5f);

    }

}
