using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;
    private List<GameObject> activeTiles;
    private Transform PlayerTransform;
    private float SpawnZ = 0.0f; 
    private float TileLength = 50.0f;
    private float safeZone = 35.0f;
    private int NoOfTiles = 5;
    private int lastPrefabIndex = 0;

    void Start()
    {
        activeTiles = new List<GameObject>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < NoOfTiles; i++)
        {
            if (i < 1)
                SpawnTile(0);
            else
                SpawnTile();
            
        }
    }
    void Update()
    {
        if(PlayerTransform.position.z - safeZone > (SpawnZ - NoOfTiles * TileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject tile;
        if (prefabIndex == -1)
            tile = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            tile = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        tile.transform.SetParent(transform);
        tile.transform.position = Vector3.forward * SpawnZ;
        SpawnZ += TileLength;
        activeTiles.Add(tile);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
