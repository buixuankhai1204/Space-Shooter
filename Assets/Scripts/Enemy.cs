using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speedEnemy = 3.0f;
    private float _timeRate = 3.0f;
    private float _nextRate = 0.0f;

    private bool die = false;

    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private GameObject _laserPrefabs;
    [SerializeField] private bool _isTranslate = false;
    private Player _playerObj;
    
    //Audio
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
        _playerObj = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source not working!!!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GetIsTranslate() == false)
        {
            Movement();
        }

        if (Time.time > _timeRate)
        {
            _timeRate = Random.Range(3, 7);
            _nextRate = Time.time + _timeRate;
            GameObject enenmyLaser = Instantiate(_laserPrefabs, transform.position, Quaternion.identity);
            Laser[] lasers = enenmyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void Movement()
    {
        float minXScence = -9;
        float maxXScence = 9;
        float minYScence = -4f;
        float maxYScence = 6.0f;

        if (transform.position.y < -5.0f)
        {
            float RandomXPosition = Random.Range(minXScence, maxXScence);
            Vector3 ObjPosition = new Vector3(RandomXPosition, maxYScence, 0);
            transform.position = ObjPosition;
        }

        transform.Translate(Vector3.down * _speedEnemy * Time.deltaTime, Space.Self);

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (_playerObj != null)
            {
                _playerObj.Damage();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
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
            _enemyAnimator.SetTrigger("OnEnemyDeath");
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
