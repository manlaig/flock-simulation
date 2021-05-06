using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    Globals g;

    [HideInInspector]
    public Vector3 velocity;

    [Tooltip("Used for identifying different flocks of fish")]
    public string fishName;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("Globals").GetComponent<Globals>();
        velocity = Random.insideUnitSphere * g.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, g.overlapRadius);

        // compute the separation, alignment, and cohesion forces here
        Vector3 dir = getAlignmentForce(overlaps) + getCohesionForce(overlaps) + getSeparationForce(overlaps);

        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, dir), out hit, g.obstacleLookahead, ~LayerMask.NameToLayer("Obstacle")))
        {
            Vector3 reflection = Vector3.Reflect(dir, hit.normal);
            dir = g.obstacleAvoidFactor / hit.distance * (reflection - transform.position);
        }

        velocity += dir * Time.deltaTime;
        float speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, g.minSpeed, g.maxSpeed);
        velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.LookRotation(velocity);

        transform.position += velocity * Time.deltaTime;

        // WrapAround();
        WrapAround3D();
    }

    // Temporary function for 2D
    void WrapAround()
    {
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

    void WrapAround3D()
    {
        Vector3 topRight = g.topRight;
        Vector3 bottomLeft = g.bottomLeft;
        if (transform.position.x >= topRight.x)
            transform.position = new Vector3(bottomLeft.x, transform.position.y, transform.position.z);
        else if (transform.position.x <= bottomLeft.x)
            transform.position = new Vector3(topRight.x, transform.position.y, transform.position.z);
        else if (transform.position.y >= topRight.y)
            transform.position = new Vector3(transform.position.x, bottomLeft.y, transform.position.z);
        else if (transform.position.y <= bottomLeft.y)
            transform.position = new Vector3(transform.position.x, topRight.y, transform.position.z);
        else if (transform.position.z >= topRight.z)
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomLeft.z);
        else if (transform.position.z <= bottomLeft.z)
            transform.position = new Vector3(transform.position.x, transform.position.y, topRight.z);
    }

    Vector3 getAlignmentForce(Collider[] overlaps)
    {
        Vector3 total = Vector3.zero;
        int count = 0;
        foreach(Collider col in overlaps)
        {
            Boid temp = col.gameObject.GetComponent<Boid>();
            if (!temp || temp.fishName != fishName)
                continue;
            total += temp.velocity;
            count++;
        }
        if(count != 0)
        {
            Vector3 avg = total / count;
            avg *= g.maxSpeed;

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
            Boid temp = col.gameObject.GetComponent<Boid>();
            if (!temp || temp.fishName != fishName)
                continue;
            
            total += temp.transform.position;
            count++;
        }
        if (count != 0)
        {
            Vector3 avg = total / count;
            avg -= transform.position;
            avg *= g.maxSpeed;

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
            Boid temp = col.gameObject.GetComponent<Boid>();
            if (!temp || temp.fishName != fishName)
                continue;

            Vector3 diff = transform.position - temp.transform.position;
            // float div = 1f / Mathf.Pow(Vector3.Distance(transform.position, temp.transform.position), 2);
            total += diff;// * Mathf.Min(10000, div);
            count++;
        }
        Vector3 avg = count != 0 ? total / count : total;
        avg *= g.maxSpeed;

        return (avg - velocity) * g.separationFactor;
    }

    void OnDrawGizmos()
    {
        // used for visualizing the velocity direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + velocity);
    }
}
