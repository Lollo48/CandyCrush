using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int maxColumn;
    public int maxRow;
    private GameObject[,] Grid;
    public List<Sprite> possibleCandySprite = new List<Sprite>();
    private Grid gridData;
    public Vector3 offset;
    

    private void Awake()
    {
        gridData = GetComponent<Grid>();
    }
    void Start()
    {

        Grid = new GameObject[maxColumn, maxRow];
        GridGenerator();
    }

    private void GridGenerator()
    {
        Vector3 startPosition = new Vector3(maxColumn * (gridData.cellSize.x + gridData.cellGap.x) / 2, maxRow * (gridData.cellSize.y + gridData.cellGap.y) / 2, 0);
        
        float x = startPosition.x;
        float y = startPosition.y;

        for (int row = 0; row < maxRow; row++)
        {
            for (int column = 0; column < maxColumn; column++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector3( x, y , 0) + offset, Quaternion.identity, transform);

                List<Sprite> possibleSprites = new List<Sprite>(possibleCandySprite);

                //Choose what sprite to use for this cell
                Sprite left1 = GetSpriteAt(column - 1, row);
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2)
                {
                    possibleSprites.Remove(left1);
                }

                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
                renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

                //Tile tile = newTile.AddComponent<Tile>();
                //tile.Position = new Vector2Int(column, row);
                newTile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                newTile.transform.localScale = gridData.cellSize;
                x += 1 * (gridData.cellSize.x + gridData.cellGap.x);
                Grid[column, row] = newTile;

            }
            x = startPosition.x;
            y -= 1 * (gridData.cellSize.z + gridData.cellGap.z);
        }

    }


    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= maxColumn || row < 0 || row >= maxRow) return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;

    }





}
