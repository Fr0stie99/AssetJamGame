using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TeamUtility.IO;

public class Ready : MonoBehaviour {
    public bool ready;
    Color color;
    Image image;
    public PlayerID id;            
	// Use this for initialization
	void Start () {
        ready = false;
        image = GetComponent<Image>();
        color = image.color;
	}

     public void ReadyUp()
    {
        image.color = Color.white;
        ready = true;
    }

    public void UnReady()
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
