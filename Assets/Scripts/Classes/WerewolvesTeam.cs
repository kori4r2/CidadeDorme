using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Team/Werewolves")]
    public class WerewolvesTeam : Team {
        public override string TeamName => "Lobisomens";
        public override int ClassWeightModifier => -1;

        public override bool CheckVictory(List<Player> playersAlive) {
            int werewolvesCount = 0;
            int villagersCount = 0;
            foreach (Player player in playersAlive) {
                if (player.PlayerClass.Team is WerewolvesTeam)
                    werewolvesCount++;
                else if (player.PlayerClass.Team is VillagersTeam)
                    villagersCount++;
            }
            return villagersCount <= werewolvesCount;
        }
    }
}
