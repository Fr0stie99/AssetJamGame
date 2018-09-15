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
    Hand hand1, hand2;
    PlayerPushable storeWeapon;

    float horizontal, shoot1Timer = 0f, shoot2Timer = 0f, shoot1Threshold, shoot2Threshold, recoilForce =0f, rotSpeed, linearRot;

    public float angleSpeed = 360f;
    public float rotationSpeed = 5f;

    bool hasCharged, recoilOn;
	// Use this for initialization
	void Awake ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gm = GetComponent<GroundedManager>();
        health = GetComponent<PlayerHealth>();
        hand1 = transform.Find("Hand1").GetComponent<Hand>();
        hand2 = transform.Find("Hand2").GetComponent<Hand>();
       
        linearRot = 0f;
        
    }

    void Start()
    {
        storeWeapon = hand1.currentWeapon;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health.IsDead())
        {
            return;
        }

        if (InputManager.GetButtonUp("Shoot1", _playerID))
        {
            Fire(hand1);
        }
        else if (InputManager.GetButton("Shoot1", _playerID))
        {
            RotateWeapon(hand1, true);
        }

        shoot1Timer += Time.fixedDeltaTime;

        if (InputManager.GetButtonUp("Shoot2", _playerID))
        {
            Fire(hand2);

        }
        else if (InputManager.GetButton("Shoot2", _playerID))
        {
            RotateWeapon(hand2, false);

        }


        shoot2Timer += Time.fixedDeltaTime;

    }

    private void Fire(Hand weapon)
    {
        linearRot = 0f;
        weapon.Fire();
    }

    //recoil/pushback handling
    void FixedUpdate()
    {
        if (recoilOn && recoilForce > 0)
        {
            recoilForce = storeWeapon.GetPushback();
            rb2D.velocity = -(storeWeapon.GetContactPoint() * recoilForce);
            recoilForce -= recoilForce;
        }
        else {
            if (recoilForce <= 0)
            {
                recoilOn = false;
                recoilForce = 1;
            }
        }


    }


    /* Rotates a given weapon 360 degrees by however much that weapon is usually rotated by*/
    /* If isforward, goes clockwise*/
    void RotateWeapon(Hand weapon, bool isClockwise = true)
    {
        if (linearRot < 1)
        {
            linearRot += 0.01f;
        }
            //hacky code to change the direction based on whether it's clockwise or not
        float direction = isClockwise ? 1 : -1;
        rotSpeed = Mathf.Lerp(0, rotationSpeed, linearRot);

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
