using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Camera camera;



    // Start is called before the first frame update
    void Awake()  
    {
        GridManager.Instance.GridGenerator();
        
    }

    


}
