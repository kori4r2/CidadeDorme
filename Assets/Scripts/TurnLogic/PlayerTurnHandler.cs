using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerTurnHandler : MonoBehaviour {
        [SerializeField] protected PlayerTurnHandlerVariable reference;
        protected Player currentPlayer;
        public abstract void Init();
        public abstract void ShowPlayerChoices(Player currentPlayer, List<Player> playersAlive);
        public abstract void Hide();
        public abstract string GetActionFeedback();

        protected virtual void Awake() {
            reference.Value = this;
        }

        protected virtual void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
        }
    }
}
