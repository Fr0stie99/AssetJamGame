using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerBullet : MonoBehaviour, PlayerPushable {

    public float damage = 30f;
    public float projSpeed = 5f;
    public float pushback = 40f;
    Rigidbody2D rb;
    PlayerID _id;
    Animator anim;

    Vector3 contactPoint;
    Collider2D bulletCollider;
    
    bool isDead = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletCollider = GetComponent<Collider2D>();
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
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.name == "Killbox")
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>()._playerID != _id)
        {
            contactPoint = -rb.velocity.normalized;
            collision.gameObject.GetComponent<PlayerHealth>().HurtMe(damage);
            collision.gameObject.GetComponent<PlayerController>().ApplyRecoil(this);
            bulletCollider.enabled = false;
            
            
        }
        rb.velocity = Vector2.zero;
        isDead = true;
        anim.Play("explosion");
    }

    public void SetAttributes(PlayerID id)
    {
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


