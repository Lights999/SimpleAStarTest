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

public class CCellData : MonoBehaviour
{
    public ECellType CellType = ECellType.Walkable;
}
