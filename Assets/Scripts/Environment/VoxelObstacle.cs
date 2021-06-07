using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelObstacle : MonoBehaviour
{
    [SerializeField] private GameObject obstacleElement;
    [SerializeField] private GameObject supportPlatform;

    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int sizeZ;

    [SerializeField] private float elementDimension;
    [SerializeField] private float elementToPlatformInstantiationBuffer = 0.05f;

    private int totalElements { get => sizeX * sizeY * sizeZ; }

    private Vector3[] positions;

    private void Start()
    {
        SpawnSupportPlatform();
        GeneratePositions();
        GenerateElements();
    }

    private void SpawnSupportPlatform()
    {
        GameObject platform = Instantiate(
            supportPlatform, 
            transform.position + 0.5f * elementDimension * (Vector3.right * sizeX + Vector3.forward * sizeZ) - Vector3.up * (0.5f * supportPlatform.transform.localScale.y + elementToPlatformInstantiationBuffer), 
            Quaternion.identity, 
            transform);

        platform.transform.localScale = new Vector3(sizeX * elementDimension, platform.transform.localScale.y, sizeZ * elementDimension);
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
                    positions[counter] = new Vector3(i, j, k) * elementDimension + 
                        Vector3.one * 0.5f * elementDimension + 
                        transform.position;
                    counter++;
                }
            }
        }
    }
}
