using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _SpeedEnemy = 3.0f;

    private bool die = false;

    [SerializeField] private Animator _EnemyAnimator;
    [SerializeField] private bool IsTranslate = false;
    private Player PlayerObj;
    
    //Audio
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _AudioClip;

    void Start()
    {
        _EnemyAnimator = GetComponent<Animator>();
        PlayerObj = GameObject.Find("Player").GetComponent<Player>();
        _AudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        if (_AudioSource == null)
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

        transform.Translate(Vector3.down * _SpeedEnemy * Time.deltaTime, Space.Self);

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (PlayerObj != null)
            {
                PlayerObj.Damage();
            }
            _EnemyAnimator.SetTrigger("OnEnemyDeath");
            _AudioSource.PlayOneShot(_AudioClip);
            StopTranslate();
            Destroy(gameObject, 2.8f);

        }
        else if(other.transform.tag == "Laser")
        {
            
            if (PlayerObj != null)
            {
                PlayerObj.UpdateScore();
            }
            _EnemyAnimator.SetTrigger("OnEnemyDeath");
            _AudioSource.PlayOneShot(_AudioClip);
            StopTranslate();
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
