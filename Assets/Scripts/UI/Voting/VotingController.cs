using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Toblerone.Toolbox;
using UnityEngine;
using UnityEngine.UI;

namespace CidadeDorme {
    public class VotingController : MonoBehaviour {
        [SerializeField] private EventSO votingCheckBegan;
        private EventListener votingCheckBeganListener;
        [SerializeField] private EventSO votingCheckEnded;
        private EventListener votingCheckEndedListener;
        [SerializeField] private PlayerEvent playerBeganVoting;
        private GenericEventListener<Player> playerBeganVotingListener;
        [SerializeField] private PlayerListVariable playersAliveVariable;
        [SerializeField] private EventSO playerFinishedVoting;
        private EventListener playerFinishedVotingListener;

        [Header("Voting Check")]
        [SerializeField] private GameObject voteCheckRootObject;
        [SerializeField] private List<Button> voteCheckButtons;

        [Header("Voting Options")]
        [SerializeField] private Player nullPlayer;
        [SerializeField] private GameObject voteOptionsRootObject;
        [SerializeField] private TextMeshProUGUI prompt;
        [SerializeField] private List<VoteButton> voteOptionsButtons;

        private void Awake() {
            HideAllUI();
            votingCheckBeganListener = new EventListener(votingCheckBegan, EnableVotingCheck);
            votingCheckEndedListener = new EventListener(votingCheckEnded, HideAllUI);
            playerBeganVotingListener = new GenericEventListener<Player>(playerBeganVoting, SetupPlayerVote);
            playerFinishedVotingListener = new EventListener(playerFinishedVoting, HideAllUI);
        }

        private void HideAllUI() {
            voteCheckRootObject.SetActive(false);
            voteOptionsRootObject.SetActive(false);
        }

        private void EnableVotingCheck() {
            voteCheckRootObject.SetActive(true);
            ActivateButtonList(voteCheckButtons);
        }

        private static void ActivateButtonList(List<Button> buttonsToActivate) {
            foreach (Button button in buttonsToActivate) {
                button.interactable = true;
            }
            buttonsToActivate[0].Select();
        }

        private void SetupPlayerVote(Player votingPlayer) {
            voteOptionsRootObject.SetActive(true);
            prompt.text = $"Em quem {votingPlayer.CharacterName} vota?";
            voteOptionsButtons[0].ActivateButton(nullPlayer);
            for (int index = 1; index < voteOptionsButtons.Count; index++) {
                if (index <= playersAliveVariable.Value.Count) {
                    voteOptionsButtons[index].ActivateButton(playersAliveVariable.Value[index - 1]);
                } else {
                    voteOptionsButtons[index].DeactivateButton();
                }
            }
            voteOptionsButtons[0].SelectButton();
        }

        public void DisableCheckButtons() {
            foreach (Button button in voteCheckButtons) {
                button.interactable = false;
            }
        }

        public void DisableVoteButtons() {
            foreach (VoteButton button in voteOptionsButtons) {
                button.DisableButton();
            }
        }

        private void OnEnable() {
            votingCheckBeganListener.StartListeningEvent();
            votingCheckEndedListener.StartListeningEvent();
            playerBeganVotingListener.StartListeningEvent();
            playerFinishedVotingListener.StartListeningEvent();
        }

        private void OnDisable() {
            votingCheckBeganListener.StopListeningEvent();
            votingCheckEndedListener.StopListeningEvent();
            playerBeganVotingListener.StopListeningEvent();
            playerFinishedVotingListener.StopListeningEvent();
        }
    }
}
