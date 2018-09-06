using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField]
    GameObject currentWeaponObject;
    [SerializeField]
    int weaponIndex;
    [HideInInspector]
    public Weapon currentWeapon;
	// Use this for initialization
	void Awake () {
        SwitchWeapon();
	}
	
	//TODO: join weapons up lol
    void SwitchWeapon()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GameObject weapon = Instantiate(currentWeaponObject);
        weapon.transform.SetParent(transform);
        currentWeapon = weapon.GetComponent<Weapon>();

    }
    public void Fire()
    {
        
        currentWeapon.Fire();
    }

}
