using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown_Project
{
    public class GameUI : BaseUI
    {
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private Slider hpSlider;

        protected override UIState GetUIState()
        {
            return UIState.Game;
        }

        private void Start()
        {
            UpdateHPSlider(1);
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
        }

        internal void UpdateWaveText(int wave)
        {
            waveText.text = wave.ToString();
        }

        internal void UpdateHPSlider(float percentage)
        {
            hpSlider.value = percentage;
        }
    }
}