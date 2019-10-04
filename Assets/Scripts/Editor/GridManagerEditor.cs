using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    void Awake()
    {
        script = (GridManager)target;
    }

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        //EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Separator();
        //EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Test"))
        {
            script.DrawGrid();
        }

        //EditorGUILayout.EndHorizontal();
    }

    GridManager script;
}
