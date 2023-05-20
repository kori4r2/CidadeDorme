using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerClass : ScriptableObject {
        public abstract string ClassName { get; }
        [SerializeField] private Sprite image;
        public Sprite Image => image;
        [SerializeField, TextArea] private string instructions;
        public string Instructions => string.IsNullOrWhiteSpace(instructions) ? string.Empty : $"\n{instructions}";
        [SerializeField] Team team;
        public Team Team => team;
        [SerializeField] private bool canSeeAllies;
        public bool CanSeeAllies => canSeeAllies;
        [SerializeField] private PlayerTurnInterface interfacePrefab;
        [SerializeField] private PlayerInterfaceVariable interfaceVariable;

        public abstract void StartTurn();
        public abstract void ChoiceMade(int choiceIndex);

        public void SetupInterface() {
            // if (interfaceVariable.Value != null)
            //     return;

            // interfaceVariable.Value = Instantiate(interfacePrefab);
        }
    }
}
