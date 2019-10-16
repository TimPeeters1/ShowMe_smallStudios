using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICatchable
{
    void DoCatch(Color color);
}

public class FallThing : MonoBehaviour, ICatchable
{
    [SerializeField] int points;
    public float moveSpeed;

    public Color _myColor;

    void Start()
    {
        GetComponent<UnityEngine.UI.Image>().color = _myColor;
    }

    void Update()
    {
        moveDown();
    }

    void moveDown()
    {
        transform.position += Vector3.down * moveSpeed * 0.01f;
    }

    public void DoCatch(Color color)
    {
        if (color == _myColor)
        {
            GameManager.Instance.addScore(points);
            GameManager.Instance.objects.Remove(this);
            Destroy(this.gameObject);
        }
        else
        {
            //GameManager.Instance.Damage();
            StartCoroutine(GameManager.Instance.CameraShake(.1f, .01f));
            GameManager.Instance.objects.Remove(this);
            Destroy(this.gameObject, 2);
        }
    }

}
