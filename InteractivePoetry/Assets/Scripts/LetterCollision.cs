using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterCollision : MonoBehaviour
{
    public GameObject ActualText;
    public GameObject LeftActualText;
    public GameObject RightActualText;
    public GameObject NextFallLetter;
    public ScrollingScript MainText;
    public Collider2D ThisCollider;
    public BackgroundTimingScript BGTimer;
    public TouchControls Controls;
    public ScreenShake Shakey;
    public ScoreScript scoreScript;
    public int g;
    private bool FallisRight;

    public int Score;
    private float BackgroundZeroHundred;

    public AnimationClip animClip;
    public GameObject AnimatiePrefab;
    AnimatiePlayer player;

    private void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
        ThisCollider = GetComponent<Collider2D>();
        Shakey = FindObjectOfType<ScreenShake>();
        BGTimer = FindObjectOfType<BackgroundTimingScript>();
        MainText = FindObjectOfType<ScrollingScript>();
        player = FindObjectOfType<AnimatiePlayer>();
        Controls = FindObjectOfType<TouchControls>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThisCollider.enabled = false;
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "wrong")
        {
            //Debug.Log("kutt");
            FallisRight = false;
            Shakey.Screenshake();
        }

        if(collision.gameObject.tag == "right")
        {
            //Debug.Log("Fukk");
            FallisRight = true;
            BackgroundZeroHundred = BGTimer.VisBackgroundTimer.sizeDelta.y / 2080 * 100 * -1;
            ActualText.GetComponent<TextMeshProUGUI>().enabled = true;
            LeftActualText.GetComponent<TextMeshProUGUI>().enabled = false;
            RightActualText.GetComponent<TextMeshProUGUI>().enabled = false;
            scoreScript.Score += 10;
            if (BackgroundZeroHundred >= -10 && BackgroundZeroHundred < -5)
            {
                scoreScript.Score += 10;
            }
            else if (BackgroundZeroHundred >= -5 && BackgroundZeroHundred < 0)
            {
                scoreScript.Score += 20;
            }
            else if (BGTimer.VisBackgroundTimer.sizeDelta.y == 0)
            {
                scoreScript.Score += 2;
            }
            else
            {
                scoreScript.Score += 10 + Mathf.FloorToInt(BackgroundZeroHundred/10);
            }
        }

        NextFallLetter.SetActive(true);
        NextFallLetter.GetComponent<TouchControls>().enabled = false;

        StartCoroutine(Animatie());
    }

    private void Update()
    {
        BackgroundZeroHundred = BGTimer.VisBackgroundTimer.sizeDelta.y / 2080 * 100 * -1;
        Debug.Log(BackgroundZeroHundred);
    }

    IEnumerator Animatie()
    {
        if (FallisRight == true)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            //Debug.Log(g);
            g++;
            StartCoroutine(AnimatiePrefab.GetComponent<AnimatiePlayer>().playAnim(animClip));
            yield return new WaitForSeconds(1.2f);
            MainText.LineBool[g] = true;
            NextFallLetter.GetComponent<TouchControls>().enabled = true;
            yield return new WaitForSeconds(1.5f);
            NextFallLetter.GetComponent<Collider2D>().enabled = true;
            this.gameObject.SetActive(false);
        }

        if (FallisRight == false)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            //Debug.Log(g);
            g++;
            yield return new WaitForSeconds(1.2f);
            MainText.LineBool[g] = true;
            NextFallLetter.GetComponent<TouchControls>().enabled = true;
            yield return new WaitForSeconds(1.5f);
            NextFallLetter.GetComponent<Collider2D>().enabled = true;
            this.gameObject.SetActive(false);
        }

    }
}
