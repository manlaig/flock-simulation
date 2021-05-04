using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 1f;

    public float overlapRadius = 8f;
    public float obstacleLookahead = 15f;

    public float separationFactor = 1;
    public float cohesionFactor = 1;
    public float alignmentFactor = 1;
    public float obstacleAvoidFactor = 1;

    void Start()
    {
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
        left.transform.localScale = new Vector3(100, 100, 0.1f);
        left.transform.position = new Vector3(bottomLeft.x, 0, 0);
        left.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        left.layer = LayerMask.NameToLayer("Obstacle");

        GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
        right.transform.localScale = new Vector3(100, 100, 0.1f);
        right.transform.position = new Vector3(topRight.x, 0, 0);
        right.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        right.layer = LayerMask.NameToLayer("Obstacle");

        GameObject top = GameObject.CreatePrimitive(PrimitiveType.Cube);
        top.transform.localScale = new Vector3(200, 200, 0.1f);
        top.transform.position = new Vector3(transform.position.x, topRight.y, transform.position.z);
        top.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        top.layer = LayerMask.NameToLayer("Obstacle");

        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottom.transform.localScale = new Vector3(200, 200, 0.1f);
        bottom.transform.position = new Vector3(transform.position.x, bottomLeft.y, transform.position.z);
        bottom.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        bottom.layer = LayerMask.NameToLayer("Obstacle");
    }
}
