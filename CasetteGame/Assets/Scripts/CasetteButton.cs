using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum button
{
    right,
    left
}

[RequireComponent(typeof(BoxCollider))]
public class CasetteButton : MonoBehaviour
{
    public button thisButton;
}
