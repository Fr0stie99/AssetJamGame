using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

public class ProjectileWeapon : MonoBehaviour {

    public float rotationSpeed, damage, recoil = 0f, recoilMax = 20f, bulletSpeed, fireRate = 0.5f, cooldownRate = 0.2f;
    public GameObject playerBullet;
    Vector2 bulletPos;
    PlayerID id;
    PlayerController player;

    float nextFire = 0.0f;

    [HideInInspector]
    public Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("Barrel");
        player = GetComponentInParent<PlayerController>();
        id = player._playerID;
    }

    public void Fire()
    {

        if (nextFire < cooldownRate)
        {

            return;
        }
        nextFire = 0.0f;
        bulletPos = new Vector2(firePoint.position.x, firePoint.position.y);

        //instantiate the projectile
        GameObject bullet = Instantiate(playerBullet, bulletPos, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().SetAttributes(damage, bulletSpeed, id);
        bullet.transform.rotation = transform.Find("Gun").rotation;
        player.setWeapon(this);
        
    }

    void Update()
    {
        nextFire += Time.fixedDeltaTime;
    }

    
}
