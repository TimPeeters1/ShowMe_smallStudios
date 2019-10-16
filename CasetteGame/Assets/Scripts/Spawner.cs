using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    Color[] possibleColors = { new Color(255f, 0f, 0f, 255f)/255, new Color(255f, 0f, 255f, 255f)/255, new Color(0f, 255f, 0f, 255f)/255};

    Canvas canvas;

    [SerializeField] GameObject spawnPrefab;

    [SerializeField] Vector2 timing;

    public float enemySpeed;

    Collider2D col;

    Vector3 getSpawnPos(Collider2D col)
    {
        Vector3 _spawnPos = col.transform.position + new Vector3(Random.Range(-col.bounds.extents.x, col.bounds.extents.x), Random.Range(-col.bounds.extents.y, col.bounds.extents.y));

        return _spawnPos;
    }


    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        col = GetComponent<BoxCollider2D>();
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        GameObject spawn = Instantiate(spawnPrefab, getSpawnPos(col), spawnPrefab.transform.rotation, canvas.transform);
        int _random = Random.Range(0, 3);
        spawn.GetComponent<FallThing>()._myColor = possibleColors[_random];
        spawn.GetComponent<FallThing>().moveSpeed = enemySpeed;

        GameManager.Instance.objects.Add(spawn.GetComponent<FallThing>());


        yield return new WaitForSeconds(Random.Range(timing.x, timing.y));

        StartCoroutine(spawnEnemy());
    }

}
