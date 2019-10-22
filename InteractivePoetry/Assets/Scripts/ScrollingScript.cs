using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    public float ScrollSpeed = 0.8f;
    public int UpScrollSpeed = 1000;
    public float UpScrollInterval = 0.1f;
    public Vector2 MainTextPos;
    public RectTransform MainTextTransform;
    public GameObject EndScreen;

    public GameObject[] LineList;

    public bool[] LineBool;
    public bool DropLine;

    void Start()
    {
        MainTextTransform = GetComponent<RectTransform>();
        MainTextPos = MainTextTransform.position;
        LineBool = new bool[LineList.Length];
    }
    
    void Update()
    {
        //Debug.Log(DropLine);
        //Debug.Log(MainTextTransform.anchoredPosition.y);

        if (MainTextTransform.anchoredPosition.y < 100 && LineBool[0] == true && DropLine == false)
        {
            MakeTransparent[] listT = LineList[0].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO = LineList[1].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 100 && LineBool[0] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(100));
            DropLine = false;
        }


        if (MainTextTransform.anchoredPosition.y < 400 && LineBool[1] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[0].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[1].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[1].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[2].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 400 && LineBool[1] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(399));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y >= 400)
        {
            MakeTransparent[] listT = LineList[1].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[2].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 700 && LineBool[2] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[1].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[2].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[2].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[3].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 700 && LineBool[2] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(699));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y >= 700)
        {
            MakeTransparent[] listT = LineList[2].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[3].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 1000 && LineBool[3] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[2].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[3].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[3].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[4].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 1000 && LineBool[3] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(999));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y >= 1000)
        {
            MakeTransparent[] listT = LineList[3].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[4].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 1300 && LineBool[4] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[3].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[4].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[4].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[5].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 1300 && LineBool[4] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(1299));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y >= 1300)
        {
            MakeTransparent[] listT = LineList[4].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[5].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 1600 && LineBool[5] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[4].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[5].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[5].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[6].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 1600 && LineBool[5] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(1599));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y == 1600)
        {
            MakeTransparent[] listT = LineList[5].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[6].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 1900 && LineBool[6] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[5].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[6].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[6].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[7].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 1900 && LineBool[6] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(1899));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y >= 1900)
        {
            MakeTransparent[] listT = LineList[6].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[7].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 2200 && LineBool[7] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[6].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[7].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[7].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            MakeOpaque[] listO2 = LineList[8].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO2)
            {
                opaque.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y < 2200 && LineBool[7] == true && DropLine == true)
        {
            StartCoroutine(UpScroller(2199));
            DropLine = false;
        }
        if (MainTextTransform.anchoredPosition.y >= 2200)
        {
            MakeTransparent[] listT = LineList[7].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO = LineList[8].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO)
            {
                opaque.enabled = false;
            }
        }


        if (MainTextTransform.anchoredPosition.y < 2250 && LineBool[8] == true && DropLine == false)
        {
            MakeTransparent[] listT0 = LineList[7].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT0)
            {
                transparent.enabled = false;
            }

            MakeOpaque[] listO1 = LineList[8].GetComponentsInChildren<MakeOpaque>();
            foreach (MakeOpaque opaque in listO1)
            {
                opaque.enabled = false;
            }

            MakeTransparent[] listT1 = LineList[8].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT1)
            {
                transparent.enabled = true;
            }

            Scroller();
        }
        if (MainTextTransform.anchoredPosition.y >= 2250)
        {
            MakeTransparent[] listT = LineList[8].GetComponentsInChildren<MakeTransparent>();
            foreach (MakeTransparent transparent in listT)
            {
                transparent.enabled = false;
            }

            EndScreen.SetActive(true);
        }

    }

    public void LeftButtonPressed()
    {
        LineBool[0] = true;
    }

    public void RightButtonPressed()
    {
        LineBool[0] = true;
    }

    public void Scroller()
    {
        MainTextPos = new Vector2(0, ScrollSpeed);
        MainTextTransform.anchoredPosition += MainTextPos;
    }

    IEnumerator UpScroller(int NewPosition)
    {
        for (int i = 0; i < 10; i++)
        {
            MainTextPos = new Vector2(0, 1f);
            MainTextTransform.anchoredPosition -= MainTextPos;
            yield return new WaitForSeconds(0.01f);
        }
        MainTextTransform.anchoredPosition = new Vector2(0, NewPosition);
        yield return new WaitForSeconds(0.1f);
        MainTextTransform.anchoredPosition += new Vector2(0, 1f);
    }

}
