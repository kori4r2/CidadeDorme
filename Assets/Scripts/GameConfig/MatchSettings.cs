using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/MatchSettings")]
    public class MatchSettings : ScriptableObject {
        private Dictionary<PlayerClass, int> classes = new Dictionary<PlayerClass, int>();
        [SerializeField] private List<Player> allPlayers;
        public List<Player> AllPlayers => allPlayers;
        [SerializeField] private List<PlayerClass> availableClasses;
        public List<PlayerClass> AvailableClasses => availableClasses;
        [SerializeField] private List<Team> teams;
        public List<Team> Teams => teams;
    }
}
