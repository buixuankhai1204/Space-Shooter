using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private Animator _asteroidAnimator;
    [SerializeField] private bool _isTranslate = false;
    private Player _playerObj;
    
    //Audio
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;



    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        RotatorAsteroid();
        _playerObj = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        
        if (_audioSource == null)
        {
            Debug.LogError("Audio source not working!!!");
        }
        
        if (_playerObj == null)
        {
            Debug.LogError("Game OVer");
        }
        _asteroidAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GetIsTranslate() == false)
        {
            Move();
        }

        RotatorAsteroid();
    }

    public void Move()
    {
        if (transform.position.y < -6f)
        {
            transform.position  = new Vector3(Random.Range(-10, 10), 6, 0);
            
        }
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
    }

    public void RotatorAsteroid()
    {
        transform.Rotate(Vector3.forward * 100 * Time.deltaTime,Space.World);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (_playerObj != null)
            {
                _playerObj.Damage();
            }
            _asteroidAnimator.SetTrigger("OnEnemyDeath");
            _audioSource.PlayOneShot(_audioClip);
            StopTranslate();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.8f);
            
        }
        else if(other.transform.tag == "Laser")
        {
            if (_playerObj != null)
            {
                _playerObj.UpdateScore();
            }
            
            _asteroidAnimator.SetTrigger("OnEnemyDeath");
            _audioSource.PlayOneShot(_audioClip);
            StopTranslate();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.8f);
            Destroy(other.gameObject);
        }
    }

    void StopTranslate()
    {
        transform.Translate(Vector3.down * 0 * Time.deltaTime,Space.World);
        _isTranslate = true;
    }

    bool GetIsTranslate()
    {
        return _isTranslate;
    }
}