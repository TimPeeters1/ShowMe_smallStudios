using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.position += transform.right * speed;
    }
}
