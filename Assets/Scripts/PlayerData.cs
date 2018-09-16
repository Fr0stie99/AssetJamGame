using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public GameObject hand1;
    public GameObject hand2;

    public PlayerData(GameObject defaultWeapon)
    {
        hand1 = defaultWeapon;
        hand2 = defaultWeapon;
    }
}
