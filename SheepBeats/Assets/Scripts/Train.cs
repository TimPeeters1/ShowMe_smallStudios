using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Train : MonoBehaviour
{
    public float Speed = 1f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInParent<Sheep>())
        {
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, Speed);
    }
}
