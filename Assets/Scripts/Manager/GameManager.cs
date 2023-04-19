using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public int m_level = 1;
    public int m_score = 0;
    public int m_mouves;

    public static GameManager m_instance;


    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }


    private void Update()
    {
        if (m_mouves == 0 && m_score < 1000)
        {
            UiManager.m_instance.m_loseScene.SetActive(true);
            GridManager.m_instance.gameObject.SetActive(false);
        }
        else if (m_score > 999)
        {
            UiManager.m_instance.m_winScene.SetActive(true);
            GridManager.m_instance.gameObject.SetActive(false);
            
            

        }

    }

    public void Pause()
    {
        UiManager.m_instance.m_pauseScene.SetActive(true);
        GridManager.m_instance.gameObject.SetActive(false);
        
    }


    public void ReturnGame()
    {
        GridManager.m_instance.gameObject.SetActive(true);
        UiManager.m_instance.m_pauseScene.SetActive(false);
    }


    //public void RestartGame()
    //{
    //    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        
    //}


}
