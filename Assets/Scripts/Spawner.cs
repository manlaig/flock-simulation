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
    int initialSpawn = 20;

    void Start()
    {
        Globals g = GameObject.Find("Globals").GetComponent<Globals>();
        for (int i = 0; i < initialSpawn; i++)
        {
            GameObject obj;
            int choice = Random.Range(0, 2);
            if(choice == 1)
                obj = Instantiate(boid);
            else
                obj = Instantiate(boid2);

            float x = Random.Range(g.bottomLeft.x, g.topRight.x);
            float y = Random.Range(g.bottomLeft.y, g.topRight.y);
            float z = Random.Range(g.bottomLeft.z, g.topRight.z);
            obj.transform.position = new Vector3(x, y, z);
        }
    }
}
