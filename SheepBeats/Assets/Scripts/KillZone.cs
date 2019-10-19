using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sheep>())
        {
            GameManager.Instance.activeSheeps.Remove(other.GetComponent<Sheep>());
            Destroy(other.gameObject);
        }
    }
}
