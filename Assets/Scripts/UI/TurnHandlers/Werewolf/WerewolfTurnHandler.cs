using TMPro;
using UnityEngine;

namespace CidadeDorme {
    public class WerewolfTurnHandler : PlayerTurnHandler {
        [SerializeField] private NightChoices nightChoices;
        [SerializeField] private PlayerVariable targetReference;
        [SerializeField] private GameObject rootObject;
        [SerializeField] private TextMeshProUGUI prompt;

        protected override bool IsValidActionTarget(Player target) {
            return !(target.PlayerClass.Team is WerewolvesTeam);
        }

        public override string GetActionFeedback() {
            Player targetSelected = targetReference.Value == nullPlayer ? null : targetReference.Value;
            nightChoices.AttackPlayer(targetSelected);
            return targetSelected != null
                ? $"{currentPlayer.CharacterName} escolheu atacar {targetSelected.CharacterName} antes de voltar a dormir."
                : $"{currentPlayer.CharacterName} escolheu dormir sem atacar ninguém.";
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
            prompt.text = $"Quem {currentPlayer.CharacterName} ataca esta noite?";
            SetupVoteButtons();
        }
    }
}
