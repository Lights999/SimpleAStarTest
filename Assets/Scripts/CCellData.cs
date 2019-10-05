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

public class CCellData : MonoBehaviour
{
    public ECellType CellType = ECellType.Walkable;

    public CCellData[] NeighborArray;

    public CCellData()
    {
        NeighborArray = new CCellData[(int)ECellNeighborDirection.MaxCount];
    }

    public void UpdateNeighborData(int iZ, int iX, int iZLength, int iXLength, ref CCellData[][] cellDataArray)
    {
        if (iZ + 1 < iZLength)
        {
            NeighborArray[(int)ECellNeighborDirection.Up] = cellDataArray[iZ + 1][iX];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Up] = null;
        }

        if (iZ - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.Down] = cellDataArray[iZ - 1][iX];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Down] = null;
        }

        if (iX - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.Left] = cellDataArray[iZ][iX - 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Left] = null;
        }

        if (iX + 1 < iXLength)
        {
            NeighborArray[(int)ECellNeighborDirection.Right] = cellDataArray[iZ][iX + 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.Right] = null;
        }


        if (iX - 1 >= 0 && iZ + 1 < iZLength)
        {
            NeighborArray[(int)ECellNeighborDirection.LeftUp] = cellDataArray[iZ + 1][iX - 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.LeftUp] = null;
        }

        if (iX + 1 < iXLength && iZ + 1 < iZLength)
        {
            NeighborArray[(int)ECellNeighborDirection.RightUp] = cellDataArray[iZ + 1][iX + 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.RightUp] = null;
        }

        if (iX - 1 >= 0 && iZ - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.LeftDown] = cellDataArray[iZ - 1][iX - 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.LeftDown] = null;
        }

        if (iX + 1 < iXLength && iZ - 1 >= 0)
        {
            NeighborArray[(int)ECellNeighborDirection.RightDown] = cellDataArray[iZ - 1][iX + 1];
        }
        else
        {
            NeighborArray[(int)ECellNeighborDirection.RightDown] = null;
        }

    }
}
