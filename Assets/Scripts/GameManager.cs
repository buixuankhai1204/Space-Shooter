using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool IsGameover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameover && Input.GetKeyDown("r"))
        {
            RestartLevel();
        }
    }

    public void GameOVer()
    {
        IsGameover = true;
    }

    public bool CheckGameVoer()
    {
        return IsGameover;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
