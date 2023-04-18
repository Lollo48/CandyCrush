using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI mouvesText;
    public GameObject pauseScene;
    public GameObject loseScene;
    public GameObject winScene;



    //public GameObject win;



    public static UiManager instance;




    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    


    private void Start()
    {
        pauseScene.SetActive(false);
        loseScene.SetActive(false);
        winScene.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.instance.score.ToString();
        levelText.text = GameManager.instance.level.ToString();
        mouvesText.text = GameManager.instance.mouves.ToString();
    }
}
