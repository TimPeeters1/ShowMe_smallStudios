using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    float timer;

    bool move = false;

    public bool inRange;

    public bool isJumping;

    public bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, -transform.up), out hit);


        if (Vector3.Distance(transform.position, hit.point) < 2.5f)
        {
            isJumping = false;
            return true;
        }
        else
        {
            return false;
        }

        return false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        { 
            move = false;
            timer = 0;
        }
    }

    public void DoJump()
    {
        isJumping = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 25f, ForceMode.Impulse);

        if (inRange)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
        }       
    }
}
