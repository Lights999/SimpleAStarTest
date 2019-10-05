using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public static GridManager ScriptTarget
    {
        get
        {
            if (script == null)
            {
                script = GameObject.FindObjectOfType<GridManager>();
            }
            return script;
        }
    }

    void Awake()
    {
        script = (GridManager)target;
    }

    public override void OnInspectorGUI()
    {
        script = (GridManager)target;

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

    static GridManager script;
}
