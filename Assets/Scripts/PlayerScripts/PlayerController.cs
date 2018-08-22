using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

[RequireComponent(typeof(TrajectoryGenerator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundedManager))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private PlayerID _playerID;
    TrajectoryGenerator leapManager;
    Rigidbody2D rb2D;
    GroundedManager gm;
    PlayerHealth health;
    

    float horizontal, lookHorizontal, lookVertical;

    public float speed;

    bool hasCharged;
	// Use this for initialization
	void Awake ()
    {
        leapManager = GetComponent<TrajectoryGenerator>();
        rb2D = GetComponent<Rigidbody2D>();
        gm = GetComponent<GroundedManager>();
        health = GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health.IsDead())
        {
            return;
        }

        horizontal = InputManager.GetAxisRaw("Horizontal");

        if (gm.isGrounded())
            rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);

        if (InputManager.GetButton("Leap") && gm.isGrounded())
        {
            Charge();
        }
        else if (InputManager.GetButtonUp("Leap") && hasCharged)
        {
            leapManager.Launch();
            leapManager.ResetPower();
            hasCharged = false;
        }

        //grounded check

        

    }

    void FixedUpdate()
    {
        

        
    }

    void Charge()
    {
        //TODO: only call new trajectory if either value changes
        //TODO: change to use angle + power
        leapManager.IncreasePower();
        leapManager.UpdateAndDrawTrajectory(Camera.main.ScreenToWorldPoint(InputManager.mousePosition));
        hasCharged = true;
        
    }


}
