using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;
using UnityEngine.EventSystems;

public class GameStateManager : MonoBehaviour {

    public string winnerName;
    public Sprite winnerPic;


    void OnLevelWasLoaded()
    {
        FixEventSystem();
    }
    private static void FixEventSystem()
    {
        UnityEngine.EventSystems.StandaloneInputModule[] im = UnityEngine.Object.FindObjectsOfType<UnityEngine.EventSystems.StandaloneInputModule>();
        if (im.Length > 0)
        {
            for (int i = 0; i < im.Length; i++)
            {
                im[i].gameObject.AddComponent<TeamUtility.IO.StandaloneInputModule>();
                UnityEngine.Object.DestroyImmediate(im[i]);
            }
            //EditorUtility.DisplayDialog("Success", "All built-in standalone input modules have been replaced!", "OK");
            Debug.LogFormat("{0} built-in standalone input module(s) have been replaced", im.Length);
        }
        else
        {
            //EditorUtility.DisplayDialog("Warning", "Unable to find any built-in input modules in the scene!", "OK");
        }
    }
}
