using System;
using Unity.VisualScripting;
using UnityEngine;

namespace TopDown_Project
{
    public enum UIState
    {
        Home,
        Game,
        GameOver
    }

    public class UIManager : MonoBehaviour
    {
        private HomeUI homeUI;
        private GameUI gameUI;
        private GameOverUI gameOverUI;
        private UIState currentState;

        private void Awake()
        {
            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI.Init(this);

            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI.Init(this);

            gameOverUI = GetComponentInChildren<GameOverUI>(true);
            gameOverUI.Init(this);

            ChangeState(UIState.Home);
        }

        public void SetPlayGame()
        {
            ChangeState(UIState.Game);
        }

        public void SetGameOver()
        {
            ChangeState(UIState.GameOver);
        }

        public void ChangeWave(int waveIndex)
        {
            gameUI.UpdateWaveText(waveIndex);
        }

        public void ChangePlayerHP(float currentHP, float maxHP)
        {
            gameUI.UpdateHPSlider(currentHP / maxHP);
        }

        private void ChangeState(UIState state)
        {
            currentState = state;
            homeUI.SetActive(currentState);
            gameUI.SetActive(currentState);
            gameOverUI.SetActive(currentState);
        }
    }
}