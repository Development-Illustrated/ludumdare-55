using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum OrbColor
{
    Orange,
    Blue,
    Red,
    Yellow,
    Purple,
    Green,
}


public class Ingredient : MonoBehaviour
{
    

    [SerializeField]
    public OrbColor color;
}
