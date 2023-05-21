using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerClass : ScriptableObject {
        public abstract string ClassName { get; }
        [SerializeField] private Sprite image;
        public Sprite Image => image;
        [SerializeField, TextArea] private string instructions;
        public string Instructions => string.IsNullOrWhiteSpace(instructions) ? string.Empty : $"\n{instructions}";
        [SerializeField] Team team;
        public Team Team => team;
        [SerializeField] private bool canSeeAllies;
        public bool CanSeeAllies => canSeeAllies;
        [SerializeField] private PlayerTurnHandler turnHandlerPrefab;
        [SerializeField] private PlayerTurnHandlerVariable turnHandlerVariable;

        public void StartTurn(List<Player> playersAlive) {
            turnHandlerVariable.Value.ShowPlayerChoices(playersAlive);
        }

        public string GetTurnResult() {
            return turnHandlerVariable.Value.GetActionFeedback();
        }

        public void SetupInterface() {
            if (turnHandlerPrefab == null || turnHandlerVariable.Value != null)
                return;

            turnHandlerVariable.Value = Instantiate(turnHandlerPrefab);
            turnHandlerVariable.Value.Init();
        }
    }
}
