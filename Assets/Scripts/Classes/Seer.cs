using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/PlayerClass/Seer")]
    public class Seer : PlayerClass {
        public override string ClassName => "Vidente";

        public override void ChoiceMade(int choiceIndex) {
            throw new System.NotImplementedException();
        }

        public override void StartTurn() {
            throw new System.NotImplementedException();
        }
    }
}
