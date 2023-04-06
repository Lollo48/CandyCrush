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
    private List<Sprite> candy;

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
                
                int witchSprite = Random.Range(0, tilePrefab.possibleCandyDatas.Count);
                tilePrefab.spriteRenderer.sprite = tilePrefab.possibleCandyDatas[witchSprite];
                //tilePrefab.possibleCandyDatas.RemoveAt(witchSprite);
                //candy.Add(tilePrefab.possibleCandyDatas[witchSprite]);
                var tile = Instantiate(tilePrefab, new Vector3(x, y,0), Quaternion.identity, transform);
                

                


                tile.transform.localScale = gridData.cellSize;
                x -= 1 * (gridData.cellSize.x + gridData.cellGap.x);
                tile.Initialize(this, row, column);
                tile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                mapTiles[new Vector2Int(row, column)] = tile.data;


            }
            x = startPosition.x;
            y -= 1 * (gridData.cellSize.z + gridData.cellGap.z);

        }
    }



    


    //public void SpawnTerrain(bool isStart, Vector3 playerPos)
    //{
    //    if ((currentPosition.z - playerPos.z < m_minDistanceFromPlayer) || (isStart))//distancefrom player 
    //    {
    //        int whichTerrain = Random.Range(0, terrainDatas.Count); //take randomly from terrainDatas 
    //        int terrainInSuccession = Random.Range(1, terrainDatas[whichTerrain].maxTerrainInSuccession); //take randomly from terrainDatas the succession of it
    //        for (int i = 0; i < terrainInSuccession; i++)
    //        {
    //            //instantiate a new terrain pick from terrainDatas all randomly and put in a terrainHolder
    //            //This quaternion identity corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
    //            GameObject terrain = Instantiate(terrainDatas[whichTerrain].terrain[Random.Range(0, terrainDatas[whichTerrain].terrain.Count - 1)], currentPosition, Quaternion.identity, terrainHolder);
    //            currentTerrains.Add(terrain); //add terrain 
    //            if (!isStart)
    //            {
    //                if (currentTerrains.Count > m_maxTerrainCount) //control from currentTerrainsCount and maxTerrainCount
    //                {
    //                    Destroy(currentTerrains[0]);//destroy currentTerrains 
    //                    currentTerrains.RemoveAt(0);
    //                }
    //            }
    //            currentPosition.z = currentPosition.z + 1; //incrementa z position for the terrains
    //        }
    //    }


    //}

}
