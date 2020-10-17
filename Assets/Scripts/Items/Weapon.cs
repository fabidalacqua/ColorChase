using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField]
    private char color;
    public char Color{
        get { return color; }
        set { color = value; }
    }

    [SerializeField]
    private int quantity;
    public int Quantity{
        get { return quantity; }
        set { quantity = value; }
    }

}
