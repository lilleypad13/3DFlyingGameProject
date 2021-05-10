using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    private float speed;
    private bool turning = false;
    private float obstacleDetectionDistance = 50.0f;

    private void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    private void Update()
    {
        // Determine the bounding box for the flocking agent
        Bounds b = new Bounds(myManager.transform.position, myManager.swimLimits * 2);

        // If agent leaves the bounds of the box it starts turning around
        RaycastHit hit = new RaycastHit(); // Initialize hit
        Vector3 direction = Vector3.zero; // Initialize direction

        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
            Debug.DrawRay(this.transform.position, this.transform.forward * obstacleDetectionDistance, Color.red);
        }
        else if(Physics.Raycast(transform.position, this.transform.forward * obstacleDetectionDistance, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
            turning = false;

        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(direction), 
                myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);

            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }

        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    private void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;

        Vector3 vcenter = Vector3.zero; // Average center position of a group
        Vector3 vavoid = Vector3.zero; // Average avoidance vector (since avoiding all members in group)
        float gSpeed = 0.01f; // Global speed of the entire group (Average speed of the group)
        float nDistance; // Neighbor distance to check if other agents are close enough to be considered within the same group
        int groupSize = 0; // Count how many agents are within a group (smaller part of the group an individual agent considers neighbors)

        foreach (GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighborDistance)
                {
                    // Sums positions of all agents within neighbor distance in a group
                    vcenter += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f) // Hard coded distance to determine when an agent is close enough to consider avoiding
                    {
                        // Sums avoidance vectors agent finds between all agents within avoidance distance
                        vavoid += this.transform.position - go.transform.position;
                    }

                    // Sums speed of all agents within a group
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed += anotherFlock.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            // Average the values obtained
            vcenter = vcenter / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            // Sum vectors to determine new flock based heading and Slerp towards that direction
            Vector3 direction = vcenter + vavoid - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
