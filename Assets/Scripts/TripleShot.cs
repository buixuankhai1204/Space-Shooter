using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed = 8.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DestroyTripleShot();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
    }

    private void DestroyTripleShot()
    {
        if (transform.position.y > 6.0f)
        {
            Destroy(gameObject);
        }
    }
}
