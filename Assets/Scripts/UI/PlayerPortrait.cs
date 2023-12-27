using UnityEngine;
using Toblerone.Toolbox;
using TMPro;

namespace CidadeDorme {
    public class PlayerPortrait : MonoBehaviour {
        private const string hiddenClassString = "?";
        [SerializeField] private Player watchedPlayer;
        [SerializeField] private EventSO playersSetupFinished;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerClass;
        [SerializeField] private GameObject overlay;
        private EventListener playerSetupListener;

        private void Awake() {
            playerSetupListener = new EventListener(playersSetupFinished, StartWatchingPlayer);
            watchedPlayer.WatchPlayer(KillPlayer, ChangeClassVisibility, ChangeTeamVisibility);
        }

        private void StartWatchingPlayer() {
            if (!watchedPlayer.IsPlaying) {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            playerName.text = watchedPlayer.CharacterName;
            playerClass.text = hiddenClassString;
            overlay.SetActive(false);
        }

        private void KillPlayer() {
            if (!watchedPlayer.IsPlaying)
                return;
            overlay.SetActive(true);
            playerName.text = $"<s>{watchedPlayer.CharacterName}</s>";
        }

        private void ChangeClassVisibility(bool isVisible) {
            if (!watchedPlayer.IsPlaying)
                return;
            playerClass.text = isVisible ? watchedPlayer.PlayerClass.ClassName : hiddenClassString;
        }

        private void ChangeTeamVisibility(bool isVisible) {
            if (!watchedPlayer.IsPlaying)
                return;
            playerClass.text = isVisible ? watchedPlayer.PlayerClass.Team.TeamName : hiddenClassString;
        }

        private void OnEnable() {
            playerSetupListener.StartListeningEvent();
        }

        private void OnDisable() {
            playerSetupListener.StopListeningEvent();
        }
    }
}
