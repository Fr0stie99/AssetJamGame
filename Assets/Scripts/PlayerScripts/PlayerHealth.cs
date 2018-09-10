﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
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
    SpriteRenderer[] renderers;
    Collider2D c;
    Rigidbody2D rb2D;
    Animator anim;

    [Header("Unity UI")]
    public Image healthBar;
    public Image[] Lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        id = GetComponent<PlayerController>()._playerID;
        anim = GetComponent<Animator>();

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
            respawnTimer += Time.deltaTime;
        }
        else if (IsDead())
        {
            Respawn();
        }

        if(currentLives > maxLives)
        {
            currentLives = maxLives;
        }

        for (int i = 0; i < Lives.Length; i++)
        {
            if (i < currentLives)
            {
                Lives[i].sprite = emptyLive;
            }
            else
            {
                Lives[i].sprite = fullLive;
            }

            if(i < maxLives)
            {
                Lives[i].enabled = true;
            }
            else
            {
                Lives[i].enabled = false;
            }
        }

        healthBar.fillAmount = currentHealth / maxHealth;
	}

    void Die()
    {
        Camera.main.GetComponent<TimeManager>().FreezeTime(Time.deltaTime);
        anim.Play("playerdead");
        FindObjectOfType<AudioManager>().Play("Ded");
        currentLives -= 1;
        c.enabled = false;
        
        rb2D.bodyType = RigidbodyType2D.Static;
        dead = true;
        respawnTimer = 0f;
    }
    public void Respawn()
    {
        anim.Play("playeridle");
        currentHealth = maxHealth;
        c.enabled = true;
        dead = false;
        SetVisibility(true);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        transform.position = spawns[Random.Range(0, spawns.Length)].transform.position; //picks a random spawn, gets its position, sets it
    }


    public void MakeInvisible()
    {
        SetVisibility(false);
    }
    private void SetVisibility(bool isVisible)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
        
    }
    public bool IsDead()
    {
        return dead;
    }

    public void HurtMe(float damage)
    {
        anim.Play("playerhurt");
        FindObjectOfType<AudioManager>().Play("Ow");
        currentHealth -= damage;
    }

    public bool NoLives()
    {
        return currentLives <= 0;
    }
}
