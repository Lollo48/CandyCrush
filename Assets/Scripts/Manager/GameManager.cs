using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public int level = 1;
    public int score = 0;
    public int mouves;

    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Update()
    {
        if (mouves >= 20 && score < 1000)
        {
            UiManager.instance.loseScene.SetActive(true);
            GridManager.Instance.gameObject.SetActive(false);
        }
        else if (score > 100)
        {
            UiManager.instance.winScene.SetActive(true);
            GridManager.Instance.gameObject.SetActive(false);
            
            

        }

    }




    public void Pause()
    {
        UiManager.instance.pauseScene.SetActive(true);
        GridManager.Instance.gameObject.SetActive(false);
        
    }


    public void ReturnGame()
    {
        GridManager.Instance.gameObject.SetActive(true);
        UiManager.instance.pauseScene.SetActive(false);
    }


    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        
    }


}
