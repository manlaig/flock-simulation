using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject boid;
    [SerializeField]
    GameObject boid2;
    [SerializeField]
    GameObject boid3;
    [SerializeField]
    int initialSpawn = 20;

    void Start()
    {
        int count = 0;
        Globals g = GameObject.Find("Globals").GetComponent<Globals>();
        for (int i = 0; i < initialSpawn; i++, count++)
        {
            GameObject obj;
            if(count % 3 == 0)
                obj = Instantiate(boid);
            else if (count % 3 == 1)
                obj = Instantiate(boid2);
            else
                obj = Instantiate(boid3);

            float x = Random.Range(g.bottomLeft.x, g.topRight.x);
            float y = Random.Range(g.bottomLeft.y, g.topRight.y);
            float z = Random.Range(g.bottomLeft.z, g.topRight.z);
            obj.transform.position = new Vector3(x, y, z);
        }
    }
}
