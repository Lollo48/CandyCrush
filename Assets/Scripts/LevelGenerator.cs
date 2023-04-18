using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //public Camera camera;




    //Start is called before the first frame update
    //void Awake()
    //{

    //    camera.orthographicSize = 6;
    //}


    //private void Update()
    //{
    //    if (GameManager.instance.score == 50)
    //    {
    //        UiManager.instance.win.SetActive(true);


    //    }
    //}

    //private void CameraAdjusting()
    //{
    //    if (GameManager.instance.level > 5)
    //    {
    //        camera.orthographicSize = 9;
    //    }
    //    else if (GridManager.Instance.maxColumn > 7 || GridManager.Instance.maxRow > 9)
    //    {
    //        camera.orthographicSize = 11;
    //    }

    //}




    //private int ReturnColumn(int level)
    //{
    //    if (level>5) GridManager.Instance.maxColumn = Random.Range(0, level) + Random.Range(0, 10);
    //    else if (level > 15) GridManager.Instance.maxColumn = 15;
    //    else if (GridManager.Instance.maxColumn > 15) GridManager.Instance.maxColumn = 15;
    //    return GridManager.Instance.maxColumn; 
    //}

    //private int ReturnRow(int level)
    //{
    //    if (level > 5) GridManager.Instance.maxRow = Random.Range(0, level) + Random.Range(0, 10);
    //    else if (level > 20) GridManager.Instance.maxRow = Random.Range(0, level) + Random.Range(0, 10);
    //    else if (GridManager.Instance.maxRow > 26) GridManager.Instance.maxRow = 26;
    //    return GridManager.Instance.maxRow;
    //}


    //public void NextLevel()
    //{
    //    GridManager.Instance.maxColumn = 10;
    //    GridManager.Instance.maxRow = 10;
    //    GridManager.Instance.GridPrefabs = new GameObject[GridManager.Instance.maxColumn, GridManager.Instance.maxRow];
    //    GridManager.Instance.GridGenerator(GridManager.Instance.maxColumn, GridManager.Instance.maxRow);
    //    //GridManager.Instance.GridPrefabs = new GameObject[ReturnColumn(GameManager.instance.level), ReturnRow(GameManager.instance.level)];
    //    //GridManager.Instance.GridGenerator(ReturnColumn(GameManager.instance.level), ReturnRow(GameManager.instance.level));
    //    //CameraAdjusting();
    //    UiManager.instance.win.SetActive(false);
    //    GameManager.instance.level++;
    //    GameManager.instance.score = 0;

    //}





}
