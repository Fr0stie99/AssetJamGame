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
        bullet.GetComponent<PlayerBullet>().SetAttributes(damage, gameObject);
        bullet.transform.rotation = transform.GetChild(0).rotation;
        
    }

    
}
