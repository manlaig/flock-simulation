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
        Vector2 rightEdge = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 leftEdge = Camera.main.ViewportToWorldPoint(new Vector2(-1, 0));
        Vector2 topEdge = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));

        for (int i = 0; i < initialSpawn; i++)
        {
            GameObject obj = Instantiate(boid);

            float x = Random.Range(leftEdge.x, rightEdge.x);
            float y = Random.Range(topEdge.y, bottomEdge.y);
            obj.transform.position = new Vector3(x, y, Random.Range(-5, 5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
