using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Game Event", menuName = "GameEvent", order = 2)]
public class RandomEvent : ScriptableObject
{
    public GameObject eventPrefab;
    public Vector2 spawnRate;
}
