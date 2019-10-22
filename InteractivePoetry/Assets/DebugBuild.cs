using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBuild : MonoBehaviour
{
    public GameObject Maintext;
    private RectTransform Maintexttransf;
    public Text DebugText;

    void Start()
    {
        DebugText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Maintexttransf = Maintext.GetComponent<RectTransform>();
        DebugText.text = Maintexttransf.anchoredPosition.y.ToString("F2");
    }
}
