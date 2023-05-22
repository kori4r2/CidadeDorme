using UnityEngine;
using UnityEngine.Events;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Player")]
    public class Player : ScriptableObject {
        private const string noName = "Ninguém";
        [SerializeField] private NameList nameList;
        private string characterName;
        public string CharacterName {
            get => nameList != null ? characterName : noName;
            private set => characterName = value;
        }
        public PlayerClass PlayerClass { get; private set; }
        public bool IsAlive { get; private set; }
        private UnityEvent OnDeath = new UnityEvent();
        private UnityEvent<bool> OnChangeClassVisibility = new UnityEvent<bool>();

        public void SetupPlayer(PlayerClass playerClass) {
            PlayerClass = playerClass;
            PlayerClass.Team.AddIfPlayerBelongs(this);
            IsAlive = true;
            nameList.ReturnName(CharacterName);
            CharacterName = nameList.GetNewName();
        }

        public void WatchPlayer(UnityAction onDeath, UnityAction<bool> onChangeClassVisibility) {
            if (onDeath != null)
                OnDeath.AddListener(onDeath);
            if (onChangeClassVisibility != null)
                OnChangeClassVisibility.AddListener(onChangeClassVisibility);
        }

        public void Kill() {
            IsAlive = false;
            OnDeath?.Invoke();
        }

        public void HideClass() {
            OnChangeClassVisibility?.Invoke(false);
        }

        public void ShowClass() {
            OnChangeClassVisibility?.Invoke(true);
        }
    }
}
