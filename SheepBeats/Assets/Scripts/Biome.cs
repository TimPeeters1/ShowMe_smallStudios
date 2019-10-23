using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Biome", order = 1)]
public class Biome : ScriptableObject
{
    public string biomeName;
    public GameObject[] tiles;
    public Vector2 biomeSize;
}
