using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldManager : MonoBehaviour {
    public float angleSpeed = 360f;
    public float gravityScale = 4f;
    public float linearDrag = 0f;
    public float angularDrag = 0.05f;
    public int maxLives = 3;
    public float maxHealth = 100f;
    public float maxRespawnTime = 3f;

    // Use this for initialization
    void Awake () {
		
	}
    void OnValidate()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>();
            rb2D.gravityScale = gravityScale;
            rb2D.drag = linearDrag;
            rb2D.angularDrag = angularDrag;
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            health.maxLives = maxLives;
            health.maxHealth = maxHealth;
            health.maxRespawnTime = maxRespawnTime;
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.angleSpeed = angleSpeed;

        }
    }
	
}
