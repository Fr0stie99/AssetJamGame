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
    public Transform firePoint;
    public float damage;
    public float fireRate = 0.5f;
    Vector2 bulletPos;
    float nextFire = 0.0f;


    // Use this for initialization
    void Awake()
    {
        _playerID = GetComponentInParent<PlayerController>()._playerID;
    }

    // Update is called once per frame
    void Update()
    {
        //controls the shooting
        if (InputManager.GetButtonUp("Shoot1", _playerID) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Quaternion firePointRot = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z);
            bulletPos = new Vector2(firePoint.position.x, firePoint.position.y);

            //instantiate the projectile
            GameObject bullet = Instantiate (playerBullet, bulletPos, firePointRot);
            bullet.GetComponent<PlayerBullet>().SetDamage(damage);
            //bullet.transform.parent = GameObject.Find("FirePoint").transform;
           
        }

    }
}
