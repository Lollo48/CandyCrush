using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    private Grid gridData;
    public GameObject tilePrefab;
    public int maxColumn;
    public int maxRow;
    private GameObject[,] GridPrefabs;
    public List<Sprite> possibleCandySprite = new List<Sprite>();

    public Vector3 offset;
    public static GridManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
 
        gridData = GetComponent<Grid>();
    }
    void Start()
    {

        GridPrefabs = new GameObject[maxColumn, maxRow];
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
                Sprite left1 = GetSpriteAt(column , row - 1);
                Sprite left2 = GetSpriteAt(column , row - 2);
                if (left2 != null && left1 == left2)
                {
                    possibleSprites.Remove(left1);
                }

                Sprite down1 = GetSpriteAt(column - 1, row );
                Sprite down2 = GetSpriteAt(column - 2, row );
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
                renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

                newTile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                
                Tile tile = newTile.AddComponent<Tile>(); //add tile component 
                tile.Position = new Vector2Int(column, row); //Representation of 2D vectors and points using integers i'm going to take the right position about column and row for the swap 

                x -= 1 * (gridData.cellSize.x + gridData.cellGap.x);
                GridPrefabs[column, row] = newTile;

            }
            x = startPosition.x;
            y -= 1 * (gridData.cellSize.z + gridData.cellGap.z);
        }

    }


    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= maxColumn || row < 0 || row >= maxRow) return null;
        GameObject tile = GridPrefabs[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;

    }


    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        GameObject tile1 = GridPrefabs[tile1Position.x, tile1Position.y];
        SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>();

        GameObject tile2 = GridPrefabs[tile2Position.x, tile2Position.y];
        SpriteRenderer renderer2 = tile2.GetComponent<SpriteRenderer>();

        Sprite temp = renderer1.sprite;
        renderer1.sprite = renderer2.sprite;
        renderer2.sprite = temp;

    }

    private void MatchRow()
    {
        List<Sprite> match = new List<Sprite>();
        for (int row = 0; row < maxRow; row++)
        {
            for (int column = 0; column < maxColumn; column++)
            {
                Sprite sprite = GetSpriteAt(column, row);

                match.Add(sprite);
                






            }
        }
    }





   

}
