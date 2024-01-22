using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/MatchSettingsPreset")]
    public class MatchSettingsPreset : ScriptableObject {
        [SerializeField] private string displayName;
        public string DisplayName => displayName;
        [SerializeField] private List<PlayerClass> availableClasses;
        public List<PlayerClass> AvailableClasses => availableClasses;
        [SerializeField] private List<Team> teams;
        public List<Team> Teams => teams;
    }
}
