using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Night Choices")]
    public class NightChoices : ScriptableObject {
        private const string werewolvesNoVoteString = "Sem votos até o momento";
        private const string werewolvesVoteHeader = "Em caso de empate haverá escolha aleatória\nVotos:";
        private Dictionary<Player, int> werewolfVotes = new Dictionary<Player, int>();
        private List<Player> werewolfTargets = new List<Player>();
        private List<Player> casualties;
        private StringBuilder stringBuilder = new StringBuilder();

        public void Clear() {
            werewolfVotes.Clear();
        }

        public void CastWerewolfVote(Player player) {
            if (player == null)
                return;
            if (werewolfVotes.ContainsKey(player))
                werewolfVotes[player]++;
            else
                werewolfVotes[player] = 1;
        }

        public List<Player> GetDeadPlayers() {
            casualties.Clear();
            TallyWerewolfVotes();
            return new List<Player>(casualties);
        }

        private void TallyWerewolfVotes() {
            werewolfTargets.Clear();
            CalculateMostVotedTargets();
            if (werewolfTargets.Count > 0) {
                casualties.Add(werewolfTargets[Random.Range(0, werewolfTargets.Count)]);
            }
        }

        private void CalculateMostVotedTargets() {
            int maxVotes = 0;
            foreach (Player votedPlayer in werewolfVotes.Keys) {
                if (werewolfVotes[votedPlayer] > maxVotes) {
                    maxVotes = werewolfVotes[votedPlayer];
                    werewolfTargets.Clear();
                    werewolfTargets.Add(votedPlayer);
                } else if (werewolfVotes[votedPlayer] == maxVotes) {
                    werewolfTargets.Add(votedPlayer);
                }
            }
        }

        public string GetWerewolfVotesString() {
            if (werewolfVotes.Count < 1) {
                return werewolvesNoVoteString;
            }
            stringBuilder.Clear();
            stringBuilder.Append(werewolvesVoteHeader);
            foreach (Player votedPlayer in werewolfVotes.Keys) {
                stringBuilder.Append($"\n{votedPlayer.CharacterName} x {werewolfVotes[votedPlayer]}");
            }
            return stringBuilder.ToString();
        }
    }
}
