using UnityEngine;
using UnityEngine.UI;

namespace CustomColor 
{
    public class ImageColor : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private ColorOption _color;

        private void Start()
        {
            _image.color = ColorManager.Instance.GetColor(_color);
        }
    }

}