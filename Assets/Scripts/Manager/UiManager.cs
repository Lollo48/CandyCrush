using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    public TextMeshProUGUI m_scoreText;
    public TextMeshProUGUI m_levelText;
    public TextMeshProUGUI m_mouvesText;
    //--------------------------------------------------------------------------

    public GameObject m_pauseScene;
    public GameObject m_loseScene;
    public GameObject m_winScene;






    public static UiManager m_instance;


    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }


    private void Start()
    {
        m_pauseScene.SetActive(false);
        m_loseScene.SetActive(false);
        m_winScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_scoreText.text = GameManager.m_instance.m_score.ToString();
        m_levelText.text = GameManager.m_instance.m_level.ToString();
        m_mouvesText.text = GameManager.m_instance.m_mouves.ToString();
    }
}
