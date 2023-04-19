using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public TileData m_data; //tileData
    public static Tile m_tile; //my tile 
    //-----------------------------------------------------------------------------

    private SpriteRenderer m_Renderer; //SpriteRenderer
    //-----------------------------------------------------------------------------

    public Vector2Int m_position; //Position 

 
    public void Initialize(GridManager gridM, int rowInit, int columnInit)
    {
        m_data = new TileData(gridM, rowInit, columnInit);
    }

    private void Start()
    {
    
        m_Renderer = GetComponent<SpriteRenderer>();
       
    }

    public void Select()
    {
        m_Renderer.color = Color.grey;
    }

    public void Unselect()
    {
        m_Renderer.color = Color.white;
    }



    public void OnMouseDown()
    {
        if (m_tile != null)
        {
            m_tile.Unselect();
            if (Vector2Int.Distance(m_tile.m_position, m_position) == 1) //tile position  == 1
            {
                GridManager.m_instance.SwapTiles(m_position, m_tile.m_position); //let's swap 
                m_tile = null;
            }
            else
            {
                m_tile = this;
                Select();
            }
        }
        else
        {
            
            m_tile = this;
            Select();
        }

    }



}

