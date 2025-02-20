using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TopDown_Project
{
    public class GameOverUI : BaseUI
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        public override void Init(UIManager uIManager)
        {
            base.Init(uIManager);
            restartButton.onClick.AddListener(OnClickRestartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        private void OnClickExitButton()
        {
            Application.Quit();
        }

        private void OnClickRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        protected override UIState GetUIState()
        {
            return UIState.GameOver;
        }
    }
}
