using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICatchable
{
    void DoCatch(Color color);
}

public class FallThing : MonoBehaviour, ICatchable
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] int points;
    public float moveSpeed;

    public Color _myColor;
    [SerializeField] UnityEngine.UI.Image trail;

    void Start()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        GetComponent<UnityEngine.UI.Image>().color = _myColor;

        trail.color = new Color(_myColor.r, _myColor.g, _myColor.b, 0.3f);
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
            GameManager.Instance.objects.Remove(this);
            StartCoroutine(GameManager.Instance.CameraShake(0.4f, 0.01f));
            Destroy(this.gameObject, 3f);
        }
    }

}
