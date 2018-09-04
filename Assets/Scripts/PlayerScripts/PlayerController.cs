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
    ProjectileWeapon weapon1, weapon2, storeWeapon;


    float horizontal, shoot1Timer = 0f, shoot2Timer = 0f, shoot1Threshold, shoot2Threshold, recoilForce, rotSpeed, linearRot;

    public float speed, buttonThreshold, angleSpeed = 360f;

    bool hasCharged, recoilOn;
	// Use this for initialization
	void Awake ()
    {
        leapManager = GetComponent<TrajectoryGenerator>();
        rb2D = GetComponent<Rigidbody2D>();
        gm = GetComponent<GroundedManager>();
        health = GetComponent<PlayerHealth>();
        weapon1 = transform.Find("Weapon1").GetComponent<ProjectileWeapon>();
        weapon2 = transform.Find("Weapon2").GetComponent<ProjectileWeapon>();
        shoot1Threshold = weapon1.cooldownRate;
        shoot2Threshold = weapon2.cooldownRate;
        storeWeapon = weapon1;
        linearRot = 0f;
        
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
            //if the cooldown time is passed, then you are able to shoot
            if (shoot1Timer > shoot1Threshold)
            {
                shoot1Timer = 0f;
                Fire(weapon1);
                linearRot = 0f;
            }
            else
            {
                //check if cooldown works
                Debug.Log("weap1 cooldown!");
            }
        }
        else if (InputManager.GetButton("Shoot1", _playerID))
        {
            //this enables to shoot while pressing down the button
            //commented out because it makes movement difficult

            /*if (shoot1Timer > shoot1Threshold)
            {
                Fire(weapon1);
                shoot1Timer = 0f;
                linearRot = 0f;
            }
            else
            {
                shoot1Timer += Time.fixedDeltaTime;
                RotateWeapon(weapon1, false);
                Debug.Log(shoot1Timer);

            }*/

            RotateWeapon(weapon1, false);
        }

        shoot1Timer += Time.fixedDeltaTime;

        if (InputManager.GetButtonUp("Shoot2", _playerID))
        {
            if (shoot2Timer > shoot2Threshold)
            {
                shoot2Timer = 0f;
                Fire(weapon2);
                linearRot = 0f;
            }
            else
            {
                //check if cooldown works
                Debug.Log("weap2 cooldown!");
            }

        }
        else if (InputManager.GetButton("Shoot2", _playerID))
        {
            /* if (shoot2Timer > shoot2Threshold)
             {
                 Fire(weapon2);
                 shoot2Timer = 0f;
                 linearRot = 0f;
             }
             else
             {
                 shoot2Timer += Time.fixedDeltaTime;
                 RotateWeapon(weapon2, false);
                 Debug.Log(shoot2Timer);
             }*/

            RotateWeapon(weapon2, false);

        }

        shoot2Timer += Time.fixedDeltaTime;

        Recoil(storeWeapon); 

        //grounded check



    }

    private void Fire(ProjectileWeapon weapon)
    { 

        weapon.Fire();
        recoilOn = true;
        storeWeapon = weapon;
    }

    //creates recoil based on rigidbody velocity
    private void Recoil(ProjectileWeapon weapon)
    {
        if (recoilOn && recoilForce > 0)
        {
            rb2D.velocity = -(storeWeapon.transform.Find("Gun").right * recoilForce);
            recoilForce -= recoilForce;
        }
        else {
            if(recoilForce <= 0)
            {
                recoilOn = false;
                recoilForce = weapon.recoilMax;
            }
        }
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


    /* Rotates a given weapon 360 degrees by however much that weapon is usually rotated by*/
    /* If isforward, goes clockwise*/
    void RotateWeapon(ProjectileWeapon weapon, bool isClockwise = true)
    {
        if (linearRot < 1)
        {
            linearRot += 0.01f;
        }
            //hacky code to change the direction based on whether it's clockwise or not
        float direction = isClockwise ? 1 : -1;
        rotSpeed = Mathf.Lerp(0, weapon.rotationSpeed, linearRot);

        float originalAngle = weapon.transform.localRotation.z;
        float smoothedAngle = Mathf.SmoothStep(originalAngle, originalAngle + angleSpeed, rotSpeed);
        weapon.transform.localRotation *= Quaternion.AngleAxis(direction*smoothedAngle*Time.deltaTime, Vector3.forward);

    }


}
