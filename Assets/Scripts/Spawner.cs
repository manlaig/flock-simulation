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
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        for (int i = 0; i < initialSpawn; i++)
        {
            GameObject obj = Instantiate(boid);

            float x = Random.Range(bottomLeft.x, topRight.x);
            float y = Random.Range(bottomLeft.y, topRight.y);
            obj.transform.position = new Vector3(x, y, 0);
        }
    }
}
