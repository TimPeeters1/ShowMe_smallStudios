using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScroll : MonoBehaviour
{
    private Transform tileTransform;
    public float scrollSpeed;
    //public float despawnPos;

    void Start()
    {
        tileTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tileTransform.position += new Vector3(-scrollSpeed, 0, 0);
    }
}
