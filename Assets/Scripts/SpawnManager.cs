using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _EnemyPrefabs;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private bool _StopSpawning = false;
    public GameObject[] _PowerUpPrefabs;
    [SerializeField] private GameObject _AsteroidPrefabs;
    
    void Start()
    {
        StartCoroutine("SpawnRoute");
        StartCoroutine("SpawnPowerUp");
        StartCoroutine("SpawnAsteroid");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoute()
    {
        while (_StopSpawning == false)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            GameObject newObj = Instantiate(_EnemyPrefabs, ObjPosition, quaternion.identity);
            newObj.transform.parent = _EnemyContainer.transform;
            
            yield return new WaitForSeconds(5);
        }
    }
    
    IEnumerator SpawnPowerUp()
    {
        while (_StopSpawning == false)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            int RandomItem = Random.Range(0, 3);
            Instantiate(_PowerUpPrefabs[RandomItem], ObjPosition, Quaternion.identity );
            
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator SpawnAsteroid()
    {
        while (_StopSpawning == false)
        {
            Instantiate(_AsteroidPrefabs, new Vector3(Random.Range(-10, 10), 5.8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3);
        }
        
    }
    
    public void OnDeathPlayer()
    {
        _StopSpawning = true;
    }

}
