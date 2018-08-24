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

    public PlayerID _playerID;
    TrajectoryGenerator leapManager;
    Rigidbody2D rb2D;
    GroundedManager gm;
    PlayerHealth health;
    ProjectileWeapon weapon1, weapon2;


    float horizontal, shoot1Timer = 0f, shoot2Timer = 0f, shoot1Threshold, shoot2Threshold;

    public float speed, buttonThreshold;

    bool hasCharged;
	// Use this for initialization
	void Awake ()
    {
        leapManager = GetComponent<TrajectoryGenerator>();
        rb2D = GetComponent<Rigidbody2D>();
        gm = GetComponent<GroundedManager>();
        health = GetComponent<PlayerHealth>();
        weapon1 = transform.Find("Weapon1").GetComponent<ProjectileWeapon>();
        weapon2 = transform.Find("Weapon2").GetComponent<ProjectileWeapon>();
        shoot1Threshold = buttonThreshold;
        shoot2Threshold = buttonThreshold;
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

        if (InputManager.GetButtonUp("Shoot1", _playerID))
        {
            shoot1Timer = 0f;
            Fire(weapon1);
        }
        else if (InputManager.GetButton("Shoot1", _playerID))
        {
            if (shoot1Timer < shoot1Threshold)
            {
                shoot1Timer += Time.fixedDeltaTime;
            }
            else
            {
                RotateWeapon(weapon1);
            }
            
        }

        if (InputManager.GetButtonUp("Shoot2", _playerID))
        {
            shoot2Timer = 0f;
            Fire(weapon2);
        }
        else if (InputManager.GetButton("Shoot2", _playerID))
        {
            if (shoot2Timer < shoot2Threshold)
            {
                shoot2Timer += Time.fixedDeltaTime;
            }
            else
            {
                RotateWeapon(weapon2, false);
            }
            
        }

          

        //grounded check



    }

    private void Fire(ProjectileWeapon weapon)
    { 

        weapon.Fire();
        ApplyRecoil(weapon);
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

    void ApplyRecoil(ProjectileWeapon weapon)
    {
        rb2D.AddForce(-(weapon.transform.GetChild(0).right * weapon.recoil));
    }

    /* Rotates a given weapon 360 degrees by however much that weapon is usually rotated by*/
    /* If isforward, goes clockwise*/
    void RotateWeapon(ProjectileWeapon weapon, bool isClockwise = true)
    {
            //hacky code to change the direction based on whether it's clockwise or not
        float direction = isClockwise ? 1 : -1;

        float originalAngle = weapon.transform.localRotation.z;
        float smoothedAngle = Mathf.SmoothStep(originalAngle, originalAngle + 360f, weapon.rotationSpeed);
        weapon.transform.localRotation *= Quaternion.AngleAxis(direction*smoothedAngle, Vector3.forward);
    }


}
