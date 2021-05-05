using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject boid;
    [SerializeField]
    int initialSpawn = 20;

    void Start()
    {
        Globals g = GameObject.Find("Globals").GetComponent<Globals>();
        for (int i = 0; i < initialSpawn; i++)
        {
            GameObject obj = Instantiate(boid);

            float x = Random.Range(g.bottomLeft.x, g.topRight.x);
            float y = Random.Range(g.bottomLeft.y, g.topRight.y);
            float z = Random.Range(g.bottomLeft.z, g.topRight.z);
            obj.transform.position = new Vector3(x, y, z);
        }
    }
}
