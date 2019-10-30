using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoEvent : MonoBehaviour, IEvent
{
    public float speed;

    [SerializeField] GameObject chargeParticle;
    [SerializeField] LineRenderer laser;

    [SerializeField] float distance;

    GameObject player;
    Vector3 TargetDir;

    bool firing;

    Coroutine routine;

    private void Start()
    {
        player = GameManager.Instance.player.gameObject;

        laser.enabled = false;
        chargeParticle.SetActive(false);

        firing = false;
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        laser.SetPosition(0, chargeParticle.transform.position);

        MoveTarget();

        if(distance < 25 && !firing)
        {
            routine = StartCoroutine(doLaser());
        }

    }

    void MoveTarget()
    {
        transform.GetChild(0).Rotate(0, 15f * Time.deltaTime, 0);

        TargetDir = player.transform.position - transform.position;

        Vector3 rotateDir = Vector3.RotateTowards(transform.forward, TargetDir, Time.deltaTime, 0.0f * 4f);

        rotateDir = new Vector3(rotateDir.x, 0, rotateDir.z);
        Debug.DrawRay(transform.position, rotateDir * 10f, Color.blue);

        transform.rotation = Quaternion.LookRotation(rotateDir);

        transform.position += transform.forward * speed * 0.1f;
    }

    IEnumerator doLaser()
    {
        firing = true;
        chargeParticle.SetActive(true);
        laser.SetPosition(1, chargeParticle.transform.position);
        laser.SetPosition(0, chargeParticle.transform.position);

        yield return new WaitForSeconds(5);

        laser.enabled = true;
        laser.SetPosition(1, player.transform.position);
        StartCoroutine(GameManager.Instance.GameOver());

        yield return new WaitForSeconds(2);
        laser.enabled = false;
        chargeParticle.SetActive(false);
    }

    public void DisableEvent()
    {
        GetComponent<Rigidbody>().useGravity = true;
        StopCoroutine(routine);
        Destroy(this.gameObject, 3);
    }
}

