using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Used tutorial 'Unity Endless Tutorial' Episode 6 and 7 from N3K EN on YouTube

    public GameObject[] tilePrefabs;
    private Transform spawnTransform;
    private float tileLength = 35f;
    private int amnTilesOnScreen = 5;
    private float h = 60f;
    private int lastPrefabIndex = 0;

    public List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        spawnTransform = GetComponent<Transform>();
        h = spawnTransform.position.x - amnTilesOnScreen * tileLength;
        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            SpawnTileStart(new Vector3(h, 0, -30));
            h += tileLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(activeTiles[activeTiles.Count - 1].transform.position.x);
        if (activeTiles[activeTiles.Count - 1].transform.position.x <= 25)
        {
            SpawnTile();
            if (activeTiles.Count - 1 == amnTilesOnScreen)
            {
                DeleteTile();
            }
        }
    }

    private void SpawnTile()
    {
        GameObject go;
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent (transform);
        go.transform.position = spawnTransform.position;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void SpawnTileStart(Vector3 tilePosition)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
