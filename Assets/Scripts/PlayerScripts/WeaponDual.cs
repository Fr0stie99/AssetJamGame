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
    public Transform firePoint1;
    public Transform firePoint2;

    // Use this for initialization
    void Awake()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //controls the shooting
        if (InputManager.GetButtonUp("Shoot2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Quaternion firePointRot1 = Quaternion.Euler(firePoint1.rotation.x, firePoint1.rotation.y, firePoint1.rotation.z);
            bulletPos = new Vector2(firePoint1.position.x, firePoint1.position.y);

            //instantiate the projectile
            GameObject bullet = Instantiate (playerBullet, bulletPos, firePointRot1);
            //bullet.transform.parent = GameObject.Find("FirePoint").transform;
            Debug.Log("shoot 2");
           
        }
        if(InputManager.GetButtonUp("Shoot1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Quaternion firePointRot2 = Quaternion.Euler(-firePoint2.rotation.x, firePoint2.rotation.y, firePoint2.rotation.z);
            bulletPos = new Vector2(firePoint2.position.x, firePoint2.position.y);

            //instantiate the projectile
            GameObject bullet = Instantiate(playerBullet, bulletPos, firePointRot2);
            //bullet.transform.parent = GameObject.Find("FirePoint").transform;

            Debug.Log("shoot 1");

        }

    }
}
