using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private float _Speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
        destroyLaser();
    }

    void destroyLaser()
    {
        if (transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * _Speed * Time.deltaTime, Space.World);
    }
}
