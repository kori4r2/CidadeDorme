using System.Collections.Generic;
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
            playerList[turnIndex].PlayerClass.StartTurn(playerList[turnIndex], playersAlive);
            StartTimer(ShowCurrentPlayerResults, turnWaitTimes.PlayerTurn);
        }

        private void ShowCurrentPlayerResults() {
            string actionResult = playerList[turnIndex].PlayerClass.GetTurnResult();
            messageHandler.ShowMessage(actionResult);
            StartTimer(StartNextPlayerTurn, turnWaitTimes.DefaultMessage);
        }

        private void ShowNightResults() {
            bool gameEnded = CheckGameEnd();
            List<Player> deadPlayers = nightChoices.GetDeadPlayers();
            foreach (Player deadPlayer in deadPlayers) {
                KillPlayer(deadPlayer);
            }
            messageHandler.ShowMorningMessage(deadPlayers);
            if (gameEnded) {
                StartTimer(ShowGameEndMessage, turnWaitTimes.DefaultMessage);
            } else {
                StartTimer(StartDiscussion, turnWaitTimes.DefaultMessage);
            }
        }
    }
}
