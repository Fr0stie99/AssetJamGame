using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class AvailableWeapons : MonoBehaviour {
    public GameObject[] weapons;
    // Use this for initialization
    Vector2Int player1, player2;
    void Awake()
    {
        player1 = new Vector2Int(0, 0);
        player2 = new Vector2Int(0, 0);
        
    }
    //0 if left weapon, 1 if right weapon
    public void ChangeWeaponP1(int index)
    {
        player1[index]++;
        player1[index] %= weapons.Length;
    }

    public void ChangeWeaponP2(int index)
    {
        player2[index]++;
        player2[index] %= weapons.Length;
    }

    public int GetWeaponIndex(HandType hand, PlayerID id)
    {
        if (id == PlayerID.One)
        {
            return player1[(int)hand];
        }
        else if (id == PlayerID.Two)
        {
            return player2[(int)hand];
        }
        else return 0;
    }





}



