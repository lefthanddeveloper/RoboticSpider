using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoboticSpider.DevLog
{
    public class FeatureButtonObj : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button _button;
        public Button button => _button;
        [SerializeField] private int _buttonIndex; //0: MOVE
                                                  //1: ANIMATION
                                                  //2: RENDER TEXTURE
                                                  //3: INTERACTION
                                                  //4: RAYCAST
        public int buttonIndex => _buttonIndex;

        private Color activeColor;
        private Color inactiveColor;

        public void SetColor(Color _activeColor, Color _inactiveColor)
        {
            activeColor = _activeColor;
            inactiveColor = _inactiveColor;
        }

        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //     image.color = activeColor;
        // }

        // public void OnPointerExit(PointerEventData eventData)
        // {
        //     image.color = inactiveColor;
        // }

        public void ChangeImageColor(Color _color)
        {
            image.color = _color;
        }
    }
}

