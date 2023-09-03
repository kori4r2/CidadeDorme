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
        private UnityEvent<bool> OnChangeTeamVisibility = new UnityEvent<bool>();

        public void SetupPlayer(PlayerClass playerClass) {
            PlayerClass = playerClass;
            PlayerClass.Team.AddIfPlayerBelongs(this);
            IsAlive = true;
            nameList.ReturnName(CharacterName);
            CharacterName = nameList.GetNewName();
        }

        public void WatchPlayer(UnityAction onDeath, UnityAction<bool> onChangeClassVisibility, UnityAction<bool> onChangeTeamVisibility) {
            if (onDeath != null)
                OnDeath.AddListener(onDeath);
            if (onChangeClassVisibility != null)
                OnChangeClassVisibility.AddListener(onChangeClassVisibility);
            if (onChangeTeamVisibility != null)
                OnChangeTeamVisibility.AddListener(onChangeTeamVisibility);
        }

        public void Kill() {
            IsAlive = false;
            OnDeath?.Invoke();
        }

        public void HideInfo() {
            OnChangeClassVisibility?.Invoke(false);
            OnChangeTeamVisibility?.Invoke(false);
        }

        public void ShowClass() {
            OnChangeClassVisibility?.Invoke(true);
        }

        public void ShowTeam() {
            OnChangeTeamVisibility?.Invoke(true);
        }
    }
}
