using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5.0f, 5.0f, 5.0f);
    public Vector3 goalPos;
    [SerializeField] private bool isFollowingPlayer = true;
    [SerializeField] private Transform playerTransform;

    [Header("Fish Settings")]
    [Range(0.0f, 100.0f)]
    public float minSpeed;
    [Range(0.0f, 100.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighborDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    private void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 randomizedLimitsPosition = new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
            Vector3 pos = this.transform.position + randomizedLimitsPosition;

            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            Debug.Log($"Fish: {allFish[i].name} spawned at position {pos}");
            allFish[i].GetComponent<Flock>().myManager = this;
        }

        if (isFollowingPlayer)
        {
            goalPos = playerTransform.position;
        }
        else
        {
            goalPos = this.transform.position;
        }
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            goalPos = playerTransform.position;
        }
        else
        {
            RandomizeGoalPosition();
        }
    }

    private void RandomizeGoalPosition()
    {
        if (Random.Range(0, 100) < 5)
        {
            Vector3 randomizedLimitsPosition = new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
            goalPos = this.transform.position + randomizedLimitsPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, 2.0f * swimLimits);
    }
}
