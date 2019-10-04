using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Material MWalkable;
    public Material MObstacle;
    public Material MStart;
    public Material MEnd;

    public GameObject PCell;
    public Transform rootTransform;
    public int mapSizeX=10;
    public int mapSizeZ=10;


    //public Material MPath;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    ////绘制效果一直显示
    //private void OnDrawGizmos()
    //{
    //    var color = Gizmos.color;
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawCube(transform.position, Vector3.one);
    //    Gizmos.color = color;
    //}

    public void DrawGrid()
    {

        Debug.Log("DrawGrid");

        ClearMap();

        for (int iZ = 0; iZ < mapSizeZ; iZ++)
        {
            for (int iX = 0; iX < mapSizeX; iX++)
            {
                var _go = Instantiate(PCell, rootTransform,false);
                _go.transform.localPosition = new Vector3(iX, 0, iZ);
                _go.SetActive(true);
            }
        }
    }

    void ClearMap()
    {
        if (rootTransform.childCount == 0)
        {
            return;
        }

        //for (int i = 0; i < rootTransform.childCount; i++)
        //{
        //    DestroyImmediate(rootTransform.GetChild(i).gameObject);
        //}
        while (rootTransform.childCount > 0)
        {
            DestroyImmediate(rootTransform.GetChild(0).gameObject);
        }
    }


    bool Refreshed;
}
