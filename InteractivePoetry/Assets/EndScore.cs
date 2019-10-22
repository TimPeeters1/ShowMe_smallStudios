using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{
    TextMeshProUGUI Text;
    public ScoreScript scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Final Score: " + scoreScript.Score;
    }
}
