using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Train : MonoBehaviour
{
    public float Speed = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Sheep>())
        {
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, Speed);
    }
}
