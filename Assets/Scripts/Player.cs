using System;
using System.Collections;
using System.Collections.Generic;
using Grpc.Core.Logging;
using UnityEditor.AI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 3.0f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private float _NextRate = 0.0f;
    [SerializeField] private float _FireRate = 2f;
    
    [SerializeField] private GameObject _Prefabs;

    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private UiManager _uiManager;
    
    private GameManager _gameManager;
    
    // PowersUp
    [SerializeField] public GameObject TripleShotPrefab;
    [SerializeField] private bool IsTripleShotActive = false;
    [SerializeField] private bool IsSpeedPowerUpActive = false;
    [SerializeField] private bool IsShieldPowerUpActive = false;
    [SerializeField] private GameObject _ShieldVisualizer;
    
    //Audio
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _AudioClip;

    private int _Score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _AudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        _uiManager.ShowGameOver(false);
        _uiManager.ShowRestartLevel(false);

        if (_AudioSource == null)
        {
            Debug.LogError("Audio source not working!!!");
        }
        
        if (_gameManager == null)
        {
            Debug.LogError("Game manager not working!!!");

        }
        
        if (_uiManager == null)
        {
            Debug.LogError("UI mannager not working!!!");
        }
        
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn mannager not working!!!");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement();
        SpawnLaser();
        ShowScore();
        ShowLives();
    }

    void Movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(HorizontalInput, VerticalInput, 0);
        transform.Translate(Direction * _Speed * Time.deltaTime);
        
        float minXScence = -9;
        float maxXScence = 9;
        float minYScence = -4f;
        float maxYScence = 0;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXScence, maxXScence), Mathf.Clamp(transform.position.y, minYScence, maxYScence), 0);
    }

    void SpawnLaser()
    {
        Vector3 PositionOffset = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _NextRate)
        {
            _NextRate = Time.time + _FireRate;
            if (IsTripleShotActive == true)
            {
                Vector3 TripleShotPosition = new Vector3(transform.position.x + -0.6384013f, transform.position.y + 1.53f, 0);
                Instantiate(TripleShotPrefab, TripleShotPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(_Prefabs, PositionOffset, Quaternion.identity);
            }
            
            _AudioSource.PlayOneShot(_AudioClip);
            
        }
    }

    public void Damage()
    {
        if (_lives == 0)
        {
            return;
        }
        
        if (IsShieldPowerUpActive == true)
        {
            IsShieldPowerUpActive = false;
            _ShieldVisualizer.SetActive(false);
            return;
        }
        
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.OnDeathPlayer();
            ShowLives();
            ShowGameOver();
            _gameManager.GameOVer();
            ShowRestartLevel();
            Destroy(this.gameObject);
        }
    }
    
    public void TripleShotIsActiveRoutine()
    {
        IsTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5);
        IsTripleShotActive = false;
    }

    public void SpeedIsActiveRoutine()
    {
        IsSpeedPowerUpActive = true;
        _Speed *= 2;
        StartCoroutine(SpeedPowerDown());

    }
    
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        _Speed /= 2;
        IsSpeedPowerUpActive = false;

    }
    
    public void ShieldIsActiveRoutine()
    {
        IsShieldPowerUpActive = true;
        _ShieldVisualizer.SetActive(true);
    }

    public void UpdateScore()
    {
        _Score += 10;
    }

    public int GetScore()
    {
        return _Score;
    }

    public void ShowScore()
    {
        _uiManager.ShowScore(GetScore());
    }
    
    public int getLives()
    {
        return _lives;
    }

    public void ShowLives()
    {
        _uiManager.ShowLives(getLives());
    }

    public void ShowGameOver()
    {
        _uiManager.ShowGameOver(true);
    }

    public void ShowRestartLevel()
    {
        _uiManager.ShowRestartLevel(true);
    }

}
