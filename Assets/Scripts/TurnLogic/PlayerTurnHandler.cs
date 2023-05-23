using System.Collections.Generic;
using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerTurnHandler : MonoBehaviour {
        [SerializeField] protected PlayerTurnHandlerVariable reference;
        [SerializeField] protected PlayerListVariable playersAliveVariable;
        [SerializeField] protected Player nullPlayer;
        [SerializeField] protected List<GenericVoteButton> buttons;
        protected Player currentPlayer;
        public abstract void Init();
        public abstract void ShowPlayerChoices(Player currentPlayer);
        public abstract void Hide();
        public abstract string GetActionFeedback();
        protected abstract bool IsValidActionTarget(Player target);

        protected void SetupVoteButtons() {
            buttons[0].ActivateButton(nullPlayer);
            for (int index = 1; index < buttons.Count; index++) {
                if (index <= playersAliveVariable.Value.Count && IsValidActionTarget(playersAliveVariable.Value[index - 1])) {
                    buttons[index].ActivateButton(playersAliveVariable.Value[index - 1]);
                } else {
                    buttons[index].DeactivateButton();
                }
            }
            buttons[0].SelectButton();
        }

        public void DisableVoteButtons() {
            foreach (GenericVoteButton button in buttons) {
                button.DisableButton();
            }
        }

        protected virtual void Awake() {
            reference.Value = this;
        }

        protected virtual void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
        }
    }
}
