using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    float screenShake;
    bool shakeTime;
    Vector3 playerPosition;

    private void Update()
    {
        
    }

    public void OutOfBoundShake(Collider2D c)
    {
        //if player gets out of bound, put an offset on his position
        Debug.Log(c.transform.position);
        playerPosition = c.transform.position;
    }

}
