using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    GameObject[] weapons;
    [HideInInspector]
    public Weapon currentWeapon;
	// Use this for initialization
	void Awake () {
        currentWeapon = transform.GetChild(0).GetComponent<Weapon>();
	}
	
	//TODO: join weapons up lol

    public void Fire()
    {
        currentWeapon.Fire();
    }

}
