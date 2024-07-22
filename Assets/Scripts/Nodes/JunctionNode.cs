using System;
using Graphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Nodes
{
    public class JunctionNode : MonoBehaviour, INode, IPointerClickHandler
    {
        #region Fields

        [SerializeField] private Sprite leftSprite;
        [SerializeField] private Sprite rightSprite;
        [SerializeField] private Image image;
        [SerializeField] private bool isTurnedRight;

        #endregion
        
        #region Unity Methods

        private void ChangeDirection()
        {
            isTurnedRight = !isTurnedRight;
            UpdateSprite();
            GraphManager.Instance.ChangeDirection(this);
        }

        private void UpdateSprite()
        {
            image.sprite = isTurnedRight ? rightSprite : leftSprite;
        }

        #endregion
        
        #region Callbacks

        private void Awake()
        {
            UpdateSprite();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ChangeDirection();
        }

        #endregion
    }
}