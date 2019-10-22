using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeTransparent : MonoBehaviour
{
    TextMeshProUGUI Text;
    public Color TransparentColor;

    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        changeOpacityTransparent();
    }

    void changeOpacityTransparent()
    {
        Text.color = Color.Lerp(Text.color, TransparentColor, 1.4f * Time.deltaTime);
    }

}
