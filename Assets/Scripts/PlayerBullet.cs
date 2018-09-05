using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerBullet : MonoBehaviour, PlayerPushable {

    float damage;
    public float projSpeed = 5f;
    Rigidbody2D rb;
    Transform firePoint;
    Transform store;
    float distance;
    PlayerID _id;
    Animator anim;

    Vector3 contactPoint;
    float pushback;
    bool isDead = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate () {
       if (!isDead) { rb.velocity = transform.right * projSpeed; }
            
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
            collision.gameObject.GetComponent<PlayerHealth>().HurtMe(damage);
            collision.gameObject.GetComponent<PlayerController>().ApplyRecoil(this);
            
        }
        rb.velocity = Vector2.zero;
        isDead = true;
        anim.Play("explosion");
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


