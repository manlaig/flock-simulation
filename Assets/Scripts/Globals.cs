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

    public Vector3 topRight = new Vector3(100, 60, 100);
    public Vector3 bottomLeft = new Vector3(-100, 1, -100);
}
