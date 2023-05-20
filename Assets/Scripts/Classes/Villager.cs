using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/PlayerClass/Villager")]
    public class Villager : PlayerClass {
        public override string ClassName => "Aldeão";

        public override void ChoiceMade(int choiceIndex) {
            throw new System.NotImplementedException();
        }

        public override void StartTurn() {
            throw new System.NotImplementedException();
        }
    }
}
