using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(Player_AirConsole))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Draw new components we want to add in the inspector
        EditorGUILayout.HelpBox("Script that controller a single player, included his movements, collisions and score", MessageType.Info);
        //Draw the default inspector
        DrawDefaultInspector();
        
    }
}