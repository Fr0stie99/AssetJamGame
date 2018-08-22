using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamUtility.IO.Examples;
using TeamUtility.IO;

public class weapon1 : MonoBehaviour
{
    [SerializeField]
    private PlayerID _playerID;

    public GameObject playerBullet;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    public Transform firePoint;

    // Use this for initialization
    void Awake()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetButtonUp("Shoot1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Quaternion firePointRot = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z);
            bulletPos = new Vector2(firePoint.position.x, firePoint.position.y);
            GameObject bullet = Instantiate (playerBullet, bulletPos, firePointRot);
            bullet.transform.parent = GameObject.Find("FirePoint").transform;
           
        }

    }
}
