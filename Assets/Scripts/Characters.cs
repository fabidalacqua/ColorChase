using System;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public Character[] list;

    private void Awake()
    {
        foreach (Character c in list)
            c.available = true;
    }
}

[Serializable]
public class Character
{
    public Sprite sprite;

    public bool available;
}
