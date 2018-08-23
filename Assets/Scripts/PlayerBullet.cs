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
    float distance;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        /*
        distance = transform.position.x - GameObject.Find("Player").transform.position.x;

        if (distance > 0)
        {
            rb = GetComponent<Rigidbody2D>();
            firePoint = GameObject.Find("Weapon1").transform.GetChild(0);
            store = firePoint;
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
            firePoint = GameObject.Find("Weapon2").transform.GetChild(0);
            store = firePoint;
        }
        */
       
	}
	
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

    public void SetAttributes(float damage, GameObject weapon)
    {
        firePoint = weapon.transform.GetChild(0);
        this.damage = damage;
    }
}

