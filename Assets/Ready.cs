using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TeamUtility.IO;

public class Ready : MonoBehaviour {
    bool ready;
    Color color;
    Image image;
    public PlayerID id;            
	// Use this for initialization
	void Start () {
        ready = false;
        image = GetComponent<Image>();
        color = image.color;
	}

     void ReadyUp()
    {
        image.color = Color.white;
        ready = true;
    }

     void UnReady()
    {
        image.color = color;
        ready = false;
    }

    public bool IsReady()
    {
        return ready;
    }

    public void ToggleReady()
    {
        if (ready) { UnReady(); }
        else { ReadyUp(); }
    }

}
