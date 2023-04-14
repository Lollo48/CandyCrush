using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    private Grid gridData;
    private Tile getTile;
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

                newTile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                newTile.transform.localScale = gridData.cellSize;
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

    SpriteRenderer GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= maxColumn || row < 0 || row >= maxRow)
            return null;
        GameObject tile = GridPrefabs[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer;
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

        bool changesOccurs = CheckMatches();
        if (!changesOccurs)
        {
            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;
            
        }
        else
        {

            do
            {
                //FillHoles();
            } while (CheckMatches());
            
        }
    }

    bool CheckMatches()
    {
        HashSet<SpriteRenderer> matchedTiles = new HashSet<SpriteRenderer>();
        for (int row = 0; row < maxRow; row++)
        {
            for (int column = 0; column < maxColumn; column++)
            {
                SpriteRenderer current = GetSpriteRendererAt(column, row);

                List<SpriteRenderer> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite);
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current);
                }

                List<SpriteRenderer> verticalMatches = FindRowMatchForTile(column, row, current.sprite);
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        foreach (SpriteRenderer renderer in matchedTiles)
        {
            renderer.sprite = null;
        }
      
        return matchedTiles.Count > 0;
    }

    List<SpriteRenderer> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = col + 1; i < maxColumn; i++)
        {
            SpriteRenderer nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    List<SpriteRenderer> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = row + 1; i < maxRow; i++)
        {
            SpriteRenderer nextRow = GetSpriteRendererAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }
            result.Add(nextRow);
        }
        return result;
    }

    //void FillHoles()
    //{
    //    for (int column = 0; column < maxColumn; column++)
    //        for (int row = 0; row < maxRow; row++)
    //        {
    //            while (GetSpriteRendererAt(column, row).sprite == null)
    //            {
    //                SpriteRenderer current = GetSpriteRendererAt(column, row);
    //                SpriteRenderer next = current;
    //                for (int filler = row; filler < maxRow - 1; filler++)
    //                {
    //                    next = GetSpriteRendererAt(column, filler + 1);
    //                    current.sprite = next.sprite;
    //                    current = next;
    //                }
    //                next.sprite = possibleCandySprite[Random.Range(0, possibleCandySprite.Count)];
    //            }
    //        }
    //}



}
