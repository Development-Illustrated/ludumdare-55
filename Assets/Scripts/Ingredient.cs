using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum OrbColor {
        Orange,
        Blue,
        Red,
        Yellow,
        Purple,
        Green,
    }

    [SerializeField]
    protected OrbColor color;
}
