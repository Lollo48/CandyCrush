using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    
    public GameObject tilePrefab;
    public int maxColumn;
    public int maxRow;
    public float Distance = 1.0f;
    private GameObject[,] Grid;
    public List<Sprite> possibleCandySprite = new List<Sprite>();
    private Vector3 positionOffset = new Vector3 (-2,-3,0);

    void Start()
    {
        
        Grid = new GameObject[maxColumn, maxRow];
        GridGenerator();
    }

    private void GridGenerator()
    {
        for (int row = 0; row < maxRow; row++)
        {
            for (int column = 0; column < maxColumn; column++)
            {
                GameObject newTile = Instantiate(tilePrefab);

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

                Tile tile = newTile.AddComponent<Tile>();
                tile.Position = new Vector2Int(column, row);

                newTile.transform.parent = transform;
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset;

                Grid[column, row] = newTile;
            }
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
