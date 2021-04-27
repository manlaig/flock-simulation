using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    Globals g;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("Globals").GetComponent<Globals>();

        // start with a random velocity
        // Vector3 dir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        // GetComponent<Rigidbody>().velocity = dir;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        /*Rigidbody rb = GetComponent<Rigidbody>();
        if(rb.velocity.magnitude > g.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * g.maxSpeed;
        }*/

        Collider[] overlaps = Physics.OverlapSphere(transform.position, g.overlapRadius);

        // compute the separation, alignment, and cohesion forces here
        Vector3 dir = getAlignmentForce(overlaps) + getCohesionForce(overlaps) + getSeparationForce(overlaps);

        float force = dir.magnitude;
        force = Mathf.Clamp(force, g.minForce, g.maxForce);
        dir = dir.normalized * force;

        // rb.AddForce(dir);

        velocity += dir * Time.deltaTime;
        float speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, g.minSpeed, g.maxSpeed);
        velocity = velocity.normalized * speed;

        transform.position += velocity * Time.deltaTime;


        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.x >= topRight.x)
            transform.position = new Vector3(bottomLeft.x, transform.position.y, transform.position.z);
        else if (transform.position.x <= bottomLeft.x)
            transform.position = new Vector3(topRight.x, transform.position.y, transform.position.z);
        else if (transform.position.y >= topRight.y)
            transform.position = new Vector3(transform.position.x, bottomLeft.y, transform.position.z);
        else if (transform.position.y <= bottomLeft.y)
            transform.position = new Vector3(transform.position.x, topRight.y, transform.position.z);
    }

    Vector3 getAlignmentForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach(Collider col in overlaps)
        {
            // Rigidbody temp = col.gameObject.GetComponent<Rigidbody>();
            Boid temp = col.gameObject.GetComponent<Boid>();
            if (!temp)
                continue;
            total += temp.velocity;
            count++;
        }
        if(count != 0)
        {
            Vector3 avg = total / count;

            float speed = avg.magnitude;
            speed = Mathf.Clamp(speed, g.minSpeed, g.maxSpeed);
            avg = avg.normalized * speed;

            // Vector3 scaled = (avg - GetComponent<Rigidbody>().velocity) * g.alignmentFactor;
            Vector3 scaled = (avg - velocity) * g.alignmentFactor;
            return scaled;
        }
        return total;
    }

    Vector3 getCohesionForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach (Collider col in overlaps)
        {
            // Rigidbody temp = col.gameObject.GetComponent<Rigidbody>();
            Boid temp = col.gameObject.GetComponent<Boid>();
            if (!temp)
                continue;
            // total += temp.position;
            total += temp.transform.position;
            count++;
        }
        if (count != 0)
        {
            Vector3 avg = total / count;
            avg -= transform.position;

            float speed = avg.magnitude;
            speed = Mathf.Clamp(speed, g.minSpeed, g.maxSpeed);
            avg = avg.normalized * speed;

            // Vector3 scaled = (avg - GetComponent<Rigidbody>().velocity) * g.cohesionFactor;
            Vector3 scaled = (avg - velocity) * g.cohesionFactor;
            return scaled;
        }
        return total;
    }

    Vector3 getSeparationForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach (Collider col in overlaps)
        {
            // Rigidbody temp = col.gameObject.GetComponent<Rigidbody>();
            Boid temp = col.gameObject.GetComponent<Boid>();
            if (!temp)
                continue;

            // Vector3 diff = transform.position - temp.position;
            Vector3 diff = transform.position - temp.transform.position;
            // float div = Mathf.Pow(Vector3.Distance(transform.position, temp.position), 2);
            float div = 1f / Mathf.Pow(Vector3.Distance(transform.position, temp.transform.position), 2);
            total += diff * Mathf.Min(10000, div);
            count++;
        }
        Vector3 avg = count != 0 ? total / count : total;

        float speed = avg.magnitude;
        speed = Mathf.Clamp(speed, g.minSpeed, g.maxSpeed);
        avg = avg.normalized * speed;

        // return (avg - GetComponent<Rigidbody>().velocity) * g.separationFactor;
        return (avg - velocity) * g.separationFactor;
    }

    void OnDrawGizmos()
    {
        // used for visualizing the velocity direction
        Gizmos.color = Color.blue;
        // Gizmos.DrawLine(transform.position, transform.position + GetComponent<Rigidbody>().velocity);
        Gizmos.DrawLine(transform.position, transform.position + velocity);
    }
}
