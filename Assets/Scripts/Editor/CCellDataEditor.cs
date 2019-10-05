using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CCellData))]
[CanEditMultipleObjects]
public class CCellDataEditor : Editor
{
    Material[] matArray;

    void Awake()
    {
        matArray = new Material[1];
    }

    public override void OnInspectorGUI()
    {

        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            // Do something when the property changes 
            Debug.Log("Changed !!!!!!!!!!!!");

            foreach (var target in targets)
            {
                var script = (CCellData)target;

                ChangeCellType(script);
            }
        }
    }

    void ChangeCellType(CCellData cellData)
    {
        var gridMgr = GridManagerEditor.ScriptTarget;
        
        var _mr = cellData.GetComponent<MeshRenderer>();
        switch (cellData.CellType)
        {
            case ECellType.None:
                break;
            case ECellType.Walkable:
                matArray[0] = gridMgr.MWalkable;
                _mr.sharedMaterials = matArray;
                break;
            case ECellType.Obstacle:
                matArray[0]= gridMgr.MObstacle;
                _mr.sharedMaterials = matArray; 
                break;
            case ECellType.Start:
                matArray[0] = gridMgr.MStart;
                _mr.sharedMaterials = matArray;
                break;
            case ECellType.End:
                matArray[0] = gridMgr.MEnd;
                _mr.sharedMaterials = matArray;
                break;
            default:
                break;
        }
    }


}
