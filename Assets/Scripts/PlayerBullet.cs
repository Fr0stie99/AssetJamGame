using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float projSpeed = 5f;
    //public float velY = 0f;
    Rigidbody2D rb;
    Transform firePoint;
    Transform store;
    float distance;

    // Use this for initialization
    void Start () {
        distance = transform.position.x - GameObject.Find("Player").transform.position.x;
        Debug.Log(distance);

        if (distance > 0)
        {
            rb = GetComponent<Rigidbody2D>();
            firePoint = GameObject.Find("FirePoint1").transform;
            store = firePoint;
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
            firePoint = GameObject.Find("FirePoint2").transform;
            store = firePoint;
        }
        
       
	}
	
	// Update is called once per frame
	void Update () {
        
            rb.velocity = Mathf.Sign(distance)*(store.right * projSpeed);
       

	}


    //Destroy projectile upon collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

