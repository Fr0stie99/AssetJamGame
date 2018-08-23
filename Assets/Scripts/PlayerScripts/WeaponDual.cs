using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

public class WeaponDual : MonoBehaviour
{
    [SerializeField]
    private PlayerID _playerID;

    public GameObject playerBullet;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    public float playerRecoil = 500f;

    public Rigidbody2D rbPlayer;
    public Transform firePoint1;
    public Transform firePoint2;
    float identity = 0f;

    // Use this for initialization
    void Awake()
    {
        rbPlayer = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //controls the shooting
        if (InputManager.GetButtonUp("Shoot2") && Time.time > nextFire)
        {
            identity = 1;

            nextFire = Time.time + fireRate;

            //assign the position and rotation of the firepoint
            Quaternion firePointRot1 = Quaternion.Euler(firePoint1.rotation.x, firePoint1.rotation.y, firePoint1.rotation.z);
            bulletPos = new Vector2(firePoint1.position.x, firePoint1.position.y);

            //instantiate the projectile
            GameObject bullet = Instantiate (playerBullet, bulletPos, firePointRot1);
            //bullet.transform.parent = GameObject.Find("FirePoint").transform;
            

            Recoil(identity);

            Debug.Log("shoot 2");
           
        }
        if(InputManager.GetButtonUp("Shoot1") && Time.time > nextFire)
        {
            identity = 2;

            nextFire = Time.time + fireRate;

            //assign the position and rotation of the firepoint
            Quaternion firePointRot2 = Quaternion.Euler(-firePoint2.rotation.x, firePoint2.rotation.y, firePoint2.rotation.z);
            bulletPos = new Vector2(firePoint2.position.x, firePoint2.position.y);

            //instantiate the projectile
            GameObject bullet = Instantiate(playerBullet, bulletPos, firePointRot2);
            //bullet.transform.parent = GameObject.Find("FirePoint").transform;
            

            Recoil(identity);

            

        }

    }

    //this function will push the player whenever it shoots
    void Recoil(float identity)
    {
        if (identity == 1)
        {
            rbPlayer.AddForce(-(firePoint1.transform.right * playerRecoil));
        }
        else
        {
            rbPlayer.AddForce(firePoint2.transform.right * playerRecoil);
        }
    }
}
