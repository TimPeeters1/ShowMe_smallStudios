using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum button
{
    right,
    left
}

public class letterController : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 LeftAdd = new Vector2(-0.6f, 0);
    public Vector2 RightAdd = new Vector2(0.6f, 0);
    public RectTransform SpriteTransform;
    private bool isPressed = false;
    button curButton;

    private void Start()
    {
        SpriteTransform = GetComponent<RectTransform>();
    }
    public void PressLeftDown()
    {
        isPressed = true;
        curButton = button.left;
    }

    public void PressLeftUp()
    {
        isPressed = false;
    }

    public void PressRightDown()
    {
        isPressed = true;
        curButton = button.right;
    }

    public void PressRightUp()
    {
        isPressed = false;
    }

    public void Move()
    {
        if(isPressed == true)
        {
            if(curButton == button.left)
            {
                SpriteTransform.anchoredPosition += LeftAdd;
            }
            if (curButton == button.right)
            {
                SpriteTransform.anchoredPosition += RightAdd;
            }
        } 
    }

    private void Update()
    {
        Move();
    }
}
