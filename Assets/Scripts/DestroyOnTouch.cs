using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour {

	void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
