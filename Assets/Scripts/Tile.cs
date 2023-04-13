using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public TileData data;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    public List<Sprite> possibleCandySprite = new List<Sprite>();


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(GridManager gridM, int rowInit, int columnInit)
    {
        data = new TileData(gridM, rowInit, columnInit);
    }




   
}
