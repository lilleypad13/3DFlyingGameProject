using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelObstacle : MonoBehaviour
{
    [SerializeField] private GameObject obstacleElement;

    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int sizeZ;

    [SerializeField] private float elementDimension;

    private int totalElements { get => sizeX * sizeY * sizeZ; }

    private Vector3[] positions;

    private void Start()
    {
        GeneratePositions();
        GenerateElements();
    }

    private void GenerateElements()
    {
        foreach (Vector3 pos in positions)
        {
            Instantiate(obstacleElement, pos, Quaternion.identity, gameObject.transform);
        }
    }

    private void GeneratePositions()
    {
        positions = new Vector3[totalElements];

        int counter = 0;

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                for (int k = 0; k < sizeZ; k++)
                {
                    positions[counter] = new Vector3(i, j, k) * elementDimension + transform.position;
                    counter++;
                }
            }
        }
    }
}
