using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    float damage;
    public float projSpeed = 5f;
    Rigidbody2D rb;
    Transform firePoint;
    Transform store;
    float distance;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
            rb.velocity = transform.right * projSpeed;
	}


    //Destroy projectile upon collision
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().HurtMe(damage);
            
        }
        Destroy(gameObject);
    }

    public void SetAttributes(float damage, float bulletSpeed)
    {
        this.damage = damage;
        projSpeed = bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}


