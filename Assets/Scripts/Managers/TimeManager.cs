using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    float maxFreezeTime;
    float timer = 0.0f;
    float oldTimeScale;
    bool hasFrozen = false;
	// Use this for initialization
	void Awake () {
        hasFrozen = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasFrozen)
        {
            return;
        }
            
        if (timer < maxFreezeTime)
        {
            timer += Time.fixedUnscaledDeltaTime;
        }
        else
        {
            Time.timeScale = oldTimeScale;
            hasFrozen = false;
        }

	}

    public void FreezeTime(float forHowLong)
    {
        oldTimeScale = Time.timeScale;
        Time.timeScale = 0.000000001f;
        maxFreezeTime = forHowLong;
        hasFrozen = true;
    }
}
