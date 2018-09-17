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
    Color changeColor, initialColor;

    private void Start()
    {
        changeColor = Color.yellow;
        initialColor = Color.white;
    }
    private void Update()
    {
        //light up Q
        if(InputManager.GetButtonDown("Shoot1", PlayerID.One))
        {
                key = GameObject.Find("Key11");
                keyText = key.GetComponent<Graphic>();
                keyText.color = changeColor;       
        }else if(InputManager.GetButtonUp("Shoot1", PlayerID.One))
        {
            key = GameObject.Find("Key11");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }
        //light up E
        if (InputManager.GetButtonDown("Shoot2", PlayerID.One))
        {
            key = GameObject.Find("Key12");
            keyText = key.GetComponent<Graphic>();
            keyText.color = changeColor;
        }
        else if (InputManager.GetButtonUp("Shoot2", PlayerID.One))
        {
            key = GameObject.Find("Key12");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }

        //light up O
        if (InputManager.GetButtonDown("Shoot1", PlayerID.Two))
        {
            key = GameObject.Find("Key21");
            keyText = key.GetComponent<Graphic>();
            keyText.color = changeColor;
        }
        else if (InputManager.GetButtonUp("Shoot1", PlayerID.Two))
        {
            key = GameObject.Find("Key21");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }

        //light up P
        if (InputManager.GetButtonDown("Shoot2", PlayerID.Two))
        {
            key = GameObject.Find("Key22");
            keyText = key.GetComponent<Graphic>();
            keyText.color = changeColor;
        }
        else if (InputManager.GetButtonUp("Shoot2", PlayerID.Two))
        {
            key = GameObject.Find("Key22");
            keyText = key.GetComponent<Graphic>();
            keyText.color = initialColor;
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    
}
