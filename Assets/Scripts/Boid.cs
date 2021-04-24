using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    [SerializeField] float overlapRadius = 8f;

    [SerializeField] float separationFactor = 1;
    [SerializeField] float cohesionFactor = 1;
    [SerializeField] float alignmentFactor = 1;

    // Start is called before the first frame update
    void Start()
    {
        // start with a random velocity
        Vector3 dir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        GetComponent<Rigidbody>().velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, overlapRadius);

        // compute the separation, alignment, and cohesion forces here
        Vector3 dir = getAlignmentForce(overlaps) + getCohesionForce(overlaps) + getSeparationForce(overlaps);
        GetComponent<Rigidbody>().velocity = dir * speed;
    }

    Vector3 getAlignmentForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach(Collider col in overlaps)
        {
            Rigidbody temp = col.gameObject.GetComponent<Rigidbody>();
            if (!temp)
                continue;
            total += temp.velocity;
            count++;
        }
        Vector3 output = count != 0 ? total / count : total;
        return output * alignmentFactor;
    }

    // TODO: this method is implemented incorrectly
    Vector3 getCohesionForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach (Collider col in overlaps)
        {
            Rigidbody temp = col.gameObject.GetComponent<Rigidbody>();
            if (!temp)
                continue;
            total += temp.position;
            count++;
        }
        Vector3 output = count != 0 ? total / count : total;
        return output * cohesionFactor;
    }

    Vector3 getSeparationForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach (Collider col in overlaps)
        {
            Rigidbody temp = col.gameObject.GetComponent<Rigidbody>();
            if (!temp)
                continue;
            total += transform.position - temp.position;
            count++;
        }
        Vector3 output = count != 0 ? total / count : total;
        return output * separationFactor;
    }

    void OnDrawGizmos()
    {
        // used for visualizing the velocity direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + GetComponent<Rigidbody>().velocity * 5);
    }
}
