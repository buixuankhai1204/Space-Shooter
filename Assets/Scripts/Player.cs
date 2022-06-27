using System;
using System.Collections;
using System.Collections.Generic;
using Grpc.Core.Logging;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 3.5f;

    [SerializeField] private int _lives = 3;

    [SerializeField] private GameObject _Prefabs;

    [SerializeField]
    private float _NextRate = 0.0f;
    [SerializeField]
    private float _FireRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement();
        SpawnLaser();
    }

    void Movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(HorizontalInput, VerticalInput, 0);
        transform.Translate(Direction * _Speed * Time.deltaTime);
        
        float minXScence = -10;
        float maxXScence = 10;
        float minYScence = -3.8f;
        float maxYScence = 0;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXScence, maxXScence), Mathf.Clamp(transform.position.y, minYScence, maxYScence), 0);
    }

    void SpawnLaser()
    {
        Vector3 PositionOffset = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _NextRate)
        {
            _NextRate = Time.time + _FireRate;
            Instantiate(_Prefabs, PositionOffset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives -= 1;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

    
}
