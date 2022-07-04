using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    public GameObject[] _powerUpPrefabs;
    [SerializeField] private GameObject _asteroidPrefabs;
    
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
        while (_stopSpawning == false)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            GameObject newObj = Instantiate(_enemyPrefabs, ObjPosition, quaternion.identity);
            newObj.transform.parent = _enemyContainer.transform;
            
            yield return new WaitForSeconds(5);
        }
    }
    
    IEnumerator SpawnPowerUp()
    {
        while (_stopSpawning == false)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            int RandomItem = Random.Range(0, 3);
            Instantiate(_powerUpPrefabs[RandomItem], ObjPosition, Quaternion.identity );
            
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator SpawnAsteroid()
    {
        while (_stopSpawning == false)
        {
            Instantiate(_asteroidPrefabs, new Vector3(Random.Range(-10, 10), 5.8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3);
        }
        
    }
    
    public void OnDeathPlayer()
    {
        _stopSpawning = true;
    }

}
