using TMPro;
using UnityEngine;

namespace CidadeDorme {
    public class VillagerTurnHandler : PlayerTurnHandler {
        [SerializeField] private DreamGenerator dreamGenerator;
        [SerializeField] private GameObject rootObject;
        [SerializeField] private TextMeshProUGUI prompt;

        protected override bool IsValidActionTarget(Player target) {
            return target != currentPlayer;
        }

        public override string GetActionFeedback() {
            return dreamGenerator.GetDreamResult(currentPlayer);
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
            prompt.text = dreamGenerator.GetDreamQuestion(currentPlayer);
            SetupVoteButtons();
        }
    }
}
