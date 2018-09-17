using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnEnter : MonoBehaviour {

    public ScreenShake mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            c.GetComponent<PlayerHealth>().HurtMe(1000f);
            Debug.Log(c.transform.position);

        }
    }
}
