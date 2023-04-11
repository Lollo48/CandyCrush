using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public TileData data;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    public List<Sprite> possibleCandyDatas;


   

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void Initialize(GridManager gridM, int rowInit, int columnInit)
    {
        data = new TileData(gridM, rowInit, columnInit);
    }


    //public void ChoseCandy()
    //{
    //    int witchSprite = Random.Range(0, possibleCandyDatas.Count);

    //    candy.Insert(index, possibleCandyDatas[witchSprite]);

    //    spriteRenderer.sprite = candy[index];

    //    possibleCandyDatas.RemoveAt(witchSprite);

    //    index += 1;


    //    if (possibleCandyDatas.Count <= 0)
    //    {
    //        for (int i = 0; candy.Count == 0; i++)
    //        {
    //            possibleCandyDatas.Add(candy[i]);
    //            candy.RemoveAt(i);
    //        }
    //    }
    //}






}
