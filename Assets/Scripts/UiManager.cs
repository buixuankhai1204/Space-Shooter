using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private Text _ScoreText;

    [SerializeField] private Image LivesImg;
    [SerializeField] private Sprite[] LiveSprites;
    [SerializeField] private Text _GameOVer;
    [SerializeField] private Text _RestartLevel; 

    // Start is called before the first frame update
    void Start()
    {
        _GameOVer.gameObject.SetActive(false);
        _ScoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    { 
    }

public void ShowScore(int Score)
    {
        _ScoreText.text = "score: " + Score;
    }

    public void ShowLives(int Lives)
    {
        
        LivesImg.sprite = LiveSprites[Lives];
        if (Lives < 1)
        {
            _GameOVer.gameObject.SetActive(true);
        }
    }

    public void ShowGameOver(bool IsGameOver)
    {
        if (IsGameOver)
        {
            _GameOVer.gameObject.SetActive(true);
            StartCoroutine(GameFlickerRoutine());
        }
    }

    IEnumerator GameFlickerRoutine()
    {
        while (true)
        {
            _GameOVer.text = "GAME OVER !!!";
            yield return new WaitForSeconds(0.5f);
            _GameOVer.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ShowRestartLevel(bool check)
    {
        _RestartLevel.gameObject.SetActive(check);
    }
    
}
