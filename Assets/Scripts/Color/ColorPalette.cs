using System;
using UnityEngine;
using UnityEngine.Events;

namespace CustomColor
{
    [CreateAssetMenu]
    public class ColorPalette : ScriptableObject
    {
        public ColorType palette;

        public ColorPair[] colors;
    }

    public enum ColorType
    {
        Default, Accessible
    }

    public enum ColorOption
    {
        None, Red, Blue, Yellow, Purple
    }

    [Serializable]
    public class ColorPair
    {
        public ColorOption option;
        public Color color;
    }

    public class ColorUnityEvent : UnityEvent<Color> { }
}