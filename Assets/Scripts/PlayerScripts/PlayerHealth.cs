using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealth : MonoBehaviour {

    public int maxLives = 3;
    public float maxHealth = 100f;
    public float maxRespawnTime = 3f;

    int currentLives;
    float currentHealth;
    float respawnTimer = 0f;
    bool dead;
    PlayerID id;

    GameObject[] spawns;
    SpriteRenderer sr, weapon1, weapon2;
    Collider2D c;
    Rigidbody2D rb2D;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        weapon1 = transform.Find("Weapon1").Find("Gun").GetComponent<SpriteRenderer>();
        weapon2 = transform.Find("Weapon2").Find("Gun").GetComponent<SpriteRenderer>();
        id = GetComponent<PlayerController>()._playerID;

    }

	// Use this for initialization
	void Start () {
        currentLives = maxLives;
        Respawn();
	}
	
	// Update is called once per frame
	void Update () {
        if (NoLives())
        {
            return;
        }
            

		if (currentHealth <= 0 && !IsDead())
        {
            Die();
        }

        if (IsDead() && respawnTimer < maxRespawnTime)
        {
            respawnTimer += Time.fixedDeltaTime;
        }
        else if (IsDead())
        {
            Respawn();
        }
	}

    void Die()
    {
        currentLives -= 1;
        c.enabled = false;
        SetVisibility(false);
        rb2D.bodyType = RigidbodyType2D.Static;
        dead = true;
        respawnTimer = 0f;
    }
    public void Respawn()
    {
        currentHealth = maxHealth;
        SetVisibility(true);
        c.enabled = true;
        dead = false;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        transform.position = spawns[Random.Range(0, spawns.Length)].transform.position; //picks a random spawn, gets its position, sets it
    }

    public void SetVisibility(bool isVisible)
    {
        sr.enabled = isVisible;
        weapon1.enabled = isVisible;
        weapon2.enabled = isVisible;
        
    }
    public bool IsDead()
    {
        return dead;
    }

    public void HurtMe(float damage)
    {
        
        currentHealth -= damage;
    }

    public bool NoLives()
    {
        return currentLives <= 0;
    }
}
