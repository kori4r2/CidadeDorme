using TMPro;
using UnityEngine;

namespace CidadeDorme {
    public class SeerTurnHandler : PlayerTurnHandler {
        [SerializeField] private PlayerVariable targetReference;
        [SerializeField] private GameObject rootObject;
        [SerializeField] private TextMeshProUGUI prompt;

        protected override bool IsValidActionTarget(Player target) {
            return target != currentPlayer;
        }

        public override string GetActionFeedback() {
            Player targetSelected = targetReference.Value == nullPlayer ? null : targetReference.Value;
            if (targetSelected != null)
                targetSelected.ShowClass();
            return targetSelected != null
                ? $"{currentPlayer.CharacterName} escolheu observar a casa de {targetSelected.CharacterName} antes de voltar a dormir."
                : $"{currentPlayer.CharacterName} escolheu dormir sem observar ninguém.";
        }

        public override void Hide() {
            rootObject.SetActive(false);
        }

        public override void Init() {
            Hide();
        }

        public override void ShowPlayerChoices(Player currentPlayer) {
            this.currentPlayer = currentPlayer;
            rootObject.SetActive(true);
            prompt.text = $"Quem {currentPlayer.CharacterName} escolhe observar esta noite?";
            SetupVoteButtons();
        }
    }
}
