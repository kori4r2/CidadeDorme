using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Team")]
    public class Team : ScriptableObject, IEnumerable<Player> {
        private List<Player> team = new List<Player>();

        public void Clear() {
            team.Clear();
        }

        public void AddIfPlayerBelongs(Player player) {
            if (player.PlayerClass.Team == this)
                team.Add(player);
        }

        public IEnumerator<Player> GetEnumerator() {
            return ((IEnumerable<Player>)team).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)team).GetEnumerator();
        }
    }
}
