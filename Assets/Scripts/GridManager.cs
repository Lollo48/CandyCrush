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
<<<<<<< Updated upstream
    private GameObject[,] Grid;
=======


>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
                List<Sprite> possibleSprites = new List<Sprite>(tilePrefab.possibleCandyDatas); // 1

                GameObject newTile = Instantiate(tilePrefab);

                //Choose what sprite to use for this cell
                Sprite left1 = GetSpriteAt(column - 1, row); //2
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2) // 3
                {
                    possibleSprites.Remove(left1); // 4
                }

                Sprite down1 = GetSpriteAt(column, row - 1); // 5
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                

                tilePrefab.spriteRenderer.sprite = tilePrefab.possibleCandyDatas[Random.Range(0, tilePrefab.possibleCandyDatas.Count)];
=======
               

                var tile = Instantiate(tilePrefab, new Vector3(x, y,0), Quaternion.identity, transform);
>>>>>>> Stashed changes

                tile.transform.localScale = gridData.cellSize;
                x -= 1 * (gridData.cellSize.x + gridData.cellGap.x);
                tile.Initialize(this, row, column);
                tile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                mapTiles[new Vector2Int(row, column)] = tile.data;

               
               

                Grid[column, row] = tile;

            }
            x = startPosition.x;
            y -= 1 * (gridData.cellSize.z + gridData.cellGap.z);
<<<<<<< Updated upstream

=======
         
>>>>>>> Stashed changes
        }
    }



<<<<<<< Updated upstream
    private Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= maxColumn || row < 0 || row >= maxRow)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;
    }



=======
>>>>>>> Stashed changes
}
