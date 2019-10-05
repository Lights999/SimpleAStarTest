using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECellType
{
    None=0,
    Walkable=1,
    Obstacle=2,
    Start=3,
    End=4

}

public enum ECellNeighborDirection
{
    Up = 0,
    Down,
    Left,
    Right,
    LeftUp,
    RightUp,
    LeftDown,
    RightDown,
    MaxCount
}

public enum EStoredState
{
    None,
    OpenList,
    CloseList
}

public class CCellData : MonoBehaviour
{
    public ECellType CellType = ECellType.Walkable;
    public int iZ;
    public int iX;

    public CCellData[] NeighborArray;

    public int F;//F = G+H
    public int G;
    public int H;

    public CCellData parentCell;

    public EStoredState StoredState;

    public CCellData()
    {
        NeighborArray = new CCellData[(int)ECellNeighborDirection.MaxCount];

        ClearSearchData();
    }

    public void UpdateNeighborData(int iZLength, int iXLength, ref CCellData[,] cellDataArray)
    {
        if (iZ + 1 < iZLength)
        {
            NeighborArray[(int)ECellNeighborDirection.Up] = cellDataArray[iZ + 1,iX];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Up] = null;
        }

        if (iZ - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.Down] = cellDataArray[iZ - 1,iX];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Down] = null;
        }

        if (iX - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.Left] = cellDataArray[iZ,iX - 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Left] = null;
        }

        if (iX + 1 < iXLength)
        {
            NeighborArray[(int)ECellNeighborDirection.Right] = cellDataArray[iZ,iX + 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Right] = null;
        }


        if (iX - 1 >= 0 && iZ + 1 < iZLength)
        {
            NeighborArray[(int)ECellNeighborDirection.LeftUp] = cellDataArray[iZ + 1,iX - 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.LeftUp] = null;
        }

        if (iX + 1 < iXLength && iZ + 1 < iZLength)
        {
            NeighborArray[(int)ECellNeighborDirection.RightUp] = cellDataArray[iZ + 1,iX + 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.RightUp] = null;
        }

        if (iX - 1 >= 0 && iZ - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.LeftDown] = cellDataArray[iZ - 1,iX - 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.LeftDown] = null;
        }

        if (iX + 1 < iXLength && iZ - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.RightDown] = cellDataArray[iZ - 1,iX + 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.RightDown] = null;
        }

    }

    public void ClearSearchData()
    {
        this.F = 0;
        this.G = 0;
        this.H = 0;
        this.parentCell = null;
        this.StoredState = EStoredState.None;
    }
}
