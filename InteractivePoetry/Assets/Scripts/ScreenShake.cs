using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform CameraTransform;

    private void Start()
    {
        CameraTransform = GetComponent<Transform>();
    }

    public void Screenshake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        //x = 0
        for (int i = 0; i < 10; i++)
        {
            CameraTransform.position += new Vector3(0.05f, 0, 0);
            yield return new WaitForSeconds(0.001f);
        }
        //x = 5
        for (int j = 0; j < 10; j++)
        {
            CameraTransform.position += new Vector3(-0.1f, 0, 0);
            yield return new WaitForSeconds(0.0003f);
        }
        //x = -5
        for (int h = 0; h < 10; h++)
        {
            CameraTransform.position += new Vector3(0.075f, 0, 0);
            yield return new WaitForSeconds(0.002f);
        }
        //x = 2.5
        for (int k = 0; k < 5; k++)
        {
            CameraTransform.position += new Vector3(-0.05f, 0, 0);
            yield return new WaitForSeconds(0.001f);
        }
        //x = 0
    }
}
