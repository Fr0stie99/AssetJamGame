﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TeamUtility.IO;


public class WeaponSelectUI : MonoBehaviour {

    PlayerID _playerID, id;
    GameObject key;
    Graphic keyText;
    Image weaponSprite;
    Text weaponText;
    Color changeColor, initialColor;
    AvailableWeapons weapons;
    Object[] ReadyIcons;

    private void Start()
    {
        weapons = GameObject.Find("God").GetComponent<AvailableWeapons>();
        changeColor = Color.yellow;
        initialColor = Color.white;
        ReadyIcons = GameObject.FindGameObjectsWithTag("Ready");
    }
    private void Update()
    {

        //light up each hand in turn
        foreach (PlayerID id in System.Enum.GetValues(typeof(PlayerID)))
        {
            if ((int)id > 1) continue;
            Ready ready = FindCorrectReady(id);
            if (InputManager.GetButtonDown("Shoot1", id) && InputManager.GetButtonDown("Shoot2", id))
            {
                ready.ToggleReady();
            }
            if (ready.IsReady())
            {
                continue;
            }
            
            foreach (HandType hand in System.Enum.GetValues(typeof(HandType)))
            {
                if (InputManager.GetButtonDown(hand.ToString(), id))
                {
                    weapons.ChangeWeapon(id, (int)hand);
                    SetKeyAndSprite(id, hand);
                    keyText.color = changeColor;
                    GameObject weapon = weapons.weapons[weapons.GetWeaponIndex(hand, id)];
                    weaponSprite.sprite = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().sprite;
                    weaponSprite.color = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().color;
                    weaponText.text = weapon.name;
                }
                else if (InputManager.GetButtonUp(hand.ToString(), id))
                {

                    SetKeyAndSprite(id, hand);
                    keyText = key.GetComponent<Graphic>();
                    keyText.color = initialColor;
                }
            }

            
        }

        bool readyCheck = true;
        foreach (GameObject readyIcon in ReadyIcons)
        {
            readyCheck &= readyIcon.GetComponent<Ready>().IsReady();
        }
        if (readyCheck)
        {
            SceneManager.LoadScene("MediumMap");
        }
    }   
        
        //light up E
        

    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    

    void SetKeyAndSprite(PlayerID id, HandType hand)
    {
        GameObject playerHand = GameObject.Find(id.ToString() + hand.ToString());
        key = playerHand.transform.Find("Key").gameObject;
        keyText = key.GetComponent<Graphic>();
        weaponSprite = playerHand.transform.Find("WeaponSprite").GetComponent<Image>();
        weaponText = playerHand.transform.Find("WeaponName").GetComponent<Text>();
        
    }

    Ready FindCorrectReady(PlayerID id)
    {
        foreach (GameObject ReadyIcon in ReadyIcons)
        {
            Ready ready = ReadyIcon.GetComponent<Ready>();
            if (ready.id == id)
                return ready;
        }
        return null;
    }
}
