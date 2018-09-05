using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

public class ProjectileWeapon : MonoBehaviour {

    public float rotationSpeed, damage, recoil = 0f, recoilMax = 20f, bulletSpeed, fireRate = 0.5f, cooldownRate = 0.2f;
    public GameObject playerBullet;
    Vector2 bulletPos;

    float nextFire = 0.0f;

    [HideInInspector]
    public Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("Barrel");
    }

    public void Fire()
    {

        if (Time.fixedDeltaTime <= nextFire)
        {

            return;
        }
            
        bulletPos = new Vector2(firePoint.position.x, firePoint.position.y);

        //instantiate the projectile
        GameObject bullet = Instantiate(playerBullet, bulletPos, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().SetAttributes(damage, bulletSpeed);
        bullet.transform.rotation = transform.Find("Gun").rotation;
        
    }

    
}
