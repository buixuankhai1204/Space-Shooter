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
    
    void Start()
    {
        StartCoroutine("SpawnRoute");
        print("Done " + Time.time);
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

    public void OnDeathPlayer()
    {
        _StopSpawning = true;
    }
}
