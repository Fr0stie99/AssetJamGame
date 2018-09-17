using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

public class Shotgun : MonoBehaviour, PlayerPushable, Weapon
{

    public float rotationSpeed, recoil = 20f, cooldownRate = 0.2f;
    public GameObject playerBullet;
    Vector2 bulletPos;
    PlayerID id;
    PlayerController player;
    public GameObject canFire;
    bool hasToldPlayer = false;

    float nextFire = 0.0f;

    [HideInInspector]
    public Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("Barrel");

    }

    void Start()
    {
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
        GameObject[] Bullets = new GameObject[3];
        FindObjectOfType<AudioManager>().Play("Shotgun");

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i] = Instantiate(playerBullet, bulletPos, Quaternion.identity);
            Bullets[i].GetComponent<ShotgunBullet>().SetAttributes(id);
            Bullets[i].transform.rotation = transform.Find("Gun").rotation;
            Bullets[i].transform.rotation = Bullets[i].transform.rotation * Quaternion.Euler(0,0,-20 + i*20);
            player.ApplyRecoil(this);
        }

    }

    void Update()
    {

        nextFire += Time.fixedDeltaTime;
        if (nextFire >= cooldownRate && !hasToldPlayer && canFire != null)
        {
            Instantiate(canFire, firePoint.position, Quaternion.identity);
            hasToldPlayer = true;
        }
        else if (nextFire < cooldownRate)
        {
            hasToldPlayer = false;
        }
    }

    public float GetPushback()
    {
        return recoil;
    }

    public Vector3 GetContactPoint()
    {
        return transform.Find("Gun").right;
    }

}
