using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float projSpeed = 5f;
    //public float velY = 0f;
    Rigidbody2D rb;
    Transform firePoint;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        firePoint = GameObject.Find("FirePoint").transform;
        
	}
	
	// Update is called once per frame
	void Update () {

        rb.velocity = firePoint.right * projSpeed;

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
}
