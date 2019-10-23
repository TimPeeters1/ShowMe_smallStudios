using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class JumpArea : MonoBehaviour
{
    Sheep  currentSheep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sheep>())
        {
            other.GetComponent<Sheep>().inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Sheep>())
        {
            other.GetComponent<Sheep>().inRange = false;
        }
    }

}
