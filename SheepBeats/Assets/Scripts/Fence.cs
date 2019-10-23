using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Fence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sheep>())
        {
            Debug.Log("Hit Fence");
            Destroy(other.gameObject);
        }
    }

}
