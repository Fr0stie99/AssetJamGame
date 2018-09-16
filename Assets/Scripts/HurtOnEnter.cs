using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnEnter : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            c.GetComponent<PlayerHealth>().HurtMe(1000f);
            Debug.Log(c.transform.position);
        }
    }
}
