using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    private Grid gridData;
    public GameObject tilePrefab;
    public int maxColumn;
    public int maxRow;
    [HideInInspector]
    public GameObject[,] GridPrefabs;
    public List<Sprite> possibleCandySprite = new List<Sprite>();

    public Vector3 offset;

    public static GridManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        GridPrefabs = new GameObject[maxColumn,maxRow];
        
        gridData = GetComponent<Grid>();
    }


    private void Start()
    {
        GridGenerator(maxColumn, maxRow);
        GameManager.instance.mouves = 0;
    }

    public void GridGenerator(int maxColumn, int maxRow)
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

                choseSprite(column, row, possibleSprites);

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

    GameObject GetGameObjectAt(int column, int row)
    {
        if (column < 0 || column >= maxColumn || row < 0 || row >= maxRow) return null;
        GameObject tile = GridPrefabs[column, row];
        return tile;

    }


    private void choseSprite(int column,int row, List<Sprite> possibleSprites)
    {

        //Choose what sprite to use for this cell
        Sprite left1 = GetSpriteAt(column, row - 1);
        Sprite left2 = GetSpriteAt(column, row - 1);
        if (left2 != null && left1 == left2)
        {
            possibleSprites.Remove(left1);
        }

        Sprite down1 = GetSpriteAt(column - 1, row);
        Sprite down2 = GetSpriteAt(column - 1, row);
        if (down2 != null && down1 == down2)
        {
            possibleSprites.Remove(down1);
        }

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

        bool changesOccurs = CheckForCombination();
        if (!changesOccurs)
        {
            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;
            

        }
        GameManager.instance.score += 50;
        GameManager.instance.mouves += 1;
        Invoke("NewSprite", 0.1f);
    }

    
    List<GameObject> FindRowCombination(int column, int row, Sprite sprite)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = row + 1; i < maxRow; i++)
        {
            GameObject object1 = GetGameObjectAt(column, i);
            SpriteRenderer sprite2 = object1.GetComponent<SpriteRenderer>();
            if (sprite2.sprite != sprite)
            {
                break;
            }
            result.Add(object1);
           
        }
        return result;
    }

    List<GameObject> FindColumnCombination(int col, int row, Sprite sprite)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = col + 1; i < maxColumn; i++)
        {
            GameObject object1 = GetGameObjectAt(i, row);
            SpriteRenderer sprite2 = object1.GetComponent<SpriteRenderer>();

            if (sprite2.sprite != sprite)
            {
                break;
            }
            result.Add(object1);
        }
        return result;
    }


    bool CheckForCombination()
    {
        HashSet<GameObject> matchedTiles = new HashSet<GameObject>();
        for (int row = 0; row < maxRow; row++)
        {
            for (int column = 0; column < maxColumn; column++)
            {
                GameObject current = GetGameObjectAt(column, row);
                SpriteRenderer sprite2 = current.GetComponent<SpriteRenderer>();

                List<GameObject> horizontalMatches = FindColumnCombination(column, row, sprite2.sprite);
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current);
                }

                List<GameObject> verticalMatches = FindRowCombination(column, row, sprite2.sprite);
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        foreach (GameObject renderer in matchedTiles)
        {
            renderer.SetActive(false);
        }
        return matchedTiles.Count > 0;
    }



    void NewSprite()
    {
        for (int column = 0; column < maxColumn; column++)
        {
            for (int row = 0; row < maxRow; row++)
            {
                if (GridPrefabs[column, row].activeInHierarchy == false )
                {
                    List<Sprite> possibleSprites = new List<Sprite>(possibleCandySprite);

                    GridPrefabs[column, row].SetActive(true);
                    choseSprite(column, row, possibleSprites);
                    SpriteRenderer renderer = GridPrefabs[column,row].GetComponent<SpriteRenderer>();
                    renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

                }
            }
        }

    }

    


}




