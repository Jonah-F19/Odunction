using UnityEngine;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab; // Assign your cloud prefab in the inspector
    public int cloudCount = 10; // Number of clouds to spawn

    public float minX = -10f; // Left boundary of the spawn area
    public float maxX = 10f; // Right boundary of the spawn area
    public float minY = 5f; // Minimum height of clouds
    public float maxY = 10f; // Maximum height of clouds

    public float minDistanceBetweenClouds = 2f; // Minimum distance to prevent overlap

    private List<Vector3> cloudPositions = new List<Vector3>();

    void Start()
    {
        SpawnClouds(); // Spawn a limited number of clouds
    }

    void SpawnClouds()
    {
        if (cloudPrefab != null)
        {
            int spawnedClouds = 0;
            while (spawnedClouds < cloudCount)
            {
                Vector3 spawnPosition = GetRandomPosition();

                // Check if the new position is far enough from existing clouds
                if (IsPositionValid(spawnPosition))
                {
                    Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
                    cloudPositions.Add(spawnPosition);
                    spawnedClouds++;
                }
            }
        }
        else
        {
            Debug.LogWarning("Cloud prefab not assigned!");
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, 0);
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (var existingPosition in cloudPositions)
        {
            if (Vector3.Distance(existingPosition, position) < minDistanceBetweenClouds)
            {
                return false; // Too close to an existing cloud
            }
        }
        return true;
    }
}
