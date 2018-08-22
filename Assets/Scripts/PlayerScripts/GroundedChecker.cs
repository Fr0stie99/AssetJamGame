using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundedChecker : MonoBehaviour {

    //adds groundedmanager if doesn't exist already
    void OnValidate()
    {
        if (transform.root.GetComponent<GroundedManager>() == null)
        {
            transform.root.gameObject.AddComponent<GroundedManager>();
        }
    }


    GroundedManager gm;
	// Use this for initialization
	void Awake () {
        gm = GetComponentInParent<GroundedManager>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D c)
    {
        gm.setGrounded(true);
    }

    void OnTriggerExit2D(Collider2D c)
    {
        gm.setGrounded(false);
    }
}
