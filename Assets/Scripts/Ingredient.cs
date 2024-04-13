using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    protected enum Color {
        Orange,
        Blue,
        Red,
        Yellow,
        Purple,
        Green,
    }

    [SerializeField]
    protected Color color;
}
