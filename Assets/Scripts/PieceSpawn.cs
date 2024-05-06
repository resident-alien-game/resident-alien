using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs; // List of unique GameObjects to spawn
    public float groundSize = 100f; // Size of the spawning area
    public float minDistance = 10f; // Minimum distance between spawned objects

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // Ensure unique positions
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Store spawned objects

    void Start()
    {
        if (objectPrefabs == null || objectPrefabs.Count == 0)
        {
            Debug.LogError("No objects specified for spawning.");
            return;
        }

        if (objectPrefabs.Count > 0)
        {
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            Vector3 spawnPosition = GetUniqueRandomPosition();
            GameObject prefabToSpawn = objectPrefabs[i]; // Get the unique object to spawn
            GameObject newObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            spawnedObjects.Add(newObject);
            occupiedPositions.Add(spawnPosition);
        }
    }

    Vector3 GetUniqueRandomPosition()
    {
        Vector3 randomPosition;
        int maxAttempts = 100; // Avoid infinite loops
        int attempt = 0;

        do
        {
            randomPosition = new Vector3(
                Random.Range(-groundSize / 2f, groundSize / 2f),
                1f,
                Random.Range(-groundSize / 2f, groundSize / 2f)
            );

            attempt++;
            if (attempt > maxAttempts)
            {
                Debug.LogWarning("Could not find a unique position.");
                break;
            }
        } while (!IsPositionUnique(randomPosition));

        return randomPosition;
    }

    bool IsPositionUnique(Vector3 position)
    {
        foreach (var occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < minDistance)
            {
                return false;
            }
        }

        return true;
    }

    public List<GameObject> GetSpawnedObjects()
    {
        return spawnedObjects;
    }
}
