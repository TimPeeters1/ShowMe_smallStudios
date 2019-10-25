using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Singleton
    public static TileManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Space]
    [Header("Biome Options/Info")]
    [SerializeField] double tileNumber;
    [SerializeField] Biome[] biomes;
    [SerializeField] Biome currentBiome;
    int curBiomeSize;

    //Script modified by Tim
    ///Used tutorial 'Unity Endless Tutorial' Episode 6 and 7 from N3K EN on YouTube 

    public GameObject[] tilePrefabs;
    private Transform spawnTransform;

    [Space]
    [SerializeField] float tileLength = 33f;

    [Space]
    [SerializeField] int tileOnScreenAmount = 5; //amount of tiles on screen

    private float h = 60f;
    private int lastPrefabIndex = 0;

    [Space]
    public List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        spawnTransform = transform;
        h = spawnTransform.position.x;
        h -= tileOnScreenAmount * tileLength - 1;

        for (int i = 0; i < tileOnScreenAmount; i++)
        {
            SpawnTileStart(new Vector3(h, 0, 0));
            h += tileLength;
        }
        
        currentBiome = biomes[0];
        tileNumber = 0;
        curBiomeSize = Mathf.RoundToInt(Random.Range(currentBiome.biomeSize.x, currentBiome.biomeSize.y));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(activeTiles[activeTiles.Count - 1].transform.position.x);
        if (activeTiles[activeTiles.Count - 1].transform.position.x <= 26)
        {
            SpawnTile();
            tileNumber++;
            if (activeTiles.Count - 1 == tileOnScreenAmount)
            {
                DeleteTile();
            }
        }

        if((tileNumber % curBiomeSize) == 0)
        {
            curBiomeSize = Mathf.RoundToInt(Random.Range(currentBiome.biomeSize.x, currentBiome.biomeSize.y));
            currentBiome = biomes[Random.Range(0, biomes.Length)];
        }
    }

    private void SpawnTile()
    {
        GameObject go = Instantiate(currentBiome.tiles[RandomPrefabIndex()]);
        //Debug.Log(go.GetComponent<Collider>().bounds.size.x);
        go.transform.SetParent(transform);
        go.transform.position = spawnTransform.position;
        activeTiles.Add(go);

        if (!go.GetComponent<TileScroll>())
            go.AddComponent<TileScroll>();
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void SpawnTileStart(Vector3 tilePosition)
    {
        GameObject go = Instantiate(currentBiome.tiles[RandomPrefabIndex()]);
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);

        if(!go.GetComponent<TileScroll>())
        go.AddComponent<TileScroll>();
    }

    private int RandomPrefabIndex()
    {
        if (currentBiome.tiles.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, currentBiome.tiles.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
