using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Graphics
{
    public class EndLevelUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameObject winUI;
        [SerializeField] private Image overlay;

        #endregion
        
        #region Unity Methods

        public void GameOver()
        {
            overlay.enabled = true;
            gameOverUI.SetActive(true);
        }

        public void CompleteLevel()
        {
            overlay.enabled = true;
            winUI.SetActive(true);
        }

        public void HideUI()
        {
            overlay.enabled = false;
            winUI.SetActive(false);
            gameOverUI.SetActive(false);
        }

        #endregion
    }
}