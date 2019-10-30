using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;

    [SerializeField] RandomEvent[] events;
    [Space]
    [SerializeField] RandomEvent curEvent;

    [Space]
    [SerializeField] float waitTime;

    GameObject curEventObject;

    private void Start()
    {
        curEvent = events[Random.Range(0, events.Length)];
        waitTime = Random.Range(curEvent.spawnRate.x, curEvent.spawnRate.y);

        StartCoroutine(PerformEvent());
    }

    IEnumerator PerformEvent()
    {
        yield return new WaitForSeconds(waitTime);

        TriggerEvent();
    }

    public void TriggerEvent()
    {
        StopCoroutine(PerformEvent());
        curEventObject = Instantiate(curEvent.eventPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)].position, curEvent.eventPrefab.transform.rotation);

        curEvent = events[Random.Range(0, events.Length)];
        waitTime = Random.Range(curEvent.spawnRate.x, curEvent.spawnRate.y);
        StartCoroutine(PerformEvent());
    }

}
