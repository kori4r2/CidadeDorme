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
            watchedPlayer.WatchPlayer(KillPlayer, ChangeClassVisibility);
        }

        private void StartWatchingPlayer() {
            playerName.text = watchedPlayer.CharacterName;
            playerClass.text = hiddenClassString;
            overlay.SetActive(false);
        }

        private void KillPlayer() {
            overlay.SetActive(true);
            playerName.text = $"<s>{watchedPlayer.CharacterName}</s>";
        }

        private void ChangeClassVisibility(bool isVisible) {
            playerClass.text = isVisible ? watchedPlayer.PlayerClass.ClassName : hiddenClassString;
        }

        private void OnEnable() {
            playerSetupListener.StartListeningEvent();
        }

        private void OnDisable() {
            playerSetupListener.StopListeningEvent();
        }
    }
}
