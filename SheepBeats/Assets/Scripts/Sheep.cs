using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public LayerMask IgnoreLayer;

    float timer;

    bool move = false;

    public bool inRange;

    public bool isJumping;

    Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, -transform.up), out hit, IgnoreLayer.value);

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

        anim.SetBool("isGrounded", isGrounded());
    }

    public void DoJump()
    {
        isJumping = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        if (inRange)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 30f, ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 25f, ForceMode.Impulse);
        }
    }
}
