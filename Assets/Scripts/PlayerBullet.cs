using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class PlayerBullet : MonoBehaviour, PlayerPushable {

    float damage;
    public float projSpeed = 5f;
    Rigidbody2D rb;
    Transform firePoint;
    Transform store;
    float distance;
    PlayerID _id;

    Vector3 contactPoint;
    float pushback;
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
            contactPoint = -rb.velocity.normalized;
            Debug.DrawLine(collision.transform.position, transform.position, Color.white, 30f);
            collision.gameObject.GetComponent<PlayerHealth>().HurtMe(damage);
            collision.gameObject.GetComponent<PlayerController>().ApplyRecoil(this);
            
        }
        Destroy(gameObject);
    }

    public void SetAttributes(float damage, float bulletSpeed, float pushback, PlayerID id)
    {
        this.damage = damage;
        projSpeed = bulletSpeed;
        this.pushback = pushback;
        this._id = id;
    }

    public float GetPushback()
    {
        return pushback;
    }

    public Vector3 GetContactPoint()
    {
        return contactPoint;
    } 

    public void SetContactPoint(Vector3 contactPoint)
    {
        this.contactPoint = contactPoint;
    }

}


