using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    public abstract class Team : ScriptableObject, IEnumerable<Player> {
        public abstract string TeamName { get; }
        public abstract int ClassWeightModifier { get; }
        private List<Player> team = new List<Player>();
        [SerializeField, TextArea] private string victoryText;
        public string VictoryText => victoryText;

        public void Clear() {
            team.Clear();
        }

        public void AddIfPlayerBelongs(Player player) {
            if (player.PlayerClass.Team == this)
                team.Add(player);
        }

        public abstract bool CheckVictory(List<Player> playersAlive);

        public IEnumerator<Player> GetEnumerator() {
            return ((IEnumerable<Player>)team).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)team).GetEnumerator();
        }
    }
}
