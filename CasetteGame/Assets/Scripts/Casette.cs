using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casette : MonoBehaviour
{
    [SerializeField] Vector2 tapeRestriction;

    [SerializeField] RectTransform tape;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    public float maxMoveSpeed;
    [SerializeField] float moveSpeed;

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
            moveSpeed += 0.0005f;

            moveSpeed = Mathf.Clamp(moveSpeed, 0f, maxMoveSpeed);


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
        else
        {
            moveSpeed = 0f;
        }

    }

    private void Update()
    {
        getInput();
        doMove();
    }

    void getInput()
    {
        RaycastHit hit;

        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<CasetteButton>())
                {
                    isPressed = true;
                    curButton = hit.collider.GetComponentInParent<CasetteButton>().thisButton;
                }
            }
            else
            {
                isPressed = false;
            }
        }
        else
        {
            isPressed = false;
        }
    }


}
