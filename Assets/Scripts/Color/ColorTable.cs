using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomColor
{
    [CreateAssetMenu]
    public class ColorTable : ScriptableObject
    {
        [SerializeField]
        private int _defaultDamage = 1;

        [SerializeField]
        private int _relativeDamage = 2;

        [SerializeField]
        private List<ColorRow> _colorRows;

        public int GetRelativeDamage(ColorOption myColor, ColorOption targetColor)
        {
            if (targetColor == ColorOption.None)
            {
                return _relativeDamage;
            }

            foreach (ColorRow row in _colorRows)
            {
                if (row.myColor == myColor)
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
        public ColorOption myColor;
        public ColorOption kill;
        public ColorOption dieFor;
    }
}