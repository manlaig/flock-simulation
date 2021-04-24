using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject boid;
    [SerializeField]
    int initialSpawn = 20;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < initialSpawn; i++)
        {
            GameObject obj = GameObject.Instantiate(boid);
            obj.transform.position = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
