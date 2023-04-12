using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public Tile tilePrefab;
    public int maxRow;
    public int maxColumn;
    private Grid gridData;
    public Dictionary<Vector2Int, TileData> mapTiles = new Dictionary<Vector2Int, TileData>();


    private void Awake()
    {
        gridData = GetComponent<Grid>();

    }

    private void Start()
    {
        GenerateGrid();

    }

    private void GenerateGrid()
    {
        Vector3 startPosition = new Vector3(maxColumn * (gridData.cellSize.x + gridData.cellGap.x) / 2, maxRow * (gridData.cellSize.z + gridData.cellGap.z) / 2 ,0);
        float x = startPosition.x;
        float y = startPosition.y;

        for (int row = maxRow; row > 0; row--)
        {
            for (int column = 0; column < maxColumn; column++)
            {



                var tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);

                tile.transform.localScale = gridData.cellSize;
                x -= 1 * (gridData.cellSize.x + gridData.cellGap.x);
                tile.Initialize(this, row, column);
                tile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                mapTiles[new Vector2Int(row, column)] = tile.data;


            }
            x = startPosition.x;
            y -= 1 * (gridData.cellSize.z + gridData.cellGap.z);

        }
    }



}
