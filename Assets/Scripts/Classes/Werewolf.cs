using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/PlayerClass/Werewolf")]
    public class Werewolf : PlayerClass {
        public override string ClassName => "Lobisomem";

        public override void ChoiceMade(int choiceIndex) {
            throw new System.NotImplementedException();
        }

        public override void StartTurn() {
            throw new System.NotImplementedException();
        }
    }
}
