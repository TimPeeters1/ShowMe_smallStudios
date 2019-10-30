using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrekkerEvent : MonoBehaviour, IEvent
{
    public float speed;

    GameObject player;
    Vector3 TargetDir;

    private void Start()
    {
        player = GameManager.Instance.player.gameObject;
    }

    private void Update()
    {
        MoveTarget();
    }

    void MoveTarget()
    {
        TargetDir = player.transform.position - transform.position;

        Vector3 rotateDir = Vector3.RotateTowards(transform.forward, TargetDir, Time.deltaTime, 0.0f * 4f);

        rotateDir = new Vector3(rotateDir.x, 0, rotateDir.z);
        Debug.DrawRay(transform.position, rotateDir, Color.blue);

        transform.rotation = Quaternion.LookRotation(rotateDir);

        transform.position += transform.forward * speed * GameManager.Instance.tileMoveSpeed * 0.8f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>())
        {
            if (other.GetComponentInParent<Sheep>() != null)
            {
                StartCoroutine(GameManager.Instance.GameOver());
            }
            else if (other.GetComponentInParent<TrekkerEvent>() != null)
            {
                //DO nothing
            }
            else
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Rigidbody>().AddForce(Vector3.up * 30f, ForceMode.Impulse);
                other.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
                Destroy(other.transform.parent.gameObject, 1.5f);
            }
        }
    }

    public void DisableEvent()
    {
        GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10f, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(Vector3.right * 3f, ForceMode.Impulse);
        Destroy(this.gameObject, 5);
    }
}

