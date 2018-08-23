using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

public class ProjectileWeapon : MonoBehaviour {

    [SerializeField]
    private PlayerID _playerID;

    public float damage;
    public float recoil;
    public GameObject playerBullet;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    public Transform firePoint;

    void Awake()
    {
        firePoint = transform.GetChild(0);
    }

    public void Fire()
    {

        if (Time.fixedDeltaTime <= nextFire)
        {

            return;
        }
            

        Quaternion firePointRot = Quaternion.Euler(-firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z);
        bulletPos = new Vector2(firePoint.position.x, firePoint.position.y);

        //instantiate the projectile
        GameObject bullet = Instantiate(playerBullet, bulletPos, firePointRot);
    }
}
