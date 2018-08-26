using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        sr.enabled = false;
        weapon1.enabled = false;
        weapon2.enabled = false;
        c.enabled = false;
        rb2D.bodyType = RigidbodyType2D.Static;
        dead = true;
        respawnTimer = 0f;
    }
    public void Respawn()
    {
        currentHealth = maxHealth;
        sr.enabled = true;
        weapon1.enabled = true;
        weapon2.enabled = true;
        c.enabled = true;
        dead = false;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        transform.position = spawns[Random.Range(0, spawns.Length)].transform.position; //picks a random spawn, gets its position, sets it
    }

    public bool IsDead()
    {
        return dead;
    }

    public void HurtMe(float damage)
    {
        currentHealth -= damage;
        //TODO: perhaps add pushback when damaged?
    }

    public bool NoLives()
    {
        return currentLives <= 0;
    }
}
