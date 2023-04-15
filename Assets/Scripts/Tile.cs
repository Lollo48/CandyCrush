using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public TileData data;
    public static Tile tile;

    private SpriteRenderer Renderer;

    
    public Vector2Int Position;

 
    public void Initialize(GridManager gridM, int rowInit, int columnInit)
    {
        data = new TileData(gridM, rowInit, columnInit);
    }

    private void Start()
    {
    
        Renderer = GetComponent<SpriteRenderer>();
       
    }

    public void Select()
    {
        Renderer.color = Color.grey;
    }

    public void Unselect()
    {
        Renderer.color = Color.white;
    }



    public void OnMouseDown()
    {

        if (tile != null)
        {
            if (tile == this)
                return;
            tile.Unselect();
            if (Vector2Int.Distance(tile.Position, Position) == 1)
            {
                GridManager.Instance.SwapTiles(Position, tile.Position);
                tile = null;
            }
            else
            {
                tile = this;
                Select();
            }
        }
        else
        {
            
            tile = this;
            Select();
        }

    }



}

