using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public TileData data;



    public Vector2Int Position;



    public void Initialize(GridManager gridM, int rowInit, int columnInit)
    {
        data = new TileData(gridM, rowInit, columnInit);
    }


}
