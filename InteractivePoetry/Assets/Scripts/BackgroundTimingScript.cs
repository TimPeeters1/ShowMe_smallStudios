using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTimingScript : MonoBehaviour
{
    public ScrollingScript MainText;
    public RectTransform VisBackgroundTimer;
    private float LerpTime = 0f;
    public float[] TimeValue;
    public bool[] CoroutineStarted;
    private IEnumerator CurrentCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < CoroutineStarted.Length; i++)
        {
            CoroutineStarted[i] = false;
        }
        VisBackgroundTimer = GetComponent<RectTransform>();
        MainText = FindObjectOfType<ScrollingScript>();
        CurrentCoroutine = Filler();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < MainText.LineBool.Length - 1; i++)
        {
            if (MainText.LineBool[i] == true)
            {
                if (CoroutineStarted[i] == false)
                {
                    StopCoroutine(CurrentCoroutine);
                    //Debug.Log("stopped" + i);
                    LerpTime = 0;
                    CurrentCoroutine = LerpTimer(i);
                    StartCoroutine(CurrentCoroutine);
                    //Debug.Log("ayy" + i);
                    CoroutineStarted[i] = true;
                }
            }
        }

        //Debug.Log(CurrentCoroutine);

        float Lerp = Mathf.Lerp(2080, 0f, LerpTime);
        VisBackgroundTimer.sizeDelta = new Vector2(VisBackgroundTimer.sizeDelta.x, Lerp);
        //Debug.Log(LerpTime);
    }

    IEnumerator LerpTimer(int bb)
    {
        VisBackgroundTimer.sizeDelta = new Vector2(1080, 2080);
        for (float j = 0f; j <= 1.05f; j += (Time.deltaTime / TimeValue[bb]) * 1.5f)
        {
            //Debug.Log(bb);
            LerpTime = j;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //Debug.Log("Routine" + bb + "done");
    }

    IEnumerator Filler()
    {
        //Big Yeet
        yield return null;
    }
}
