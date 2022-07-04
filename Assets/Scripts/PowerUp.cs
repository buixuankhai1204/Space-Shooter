using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerUpId;
    
    //Audio
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    void Start()
    {
        _audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source not working!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DestroyPowerUp();
    }

    private void Move()
    {
        if (transform.position.y < -4f)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyPowerUp()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            GameObject Player = GameObject.FindWithTag("Player");
            Player PlayerObj = Player.GetComponent<Player>();
            if (PlayerObj != null)
            {
                if (_powerUpId == 0)
                {
                    PlayerObj.TripleShotIsActiveRoutine();
                } 
                else if (_powerUpId == 1)
                {
                    PlayerObj.SpeedIsActiveRoutine();
                }
                else if(_powerUpId == 2)
                {
                    PlayerObj.ShieldIsActiveRoutine();
                }
                
                Destroy(gameObject);
                _audioSource.PlayOneShot(_audioClip);
            }
        }
    }
}
