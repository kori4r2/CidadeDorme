using System;
using System.Collections;
using TMPro;
using Toblerone.Toolbox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CidadeDorme {
    public class SettingsEditView : MonoBehaviour {
        [SerializeField] private MatchSettings currentSettings;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button firstSelectedButton;
        [SerializeField] private Gradient balanceColorGradient;
        [SerializeField] private TextMeshProUGUI currentBalance;
        [Header("Gambiarra")]
        [SerializeField] private BoolVariable canSelectBackButton;

        void Start() {
            currentSettings.AddClassesUpdatedListener(ShowBalanceScoreAndUpdateButton);
            currentSettings.AddPresetLoadListener(ShowBalanceScoreAndUpdateButton);
            ShowBalanceScoreAndUpdateButton();
        }

        public void GambiarraBackButtonSelected() {
            if (!canSelectBackButton.Value && startGameButton.interactable)
                StartCoroutine(GambiarraCorrotina());
        }

        private IEnumerator GambiarraCorrotina() {
            yield return new WaitForEndOfFrame();
            startGameButton.Select();
        }

        private void ShowBalanceScoreAndUpdateButton() {
            ShowBalanceScore();
            startGameButton.interactable = currentSettings.CanStartGame();
        }

        private void ShowBalanceScore() {
            int currentValue = currentSettings.GetCurrentClassesWeight();
            currentBalance.text = $"{(currentValue > 0 ? "+" : string.Empty)}{currentValue}";
            currentValue = Mathf.Clamp(currentValue, currentSettings.MinBalanceValue, currentSettings.MaxBalanceValue);
            float gradientKey = currentValue * 1.0f;
            if (gradientKey < 0)
                gradientKey /= currentSettings.MinBalanceValue * 1.0f;
            else if (gradientKey > 0)
                gradientKey /= currentSettings.MaxBalanceValue * 1.0f;
            currentBalance.color = balanceColorGradient.Evaluate(gradientKey);
        }

        public void ResetSelection() {
            firstSelectedButton.Select();
        }
    }
}
