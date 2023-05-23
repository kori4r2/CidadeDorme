using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Night Choices")]
    public class NightChoices : ScriptableObject {
        private HashSet<Player> playersAttacked = new HashSet<Player>();

        public void Clear() {
            playersAttacked.Clear();
        }

        public void AttackPlayer(Player player) {
            if (player != null)
                playersAttacked.Add(player);
        }

        public List<Player> GetDeadPlayers() {
            return new List<Player>(playersAttacked);
        }
    }
}
