using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public AnimationCurve hopCurve;

    bool move = false;

    bool doJump;

    [SerializeField] float timer;

    public bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, -transform.up), out hit);

        if(Vector3.Distance(transform.position, hit.point) < 3)
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
        if (move && isGrounded())
        {
            transform.position += transform.forward * 0.07f;
        }
    }

    void FixedHop()
    {
        if (!doJump && isGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
            doJump = true;
        }

        doJump = false;
    }

    public void DoMove()
    {
        timer = 0.5f;

        move = true;
        FixedHop();
    }

    public void DoJump()
    {
       GetComponent<Rigidbody>().AddForce(Vector3.up * 20f, ForceMode.Impulse);
       GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
        
    }
} 
