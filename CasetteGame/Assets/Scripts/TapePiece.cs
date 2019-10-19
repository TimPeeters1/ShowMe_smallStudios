using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TapePiece : MonoBehaviour
{
    [SerializeField] Color _color;

    [SerializeField] GameObject ParticleSystem;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Image>().color = _color;
        ParticleSystem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            collision.GetComponent<ICatchable>().DoCatch(_color);

            if (collision.GetComponent<FallThing>()._myColor == _color)
            {
                StartCoroutine(doParticle());
            }
        }
        catch 
        {

        }
    }

    IEnumerator doParticle()
    {
        ParticleSystem.SetActive(true);
        yield return new WaitForSeconds(1f);
        ParticleSystem.SetActive(false);
    }

}
