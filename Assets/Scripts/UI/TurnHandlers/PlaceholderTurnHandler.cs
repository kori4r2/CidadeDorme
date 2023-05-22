using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    public class PlaceholderTurnHandler : PlayerTurnHandler {
        [SerializeField] private MessageHandler messageHandler;

        public override string GetActionFeedback() {
            return $"{currentPlayer.CharacterName} teve uma boa noite de sono. (ação não implementada)";
        }

        public override void Hide() {
            messageHandler.HideMessage();
        }

        public override void Init() {
        }

        public override void ShowPlayerChoices(Player currentPlayer) {
            this.currentPlayer = currentPlayer;
            messageHandler.ShowMessage($"Sinto muito {currentPlayer.CharacterName}, a sua interface está em outro castelo. (ação não implementada)");
        }
    }
}
