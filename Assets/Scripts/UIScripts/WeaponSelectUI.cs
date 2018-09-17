using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TeamUtility.IO;


public class WeaponSelectUI : MonoBehaviour {

    public PlayerID _playerID, id;

    private void Update()
    {
        if(InputManager.GetButtonDown("Shoot1", _playerID))
        {
            if(_playerID == PlayerID.One)
            {

            }
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    
}
