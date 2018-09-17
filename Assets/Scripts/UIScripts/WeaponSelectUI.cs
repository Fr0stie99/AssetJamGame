using System.Collections;
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

    private void Start()
    {
        weapons = GameObject.Find("God").GetComponent<AvailableWeapons>();
        changeColor = Color.yellow;
        initialColor = Color.white;
    }
    private void Update()
    {
        //light up Q
        foreach (HandType hand in System.Enum.GetValues(typeof(HandType))){
            foreach (PlayerID id in System.Enum.GetValues(typeof(PlayerID)))
            {
                if (InputManager.GetButtonDown("Shoot1", PlayerID.One))
                {
                    weapons.ChangeWeaponP1((int)hand);
                    SetKeyAndSprite("Player1Hand1");
                    keyText.color = changeColor;
                    GameObject weapon = weapons.weapons[weapons.GetWeaponIndex(HandType.Shoot1, PlayerID.One)];
                    weaponSprite.sprite = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().sprite;
                    weaponSprite.color = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().color;
                    weaponText.text = weapon.name;
                }
            }
        }
        else if(InputManager.GetButtonUp("Shoot1", PlayerID.One))
        {

            SetKeyAndSprite("Player1Hand1");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }
        //light up E
        if (InputManager.GetButtonDown("Shoot2", PlayerID.One))
        {
            weapons.ChangeWeaponP1(1);
            SetKeyAndSprite("Player1Hand2");
            keyText = key.GetComponent<Graphic>();
            keyText.color = changeColor;
            GameObject weapon = weapons.weapons[weapons.GetWeaponIndex(HandType.Shoot2, PlayerID.One)];
            weaponSprite.sprite = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().sprite;
            weaponSprite.color = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().color;
            weaponText.text = weapon.name;
        }
        else if (InputManager.GetButtonUp("Shoot2", PlayerID.One))
        {

            SetKeyAndSprite("Player1Hand2");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }

        //light up O
        if (InputManager.GetButtonDown("Shoot1", PlayerID.Two))
        {
            weapons.ChangeWeaponP2(0);
            SetKeyAndSprite("Player2Hand1");
            keyText = key.GetComponent<Graphic>();
            keyText.color = changeColor;
            GameObject weapon = weapons.weapons[weapons.GetWeaponIndex(HandType.Shoot1, PlayerID.Two)];
            weaponSprite.sprite = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().sprite;
            weaponSprite.color = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().color;
            weaponText.text = weapon.name;
        }
        else if (InputManager.GetButtonUp("Shoot1", PlayerID.Two))
        {
            SetKeyAndSprite("Player2Hand1");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }

        //light up P
        if (InputManager.GetButtonDown("Shoot2", PlayerID.Two))
        {
            weapons.ChangeWeaponP2(1);
            SetKeyAndSprite("Player2Hand2");
            keyText = key.GetComponent<Graphic>();
            keyText.color = changeColor;
            GameObject weapon = weapons.weapons[weapons.GetWeaponIndex(HandType.Shoot2, PlayerID.Two)];
            weaponSprite.sprite = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().sprite;
            weaponSprite.color = weapon.transform.Find("Gun").GetComponent<SpriteRenderer>().color;
            weaponText.text = weapon.name;
        }
        else if (InputManager.GetButtonUp("Shoot2", PlayerID.Two))
        {
            SetKeyAndSprite("Player2Hand2");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    

    void SetKeyAndSprite(string name)
    {
        GameObject playerHand = GameObject.Find(name);
        key = playerHand.transform.Find("Key").gameObject;
        keyText = key.GetComponent<Graphic>();
        weaponSprite = playerHand.transform.Find("WeaponSprite").GetComponent<Image>();
        weaponText = playerHand.transform.Find("WeaponName").GetComponent<Text>();
        
    }
}
