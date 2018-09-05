using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

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
    PlayerPushable storeWeapon;

    float horizontal, shoot1Timer = 0f, shoot2Timer = 0f, shoot1Threshold, shoot2Threshold, recoilForce =0f, rotSpeed, linearRot;

    public float speed, buttonThreshold, angleSpeed = 360f;

    bool hasCharged, recoilOn;
	// Use this for initialization
	void Awake ()
    {
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

        if (InputManager.GetButtonUp("Shoot1", _playerID))
        {
            Fire(weapon1);
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

            RotateWeapon(weapon1, true);
        }

        shoot1Timer += Time.fixedDeltaTime;

        if (InputManager.GetButtonUp("Shoot2", _playerID))
        {
            Fire(weapon2);

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

    }

    private void Fire(ProjectileWeapon weapon)
    {
        linearRot = 0f;
        weapon.Fire();
    }

    //recoil/pushback handling
    void FixedUpdate()
    {
        if (recoilOn && recoilForce > 0)
        {
            rb2D.velocity = -(storeWeapon.GetContactPoint() * recoilForce);
            recoilForce -= recoilForce;
        }
        else {
            if (recoilForce <= 0)
            {
                recoilOn = false;
                recoilForce = storeWeapon.GetPushback();
            }
        }


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

    public void ApplyRecoil(PlayerPushable forceSource)
    {
        recoilOn = true;
        storeWeapon = forceSource;
    }


}
