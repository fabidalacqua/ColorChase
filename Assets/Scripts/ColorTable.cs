using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorTable : ScriptableObject
{
    [SerializeField]
    private int _defaultDamage = 1;

    [SerializeField]
    private int _relativeDamage = 3;

    [SerializeField]
    private List<ColorRow> _colorRows;

    public int GetRelativeDamage(PlayerColor playerColor, PlayerColor targetColor)
    {
        foreach (ColorRow row in _colorRows)
        {
            if (row.myColor == playerColor)
            {
                if (row.kill == targetColor)
                {
                    return _relativeDamage;
                }
                else
                {
                    return _defaultDamage;
                }
            }
        }
        return _defaultDamage;
    }
}

[Serializable]
public class ColorRow
{
    public PlayerColor myColor;
    public PlayerColor kill;
    public PlayerColor dieFor;
}

public enum PlayerColor 
{
    None = 'n',
    Red = 'r', 
    Blue = 'b', 
    Yellow = 'y', 
    Purple = 'p' 
}