using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}