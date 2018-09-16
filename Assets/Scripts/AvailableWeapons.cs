using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableWeapons : MonoBehaviour {
    public GameObject[] weapons;
    // Use this for initialization
    PlayerData player1, player2;
    void Awake()
    {
        player1 = new PlayerData(weapons[0]);
        player2 = new PlayerData(weapons[0]);
    }
}



