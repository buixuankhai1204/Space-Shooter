using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;

    [SerializeField] private Image _livesImg;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Text _gameOVer;
    [SerializeField] private Text _restartLevel; 

    // Start is called before the first frame update
    void Start()
    {
        _gameOVer.gameObject.SetActive(false);
        _scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    { 
    }

public void ShowScore(int Score)
    {
        _scoreText.text = "score: " + Score;
    }

    public void ShowLives(int Lives)
    {
        
        _livesImg.sprite = _liveSprites[Lives];
        if (Lives < 1)
        {
            _gameOVer.gameObject.SetActive(true);
        }
    }

    public void ShowGameOver(bool IsGameOver)
    {
        if (IsGameOver)
        {
            _gameOVer.gameObject.SetActive(true);
            StartCoroutine(GameFlickerRoutine());
        }
    }

    IEnumerator GameFlickerRoutine()
    {
        while (true)
        {
            _gameOVer.text = "GAME OVER !!!";
            yield return new WaitForSeconds(0.5f);
            _gameOVer.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ShowRestartLevel(bool check)
    {
        _restartLevel.gameObject.SetActive(check);
    }
    
}
