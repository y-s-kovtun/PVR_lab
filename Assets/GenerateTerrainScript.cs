using UnityEngine;
using System.Collections.Generic;

public class GenerateTerrainScript : MonoBehaviour
{
    public GameObject[] terrainTiles; // ������ �������� ��� ���������
    public Transform player;          // ������ �� ������ ��� ���������
    public float tileSize = 10f;      // ������ ������ (����� ���������)
    public float groundHeight = 0f;   // ������, �� ������� ����� �������������� ������
    public int gridSize = 5;          // ������ ����� (���������� ������ �� ����)

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // ��������� ������� ������� ������
    private Vector3 lastPlayerPosition; // ��� ������������ �������� ������

    void Start()
    {
        lastPlayerPosition = player.position;
        GenerateInitialTiles();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, lastPlayerPosition) > tileSize)
        {
            lastPlayerPosition = player.position;
            GenerateSurroundingTiles();
        }
    }

    void GenerateInitialTiles()
    {
        // ��������� ������ � ����� ������ ��������� ������� ������
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int z = -gridSize; z <= gridSize; z++)
            {
                Vector3 spawnPos = new Vector3(player.position.x + x * tileSize, groundHeight, player.position.z + z * tileSize);
                SpawnTile(spawnPos);
            }
        }
    }

    void GenerateSurroundingTiles()
    {
        // �������� � ��������� ������ �� ����� ������ ������
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int z = -gridSize; z <= gridSize; z++)
            {
                Vector3 spawnPos = new Vector3(Mathf.Round((player.position.x + x * tileSize) / tileSize) * tileSize,
                                                groundHeight,
                                                Mathf.Round((player.position.z + z * tileSize) / tileSize) * tileSize);

                // ��������, ���������� �� ��� ������ �� ���� �������
                if (!occupiedPositions.Contains(spawnPos))
                {
                    SpawnTile(spawnPos);
                }
            }
        }
    }

    void SpawnTile(Vector3 position)
    {
        GameObject tile = Instantiate(terrainTiles[Random.Range(0, terrainTiles.Length)], position, Quaternion.identity);
        occupiedPositions.Add(position); // ��������� ������� � �������
    }
}
