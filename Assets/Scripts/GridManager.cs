using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance
    {
        get {
            return instance;
        }
    }

    public Material MWalkable;
    public Material MObstacle;
    public Material MStart;
    public Material MEnd;

    public GameObject PCell;
    public Transform rootTransform;
    public int mapSizeX=10;
    public int mapSizeZ=10;

    public CCellData StartCell;
    public CCellData EndCell;

    bool Refreshed;

    static GridManager instance;

    CCellData[][] CellArray;



    private void Awake()
    {
        instance = this;
    }

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

        this.StartCell = null;
        this.EndCell = null;

        ClearMap();

        GenerateCell();

        UpdateCellInfo();
    }

    void GenerateCell()
    {
        CellArray = new CCellData[mapSizeZ][];

        for (int iZ = 0; iZ < mapSizeZ; iZ++)
        {
            CellArray[iZ] = new CCellData[mapSizeX];

            for (int iX = 0; iX < mapSizeX; iX++)
            {
                var _go = Instantiate(PCell, rootTransform, false);
                _go.transform.localPosition = new Vector3(iX, 0, iZ);
                _go.SetActive(true);

                CCellData _cellData = _go.GetComponent<CCellData>();
                if (_cellData == null)
                {
                    _cellData = _go.AddComponent<CCellData>();
                }

                _cellData.iZ = iZ;
                _cellData.iX = iX;
                CellArray[iZ][iX] = _cellData;
            }
        }
    }
    void UpdateCellInfo()
    {
        for (int iZ = 0; iZ < mapSizeZ; iZ++)
        {

            for (int iX = 0; iX < mapSizeX; iX++)
            {
                var _cellData = CellArray[iZ][iX];

                _cellData.UpdateNeighborData(mapSizeZ, mapSizeX, ref CellArray);
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

        CellArray = null;
    }



}
