using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    float timer;

    bool move = false;

    bool doHop;

    public bool isJumping;

    public bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, -transform.up), out hit);

        if (Vector3.Distance(transform.position, hit.point) < 2)
        {
            return true;
        }
        else
        {
            return false;
        }
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


    void FixedUpdate()
    {
        if (move && !isJumping)
        {
            transform.position += transform.forward * 0.07f;
        }
    }

    void FixedHop()
    {
        if (!doHop && !isJumping)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
            doHop = true;
        }

        doHop = false;
    }

    public void DoMove()
    {
        timer = 0.4f;

        move = true;
        FixedHop();
    }

    public void DoJump()
    {
        isJumping = true;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 25f, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
    }
}
