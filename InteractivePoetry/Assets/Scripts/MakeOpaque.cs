using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeOpaque : MonoBehaviour
{
    TextMeshProUGUI Text;
    public Color OpaqueColor;


    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        changeOpacityOpaque();
    }

    void changeOpacityOpaque()
    {
        Text.color = Color.Lerp(Text.color, OpaqueColor, 1.4f * Time.deltaTime);
    }

}
