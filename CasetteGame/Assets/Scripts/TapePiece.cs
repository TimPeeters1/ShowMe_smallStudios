using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TapePiece : MonoBehaviour
{
    [SerializeField] Color _color;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Image>().color = _color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            collision.GetComponent<ICatchable>().DoCatch(_color);
        }
        catch 
        {

        }
    }

}
