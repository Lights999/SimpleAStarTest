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

    public bool EnableDrawGizmos
    {
        get {
            return enableDrawGizmos;
        }

        set
        {
            enableDrawGizmos = value;

            //if(enableDrawGizmos)
            //{
            //    startP = StartCell.transform.position;
            //    startP.y = 0.1f;

            //    endP = EndCell.transform.position;
            //    endP.y = 0.1f;
            //}

        }
    }

    bool Refreshed;

    static GridManager instance;

    CCellData[,] CellArray;

    bool enableDrawGizmos;

    public List<CCellData> OpenList;
    public List<CCellData> CloseList;

    Vector3 startP;
    Vector3 endP;



    private void Awake()
    {
        instance = this;
    }

    public void DrawGrid()
    {

        Debug.Log("DrawGrid");

        this.StartCell = null;
        this.EndCell = null;

        ClearMap();

        GenerateCell();

        UpdateCellInfo();
    }

    void OnDrawGizmos()
    {
        if (EnableDrawGizmos)
        {
            DrawPath();
        }
    }

    void DrawPath()
    {
        RefreshCellArray();

        var colorPrev = Gizmos.color;

        Gizmos.color = Color.red;

        var _curCell = this.EndCell;
        var _parent = this.EndCell.parentCell;
        while (_parent != null && _curCell != null)
        {
            var _startP = _curCell.transform.position;
            _startP.y = 0.1f;
            var _endP = _parent.transform.position;
            _endP.y = 0.1f;
            Gizmos.DrawLine(_startP, _endP);

            _curCell = _curCell.parentCell;
        }

        //Gizmos.DrawLine(startP, endP);

        Gizmos.color = colorPrev;

        //this.EnableDrawGizmos = false;
    }

    void GenerateCell()
    {
        CellArray = new CCellData[mapSizeZ, mapSizeX];

        for (int iZ = 0; iZ < mapSizeZ; iZ++)
        {
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
                CellArray[iZ,iX] = _cellData;
            }
        }
    }
    void UpdateCellInfo()
    {
        for (int iZ = 0; iZ < mapSizeZ; iZ++)
        {

            for (int iX = 0; iX < mapSizeX; iX++)
            {
                var _cellData = CellArray[iZ,iX];

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

    void RefreshCellArray()
    {
        if (CellArray == null)
        {
            CellArray = new CCellData[mapSizeZ, mapSizeX];

            for (int i = 0; i < rootTransform.childCount; i++)
            {
                var _trans = rootTransform.GetChild(i);
                var _cellArray = _trans.GetComponent<CCellData>();
                CellArray[_cellArray.iZ, _cellArray.iX] = _cellArray;
            }
        }
    }

    void ClearSearchData()
    {
        for (int iZ = 0; iZ < mapSizeZ; iZ++)
        {

            for (int iX = 0; iX < mapSizeX; iX++)
            {
                var _cellData = CellArray[iZ,iX];

                _cellData.ClearSearchData();
            }
        }

        this.OpenList = new List<CCellData>();
        this.CloseList = new List<CCellData>();
    }

    public bool AStarSearch()
    {
        RefreshCellArray();
        ClearSearchData();

        this.StartCell.H = CaculateH(this.StartCell, this.EndCell);
        this.StartCell.F = this.StartCell.G + this.StartCell.H;

        this.OpenList.Clear();
        this.OpenList.Add(this.StartCell);
        this.StartCell.StoredState = EStoredState.OpenList;//Optmization1 标记存储在哪个表，不用每次遍历表

        while (this.OpenList.Count > 0)
        {
            var baseCell = FindMinF(this.OpenList);
            if (baseCell.CellType == ECellType.End)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < baseCell.NeighborArray.Length; i++)
                {
                    var _neighbor = baseCell.NeighborArray[i];
                    if (_neighbor == null)
                    {
                        continue;
                    }

                    if (_neighbor.CellType == ECellType.Obstacle)
                    {
                        continue;
                    }

                    var newG = CaculateG(baseCell, (ECellNeighborDirection)i);

                    if (_neighbor.StoredState == EStoredState.OpenList || _neighbor.StoredState == EStoredState.CloseList)
                    {
                        if (newG > _neighbor.G)
                        {
                            continue;
                        }
                    }

                    //Update New Node
                    UpdateCost(_neighbor, baseCell, newG, this.EndCell);

                    if (_neighbor.StoredState == EStoredState.None)
                    {
                        this.OpenList.Add(_neighbor);
                        _neighbor.StoredState = EStoredState.OpenList;
                        continue;
                    }

                    if (_neighbor.StoredState == EStoredState.OpenList)
                    {
                        //Sort OpenList
                        continue;
                    }

                    if (_neighbor.StoredState == EStoredState.CloseList)
                    {
                        this.CloseList.Remove(_neighbor);
                        this.OpenList.Add(_neighbor);
                        _neighbor.StoredState = EStoredState.OpenList;
                        continue;
                    }
                }
            }

            this.CloseList.Add(baseCell);
            baseCell.StoredState = EStoredState.CloseList;
        }

        return false;
    }

    int CaculateH(CCellData startCell, CCellData endCell)
    {
        int lenX = Mathf.Abs(startCell.iX - endCell.iX);
        int lenZ = Mathf.Abs(startCell.iZ - endCell.iZ);

        int sum = lenX + lenZ;
        return sum;
    }

    int CaculateG(CCellData baseCell, ECellNeighborDirection neighborDirection)
    {
        int costG = 0;
        
        switch (neighborDirection)
        {
            case ECellNeighborDirection.Up:
            case ECellNeighborDirection.Down:
            case ECellNeighborDirection.Left:
            case ECellNeighborDirection.Right:
                costG = 10;
                break;

            case ECellNeighborDirection.LeftUp:
            case ECellNeighborDirection.RightUp:
            case ECellNeighborDirection.LeftDown:
            case ECellNeighborDirection.RightDown:
                costG = 14;
                break;

            default:
                //costG = int.MaxValue;
                throw new System.Exception("Invalid ECellNeighborDirection");
        }

        int sum = baseCell.G + costG;
        return sum;
    }

    void UpdateCost(CCellData targetCell, CCellData parentCell, int newG, CCellData endCell)
    {
        targetCell.parentCell = parentCell;
        targetCell.G = newG;
        targetCell.H = CaculateH(targetCell, endCell);
        targetCell.F = targetCell.G + targetCell.H;
    }

    CCellData FindMinF(List<CCellData> cellList)
    {
        int minF = int.MaxValue;
        CCellData target = null;
        cellList.ForEach(item =>
        {
            if (item.F < minF)
            {
                target = item;
                minF = item.F;
            }
        });

        cellList.Remove(target);

        return target;
    }

}
