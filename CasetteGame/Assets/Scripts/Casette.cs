using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum button
{
    right,
    left
}


public class Casette : MonoBehaviour
{
    [SerializeField] Vector2 tapeRestriction;

    [SerializeField] RectTransform tape;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    public float moveSpeed;

    [SerializeField] bool isPressed = false;
    [SerializeField] button curButton;

    public void leftDown() { isPressed = true; curButton = button.left; }
    public void leftUp() { isPressed = false;}

    public void rightDown() { isPressed = true; curButton = button.right; }
    public void rightUp() { isPressed = false;}

    public void doMove()
    {
        if (isPressed == true)
        {
            if (curButton == button.left)
            {
                Vector3 _newPos = new Vector3(tape.position.x - moveSpeed, tape.position.y, tape.position.z);
                _newPos.x = Mathf.Clamp(_newPos.x, tapeRestriction.x, tapeRestriction.y);
                tape.position = _newPos;
            }
            if(curButton == button.right)
            {
                Vector3 _newPos = new Vector3(tape.position.x + moveSpeed, tape.position.y, tape.position.z);
                _newPos.x = Mathf.Clamp(_newPos.x, tapeRestriction.x, tapeRestriction.y);
                tape.position = _newPos;
            }
        }

    }

    private void Update()
    {
        doMove();
    }


}
