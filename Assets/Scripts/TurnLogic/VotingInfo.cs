using System.Collections.Generic;
using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Voting Info")]
    public class VotingInfo : ScriptableObject {
        public bool VotingShouldHappen { get; set; }
        private Dictionary<Player, int> voteCount;
        [SerializeField] private Player nullPlayer;
        [SerializeField] private EventSO votingCheckBegan;
        [SerializeField] private EventSO votingCheckEnded;
        [SerializeField] private PlayerEvent playerBeganVoting;
        [SerializeField] private EventSO playerFinishedVoting;
        private Player selectedPlayer = null;

        public void BeginVotingCheck() {
            votingCheckBegan.Raise();
        }

        public void EndVotingCheck() {
            votingCheckEnded.Raise();
        }

        public void PrepareForVoting(List<Player> playersAlive) {
            voteCount = new Dictionary<Player, int>();
            voteCount.Add(nullPlayer, 0);
            foreach (Player player in playersAlive) {
                voteCount.Add(player, 0);
            }
            selectedPlayer = nullPlayer;
        }

        public void GetPlayerVote(Player player) {
            selectedPlayer = nullPlayer;
            playerBeganVoting.Raise(player);
        }

        public void VoteFor(Player player) {
            selectedPlayer = player;
        }

        public void EndPlayerVote() {
            voteCount[selectedPlayer]++;
            playerFinishedVoting.Raise();
            selectedPlayer = nullPlayer;
        }

        public Player GetVoteResult() {
            Player mostVotedPlayer = nullPlayer;
            int highestVoteCount = -1;

            foreach (Player player in voteCount.Keys) {
                if (voteCount[player] > highestVoteCount) {
                    highestVoteCount = voteCount[player];
                    mostVotedPlayer = player;
                } else if (voteCount[player] == highestVoteCount) {
                    mostVotedPlayer = nullPlayer;
                }
            }

            return mostVotedPlayer == nullPlayer ? null : mostVotedPlayer;
        }
    }
}
