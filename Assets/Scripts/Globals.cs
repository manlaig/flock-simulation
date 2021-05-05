using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    [Tooltip("Minimum speed of a boid")]
    public float minSpeed = 1f;
    [Tooltip("Maximum speed of a boid")]
    public float maxSpeed = 1f;

    [Tooltip("The perception radius, radius at which it overlaps with other boids")]
    public float overlapRadius = 8f;
    [Tooltip("The max distance of the ray used to recognize obstacles")]
    public float obstacleLookahead = 15f;

    [Tooltip("Force factor for the separation force")]
    public float separationFactor = 1;
    [Tooltip("Force factor for the cohesion force")]
    public float cohesionFactor = 1;
    [Tooltip("Force factor for the alignment force")]
    public float alignmentFactor = 1;
    [Tooltip("Force factor for the obstacle avoid force")]
    public float obstacleAvoidFactor = 1;

    [Tooltip("Max bounds used to wrap the boid around the environment")]
    public Vector3 topRight = new Vector3(100, 60, 100);
    [Tooltip("Min bounds used to wrap the boid around the environment")]
    public Vector3 bottomLeft = new Vector3(-100, 1, -100);
}
