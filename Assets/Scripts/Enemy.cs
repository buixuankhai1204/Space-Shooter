using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _SpeedEnemy = 3.0f;

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
        Player PlayerObj = GameObject.Find("Player").GetComponent<Player>();
        if (other.transform.tag == "Player")
        {
            if (PlayerObj != null)
            {
                PlayerObj.Damage();
                Destroy(gameObject);
            }
        }
        else 
        {
            
            if (PlayerObj != null)
            {
                PlayerObj.UpdateScore();
            }
            Destroy(gameObject);
            Destroy((other.gameObject));
        }
    }
}
