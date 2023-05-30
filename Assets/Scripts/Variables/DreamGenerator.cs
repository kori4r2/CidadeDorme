using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Dream Generator")]
    public class DreamGenerator : ScriptableObject {
        [SerializeField] private List<string> maleNouns;
        [SerializeField] private List<string> femaleNouns;
        [SerializeField] private List<string> maleAdjectives;
        [SerializeField] private List<string> femaleAdjectives;

        public string GetDreamQuestion(Player player) {
            return $"Enquanto {player.CharacterName} dorme, um nome lhe vem à mente...";
        }

        private string GetRandomItem(List<string> stringList) {
            int randomIndex = Random.Range(0, stringList.Count);
            return stringList[randomIndex];
        }

        public string GetDreamResult(Player player) {
            bool isMale = Random.Range(0, 10) % 2 == 0;
            string randomNoun = isMale ? GetRandomItem(maleNouns) : GetRandomItem(femaleNouns);
            string randomAdjective = isMale ? GetRandomItem(maleAdjectives) : GetRandomItem(femaleAdjectives);
            return $"{player.CharacterName} tem sonhos de {randomNoun} {randomAdjective}";
        }
    }
}
