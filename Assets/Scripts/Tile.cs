using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public TileData data;
<<<<<<< Updated upstream
    public SpriteRenderer spriteRenderer;
    public List<Sprite> possibleCandyDatas;
   

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
=======
 


   

>>>>>>> Stashed changes

    public void Initialize(GridManager gridM, int rowInit, int columnInit)
    {
        data = new TileData(gridM, rowInit, columnInit);
    }


<<<<<<< Updated upstream
    
=======
   




>>>>>>> Stashed changes


   
}
