using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float maxSpeed = 1f;

    [SerializeField] float minForce = 1f;
    [SerializeField] float maxForce = 1f;

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
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        Collider[] overlaps = Physics.OverlapSphere(transform.position, overlapRadius);

        // compute the separation, alignment, and cohesion forces here
        Vector3 dir = getAlignmentForce(overlaps) + getCohesionForce(overlaps) + getSeparationForce(overlaps);

        if (dir.magnitude < minForce)
            dir = dir.normalized * minForce;
        else if (dir.magnitude > maxForce)
            dir = dir.normalized * maxForce;

        rb.AddForce(dir);
        // rb.velocity = dir;

        Vector2 rightEdge = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 leftEdge = Camera.main.ViewportToWorldPoint(new Vector2(-1, 0));
        Vector2 topEdge = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
        if (transform.position.x >= rightEdge.x)
            transform.position = new Vector3(leftEdge.x, transform.position.y, transform.position.z);
        else if (transform.position.x <= leftEdge.x)
            transform.position = new Vector3(rightEdge.x, transform.position.y, transform.position.z);
        else if (transform.position.y >= topEdge.y)
            transform.position = new Vector3(transform.position.x, bottomEdge.y, transform.position.z);
        else if (transform.position.y <= bottomEdge.y)
            transform.position = new Vector3(transform.position.x, topEdge.y, transform.position.z);
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
        if(count != 0)
        {
            Vector3 avg = total / count;
            avg = avg.normalized * maxSpeed;
            return (avg - GetComponent<Rigidbody>().velocity) * alignmentFactor;
        }
        return total;
    }

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
        if (count != 0)
        {
            Vector3 avg = total / count;
            avg -= transform.position;
            avg = avg.normalized * maxSpeed;
            return (avg - GetComponent<Rigidbody>().velocity) * cohesionFactor;
        }
        return total;
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

            Vector3 diff = transform.position - temp.position;
            float div = 1f / Vector3.Distance(transform.position, temp.position);
            total = total + Mathf.Min(10000, div) * diff;
            count++;
        }
        Vector3 avg = count != 0 ? total / count : total;
        return (avg - GetComponent<Rigidbody>().velocity) * separationFactor;
    }

    void OnDrawGizmos()
    {
        // used for visualizing the velocity direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + GetComponent<Rigidbody>().velocity);
    }
}
