using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class JumpArea : MonoBehaviour
{
    Sheep currentSheep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sheep>())
        {
            currentSheep = other.GetComponent<Sheep>();

            GameManager.Instance.currentScore += 10;
        }
    }

}
