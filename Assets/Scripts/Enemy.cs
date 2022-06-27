using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _SpeedEnemy = 4.0f;
    [SerializeField] private GameObject _Prefabs;
    private bool die = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {

        if (die == true)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            Instantiate(_Prefabs, ObjPosition, Quaternion.identity);
            die = false;
        }

        if (transform.position.y < -5.0f)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            transform.position = ObjPosition;
        }

        transform.Translate(Vector3.down * _SpeedEnemy * Time.deltaTime, Space.Self);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello" + other.transform.name);
        
        if (other.transform.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Destroy(gameObject);
            }
        } else if (other.transform.tag == "Laser")
        {
            Destroy(gameObject);
            Destroy((other.gameObject));
        }
    }
}
