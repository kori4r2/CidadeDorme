using System;
using UnityEngine;
using UnityEngine.UI;
using Toblerone.Toolbox;
using TMPro;

namespace CidadeDorme {
    public class TimerNumber : TimerUIElement {
        private const int minFontSize = 10;
        private const int maxFontSize = 44;
        [Space]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private FloatVariable timerThresholdVariable;
        [SerializeField, Range(minFontSize, maxFontSize)] private int smallFontSize;
        [SerializeField] private Color regularFontColor;
        [SerializeField, Range(minFontSize, maxFontSize)] private int largeFontSize;
        [SerializeField] private Color hurriedFontColor;
        private float lastTotalSecondsValue = int.MinValue;

        protected override void Awake() {
            smallFontSize = Mathf.Max(minFontSize, smallFontSize);
            largeFontSize = Mathf.Max(smallFontSize, largeFontSize);
            base.Awake();
        }

        protected override void UpdatedTimer(float newTimeSeconds) {
            int totalSeconds = Mathf.CeilToInt(Mathf.Max(0f, newTimeSeconds));
            if (totalSeconds == lastTotalSecondsValue)
                return;
            lastTotalSecondsValue = totalSeconds;
            FormatText(newTimeSeconds);
            int seconds = 0;
            int minutes = Math.DivRem(totalSeconds, 60, out seconds);
            textField.text = $"{minutes:00}:{seconds:00}";
        }

        private void FormatText(float newTimeSeconds) {
            bool shouldHurry = newTimeSeconds <= timerThresholdVariable.Value;
            textField.fontSize = shouldHurry ? largeFontSize : smallFontSize;
            textField.color = shouldHurry ? hurriedFontColor : regularFontColor;
        }

        protected override void Show() {
            backgroundImage.enabled = true;
            textField.enabled = true;
            lastTotalSecondsValue = int.MinValue;
            UpdatedTimer(timerVariable.Value);
            base.Show();
        }

        protected override void Hide() {
            backgroundImage.enabled = false;
            textField.enabled = false;
            lastTotalSecondsValue = int.MinValue;
            base.Hide();
        }
    }
}
