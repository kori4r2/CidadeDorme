using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CidadeDorme {
    public class VoteButton : MonoBehaviour {
        [SerializeField] private Button button;
        [SerializeField] private VotingInfo votingInfo;
        [SerializeField] private TextMeshProUGUI playerName;
        private Player player;

        public void ActivateButton(Player player) {
            this.player = player;
            playerName.text = player.CharacterName;
            button.interactable = true;
            gameObject.SetActive(true);
        }

        public void SelectButton() {
            button.Select();
        }

        public void DeactivateButton() {
            player = null;
            button.interactable = false;
            gameObject.SetActive(false);
        }

        public void DisableButton() {
            button.interactable = false;
        }

        public void OnSelect() {
            votingInfo.VoteFor(player);
        }
    }
}
