using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedManager : MonoBehaviour {

    bool grounded;


    public bool isGrounded()
    {
        return grounded;
    }

    public void setGrounded(bool isTouching)
    {
        grounded = isTouching;
    }
    
}
