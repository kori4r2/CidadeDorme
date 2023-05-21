using UnityEngine;

namespace CidadeDorme {
    public partial class TurnManager : MonoBehaviour {
        private void StartNextPlayerIntroduction() {
            foreach (Player player in playerList) {
                player.HideClass();
            }
            turnIndex++;
            if (turnIndex >= playerList.Count) {
                messageHandler.ShowMorningMessage(null);
                StartTimer(StartDiscussion, turnWaitTimes.DefaultMessage);
                return;
            }

            messageHandler.ShowNextPlayerMessage(playerList[turnIndex], false);
            StartTimer(CurrentPlayerIntroduction, turnWaitTimes.Introduction);
        }

        private void CurrentPlayerIntroduction() {
            Player player = playerList[turnIndex];
            messageHandler.ShowPlayerIntroduction(player);
            ShowVisibleClasses(player);
            StartTimer(StartNextPlayerIntroduction, turnWaitTimes.Introduction);
        }

        private void StartNextPlayerTurn() {
            foreach (Player player in playerList) {
                player.HideClass();
            }
            turnIndex++;
            CalculateNewTurnIndex();
            if (turnIndex >= playerList.Count) {
                ShowNightResults();
            } else {
                messageHandler.ShowNextPlayerMessage(playerList[turnIndex], true);
                StartTimer(ShowCurrentPlayerChoices, turnWaitTimes.Introduction);
            }
        }

        private void ShowCurrentPlayerChoices() {
            messageHandler.HideMessage();
        }

        private void ShowCurrentPlayerResults() {
        }

        private void ShowNightResults() {
            bool gameEnded = CheckGameEnd();
            messageHandler.ShowMorningMessage(null);
            if (gameEnded) {
                StartTimer(ShowGameEndMessage, turnWaitTimes.DefaultMessage);
            } else {
                StartTimer(StartDiscussion, turnWaitTimes.DefaultMessage);
            }
        }
    }
}
