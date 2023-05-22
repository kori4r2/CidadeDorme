using UnityEngine;

namespace CidadeDorme {
    public partial class TurnManager : MonoBehaviour {
        private void StartDiscussion() {
            messageHandler.ShowDiscussionMessage();
            votingInfo.BeginVotingCheck();
            StartTimer(CheckNeedForAVote, turnWaitTimes.Discussion);
        }

        private void CheckNeedForAVote() {
            messageHandler.HideMessage();
            votingInfo.EndVotingCheck();
            turnIndex = -1;
            if (votingInfo.VotingShouldHappen) {
                votingInfo.PrepareForVoting(playersAlive);
                StartNextPlayerVote();
            } else {
                StartNextPlayerTurn();
            }
        }

        private void StartNextPlayerVote() {
            if (turnIndex > -1)
                votingInfo.EndPlayerVote();
            CalculateNewTurnIndex();
            if (turnIndex >= playerList.Count) {
                ShowVoteResults();
            } else {
                votingInfo.GetPlayerVote(playerList[turnIndex]);
                StartTimer(StartNextPlayerVote, turnWaitTimes.Voting);
            }
        }

        private void ShowVoteResults() {
            Player votedPlayer = votingInfo.GetVoteResult();
            KillPlayer(votedPlayer);
            bool gameEnded = CheckGameEnd();
            messageHandler.ShowVoteResult(votedPlayer, gameEnded);
            if (gameEnded) {
                StartTimer(ShowGameEndMessage, turnWaitTimes.DefaultMessage);
            } else {
                turnIndex = -1;
                nightChoices.Clear();
                StartTimer(StartNextPlayerTurn, turnWaitTimes.DefaultMessage);
            }
        }
    }
}
