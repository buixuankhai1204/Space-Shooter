using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _Speed = 3.0f;
    [SerializeField] private int _PowerUpId;
    
    //Audio
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _AudioClip;
    void Start()
    {
        _AudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        if (_AudioSource == null)
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
        transform.Translate(Vector3.down * _Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            GameObject Player = GameObject.FindWithTag("Player");
            Player PlayerObj = Player.GetComponent<Player>();
            if (PlayerObj != null)
            {
                if (_PowerUpId == 0)
                {
                    PlayerObj.TripleShotIsActiveRoutine();
                } 
                else if (_PowerUpId == 1)
                {
                    PlayerObj.SpeedIsActiveRoutine();
                }
                else if(_PowerUpId == 2)
                {
                    PlayerObj.ShieldIsActiveRoutine();
                }
                
                Destroy(gameObject);
                _AudioSource.PlayOneShot(_AudioClip);
            }
        }
    }
}
