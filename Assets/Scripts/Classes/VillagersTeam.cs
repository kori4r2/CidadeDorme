using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Team/Villagers")]
    public class VillagersTeam : Team {
        public override string TeamName => "Aldeões";
        public override int ClassWeightModifier => 1;

        public override bool CheckVictory(List<Player> playersAlive) {
            foreach (Player player in playersAlive) {
                if (player.PlayerClass.Team is WerewolvesTeam)
                    return false;
            }
            return true;
        }
    }
}
