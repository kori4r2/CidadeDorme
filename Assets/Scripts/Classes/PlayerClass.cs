using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerClass : ScriptableObject {
        public abstract void StartTurn();
        public abstract void ChoiceMade(int choiceIndex);
    }
}
