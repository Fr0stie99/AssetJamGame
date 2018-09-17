using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;
public enum HandType { Shoot1, Shoot2 }
public class Hand : MonoBehaviour {
    [SerializeField]
    GameObject currentWeaponObject;
    [SerializeField]
    int weaponIndex;
    [HideInInspector]
    public Weapon currentWeapon;

    PlayerID id;
    public HandType hand;
    

    AvailableWeapons weapons;
	// Use this for initialization
	void Awake () {
        //weaponIndex = weapons.GetWeaponIndex(hand, id);
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

    public void SetCurrentWeapon(GameObject weapon)
    {
        currentWeaponObject = weapon;
    }
}
