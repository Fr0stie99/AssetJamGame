using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class PlayerBullet : MonoBehaviour {

    float damage;
    public float projSpeed = 5f;
    Rigidbody2D rb;
    Transform firePoint;
    Transform store;
    float distance;
    PlayerID _id;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
            rb.velocity = transform.right * projSpeed;
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    //Destroy projectile upon collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().HurtMe(damage);
            
        }
        Destroy(gameObject);
    }

    public void SetAttributes(float damage, float bulletSpeed, PlayerID id)
    {
        this.damage = damage;
        projSpeed = bulletSpeed;
        this._id = id;
    }


}


