using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/MatchSettings")]
    public class MatchSettings : ScriptableObject {
        private Dictionary<PlayerClass, int> classes = new Dictionary<PlayerClass, int>();
        public ReadOnlyDictionary<PlayerClass, int> Classes;
        [SerializeField] private List<Player> allPlayers;
        public List<Player> AllPlayers => allPlayers;
        [SerializeField] private List<PlayerClass> availableClasses;
        public List<PlayerClass> AvailableClasses => availableClasses;
        [SerializeField] private List<Team> teams;
        public List<Team> Teams => teams;
        private UnityEvent presetLoaded = new UnityEvent();
        private UnityEvent classesUpdated = new UnityEvent();

        [SerializeField, Range(4, 9)] private int minPlayerCount = 4;
        [SerializeField, Range(4, 9)] private int maxPlayerCount = 4;
        [SerializeField, Range(-5, 5)] private int minBalanceValue = -2;
        [SerializeField, Range(-5, 5)] private int maxBalanceValue = 2;

        public int GetClassCount(PlayerClass playerClass) {
            return classes.ContainsKey(playerClass) ? classes[playerClass] : 0;
        }

        public void AddPresetLoadListener(UnityAction action) {
            presetLoaded.AddListener(action);
        }

        public void RemovePresetLoadListener(UnityAction action) {
            presetLoaded.RemoveListener(action);
        }

        public void AddClassesUpdatedListener(UnityAction action) {
            classesUpdated.AddListener(action);
        }

        public void RemoveClassesUpdatedListener(UnityAction action) {
            classesUpdated.RemoveListener(action);
        }

        public void OnEnable() {
            classes ??= new Dictionary<PlayerClass, int>();
            Classes = new ReadOnlyDictionary<PlayerClass, int>(classes);
        }

        public void LoadPreset(MatchSettingsPreset preset) {
            classes.Clear();
            availableClasses.Clear();
            availableClasses.AddRange(preset.AvailableClasses);
            teams.Clear();
            teams.AddRange(preset.Teams);
            foreach (PlayerClass playerClass in availableClasses) {
                RegisterAddedClass(playerClass);
            }
            Classes = new ReadOnlyDictionary<PlayerClass, int>(classes);
            presetLoaded?.Invoke();
            classesUpdated?.Invoke();
        }

        private void RegisterAddedClass(PlayerClass playerClass) {
            if (classes.ContainsKey(playerClass))
                classes[playerClass]++;
            else
                classes[playerClass] = 1;
        }

        public bool CanAddClass(PlayerClass playerClass) {
            if (availableClasses.Count >= maxPlayerCount)
                return false;
            int newBalanceWeight = GetCurrentClassesWeight() + playerClass.BalanceWeight;
            return minBalanceValue <= newBalanceWeight && newBalanceWeight <= maxBalanceValue;
        }

        public bool CanRemoveClass(PlayerClass playerClass) {
            if (!classes.ContainsKey(playerClass) || classes[playerClass] <= 0)
                return false;
            int newBalanceWeight = GetCurrentClassesWeight() - playerClass.BalanceWeight;
            return minBalanceValue <= newBalanceWeight && newBalanceWeight <= maxBalanceValue;
        }

        private int GetCurrentClassesWeight() {
            int weight = 0;
            foreach (PlayerClass playerClass in availableClasses) {
                weight += playerClass.BalanceWeight;
            }
            return weight;
        }

        public bool CanStartGame() {
            int balanceWeight = GetCurrentClassesWeight();
            bool gameBalanced = minBalanceValue <= balanceWeight && balanceWeight <= maxBalanceValue;
            bool validPlayerCount = minPlayerCount <= availableClasses.Count && availableClasses.Count <= maxPlayerCount;
            return gameBalanced && validPlayerCount;
        }

        public void AddClass(PlayerClass playerClass) {
            if (!CanAddClass(playerClass))
                return;
            availableClasses.Add(playerClass);
            RegisterAddedClass(playerClass);
            Classes = new ReadOnlyDictionary<PlayerClass, int>(classes);
            classesUpdated?.Invoke();
        }

        public void RemoveClass(PlayerClass playerClass) {
            if (!CanRemoveClass(playerClass))
                return;
            int index = availableClasses.FindLastIndex(item => item == playerClass);
            availableClasses.RemoveAt(index);
            classes[playerClass]--;
            Classes = new ReadOnlyDictionary<PlayerClass, int>(classes);
            classesUpdated?.Invoke();
        }
    }
}
