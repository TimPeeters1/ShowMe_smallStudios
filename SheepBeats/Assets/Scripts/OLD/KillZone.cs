using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sheep>())
        {
            GameManagerOld.Instance.activeSheeps.Remove(other.GetComponent<SheepOld>());
            Destroy(other.gameObject);
        }
    }
}
