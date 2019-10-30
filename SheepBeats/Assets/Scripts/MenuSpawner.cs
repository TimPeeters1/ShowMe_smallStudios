using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{

    [Header("Spawn Prefab")]
    public GameObject prefab;

    [Space]
    public List<SheepOld> activeSheeps;

    [Space]
    [Header("Spawn Settings")]
    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnInterval;
    [SerializeField] float walkInterval;

    void Start()
    {
        InvokeRepeating("SpawnSheep", 0f, spawnInterval);

        InvokeRepeating("UpdateRow", 0f, walkInterval);
    }

    void SpawnSheep()
    {
        GameObject sheep = Instantiate(prefab, spawnPosition.position, spawnPosition.rotation);
        activeSheeps.Add(sheep.GetComponent<SheepOld>());
    }

    void UpdateRow()
    {
        for (int i = 0; i < activeSheeps.Count; i++)
        {
            activeSheeps[i].StartCoroutine("DoMove");
        }
    }
}
