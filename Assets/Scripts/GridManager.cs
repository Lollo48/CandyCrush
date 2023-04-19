using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    //----------------------------------------------------------------------------------------
    private Grid m_GridData; 
    public GameObject m_tilePrefab; //the tile prefabs used for the Instantiate

    //----------------------------------------------------------------------------------------
    public int m_maxColumn; //max number of column
    public int m_maxRow; //max number of row
    [HideInInspector] //i'd like to use this method becasue i need it public for the next level but i want to [HideInInspector] for the design 
    public GameObject[,] m_gridObject; //my grid object i use this to set the position 
    public List<Sprite> m_possibleCandySprite = new List<Sprite>(); //possible candy sprite
    public Vector3 m_offset; //set new position to centre on the world 

    //----------------------------------------------------------------------------------------
    public static GridManager m_instance; //Singleton 


    private void Awake()
    {
        //set the singleton 
        if (m_instance == null)
            m_instance = this;
        m_gridObject = new GameObject[m_maxColumn,m_maxRow]; //set the new gameobject 
        m_GridData = GetComponent<Grid>(); //take grid component to set cellsize and cellgap
    }


    private void Start()
    {
        GridGenerator(m_maxColumn, m_maxRow); //create the grid 
        
    }                                    



    //grid generator permit to implement the gird 
    private void GridGenerator(int maxColumn, int maxRow)
    {
        //startPosition 
        Vector3 startPosition = new Vector3(maxColumn * (m_GridData.cellSize.x + m_GridData.cellGap.x) / 2, maxRow * (m_GridData.cellSize.y + m_GridData.cellGap.y) / 2, 0);
        
        float x = startPosition.x;
        float y = startPosition.y;

        for (int row = 0; row < maxRow; row++)
        {
            for (int column = 0; column < maxColumn; column++)
            {
                //Instantiate the tile gameobject and the x,y position z=0 because i'm working in 2d world
                GameObject newTile = Instantiate(m_tilePrefab, new Vector3( x, y , 0) + m_offset, Quaternion.identity, transform);
                List<Sprite> possibleSprites = new List<Sprite>(m_possibleCandySprite); //take all the possibleSprite because i'll switch sprite that 
                                                                                        //allows to implement new candy more easly 

                choseSprite(column, row, possibleSprites); //chose sprite i explain that funcion below 

                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>(); //for every gameobject i take the spriterenderer that allow to acces to the sprite
                renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)]; //random sprite 

                newTile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")"; //name every single cell in the Hierarchy

                Tile tile = newTile.AddComponent<Tile>(); //add tile component 
                tile.m_position = new Vector2Int(column, row); //Representation of 2D vectors and points using integers
                                                             //i'm going to take the right position about column and row for the swap 

                x -= 1 * (m_GridData.cellSize.x + m_GridData.cellGap.x); //start new row
                m_gridObject[column, row] = newTile; //i'm going to set for every single newTile the right position on my gameObject 
                                                    //that permit to know for every single cell the right position on x and y

            }
            x = startPosition.x; 
            y -= 1 * (m_GridData.cellSize.z + m_GridData.cellGap.z); //new Column
        }

    }


    //i create this function becasue i neew to know every single sprite at the right column and row 
    //this function returns a sprite and as input it wants the number of columns and rows
    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= m_maxColumn || row < 0 || row >= m_maxRow) return null; //controll
        GameObject tile = m_gridObject[column, row]; //take the tile gameobject and i'm saying that is in the place m_gridObject[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>(); //i'm taking the priteRenderer to acces at the sprite 
        return renderer.sprite; // return the sprite 

    }

    //i create this function becasue we can't have 3 row or 3 column combo sprite 
    //i used this function in two separate situation
    //the first one when i'm going to create the grid i'm going to check the sprite every single cicle 
    //the second when i'm going to fill the hole when i do combo
    private void choseSprite(int column,int row, List<Sprite> possibleSprites)
    {

        //Choose what sprite to use for this cell
        Sprite left1 = GetSpriteAt(column, row - 1);  //i'm going to take the Sprite at (column, row - 1)
        Sprite left2 = GetSpriteAt(column, row - 2); //i'm going to take the Sprite at (column, row - 2)
        if (left2 != null && left1 == left2) //if the sprite are equals
        {
            possibleSprites.Remove(left1); //remove 
        }

        //Choose what sprite to use for this cell
        Sprite down1 = GetSpriteAt(column - 1, row);  //i'm going to take the Sprite at (column - 1, row)
        Sprite down2 = GetSpriteAt(column - 2, row); //i'm going to take the Sprite at (column - 2, row)
        if (down2 != null && down1 == down2) //if the sprite are equals
        {
            possibleSprites.Remove(down1); //remove
        }

    }


    //i create this function becasue i neew to know every single gameobject at the right column and row 
    //this function returns a gameobject and as input it wants the number of columns and rows
    GameObject GetGameObjectAt(int column, int row)
    {
        if (column < 0 || column >= m_maxColumn || row < 0 || row >= m_maxRow) return null; //controll
        GameObject tile = m_gridObject[column, row]; //take the tile gameobject and i'm saying that is in the place m_gridObject[column, row];
        return tile; //return gameobject

    }

    //this function allows to SwipeTiles
    //swapTiles with Sprite i need the first position and the second position 
    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        GameObject tile1 = m_gridObject[tile1Position.x, tile1Position.y]; //take the tile gameobject and i'm saying that is in the place m_gridObject[tile1Position.x, tile1Position.y];
        SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>(); //take the sprite renderer for access to the sprite

        GameObject tile2 = m_gridObject[tile2Position.x, tile2Position.y]; //take the tile gameobject and i'm saying that is in the place m_gridObject[tile2Position.x, tile2Position.y];
        SpriteRenderer renderer2 = tile2.GetComponent<SpriteRenderer>(); //take the sprite renderer for access to the sprite

        //simple swap the sprite to make that i need a 3 Sprite to safe the firstone
        Sprite temp = renderer1.sprite; 
        renderer1.sprite = renderer2.sprite;
        renderer2.sprite = temp;

        bool change = CheckForCombination(); //chack combos i explain that funcion below  
        if (!change)
        {

            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;
            

        }
       
        Invoke("NewSprite", 0.1f); //newSprite i explain that funcion below  
    }

    //this function allow to FindRowCombination
    //find FindRowCombination this function returns a List<GameObject> and as input it wants the number of columns and rows and sprite
    List<GameObject> FindRowCombination(int column, int row, Sprite sprite)
    {
        List<GameObject> result = new List<GameObject>(); //my gameobject result list
        for (int i = row + 1; i < m_maxRow; i++) //every single row 
        {
            GameObject object1 = GetGameObjectAt(column, i); //take the gameobject 
            SpriteRenderer sprite2 = object1.GetComponent<SpriteRenderer>(); //take the spriteRenderer 
            if (sprite2.sprite != sprite) //different sprite do nothing
            {
                break;
            }
            result.Add(object1); //add the result to my list 
           
        }
        return result; //return my list 
    }

    //this function allow to FindColumnCombination
    //find FindColumnCombination this function returns a List<GameObject> and as input it wants the number of columns and rows and sprite
    List<GameObject> FindColumnCombination(int col, int row, Sprite sprite)
    {
        List<GameObject> result = new List<GameObject>(); //my gameobject result list
        for (int i = col + 1; i < m_maxColumn; i++) //every single Column 
        {
            GameObject object1 = GetGameObjectAt(i, row); //take the gameobject 
            SpriteRenderer sprite2 = object1.GetComponent<SpriteRenderer>(); //take the spriteRenderer 

            if (sprite2.sprite != sprite) //different sprite do nothing
            {
                break;
            }
            result.Add(object1); //add the result to my list 
        }
        return result; //return my list
    }

    //this function allow to CheckForCombination
    //retunr a bool 
    bool CheckForCombination()
    {
        HashSet<GameObject> matchedTiles = new HashSet<GameObject>(); // object that contains all elements that are present in itself, the specified collection, or both.
                                                                     // i can add GameObject 
        for (int row = 0; row < m_maxRow; row++)
        {
            for (int column = 0; column < m_maxColumn; column++)
            {
                GameObject currentGameObject = GetGameObjectAt(column, row); // take my GameObject
                SpriteRenderer sprite2 = currentGameObject.GetComponent<SpriteRenderer>(); // take SpriteRenderer 

                List<GameObject> horizontalMatches = FindColumnCombination(column, row, sprite2.sprite);
                if (horizontalMatches.Count >= 2) //Combos >=2 
                {
                    matchedTiles.UnionWith(horizontalMatches); //i use UnionWith because with add i have a error
                                                               // i can't convert System.generic.list<UnityEngine.gameObject> to UnityEngine.GameObject
                    matchedTiles.Add(currentGameObject);  // add current to the list 
                }

                List<GameObject> verticalMatches = FindRowCombination(column, row, sprite2.sprite);
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);//i use UnionWith because with add i have a error
                                                            // i can't convert System.generic.list<UnityEngine.gameObject> to UnityEngine.GameObject
                    matchedTiles.Add(currentGameObject); // add current to the list
                }
            }
        }

        foreach (GameObject renderer in matchedTiles)  
        {
            renderer.SetActive(false); //setActive InHierarchy false
        }
        return matchedTiles.Count > 0; //return matchedTiles.Count
    }


    //this function allow to fill with new sprite 
    public void NewSprite()
    {
        for (int column = 0; column < m_maxColumn; column++) //every single column 
        {
            for (int row = 0; row < m_maxRow; row++) //every single row
            {
                if (m_gridObject[column, row].activeInHierarchy == false) //m_gridObject[column, row].activeInHierarchy == false
                {
                    List<Sprite> possibleSprites = new List<Sprite>(m_possibleCandySprite); //let's change sprite 

                    m_gridObject[column, row].SetActive(true); //set the activity to the gameobject to true 
                    choseSprite(column, row, possibleSprites); //chose sprite funciont becasue it's possible to take 3 combos again 
                    SpriteRenderer renderer = m_gridObject[column,row].GetComponent<SpriteRenderer>(); //take SpriteRenderer 
                    renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)]; //put the new sprite 

                }
            }
        }

    }

    


}




