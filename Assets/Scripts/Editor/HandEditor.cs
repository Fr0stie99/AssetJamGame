using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
// Custom Editor using SerializedProperties.
// Automatic handling of multi-object editing, undo, and prefab overrides.
[CustomEditor(typeof(Hand))]
[CanEditMultipleObjects]
public class HandEditor : Editor
{

    SerializedProperty currentWeaponProperty;
    GameObject[] _weapons;
    private static string[] _choices;
    private int _choiceIndex = 0;
    private HandType hand;
    private int handIndex = 0;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        currentWeaponProperty = serializedObject.FindProperty("currentWeaponObject");
        
        // Set the choice index to the previously selected index
        //DON'T DELETE GOD OK
        _weapons = GameObject.Find("God").GetComponent<AvailableWeapons>().weapons;
        List<string> choiceList = new List<string>();
        foreach (GameObject weapon in _weapons)
        {
            choiceList.Add(weapon.name);
        }
        _choices = choiceList.ToArray();
        _choiceIndex = serializedObject.FindProperty("weaponIndex").intValue;

    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        //doing the orientation thing
         _choiceIndex = EditorGUILayout.Popup("Current Weapon", _choiceIndex, _choices);
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        currentWeaponProperty.objectReferenceValue = _weapons[_choiceIndex];
        serializedObject.FindProperty("weaponIndex").intValue = _choiceIndex;
        hand = (HandType) EditorGUILayout.EnumPopup(hand);

        serializedObject.FindProperty("hand").enumValueIndex= (int) hand;

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

}