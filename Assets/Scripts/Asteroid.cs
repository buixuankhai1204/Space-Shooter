using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _Speed = 3.0f;
    [SerializeField] private Animator _AsteroidAnimator;
    [SerializeField] private bool IsTranslate = false;
    private Player PlayerObj;
    
    //Audio
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _AudioClip;



    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        RotatorAsteroid();
        PlayerObj = GameObject.Find("Player").GetComponent<Player>();
        _AudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        
        if (_AudioSource == null)
        {
            Debug.LogError("Audio source not working!!!");
        }
        
        if (PlayerObj == null)
        {
            Debug.LogError("Game OVer");
        }
        _AsteroidAnimator = GetComponent<Animator>();

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
        
        transform.Translate(Vector3.down * _Speed * Time.deltaTime, Space.World);
    }

    public void RotatorAsteroid()
    {
        transform.Rotate(Vector3.forward * 100 * Time.deltaTime,Space.World);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (PlayerObj != null)
            {
                PlayerObj.Damage();
            }
            _AsteroidAnimator.SetTrigger("OnEnemyDeath");
            _AudioSource.PlayOneShot(_AudioClip);
            StopTranslate();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.8f);
            
        }
        else if(other.transform.tag == "Laser")
        {
            if (PlayerObj != null)
            {
                PlayerObj.UpdateScore();
            }
            
            _AsteroidAnimator.SetTrigger("OnEnemyDeath");
            _AudioSource.PlayOneShot(_AudioClip);
            StopTranslate();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.8f);
            Destroy(other.gameObject);
        }
    }

    void StopTranslate()
    {
        transform.Translate(Vector3.down * 0 * Time.deltaTime,Space.World);
        IsTranslate = true;
    }

    bool GetIsTranslate()
    {
        return IsTranslate;
    }
}