using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private float ZeroPoint;
    private float RelativePos;
    private bool FirstPhasePress = true;
    private int TimerValue;
    private float NewPos;
    public bool StartGame;
    public bool DropCooldown = true;
    public GameObject TutorialSprite1;
    public GameObject TutorialSprite2;
    public ScrollingScript MainText;
    public GameObject AnimatiePrefabBegin;
    public AnimationClip animClipBegin;
    private bool NoDrop = true;

    public Transform spriteTransform;

    private void Start()
    {
        MainText = FindObjectOfType<ScrollingScript>();
        spriteTransform = GetComponent<Transform>();
        //Debug.Log("New Lead");
        //StartCoroutine(StartDropCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            if (TimerValue <= 8 && TimerValue > 0 && DropCooldown == false && NoDrop == false)
            {
                //Debug.LogWarning("Drop");
                //Debug.Log("1");
                MainText.DropLine = true;
                //Debug.Log("2");
                StartCoroutine(StartDropCooldown());
                //Debug.Log("3");
                DropCooldown = true;
                //Debug.Log("4");
            }
            if (StartGame == false)
            {
                NoDrop = false;
            }
            FirstPhasePress = true;
            TimerValue = 0;
        }
        //Debug.Log(DropCooldown);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (FirstPhasePress == true)
            {
                StartCoroutine(Timer());
                ZeroPoint = touchPosition.x;
                FirstPhasePress = false;
            }

            if (StartGame == true)
            {
                StartCoroutine(StartGameRoutine());
            }

            if (TimerValue >= 5)
            {
                RelativePos = touchPosition.x - ZeroPoint;
                NewPos = (RelativePos * 0.5f) + spriteTransform.position.x;
                ZeroPoint = touchPosition.x;
                spriteTransform.position = new Vector3(NewPos, spriteTransform.position.y, spriteTransform.position.z);
            }
        }
        //Debug.Log(DropCooldown);
    }

    IEnumerator Timer()
    {
        while(Input.touchCount > 0)
        {
            TimerValue++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator StartDropCooldown()
    {
        //Debug.Log("before");
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("after");
        DropCooldown = false;
        //Debug.Log("afterafter");
    }

    IEnumerator StartGameRoutine()
    {   
        TutorialSprite1.SetActive(false);
        TutorialSprite2.SetActive(false);
        StartCoroutine(AnimatiePrefabBegin.GetComponent<AnimatiePlayer>().playAnim(animClipBegin));
        yield return new WaitForSeconds(0.8f);
        //Debug.LogWarning("WAAAAHHH");
        MainText.LineBool[0] = true;
        StartCoroutine(StartDropCooldown());
        StartGame = false;
    }
}
