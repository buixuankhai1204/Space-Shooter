using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
        if (transform.position.y > 6f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);                                
            }
            Destroy(gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        if (transform.position.y < -6f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);                                
            }
            Destroy(this);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

}
