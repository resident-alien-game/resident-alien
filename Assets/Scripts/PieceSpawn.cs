using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawn : MonoBehaviour
{
    public GameObject piecePrefab;
    public int pieceCount = 9;
    public float groundSize = 100f;

    private List<Vector3> piecePositions = new List<Vector3>();
    private List<GameObject> pieces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnPiece();
    }

    void SpawnPiece()
    {
        for (int i = 0; i < pieceCount; i++)
        {
            Vector3 spawnPosition = GetValidRandomPosition();
            GameObject newPiece = Instantiate(piecePrefab, spawnPosition, Quaternion.identity);
            pieces.Add(newPiece);
            piecePositions.Add(spawnPosition);
        }
    }

    Vector3 GetValidRandomPosition()
    {
        Vector3 randomPosition;
        do{
            // Generate a random position within the groundSize
            randomPosition = new Vector3(Random.Range(-groundSize / 2f, groundSize / 2f), 1f, Random.Range(-groundSize / 2f, groundSize / 2f));
        } while (!IsPositionValid(randomPosition));

        return randomPosition;
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 piecePosition in piecePositions)
        {
            if (Vector3.Distance(position, piecePosition) < 25f)
            {
                return false;
            }
        }
        return true;
    }

    public List<Vector3> GetPiecePositions()
    {
        return piecePositions;
    }

    public GameObject GetPieceAtPosition(Vector3 position)
    {
        foreach (GameObject piece in pieces)
        {
            if (piece != null && piece.transform.position == position)
            {
                return piece;
            }
        }
        return null;
    }

}
