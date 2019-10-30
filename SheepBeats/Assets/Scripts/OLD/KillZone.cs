using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    MenuSpawner mgr;

    private void Start()
    {
        mgr = FindObjectOfType<MenuSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<SheepOld>())
        {
            mgr.activeSheeps.Remove(other.GetComponentInParent<SheepOld>());
            Destroy(other.gameObject);
        }
    }
}
