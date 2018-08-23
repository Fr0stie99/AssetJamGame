using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    float damage;
    public float projSpeed = 5f;
    //public float velY = 0f;
    Rigidbody2D rb;
    Transform firePoint;
    Transform store;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        firePoint = GameObject.Find("FirePoint").transform;
        store = firePoint;
	}
	
	// Update is called once per frame
	void Update () {

        rb.velocity = store.right * projSpeed;

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

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}

