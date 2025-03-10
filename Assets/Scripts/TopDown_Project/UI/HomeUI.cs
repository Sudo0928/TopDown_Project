using UnityEngine;
using UnityEngine.UI;

namespace TopDown_Project
{
    public class HomeUI : BaseUI
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        public void OnClickStartButton()
        {
            GameManager.instance.StartGame();
        }

        public void OnClickExitButton()
        {
            Application.Quit();
        }

        protected override UIState GetUIState()
        {
            return UIState.Home;
        }
    }
}
