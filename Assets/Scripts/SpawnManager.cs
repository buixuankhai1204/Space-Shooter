using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _Prefabs;
    
    IEnumerator  Start()
    {
        yield return StartCoroutine("SpawnRoute");
        print("Done " + Time.time);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnRoute()
    {
        while (true)
        {
            float RandomXPosition = Random.Range(-10, 10);
            Vector3 ObjPosition = new Vector3(RandomXPosition, 5.8f, 0);
            Instantiate(_Prefabs, ObjPosition, quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
}
