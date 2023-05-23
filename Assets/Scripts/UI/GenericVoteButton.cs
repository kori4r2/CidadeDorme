using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CidadeDorme {
    public abstract class GenericVoteButton : MonoBehaviour {
        [SerializeField] protected Button button;
        [SerializeField] protected TextMeshProUGUI playerName;
        protected Player player;

        public virtual void ActivateButton(Player player) {
            this.player = player;
            playerName.text = player.CharacterName;
            button.interactable = true;
            gameObject.SetActive(true);
        }

        public virtual void SelectButton() {
            button.Select();
        }

        public virtual void DeactivateButton() {
            player = null;
            button.interactable = false;
            gameObject.SetActive(false);
        }

        public virtual void DisableButton() {
            button.interactable = false;
        }

        public abstract void OnSelect();
    }
}
