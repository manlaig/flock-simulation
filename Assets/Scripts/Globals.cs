using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 1f;

    public float minForce = 1f;
    public float maxForce = 1f;

    public float overlapRadius = 8f;

    public float separationFactor = 1;
    public float cohesionFactor = 1;
    public float alignmentFactor = 1;
}
