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

        if (GUILayout.Button("Init Map"))
        {
            script.DrawGrid();
            script.EnableDrawGizmos = false;
        }

        if (GUILayout.Button("Caculate Path"))
        {
            bool result = script.AStarSearch();
        }

        if (GUILayout.Button("Draw Path"))
        {
            script.EnableDrawGizmos = true;
            //if (script.EnableDrawGizmos == false)
            //{
            //    script.EnableDrawGizmos = true;
            //}
        }

        //EditorGUILayout.EndHorizontal();
    }

    static GridManager script;
}
